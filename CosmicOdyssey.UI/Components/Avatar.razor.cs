using Microsoft.AspNetCore.Components;
using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.UI.Components;

public partial class Avatar
{
    [Parameter]
    [EditorRequired]
    public ProfileModel Profile { get; set; }

    [Parameter]
    public bool IsLarge { get; set; }

    [Parameter]
    public bool HasBorder { get; set; }

    [Parameter]
    public bool ForceLoad { get; set; } = false;

    private void OnClick()
    {
        string url = $"/profiles/{Profile?.Id}";
        Navigation.NavigateTo(url, ForceLoad);
    }

    private string GetProfileImage()
    {
        if (string.IsNullOrWhiteSpace(Profile?.ProfileImage))
        {
            return "astronaut.png";
        }

        return Profile?.ProfileImage;
    }

    private string GetBorderClass()
    {
        if (HasBorder)
        {
            return "border-4 border-black";
        }

        return "";
    }

    private string GetLargeClass()
    {
        if (IsLarge)
        {
            return $"height:150px; width:150px";
        }

        return $"height:50px; width:50px";
    }
}