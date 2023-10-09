using Dapper;

namespace CosmicOdyssey.Library.DataAccess.Interfaces;
public interface ISqlDataAccess
{
    Task<List<T>> LoadDataAsync<T>(string storedProcedure, DynamicParameters parameters);
    Task<T> LoadFirstDataAsync<T>(string storedProcedure, DynamicParameters parameters);
}