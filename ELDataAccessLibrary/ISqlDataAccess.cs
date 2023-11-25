namespace ELDataAccessLibrary;

public interface ISqlDataAccess
{
    /// <summary>
    /// Loads data from the database based on the provided SQL query and parameters.
    /// </summary>
    /// <typeparam name="T">The type of the data to be loaded.</typeparam>
    /// <typeparam name="U">The type of the parameters used in the SQL query.</typeparam>
    /// <param name="sql">The SQL query to execute.</param>
    /// <param name="parameters">The parameters used in the SQL query.</param>
    /// <returns>A list of objects of type <typeparamref name="T"/> representing the loaded data.</returns>
    public Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters);

    /// <summary>
    /// Saves data to the database using the provided stored procedure and parameters.
    /// </summary>
    /// <typeparam name="T">The type of the parameters used in the stored procedure.</typeparam>
    /// <param name="storedProcedure">The name of the stored procedure to execute.</param>
    /// <param name="parameters">The parameters used in the stored procedure.</param>
    public Task SaveData<T>(string sql, T parameters);
}

