using CosmicOdyssey.Library.Helpers.Interfaces;
using CosmicOdyssey.Library.Models;

namespace CosmicOdyssey.Library.Helpers;
public class SqlHelper : ISqlHelper
{
    public string GetStoredProcedure<T>(Procedure procedure)
    {
        var tableNames = new Dictionary<Type, string>
        {
            { typeof(BlogModel), "Blog" },
            { typeof(CommentModel), "Comment" },
            { typeof(NotificationModel), "Notification" },
            { typeof(ProfileModel), "Profile" },
            { typeof(LikeModel), "Like" },
            { typeof(FollowingModel), "Following" },
        };

        if (tableNames.TryGetValue(typeof(T), out string tableName) is false )
        {
            throw new ArgumentException($"Type {typeof(T)} is not supported.");
        }

        string procedureName = procedure switch
        {
            Procedure.GETALL => "GetAll",
            Procedure.GETBYOID => "GetByOid",
            Procedure.GETBYID => "GetById",
            Procedure.GETBYBLOGID => "GetByBlogId",
            Procedure.GETBYPROFILEID => "GetByProfileId",
            Procedure.DELETE => "Delete",
            Procedure.INSERT => "Insert",
            Procedure.UPDATE => "Update",
            Procedure.FOLLOWEEID => "GetByFolloweeId",
            Procedure.FOLLOWERID => "GetByFollowerId",
            _ => "",
        };

        return $"sp{tableName}_{procedureName}";
    }
}
