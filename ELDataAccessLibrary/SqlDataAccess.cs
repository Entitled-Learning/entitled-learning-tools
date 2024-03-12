using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELDataAccessLibrary
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        private readonly string _connectionStringName = "ELSQLConnectionString";
        private string _connectionString { get; set; }

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString(_connectionStringName)!;
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

        public async Task CommitTransactionAsync()
        {
            var transaction = TransactionContext.CurrentTransaction;
            if (transaction != null)
            {
                var connection = TransactionContext.CurrentConnection;
                transaction.Commit();
                TransactionContext.CurrentTransaction = null;
                TransactionContext.CurrentConnection = null;
                connection.Close();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            var transaction = TransactionContext.CurrentTransaction;
            if (transaction != null)
            {
                var connection = TransactionContext.CurrentConnection;
                transaction.Rollback();
                TransactionContext.CurrentTransaction = null;
                TransactionContext.CurrentConnection = null;
                connection.Close();
            }
        }
    }

    public static class TransactionContext
    {
        public static IDbTransaction? CurrentTransaction { get; set; }
        public static SqlConnection? CurrentConnection { get; set; }
    }
}
