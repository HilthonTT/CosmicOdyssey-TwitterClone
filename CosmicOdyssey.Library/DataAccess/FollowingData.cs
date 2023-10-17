using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.Library.Models;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CosmicOdyssey.Library.DataAccess;
public class FollowingData : IFollowingData
{
    private readonly ISqlDataAccess _sql;
    private readonly ISqlHelper _sqlHelper;
    private readonly ILogger<FollowingData> _logger;

    public FollowingData(
        ISqlDataAccess sql,
        ISqlHelper sqlHelper,
        ILogger<FollowingData> logger)
    {
        _sql = sql;
        _sqlHelper = sqlHelper;
        _logger = logger;
    }

    public async Task<List<FollowingModel>> GetFollowersAsync(int followeeId)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<FollowingModel>(Procedure.FOLLOWEEID);
        var parameters = new DynamicParameters();
        parameters.Add("FolloweeId", followeeId);

        return await _sql.LoadDataAsync<FollowingModel>(storedProcedure, parameters);
    }

    public async Task<bool> ToggleFollowAsync(int currentProfileId, int profileId)
    {
        if (profileId == currentProfileId)
        {
            return false;
        }

        try
        {
            var followers = await GetFollowersAsync(profileId);
            var userFollow = followers.FirstOrDefault(x => x.FollowerId == currentProfileId);

            string storedProcedure;
            var parameters = new DynamicParameters();
            parameters.Add("FollowerId", currentProfileId);
            parameters.Add("FolloweeId", profileId);

            if (userFollow is null)
            {
                storedProcedure = _sqlHelper.GetStoredProcedure<FollowingModel>(Procedure.INSERT);
                

                var newFollow = new FollowingModel
                {
                    FollowerId = currentProfileId,
                    FolloweeId = profileId,
                };

                followers.Add(newFollow);
            }
            else
            {
                storedProcedure = _sqlHelper.GetStoredProcedure<FollowingModel>(Procedure.DELETE);

                followers.Remove(userFollow);
            }

            await _sql.SaveDataAsync(storedProcedure, parameters);

            return userFollow is null;
        }
        catch (Exception ex)
        {
            _logger.LogError("Internal Error [FOLLOWING_TOGGLE]: {error}", ex.Message);
            Console.WriteLine($"Internal Error: {ex.Message}");
            throw;
        }
    }
}
