using CosmicOdyssey.Library.Cache.Interfaces;
using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.Library.Models;
using Dapper;

namespace CosmicOdyssey.Library.DataAccess;
public class CommentData : ICommentData
{
    private const string CacheName = nameof(CommentData);
    private readonly ISqlDataAccess _sql;
    private readonly ISqlHelper _sqlHelper;
    private readonly IRedisCache _cache;

    public CommentData(
        ISqlDataAccess sql,
        ISqlHelper sqlHelper,
        IRedisCache cache)
    {
        _sql = sql;
        _sqlHelper = sqlHelper;
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
                "Id", new BasicProfileModel());
            
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
            "Id", new BasicProfileModel());
    }

    public async Task CreateCommentAsync(CommentModel comment)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<CommentModel>(Procedure.INSERT);
        var parameters = new DynamicParameters();
        parameters.Add("ProfileId", comment.Profile.Id);
        parameters.Add("BlogId", comment.BlogId);
        parameters.Add("Body", comment.Body);

        await _sql.SaveDataAsync(storedProcedure, parameters);
    }

    public async Task UpdateCommentAsync(CommentModel comment)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<CommentModel>(Procedure.UPDATE);
        var parameters = new DynamicParameters();
        parameters.Add("Id", comment.Id);
        parameters.Add("Body", comment.Body);

        await _sql.SaveDataAsync(storedProcedure, parameters);
    }

    public async Task DeleteCommentAsync(CommentModel comment)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<CommentModel>(Procedure.DELETE);
        var parameters = new DynamicParameters();
        parameters.Add("Id", comment.Id);

        await _sql.SaveDataAsync(storedProcedure, parameters);
    }
}
