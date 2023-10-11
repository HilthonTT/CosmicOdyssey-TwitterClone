using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.Library.Models;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CosmicOdyssey.Library.DataAccess;
public class LikeData : ILikeData
{
    private const string CacheName = nameof(LikeData);
    private readonly ISqlDataAccess _sql;
    private readonly ISqlHelper _sqlHelper;
    private readonly ILogger<LikeData> _logger;

    public LikeData(
        ISqlDataAccess sql,
        ISqlHelper sqlHelper,
        ILogger<LikeData> logger)
    {
        _sql = sql;
        _sqlHelper = sqlHelper;
        _logger = logger;
    }

    private async Task<BlogModel> GetBlogInTransactionAsync(int blogId)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.GETBYID);
        var parameters = new DynamicParameters();
        parameters.Add("Id", blogId);

        return await _sql.LoadFirstDataInTransactionAsync<BlogModel>(storedProcedure, parameters);
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

    public async Task ToggleLikeAsync(int profileId, int blogId)
    {
        try
        {
            _sql.StartTransaction();

            var blog = await GetBlogInTransactionAsync(blogId);
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
        }
        catch (Exception ex)
        {
            _sql.RollbackTransaction();
            _logger.LogError("Internal Error: {error}", ex.Message);
            Console.WriteLine($"Internal Error: {ex.Message}");
        }
    }
}
