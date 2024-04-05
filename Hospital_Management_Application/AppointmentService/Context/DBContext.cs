using Microsoft.Data.SqlClient;
using System.Data;

namespace AppointmentService.Context
{
    public class DBContext
    {
        private readonly IConfiguration config;
        private readonly string connectStr;

        public DBContext(IConfiguration _config)
        {
            this.config = _config;
            connectStr = config.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(connectStr);
    }
}
