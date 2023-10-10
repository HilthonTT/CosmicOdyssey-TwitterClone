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
        };

        if (tableNames.TryGetValue(typeof(T), out string tableName))
        {
            string procedureName = procedure switch
            {
                Procedure.GETALL => "GetAll",
                Procedure.GETBYID => "GetById",
                Procedure.GETBYBLOGID => "GetBlogId",
                Procedure.GETBYPROFILEID => "GetByProfileId",
                Procedure.DELETE => "Delete",
                Procedure.INSERT => "Insert",
                Procedure.UPDATE => "Update",
                _ => "",
            };

            return $"sp{tableName}_{procedureName}";
        }

        throw new ArgumentException($"Type {typeof(T)} is not supported.");
    }
}
