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
        if (string.IsNullOrWhiteSpace(objectId))
        {
            return null;
        }

        var currentProfile = await profileData.GetProfileFromAuthAsync(objectId) ?? new();

        bool isDirty = false;

        string firstName = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("givenname"))?.Value;
        string lastName = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("surname"))?.Value;
        string email = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value;
        string fullName = $"{firstName} {lastName}";

        if (objectId?.Equals(currentProfile.ObjectIdentifier) is false)
        {
            isDirty = true;
            currentProfile.ObjectIdentifier = objectId;
        }

        if (fullName?.Equals(currentProfile.Name) is false)
        {
            isDirty = true;
            currentProfile.Name = fullName;
        }

        if (email?.Equals(currentProfile.Email) is false)
        {
            isDirty = true;
            currentProfile.Email = email;
        }

        if (isDirty)
        {
            if (currentProfile.Id is 0)
            {
                await profileData.CreateProfileAsync(currentProfile);
            }
            else
            {
                await profileData.UpdateProfileAsync(currentProfile);
            }
        }

        return await profileData.GetProfileFromAuthAsync(objectId);
    }
}
