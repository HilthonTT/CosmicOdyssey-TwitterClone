using CosmicOdyssey.Library.Cache.Interfaces;
using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.Library.Models;
using Dapper;

namespace CosmicOdyssey.Library.DataAccess;
public class BlogData : IBlogData
{
    private const string CacheName = nameof(BlogData);
    private readonly ISqlDataAccess _sql;
    private readonly ISqlHelper _sqlHelper;
    private readonly IRedisCache _cache;

    public BlogData(
        ISqlDataAccess sql,
        ISqlHelper sqlHelper,
        IRedisCache cache)
    {
        _sql = sql;
        _sqlHelper = sqlHelper;
        _cache = cache;
    }

    public async Task<List<BlogModel>> GetAllBlogsAsync()
    {
        var output = await _cache.GetRecordAsync<List<BlogModel>>(CacheName);
        if (output is null)
        {
            string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.GETALL);
            output = await _sql.LoadDataAsync<BlogModel>(storedProcedure, splitOnColumn: "Id",
                secondaryObjects: new ProfileModel());

            await _cache.SetRecordAsync(CacheName, output, TimeSpan.FromHours(1));
        }

        return output;
    }

    public async Task<List<BlogModel>> GetProfileBlogsAsync(int profileId)
    {
        string key = $"{CacheName}_{profileId}";
        var output = await _cache.GetRecordAsync<List<BlogModel>>(key);
        if (output is null)
        {
            string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.GETBYPROFILEID);
            var parameters = new DynamicParameters();
            parameters.Add("ProfileId", profileId);

            output = await _sql.LoadDataAsync<BlogModel>(storedProcedure, parameters, "Id",
                new ProfileModel());

            await _cache.SetRecordAsync(key, output, TimeSpan.FromHours(1));
        }

        return output;
    }

    public async Task<BlogModel> GetBlogAsync(int id)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.GETBYID);
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        var blog = await _sql.LoadFirstDataAsync<BlogModel>(storedProcedure, parameters,
            splitOnColumn: "Id",
            secondaryObjects: new ProfileModel());

        return blog;
    }

    public async Task<int?> InsertBlogAsync(BlogModel blog)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.INSERT);
        var parameters = new DynamicParameters();
        parameters.Add("ProfileId", blog.Profile.Id);
        parameters.Add("Body", blog.Body);

        await _cache.RemoveRecordAsync(CacheName);

        return await _sql.SaveDataAsync(storedProcedure, parameters);
    }

    public async Task UpdateBlogAsync(BlogModel blog)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.UPDATE);
        var parameters = new DynamicParameters();
        parameters.Add("Id", blog.Id);
        parameters.Add("Body", blog.Body);

        await _sql.SaveDataAsync(storedProcedure, parameters);
    }

    public async Task DeleteBlogAsync(BlogModel blog)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.DELETE);
        var parameters = new DynamicParameters();
        parameters.Add("Id", blog.Id);

        await _sql.SaveDataAsync(storedProcedure, parameters);

        string key = $"{CacheName}_{blog.Profile.Id}";
        await _cache.RemoveRecordAsync(key);
        await _cache.RemoveRecordAsync(CacheName);
    }
}
