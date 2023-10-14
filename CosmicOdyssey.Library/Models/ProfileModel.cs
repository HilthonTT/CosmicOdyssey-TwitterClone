namespace CosmicOdyssey.Library.Models;
public class ProfileModel
{
    public int Id { get; set; }
    public string ObjectIdentifier { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string ImageUrl { get; set; }
    public string Email { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    public List<int> FollowingIds { get; set; } = new();
    public int FollowerCount { get; set; }
    public bool HasNotification { get; set; }
}
