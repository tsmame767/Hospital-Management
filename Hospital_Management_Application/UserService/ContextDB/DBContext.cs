using Microsoft.Data.SqlClient;
using System.Data;

namespace Users.ContextDB
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectStr;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            connectStr = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(this.connectStr);
    }
}
