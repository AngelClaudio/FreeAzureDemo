using Dapper; // For QueryAsync
using Microsoft.Data.SqlClient; // For SqlConnection
using System.Data; // For IDbConnection

namespace DatabaseDemoApp
{
    public class SqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        // LoadData method to load data from the database,
        // T is the type of data we want to return, which is w.e. we want,
        // it's a generic (in this case it will be the Person model)
        public async Task<List<T>> LoadData<T, U>(
            string storedProcedure, 
            U parameters,
            string connectionStringName = "default")
        {
            // Using statement to open and close the connection properly, lives until end of curly braces
            using IDbConnection connection = new SqlConnection(
                _config.GetConnectionString(connectionStringName)); // Get the connection string from appsettings.json

            // QueryAsync returns a Task<IEnumerable<T>> so we need to await it
            List<T> rows = (await connection.QueryAsync<T>(
                storedProcedure, 
                parameters, 
                commandType: CommandType.StoredProcedure)).ToList(); // we will pass person usp

            return rows;
            // return rows, of whatever type T is

        }
    }
}
