// ----------------------------------------------------------------
// <copyright company="Tope Daramola">
//     Copyright (c) Tope Daramola. All rights reserved.
// </copyright>
// ----------------------------------------------------------------

using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;

namespace EntitledLearning.Data.SqlClient;

public class SqlDataClient : ISqlDataClient
{
    private readonly IConfiguration _config;
    //private readonly string _connectionStringName = "ELSQLConnectionString";
    private string _connectionString { get; set; }

    public SqlDataClient(IConfiguration config)
    {
        _config = config;
        _connectionString = "Server=tcp:entitled-learning-dbserver.database.windows.net,1433;Initial Catalog=entitled-learning-devdb1;Persist Security Info=False;User ID=entitled-learning-dbadmin;Password=FVlE\"O#g^%kQ$'X-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(); // Open the connection asynchronously
            var data = await connection.QueryAsync<T>(sql, parameters);
            return data;
        }
    }

    public async Task SaveData<T>(string sql, T parameters)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync(); // Open the connection asynchronously
            await connection.ExecuteAsync(sql, parameters);
        }
    }

    public async Task BeginTransactionAsync()
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(); // Open the connection asynchronously

        var transaction = connection.BeginTransaction();
        TransactionContext.CurrentTransaction = transaction;
        TransactionContext.CurrentConnection = connection;
    }

    public void CommitTransactionAsync()
    {
        var transaction = TransactionContext.CurrentTransaction;
        if (transaction != null)
        {
            var connection = TransactionContext.CurrentConnection;
            transaction.Commit();
            TransactionContext.CurrentTransaction = null;
            TransactionContext.CurrentConnection = null;

            if(connection is not null)
            {
                connection.Close();
            }
        }
    }

    public void RollbackTransactionAsync()
    {
        var transaction = TransactionContext.CurrentTransaction;
        if (transaction != null)
        {
            var connection = TransactionContext.CurrentConnection;
            transaction.Rollback();
            TransactionContext.CurrentTransaction = null;
            TransactionContext.CurrentConnection = null;

            if (connection is not null)
            {
                connection.Close();
            }
        }
    }
}

public static class TransactionContext
{
    public static IDbTransaction? CurrentTransaction { get; set; }
    public static SqlConnection? CurrentConnection { get; set; }
}
