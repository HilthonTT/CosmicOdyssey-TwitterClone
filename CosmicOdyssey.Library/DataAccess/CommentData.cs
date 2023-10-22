using CosmicOdyssey.Library.Cache.Interfaces;
using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.Library.Models;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CosmicOdyssey.Library.DataAccess;
public class CommentData : ICommentData
{
    private const string CacheName = nameof(CommentData);
    private readonly ISqlDataAccess _sql;
    private readonly ISqlHelper _sqlHelper;
    private readonly INotificationData _notificationData;
    private readonly ILogger<CommentData> _logger;
    private readonly IRedisCache _cache;

    public CommentData(
        ISqlDataAccess sql,
        ISqlHelper sqlHelper,
        INotificationData notificationData,
        ILogger<CommentData> logger,
        IRedisCache cache)
    {
        _sql = sql;
        _sqlHelper = sqlHelper;
        _notificationData = notificationData;
        _logger = logger;
        _cache = cache;
    }

    public async Task<List<CommentModel>> GetBlogCommentsAsync(int blogId)
    {
        string key = $"{CacheName}_{blogId}";
        var output = await _cache.GetRecordAsync<List<CommentModel>>(key);
        if (output is null)
        {
            string storedProcedure = _sqlHelper.GetStoredProcedure<CommentModel>(Procedure.GETBYBLOGID);
            var parameters = new DynamicParameters();
            parameters.Add("BlogId", blogId);

            output = await _sql.LoadDataAsync<CommentModel>(storedProcedure, parameters,
                "Id", new ProfileModel());

            await _cache.SetRecordAsync(key, output, TimeSpan.FromHours(1));
        }

        return output;
    }

    public async Task<CommentModel> GetCommentAsync(int id)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<CommentModel>(Procedure.GETBYID);
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        return await _sql.LoadFirstDataAsync<CommentModel>(storedProcedure, parameters,
            "Id", new ProfileModel());
    }

    public async Task CreateCommentAsync(CommentModel comment)
    {
        try
        {
            _sql.StartTransaction();

            string storedProcedure = _sqlHelper.GetStoredProcedure<CommentModel>(Procedure.INSERT);
            var parameters = new DynamicParameters();
            parameters.Add("ProfileId", comment.Profile?.Id);
            parameters.Add("BlogId", comment.BlogId);
            parameters.Add("Body", comment.Body);

            await _sql.SaveDataInTransactionAsync(storedProcedure, parameters);

            try
            {
                var newNotification = new NotificationModel()
                {
                    Body = "Someone replied to your tweet!",
                    ProfileId = comment.Profile.Id,
                };

                await _notificationData.CreateNotificationAsync(newNotification, true);
            }
            catch (Exception ex)
            {
                _sql.RollbackTransaction();
                _logger.LogError("Internal Error [COMMENT_CREATE]: {error}", ex.Message);
                throw;
            }

            string key = $"{CacheName}_{comment.BlogId}";
            await _cache.RemoveRecordAsync(key);

            _sql.CommitTransaction();
        }
        catch (Exception ex)
        {
            _sql.RollbackTransaction();
            _logger.LogError("Internal Error [COMMENT_CREATE]: {error}", ex.Message);
            throw;
        }
    }

    private async Task RemoveCacheAsync(CommentModel comment)
    {
        string key = $"{CacheName}_{comment.BlogId}";
        await _cache.RemoveRecordAsync(key);
    }

    public async Task UpdateCommentAsync(CommentModel comment)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<CommentModel>(Procedure.UPDATE);
        var parameters = new DynamicParameters();
        parameters.Add("Id", comment.Id);
        parameters.Add("Body", comment.Body);

        await _sql.SaveDataAsync(storedProcedure, parameters);
        await RemoveCacheAsync(comment);
    }

    public async Task DeleteCommentAsync(CommentModel comment)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<CommentModel>(Procedure.DELETE);
        var parameters = new DynamicParameters();
        parameters.Add("Id", comment.Id);

        await _sql.SaveDataAsync(storedProcedure, parameters);
        await RemoveCacheAsync(comment);
    }
}