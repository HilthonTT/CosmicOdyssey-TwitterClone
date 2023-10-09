namespace CosmicOdyssey.Library.Models;
public class CommentModel
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public int BlogId { get; set; }
    public string Body { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdated { get; set; }
}
