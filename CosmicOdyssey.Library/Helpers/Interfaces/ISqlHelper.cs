namespace CosmicOdyssey.Library.Helpers.Interfaces;

public interface ISqlHelper
{
    string GetStoredProcedure<T>(Procedure procedure);
}