using CosmicOdyssey.UI.Authentication;
using MudBlazor;
using CosmicOdyssey.UI.Models;
using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.UI.Components;

public partial class Sidebar
{
    private List<SidebarItemModel> items = new();
    private List<NotificationModel> notifications;
    private ProfileModel currentProfile;
    private bool isAlert = false;

    protected override async Task OnInitializedAsync()
    {
        currentProfile = await AuthProvider.GetUserFromAuthAsync(ProfileData, false);
        LoadNavbarItems();

        if (currentProfile is not null)
        {
            notifications = await NotificationData.GetProfileNotificationAsync(currentProfile.Id);
            isAlert = notifications?.Count > 0 ? true : false;
        }
    }

    private void LoadNavbarItems()
    {
        items = new()
        {
            new()
            {
                Label = "Home",
                Href = "/",
                Icon = Icons.Material.Filled.House
            },

            new()
            {
                Label = "Notifications",
                Href = "/Notifications",
                Icon = Icons.Material.Filled.Doorbell
            },
            new()
            {
                Label = "Profile",
                Href = GetProfileUrl(),
                Icon = Icons.Material.Filled.Person
            },
        };
    }

    private void LoadHomePage()
    {
        Navigation.NavigateTo("/");
    }

    private void LogOut()
    {
        Navigation.NavigateTo("/MicrosoftIdentity/Account/SignOut", true);
    }

    private void LogIn()
    {
        Navigation.NavigateTo("/MicrosoftIdentity/Account/SignIn", true);
    }

    private void NavigateTo(string href)
    {
        if (href == "Profile" || currentProfile is null)
        {
            LogIn();
        }
        else
        {
            Navigation.NavigateTo(href);
        }
    }

    private string GetProfileUrl()
    {
        if (currentProfile is null)
        {
            return "/MicrosoftIdentity/Account/SignIn";
        }

        return $"/profiles/{currentProfile?.Id}";
    }
}