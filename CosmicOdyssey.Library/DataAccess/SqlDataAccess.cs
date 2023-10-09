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

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    private string GetConnectionString()
    {
        return _config.GetConnectionString(DbName);
    }

    public async Task<List<T>> LoadDataAsync<T>(string storedProcedure, DynamicParameters parameters)
    {
        string connectionString = GetConnectionString();

        using var connection = new SqlConnection(connectionString);
        var rows = await connection.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);

        return rows.ToList();
    }

    public async Task<T> LoadFirstDataAsync<T>(string storedProcedure, DynamicParameters parameters)
    {
        string connectionString = GetConnectionString();

        using var connection = new SqlConnection(connectionString);
        var row = await connection.QueryFirstOrDefaultAsync(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);

        return row;
    }
}
