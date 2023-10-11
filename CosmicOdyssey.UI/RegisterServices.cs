using CosmicOdyssey.Library.DataAccess;
using CosmicOdyssey.Library.DataAccess.Interfaces;
using CosmicOdyssey.Library.Helpers;
using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.UI.Data;

namespace CosmicOdyssey.UI;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<WeatherForecastService>();

        builder.Services.AddTransient<ISqlHelper, SqlHelper>();
        builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();

        builder.Services.AddSingleton<IBlogData, BlogData>();
        builder.Services.AddSingleton<ICommentData, CommentData>();
        builder.Services.AddSingleton<ILikeData, LikeData>();
        builder.Services.AddSingleton<INotificationData, NotificationData>();
        builder.Services.AddSingleton<IProfileData, ProfileData>();
    }
}
