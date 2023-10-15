namespace CosmicOdyssey.Library.Models;
public class BasicProfileModel
{
    public int Id { get; set; }
    public string DisplayName { get; set; }
    public string ProfileImage { get; set; }

    public BasicProfileModel()
    {
        
    }

    public BasicProfileModel(ProfileModel profile)
    {
        Id = profile.Id;
        DisplayName = profile.DisplayName;
        ProfileImage = profile.ProfileImage;
    }
}
