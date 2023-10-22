using Microsoft.AspNetCore.Components;
using MudBlazor;
using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.UI.Dialogs;

public partial class DeleteBlogDialog
{
    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public BlogModel Blog { get; set; }

    private bool isDeleting = false;
    private async Task DeleteAsync()
    {
        if (isDeleting)
        {
            return;
        }

        try
        {
            isDeleting = true;
            await BlogData.DeleteBlogAsync(Blog);

            Submit();
            Navigation.NavigateTo("/");
        }
        catch (Exception)
        {
            Snackbar.Add("Something went wrong.", Severity.Error);
            Cancel();
        }
        finally
        {
            isDeleting = false;
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