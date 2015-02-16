using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace goatMGMt.DAL
{
    public class DataConfiguration : DbConfiguration
    {
        public DataConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}