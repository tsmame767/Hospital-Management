using Microsoft.Data.SqlClient;
using System.Data;

namespace DoctorService.Context
{
    public class ContextDB
    {
        private readonly IConfiguration _configuration;
        private readonly string connectStr;

        public ContextDB(IConfiguration configuration)
        {
            _configuration = configuration;
            connectStr = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(this.connectStr);
    }

}
