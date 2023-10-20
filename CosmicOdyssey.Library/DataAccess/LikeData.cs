using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.Library.Models;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CosmicOdyssey.Library.DataAccess;
public class LikeData : ILikeData
{
    private readonly ISqlDataAccess _sql;
    private readonly ISqlHelper _sqlHelper;
    private readonly INotificationData _notificationData;
    private readonly IBlogData _blogData;
    private readonly ILogger<LikeData> _logger;

    public LikeData(
        ISqlDataAccess sql,
        ISqlHelper sqlHelper,
        INotificationData NotificationData,
        IBlogData blogData,
        ILogger<LikeData> logger)
    {
        _sql = sql;
        _sqlHelper = sqlHelper;
        _notificationData = NotificationData;
        _blogData = blogData;
        _logger = logger;
    }

    private async Task SaveBlogInTransactionAsync(BlogModel blog)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.UPDATE);
        var parameters = new DynamicParameters();
        parameters.Add("Id", blog.Id);
        parameters.Add("Body", blog.Body);
        parameters.Add("LikeCount", blog.LikeCount);

        await _sql.SaveDataInTransactionAsync(storedProcedure, parameters);
    }

    public async Task<List<LikeModel>> GetBlogLikesAsync(int blogId)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<LikeModel>(Procedure.GETBYBLOGID);
        var parameters = new DynamicParameters();
        parameters.Add("BlogId", blogId);

        return await _sql.LoadDataAsync<LikeModel>(storedProcedure, parameters);
    }

    public async Task<bool> ToggleLikeAsync(int profileId, int blogId)
    {
        try
        {
            _sql.StartTransaction();

            var blog = await _blogData.GetBlogAsync(blogId);
            var likes = await GetBlogLikesAsync(blogId);
            var userLike = likes.FirstOrDefault(x => x.ProfileId == profileId);

            string storedProcedure;
            var parameters = new DynamicParameters();
            parameters.Add("ProfileId", profileId);

            if (userLike is null)
            {
                storedProcedure = _sqlHelper.GetStoredProcedure<LikeModel>(Procedure.INSERT);
                parameters.Add("BlogId", blogId);

                var newLike = new LikeModel()
                {
                    ProfileId = profileId,
                    BlogId = blogId,
                };

                likes.Add(userLike);

                try
                {
                    var newNotification = new NotificationModel()
                    {
                        Body = "Someone liked your tweet!",
                        ProfileId = blog.Profile.Id,
                    };

                    await _notificationData.CreateNotificationAsync(newNotification);
                }
                catch (Exception ex)
                {
                    _sql.RollbackTransaction();
                    _logger.LogError("Internal Error [LIKE_TOGGLE]: {error}", ex.Message);
                    throw;
                }
            }
            else
            {
                storedProcedure = _sqlHelper.GetStoredProcedure<LikeModel>(Procedure.DELETE);
                parameters.Add("ProfileId", profileId);

                likes.Remove(userLike);
            }

            blog.LikeCount = likes.Count;

            await _sql.SaveDataInTransactionAsync(storedProcedure, parameters);
            await SaveBlogInTransactionAsync(blog);

            _sql.CommitTransaction();

            return userLike is null;
        }
        catch (Exception ex)
        {
            _sql.RollbackTransaction();
            _logger.LogError("Internal Error [LIKE_TOGGLE]: {error}", ex.Message);
            throw;
        }
    }
}
