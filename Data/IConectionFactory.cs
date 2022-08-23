using System.Data;

namespace Dron.Data
{
    public interface IConnectionFactory
    {
        IDbConnection CreateConnection(string connectionStringName = "DB");
    }

}