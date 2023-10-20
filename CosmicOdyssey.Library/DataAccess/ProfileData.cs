using CosmicOdyssey.Library.Cache.Interfaces;
using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.Library.Models;
using Dapper;

namespace CosmicOdyssey.Library.DataAccess;
public class ProfileData : IProfileData
{
    private const string CacheName = nameof(ProfileData);
    private readonly ISqlDataAccess _sql;
    private readonly ISqlHelper _sqlHelper;
    private readonly IRedisCache _cache;

    public ProfileData(
        ISqlDataAccess sql,
        ISqlHelper sqlHelper,
        IRedisCache cache)
    {
        _sql = sql;
        _sqlHelper = sqlHelper;
        _cache = cache;
    }

    public async Task<List<ProfileModel>> GetAllProfilesAsync()
    {
        var output = await _cache.GetRecordAsync<List<ProfileModel>>(CacheName);
        if (output is null)
        {
            string storedProcedure = _sqlHelper.GetStoredProcedure<ProfileModel>(Procedure.GETALL);
            output = await _sql.LoadDataAsync<ProfileModel>(storedProcedure);

            await _cache.SetRecordAsync(CacheName, output, TimeSpan.FromHours(1));
        }

        return output;
    }

    public async Task<ProfileModel> GetProfileAsync(int id)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<ProfileModel>(Procedure.GETBYID);
        var parameters = new DynamicParameters();
        parameters.Add("Id", id);

        return await _sql.LoadFirstDataAsync<ProfileModel>(storedProcedure, parameters);
    }

    public async Task<ProfileModel> GetProfileFromAuthAsync(string oid)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<ProfileModel>(Procedure.GETBYOID);
        var parameters = new DynamicParameters();
        parameters.Add("ObjectIdentifier", oid);

        return await _sql.LoadFirstDataAsync<ProfileModel>(storedProcedure, parameters);
    }

    public async Task<int?> CreateProfileAsync(ProfileModel profile)
    {
        try
        {
            string storedProcedure = _sqlHelper.GetStoredProcedure<ProfileModel>(Procedure.INSERT);
            var parameters = new DynamicParameters();
            parameters.Add("ObjectIdentifier", profile.ObjectIdentifier);
            parameters.Add("FirstName", profile.FirstName);
            parameters.Add("LastName", profile.LastName);
            parameters.Add("DisplayName", profile.DisplayName);
            parameters.Add("Email", profile.Email);
            parameters.Add("Bio", profile.Bio);

            return await _sql.SaveDataAsync(storedProcedure, parameters);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task UpdateProfileAsync(ProfileModel profile)
    {
        string storedProcedure = _sqlHelper.GetStoredProcedure<ProfileModel>(Procedure.UPDATE);
        var parameters = new DynamicParameters();
        parameters.Add("Id", profile.Id);
        parameters.Add("FirstName", profile.FirstName);
        parameters.Add("LastName", profile.LastName);
        parameters.Add("DisplayName", profile.DisplayName);
        parameters.Add("ProfileImage", profile.ProfileImage);
        parameters.Add("CoverImage", profile.CoverImage);
        parameters.Add("Email", profile.Email);
        parameters.Add("Bio", profile.Bio);
        parameters.Add("DateUpdated", DateTime.UtcNow);

        await _sql.SaveDataAsync(storedProcedure, parameters);
    }
}
