using Dapper;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface ISqlDataAccess
{
    void CommitTransaction();
    Task<List<T>> LoadDataAsync<T>(string storedProcedure, DynamicParameters parameters = null, string splitOnColumn = null, params object[] secondaryObjects);
    Task<List<T>> LoadDataInTransactionAsync<T>(string storedProcedure, DynamicParameters parameters);
    Task<T> LoadFirstDataAsync<T>(string storedProcedure, DynamicParameters parameters = null, string splitOnColumn = null, params object[] secondaryObjects);
    Task<T> LoadFirstDataInTransactionAsync<T>(string storedProcedure, DynamicParameters parameters);
    void RollbackTransaction();
    Task<int?> SaveDataAsync(string storedProcedure, DynamicParameters parameters);
    Task<int?> SaveDataInTransactionAsync(string storedProcedure, DynamicParameters parameters);
    void StartTransaction();
}