using Microsoft.AspNetCore.Components;
using MudBlazor;
using CosmicOdyssey.UI.Models;
using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.UI.Dialogs;

public partial class EditCommentDialog
{
    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public CommentModel Comment { get; set; }

    private CreateCommentModel model = new();
    private bool isEditing = false;

    protected override void OnInitialized()
    {
        model.Body = Comment.Body;
    }

    private async Task OnSubmitAsync()
    {
        if (isEditing)
        {
            return;
        }

        try
        {
            isEditing = true;
            Comment.Body = model.Body;
            await CommentData.UpdateCommentAsync(Comment);

            Submit();
            Navigation.NavigateTo($"/Blog/{Comment.BlogId}", true);
        }
        catch (Exception)
        {
            Snackbar.Add("Something went wrong.", Severity.Error);
            Cancel();
        }
        finally
        {
            isEditing = false;
        }
    }

    private void Submit()
    {
        MudDialog?.Close(DialogResult.Ok(true));
    }

    private void Cancel()
    {
        MudDialog?.Cancel();
    }
}