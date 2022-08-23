using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Dron.Data
{

    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection(string ConnectionStringName = "DB")
        {
            var _connectionString = _configuration.GetConnectionString(ConnectionStringName);
            return new SqlConnection(_connectionString);
        }


    }

}