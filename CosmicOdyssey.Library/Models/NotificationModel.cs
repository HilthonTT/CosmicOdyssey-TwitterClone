namespace CosmicOdyssey.Library.Models;
public class NotificationModel
{
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public string Body { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}
