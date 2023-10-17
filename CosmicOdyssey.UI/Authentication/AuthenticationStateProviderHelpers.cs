using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text;

namespace CosmicOdyssey.UI.Authentication;

public static class AuthenticationStateProviderHelpers
{
    public static async Task<ProfileModel> GetUserFromAuthAsync(
        this AuthenticationStateProvider provider,
        IProfileData profileData)
    {
        var authState = await provider.GetAuthenticationStateAsync();
        string objectId = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("objectidentifier"))?.Value;
        if (string.IsNullOrWhiteSpace(objectId))
        {
            return null;
        }

        var currentProfile = await profileData.GetProfileFromAuthAsync(objectId) ?? new();

        bool isDirty = false;

        string firstName = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("givenname"))?.Value ?? "";
        string lastName = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("surname"))?.Value ?? "";
        string displayName = authState.User.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value ?? "";
        string email = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("email"))?.Value ?? "";
        string bio = authState.User.Claims.FirstOrDefault(c => c.Type.Contains("extension_bio") || 
            c.Type == "extension_Bio")?.Value ?? "";

        if (objectId?.Equals(currentProfile.ObjectIdentifier) is false)
        {
            isDirty = true;
            currentProfile.ObjectIdentifier = objectId;
        }

        if (firstName?.Equals(currentProfile.FirstName) is false)
        {
            isDirty = true;
            currentProfile.FirstName = firstName;
        }

        if (lastName?.Equals(currentProfile.LastName) is false)
        {
            isDirty = true;
            currentProfile.LastName = lastName;
        }

        if (displayName?.Equals(currentProfile.DisplayName) is false)
        {
            isDirty = true;
            currentProfile.DisplayName = displayName;
        }

        if (bio?.Equals(currentProfile.Bio) is false)
        {
            isDirty = true;
            currentProfile.Bio = bio;
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
