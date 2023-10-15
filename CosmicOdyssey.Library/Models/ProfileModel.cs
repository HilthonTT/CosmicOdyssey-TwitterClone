namespace CosmicOdyssey.Library.Models;
public class ProfileModel
{
    public int Id { get; set; }
    public string ObjectIdentifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    public string Bio { get; set; }
    public string ProfileImage { get; set; }
    public string CoverImage { get; set; }
    public string Email { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    public List<int> FollowingIds { get; set; } = new();
    public int FollowerCount { get; set; }
    public bool HasNotification { get; set; }
}
