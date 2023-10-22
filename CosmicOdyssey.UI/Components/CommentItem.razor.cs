using Microsoft.AspNetCore.Components;
using CosmicOdyssey.Library.Models;
using CosmicOdyssey.UI.Dialogs;
using MudBlazor;

namespace CosmicOdyssey.UI.Components;

public partial class CommentItem
{
    [Parameter]
    [EditorRequired]
    public CommentModel Comment { get; set; }

    private void LoadProfilePage()
    {
        Navigation.NavigateTo($"/Profiles/{Comment?.Profile?.Id}");
    }

    private async Task OpenEditDialogAsync()
    {
        var parameters = new DialogParameters<EditCommentDialog>
        {
            { x => x.Comment, Comment }
        };

        await DialogService.ShowAsync<EditCommentDialog>($"Edit your comment?", parameters);
    }

    private async Task OpenDeleteDialogAsync()
    {
        var parameters = new DialogParameters<DeleteCommentDialog>
        {
            { x => x.Comment, Comment }
        };

        await DialogService.ShowAsync<DeleteCommentDialog>($"Delete your comment?", parameters);
    }
}