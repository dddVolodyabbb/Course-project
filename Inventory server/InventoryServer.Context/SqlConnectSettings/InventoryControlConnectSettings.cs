
using System.Data.SqlClient;

namespace InventoryServer.Context.SqlConnectSettings
{
    public class InventoryControlConnectSettings
    {
        public static string SqlConnectionIntegratedSecurity
        {
            get
            {
                var sb = new SqlConnectionStringBuilder
                {
                    DataSource = "DESKTOP-01LDJSC\\SQLEXPRESS",
                    IntegratedSecurity = true,
                    InitialCatalog = "InventoryControl"
                };

                return sb.ConnectionString;
            }
        }
    }
}
