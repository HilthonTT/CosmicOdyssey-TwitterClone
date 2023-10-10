using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.Library.Models;
using Dapper;
using Microsoft.Extensions.Caching.Memory;

namespace CosmicOdyssey.Library.DataAccess;
public class BlogData : IBlogData
{
    private const string CacheName = nameof(BlogData);
    private readonly ISqlDataAccess _sql;
    private readonly ISqlHelper _sqlHelper;
    private readonly IMemoryCache _cache;

    public BlogData(
        ISqlDataAccess sql,
        ISqlHelper sqlHelper,
        IMemoryCache cache)
    {
        _sql = sql;
        _sqlHelper = sqlHelper;
        _cache = cache;
    }

    public async Task<List<BlogModel>> GetAllBlogsAsync()
    {
        var output = _cache.Get<List<BlogModel>>(CacheName);
        if (output is null)
        {
            string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.GETALL);
            output = await _sql.LoadDataAsync<BlogModel>(storedProcedure, splitOnColumn: "Id",
                secondaryObjects: new BasicProfileModel());

            _cache.Set(CacheName, output, TimeSpan.FromHours(1));
        }

        return output;
    }

    public async Task<BlogModel> LoadBlogAsync(int id)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.GETALL);
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        var blog = await _sql.LoadFirstDataAsync<BlogModel>(storedProcedure, parameters,
            splitOnColumn: "Id",
            secondaryObjects: new BasicProfileModel());

        return blog;
    }

    public async Task<int?> InsertBlogAsync(BlogModel blog)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.INSERT);
        var parameters = new DynamicParameters();
        parameters.Add("ProfileId", blog.Profile.Id);
        parameters.Add("Body", blog.Body);

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

    public async Task DeleteBlogAsync(int id)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<BlogModel>(Procedure.DELETE);
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        await _sql.SaveDataAsync(storedProcedure, parameters);
    }
}
