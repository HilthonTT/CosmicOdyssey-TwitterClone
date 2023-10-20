using Microsoft.AspNetCore.Components;
using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.UI.Components;

public partial class BlogItem
{
    [Parameter]
    [EditorRequired]
    public BlogModel Blog { get; set; }

    [Parameter]
    [EditorRequired]
    public ProfileModel CurrentProfile { get; set; }

    private List<CommentModel> comments;
    private List<LikeModel> likes;
    private bool isLiked = false;
    private int likeCount = 0;

    protected override async Task OnInitializedAsync()
    {
        comments = await CommentData.GetBlogCommentsAsync(Blog.Id);
        likes = await LikeData.GetBlogLikesAsync(Blog.Id);

        likeCount = likes.Count;
        isLiked = likes.FirstOrDefault(x => x.ProfileId == CurrentProfile?.Id) is not null;
    }

    private void LoadProfilePage()
    {
        Navigation.NavigateTo($"/Profile/{Blog.Profile.Id}");
    }

    private void LoadBlogPage()
    {
        Navigation.NavigateTo($"/Blog/{Blog.Id}");
    }

    private async Task OnLikeAsync()
    {
        if (CurrentProfile is null)
        {
            Navigation.NavigateTo("/MicrosoftIdentity/Account/SignIn", true);
        }

        isLiked = await LikeData.ToggleLikeAsync(CurrentProfile.Id, Blog.Id);
        if (isLiked)
        {
            likeCount += 1;
        }
        else
        {
            likeCount -= 1;
        }
    }
}