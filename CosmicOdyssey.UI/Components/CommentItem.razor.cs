using Microsoft.AspNetCore.Components;
using CosmicOdyssey.Library.Models;

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
}