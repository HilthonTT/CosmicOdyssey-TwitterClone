namespace CosmicOdyssey.Library.Models;
public class BlogModel
{
    public int Id { get; set; }
    public ProfileModel Profile { get; set; }
    public string Body { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdated { get; set; }
    public List<int> LikeIds { get; set; } = new();
}
