using CosmicOdyssey.Library.DataAccess.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CosmicOdyssey.Library.DataAccess;
public class SqlDataAccess : ISqlDataAccess
{
    private const string DbName = "CosmicOdysseyData";
    private readonly IConfiguration _config;
    private static IDbConnection _connection;
    private static IDbTransaction _transaction;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    private string GetConnectionString()
    {
        return _config.GetConnectionString(DbName);
    }

    private static void Dispose()
    {
        _transaction?.Dispose();
        _connection?.Dispose();

        _transaction = null;
        _connection = null;
    }

    private static void MapProperty<T, U>(T primaryEntity, U secondaryObject)
    {
        var properties = typeof(T).GetProperties();
        var selectedProperty = properties.FirstOrDefault(
            x => x.PropertyType == secondaryObject.GetType() ||
            x.Name == secondaryObject.GetType().Name);

        selectedProperty?.SetValue(primaryEntity, secondaryObject);
    }

    public async Task<List<T>> LoadDataAsync<T>(
        string storedProcedure,
        DynamicParameters parameters = null,
        string? splitOnColumn = null,
        params object[]? secondaryObjects)
    {
        string connectionString = GetConnectionString();
        using var connection = new SqlConnection(connectionString);

        if (string.IsNullOrWhiteSpace(splitOnColumn) || secondaryObjects?.Length <= 0)
        {
            var rows = await connection.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);

            return rows.ToList();
        }

        var types = new List<Type> { typeof(T) };
        types.AddRange(secondaryObjects.Select(obj => obj.GetType()));

        var entities = await connection.QueryAsync(
            storedProcedure,
            types.ToArray(),
            map: (objects) =>
            {
                var primaryEntity = (T)objects[0];

                Parallel.For(0, objects.Length, (i) =>
                {
                    MapProperty(primaryEntity, objects[i]);
                });

                return primaryEntity;
            },
            splitOn: splitOnColumn,
            param: parameters,
            commandType: CommandType.StoredProcedure);

        return entities.ToList();
    }

    public async Task<T> LoadFirstDataAsync<T>(
        string storedProcedure,
        DynamicParameters parameters,
        string? splitOnColumn = null,
        params object[]? secondaryObjects)
    {
        var rows = await LoadDataAsync<T>(storedProcedure, parameters, splitOnColumn, secondaryObjects);

        return rows.FirstOrDefault();
    }

    public async Task<int?> SaveDataAsync(string storedProcedure, DynamicParameters parameters)
    {
        string connectionString = GetConnectionString();
        using var connection = new SqlConnection(connectionString);

        int? insertedId = await connection.QueryFirstOrDefaultAsync<int?>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);

        return insertedId ?? 0;
    }

    public void StartTransaction()
    {
        string connectionString = GetConnectionString();

        _connection = new SqlConnection(connectionString);
        _connection.Open();

        _transaction = _connection.BeginTransaction();
    }

    public void CommitTransaction()
    {
        _transaction?.Commit();
        _connection?.Close();

        Dispose();
    }

    public void RollbackTransaction()
    {
        _transaction?.Rollback();
        _connection?.Close();

        Dispose();
    }

    public async Task<List<T>> LoadDataInTransactionAsync<T>(string storedProcedure, DynamicParameters parameters)
    {
        var rows = await _connection.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure, transaction: _transaction);

        return rows.ToList();
    }

    public async Task<T> LoadFirstDataInTransactionAsync<T>(string storedProcedure, DynamicParameters parameters)
    {
        var row = await _connection.QueryFirstOrDefaultAsync<T>(storedProcedure, parameters, 
            commandType: CommandType.StoredProcedure, transaction: _transaction);

        return row;
    }

    public async Task<int?> SaveDataInTransactionAsync(string storedProcedure, DynamicParameters parameters)
    {
        int? insertedId = await _connection.QueryFirstOrDefaultAsync<int?>(storedProcedure, parameters,
           commandType: CommandType.StoredProcedure, transaction: _transaction);

        return insertedId ?? 0;
    }
}
