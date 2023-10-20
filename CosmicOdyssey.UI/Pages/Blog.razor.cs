using Microsoft.AspNetCore.Components;
using CosmicOdyssey.UI.Authentication;
using MudBlazor;
using CosmicOdyssey.UI.Models;
using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.UI.Pages;

public partial class Blog
{
    [Parameter]
    public int Id { get; set; }

    private CreateCommentModel model = new();
    private BlogModel fetchedBlog;
    private ProfileModel currentProfile;
    private List<CommentModel> comments;
    private bool isLoading = true;
    private bool isCreating = false;

    protected override async Task OnInitializedAsync()
    {
        fetchedBlog = await BlogData.GetBlogAsync(Id);
        comments = await CommentData.GetBlogCommentsAsync(Id);
        currentProfile = await AuthProvider.GetUserFromAuthAsync(ProfileData);
        isLoading = false;
    }

    private async Task OnSubmitAsync()
    {
        if (currentProfile is null)
        {
            Navigation.NavigateTo("/MicrosoftIdentity/Account/SignIn", true);
        }

        try
        {
            isCreating = true;
            var c = new CommentModel
            {
                BlogId = Id,
                Profile = currentProfile,
                Body = model.Body
            };
            await CommentData.CreateCommentAsync(c);
            model = new();

            comments.Insert(0, c);
            Snackbar.Add("Replied!", Severity.Info);
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