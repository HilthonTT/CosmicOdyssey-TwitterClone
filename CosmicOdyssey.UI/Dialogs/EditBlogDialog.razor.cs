using Microsoft.AspNetCore.Components;
using MudBlazor;
using CosmicOdyssey.UI.Models;
using CosmicOdyssey.Library.Models;
namespace CosmicOdyssey.UI.Dialogs;

public partial class EditBlogDialog
{
    [CascadingParameter]
    public MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public BlogModel Blog { get; set; }

    private CreateBlogModel model = new();
    private bool isEditing = false;

    protected override void OnInitialized()
    {
        model.Body = Blog.Body;
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
            Blog.Body = model.Body;

            await BlogData.UpdateBlogAsync(Blog);

            Submit();
            Navigation.NavigateTo($"/Blog/{Blog.Id}", true);
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