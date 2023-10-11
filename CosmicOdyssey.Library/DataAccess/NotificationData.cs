using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.Library.Models;
using Dapper;
using Microsoft.Extensions.Caching.Memory;

namespace CosmicOdyssey.Library.DataAccess;
public class NotificationData : INotificationData
{
    private const string CacheName = nameof(NotificationData);
    private readonly ISqlDataAccess _sql;
    private readonly ISqlHelper _sqlHelper;
    private readonly IMemoryCache _cache;

    public NotificationData(
        ISqlDataAccess sql,
        ISqlHelper sqlHelper,
        IMemoryCache cache)
    {
        _sql = sql;
        _sqlHelper = sqlHelper;
        _cache = cache;
    }

    public async Task<List<NotificationModel>> GetProfileNotificationAsync(int profileId)
    {
        string key = $"{CacheName}_{profileId}";
        var output = _cache.Get<List<NotificationModel>>(key);
        if (output is null)
        {
            string storedProcedure = _sqlHelper.GetStoredProcedure<NotificationModel>(Procedure.GETBYPROFILEID);
            var parameters = new DynamicParameters();
            parameters.Add("ProfileId", profileId);

            output = await _sql.LoadDataAsync<NotificationModel>(storedProcedure, parameters);
            _cache.Set(key, output, TimeSpan.FromMinutes(5));
        }

        return output;
    }

    public async Task<NotificationModel> GetNotificationAsync(int id)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<NotificationModel>(Procedure.GETBYID);
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        return await _sql.LoadFirstDataAsync<NotificationModel>(storedProcedure, parameters);
    }

    public async Task<int?> CreateNotificationAsync(NotificationModel notification)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<NotificationModel>(Procedure.INSERT);
        var parameters = new DynamicParameters();
        parameters.Add("ProfileId", notification.ProfileId);
        parameters.Add("Body", notification.Body);

        return await _sql.SaveDataAsync(storedProcedure, parameters);
    }
}
