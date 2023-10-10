namespace CosmicOdyssey.Library.Models;
public class BasicProfileModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }

    public BasicProfileModel()
    {
        
    }

    public BasicProfileModel(ProfileModel profile)
    {
        Id = profile.Id;
        Name = profile.Name;
        ImageUrl = profile.ImageUrl;
    }
}
