using Dapper;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface ISqlDataAccess
{
    Task<List<T>> LoadDataAsync<T>(string storedProcedure, DynamicParameters parameters = null, string splitOnColumn = null, params object[] secondaryObjects);
    Task<T> LoadFirstDataAsync<T>(string storedProcedure, DynamicParameters parameters = null, string splitOnColumn = null, params object[] secondaryObjects);
    Task<int?> SaveDataAsync(string storedProcedure, DynamicParameters parameters);
}