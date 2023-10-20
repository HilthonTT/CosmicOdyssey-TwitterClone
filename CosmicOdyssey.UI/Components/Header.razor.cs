using Microsoft.AspNetCore.Components;

namespace CosmicOdyssey.UI.Components;

public partial class Header
{
    [Parameter]
    [EditorRequired]
    public string Label { get; set; }

    [Parameter]
    public bool ShowBackArrow { get; set; }

    private void HandleBack()
    {
        Navigation.NavigateTo("/");
    }
}