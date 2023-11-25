using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Collections;

namespace ELDataAccessLibrary;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;
    private readonly string _connectionStringName = "ELSQLConnectionString";
    private string _connectionString { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SqlDataAccess"/> class.
    /// </summary>
    /// <param name="config">The configuration object used to retrieve the connection string.</param>
    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString(_connectionStringName)!;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters) 
    {        
        using (IDbConnection connection = new SqlConnection(_connectionString))
        {
            var data = await connection.QueryAsync<T>(sql, parameters);
            return data;
        }
    }

    public async Task SaveData<T>(string sql, T parameters) 
    {        
        using (IDbConnection connection = new SqlConnection(_connectionString))
        {
            await connection.ExecuteAsync(sql, parameters);
        }
    }
}

