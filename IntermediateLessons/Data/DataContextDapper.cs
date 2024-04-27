using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace IntermediateLessons.Data
{
    public class DataContextDapper
    {
        //private IConfiguration _config;
        private string _connectionString;
        public DataContextDapper(IConfiguration config)
        {
            //_config = config;
            // String contains info for creating connection.
            _connectionString = config.GetConnectionString("DefaultConnection");
        }



        // T is a generic type.
        // Call the method, define the type, do the query.
        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            // returns IEnumerable of T that query of T returns.
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            // No more IEnumerable, thanks QuerySingle.
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            // returns an integer of rows effected.  1+ if it worked, 0 if it did not work.
            // Conditional is trye if rows effected, false if not.
            return (dbConnection.Execute(sql) > 0);
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            // returns an integer of rows effected.
            return dbConnection.Execute(sql);
        }


        // IDbConnection object is used to connect, uses info from the connectString to create
        // the SqlConnection.
        // Now dbConnection has access to query the database.
    }
}
