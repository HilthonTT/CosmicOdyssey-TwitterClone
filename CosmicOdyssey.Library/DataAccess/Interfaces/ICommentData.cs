using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface ICommentData
{
    Task CreateCommentAsync(CommentModel comment);
    Task DeleteCommentAsync(CommentModel comment);
    Task<List<CommentModel>> GetBlogCommentsAsync(int blogId);
    Task<CommentModel> GetCommentAsync(int id);
    Task UpdateCommentAsync(CommentModel comment);
}