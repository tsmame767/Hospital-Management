using Microsoft.Data.SqlClient;
using System.Data;

namespace PatientService.ContextDB
{
    public class DBContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectStr;

        public DBContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectStr = configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()=> new SqlConnection(connectStr);


    }
}
