using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface IFollowingData
{
    Task<List<FollowingModel>> GetFollowersAsync(int followeeId);
    Task<bool> ToggleFollowAsync(int currentProfileId, int profileId);
}