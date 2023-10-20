using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface INotificationData
{
    Task<int?> CreateNotificationAsync(NotificationModel notification, bool isTransaction = false);
    Task<NotificationModel> GetNotificationAsync(int id);
    Task<List<NotificationModel>> GetProfileNotificationAsync(int profileId);
}