using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface IBlogData
{
    Task DeleteBlogAsync(int id);
    Task<List<BlogModel>> GetAllBlogsAsync();
    Task<List<BlogModel>> GetProfileBlogsAsync(int profileId);
    Task<int?> InsertBlogAsync(BlogModel blog);
    Task<BlogModel> GetBlogAsync(int id);
    Task UpdateBlogAsync(BlogModel blog);
}