using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.UI.Components;

public partial class FollowBar
{
    private List<ProfileModel> profiles;
    protected override async Task OnInitializedAsync()
    {
        profiles = await ProfileData.GetAllProfilesAsync();
    }
}