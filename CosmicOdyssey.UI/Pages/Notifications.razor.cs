using CosmicOdyssey.UI.Authentication;
using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.UI.Pages;

public partial class Notifications
{
    private ProfileModel currentProfile;
    private List<NotificationModel> notifications;

    protected override async Task OnInitializedAsync()
    {
        currentProfile = await AuthProvider.GetUserFromAuthAsync(ProfileData);
        notifications = await NotificationData.GetProfileNotificationAsync(currentProfile.Id);
    }
}