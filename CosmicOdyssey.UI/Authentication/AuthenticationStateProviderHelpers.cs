using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace CosmicOdyssey.UI.Authentication;

public static class AuthenticationStateProviderHelpers
{
    public static async Task<ProfileModel> GetUserFromAuthAsync(
        this AuthenticationStateProvider provider,
        IProfileData profileData)
    {
        var authState = await provider.GetAuthenticationStateAsync();
        string objectId = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("objectidentifier"))?.Value ?? "";

        return await profileData.GetUserFromAuthAsync(objectId);
    }
}
