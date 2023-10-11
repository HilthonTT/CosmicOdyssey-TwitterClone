using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface IProfileData
{
    Task<int?> CreateProfileAsync(ProfileModel profile);
    Task<List<ProfileModel>> GetAllProfilesAsync();
    Task<ProfileModel> GetUserAsync(int id);
    Task<ProfileModel> GetUserFromAuthAsync(string oid);
    Task UpdateProfileAsync(ProfileModel profile);
}