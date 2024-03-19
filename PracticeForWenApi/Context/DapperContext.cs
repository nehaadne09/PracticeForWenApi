using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace PracticeForWebApi.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MySqlConnection");
        }
        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}

    