using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface IProfileData
{
    Task<int?> CreateProfileAsync(ProfileModel profile);
    Task<List<ProfileModel>> GetAllProfilesAsync();
    Task<ProfileModel> GetProfileAsync(int id);
    Task<ProfileModel> GetProfileFromAuthAsync(string oid);
    Task UpdateProfileAsync(ProfileModel profile);
}