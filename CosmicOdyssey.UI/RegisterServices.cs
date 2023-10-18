using CosmicOdyssey.Library.Cache;
using CosmicOdyssey.Library.Cache.Interfaces;
using CosmicOdyssey.Library.DataAccess;
using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;

namespace CosmicOdyssey.UI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMudServices();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
        builder.Services.AddMemoryCache();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireClaim("jobTitle", "Admin");
            });
        });

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
            options.InstanceName = "CosmicOdyssey_";
        });

        builder.Services.AddTransient<ISqlHelper, SqlHelper>();

        builder.Services.AddSingleton<IRedisCache, RedisCache>();

        builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
        builder.Services.AddSingleton<IBlogData, BlogData>();
        builder.Services.AddSingleton<ICommentData, CommentData>();
        builder.Services.AddSingleton<ILikeData, LikeData>();
        builder.Services.AddSingleton<INotificationData, NotificationData>();
        builder.Services.AddSingleton<IProfileData, ProfileData>();
        builder.Services.AddSingleton<IFollowingData, FollowingData>();
    }
}
