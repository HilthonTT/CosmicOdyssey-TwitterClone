using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface ILikeData
{
    Task<List<LikeModel>> GetBlogLikesAsync(int blogId);
    Task<bool> ToggleLikeAsync(int profileId, int blogId);
}