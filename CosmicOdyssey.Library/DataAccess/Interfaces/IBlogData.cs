using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface IBlogData
{
    Task DeleteBlogAsync(int id);
    Task<List<BlogModel>> GetAllBlogsAsync();
    Task<int?> InsertBlogAsync(BlogModel blog);
    Task<BlogModel> LoadBlogAsync(int id);
    Task UpdateBlogAsync(BlogModel blog);
}