using CosmicOdyssey.UI.Authentication;
using MudBlazor;
using CosmicOdyssey.UI.Models;
using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.UI.Pages;

public partial class Index
{
    private CreateBlogModel model = new();
    private ProfileModel currentProfile;
    private List<BlogModel> blogs;
    private bool isCreating = false;
    protected override async Task OnInitializedAsync()
    {
        currentProfile = await AuthProvider.GetUserFromAuthAsync(ProfileData);
        blogs = await BlogData.GetAllBlogsAsync();
    }

    private void LogIn()
    {
        Navigation.NavigateTo("/MicrosoftIdentity/Account/SignIn", true);
    }

    private async Task OnSubmitAsync()
    {
        if (currentProfile is null)
        {
            LogIn();
        }

        try
        {
            isCreating = true;
            var b = new BlogModel
            {
                Profile = currentProfile,
                Body = model.Body
            };

            await BlogData.InsertBlogAsync(b);
            model = new();

            blogs.Insert(0, b);
            Snackbar.Add("Tweeted!", Severity.Info);
        }
        catch (Exception)
        {
            Snackbar.Add("Something went wrong.", Severity.Error);
        }
        finally
        {
            isCreating = false;
        }
    }
}