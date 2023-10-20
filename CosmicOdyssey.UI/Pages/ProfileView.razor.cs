using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using CosmicOdyssey.UI.Authentication;
using MudBlazor;
using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.UI.Pages;

public partial class ProfileView
{
    [Parameter]
    public int Id { get; set; }

    private bool isLoading = true;
    private bool isFollowing = false;
    private bool isBusy = false;
    private List<BlogModel> blogs;
    private List<FollowingModel> followers;
    private ProfileModel currentProfile;
    private ProfileModel fetchedProfile;

    protected override async Task OnInitializedAsync()
    {
        currentProfile = await AuthProvider.GetUserFromAuthAsync(ProfileData);
        await LoadFetchedProfileAsync();
    }

    private async Task LoadFetchedProfileAsync()
    {
        fetchedProfile = await ProfileData.GetProfileAsync(Id);
        if (fetchedProfile is not null)
        {
            blogs = await BlogData.GetProfileBlogsAsync(fetchedProfile.Id);
            followers = await FollowingData.GetFollowersAsync(fetchedProfile.Id);
        }

        isFollowing = followers?.FirstOrDefault(x => x.FollowerId == currentProfile.Id)is not null;
        isLoading = false;
    }

    private void EditProfile()
    {
        Navigation.NavigateTo("/MicrosoftIdentity/Account/EditProfile", true);
    }

    private async Task UploadFileAsync(IBrowserFile file, string cover = "Profile")
    {
        if (fetchedProfile?.Id != currentProfile?.Id)
        {
            return;
        }

        using var stream = file.OpenReadStream(file.Size);
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        string base64String = Convert.ToBase64String(memoryStream.ToArray());

        switch (cover)
        {
            case "Profile":
                fetchedProfile.ProfileImage = $"data:image/png;base64,{base64String}";
                break;
            default:
                fetchedProfile.CoverImage = $"data:image/png;base64,{base64String}";
                break;
        }

        await ProfileData.UpdateProfileAsync(fetchedProfile);
        Navigation.NavigateTo($"/Profiles/{currentProfile?.Id}", true);
    }

    private async Task FollowAsync()
    {
        if (currentProfile is null)
        {
            Navigation.NavigateTo("/MicrosoftIdentity/Account/SignIn", true);
            return;
        }

        if (fetchedProfile is null)
        {
            Snackbar.Add("Something went wrong.", Severity.Error);
            return;
        }

        try
        {
            isBusy = true;
            isFollowing = await FollowingData.ToggleFollowAsync(currentProfile.Id, fetchedProfile.Id);

            if (isFollowing)
            {
                Snackbar.Add($"Following {fetchedProfile?.DisplayName}", Severity.Info);
                fetchedProfile.FollowerCount += 1;
            }
            else
            {
                Snackbar.Add($"Unfollowed {fetchedProfile?.DisplayName}", Severity.Info);
                fetchedProfile.FollowerCount -= 1;
            }
        }
        catch (Exception)
        {
            Snackbar.Add("Something went wrong.", Severity.Error);
        }
        finally
        {
            isBusy = false;
        }
    }

    private string GetFollowButtonClass()
    {
        string defaultClasses = "rounded-full px-4 p-2 transition border-b-white border-2 border";
        if (isFollowing)
        {
            return $"{defaultClasses} bg-black text-white";
        }

        return $"{defaultClasses} bg-white text-black";
    }
}