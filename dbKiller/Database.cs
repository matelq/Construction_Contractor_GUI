using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbKiller
{
    public class Database
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
        private SqlConnection sqlConnection = new SqlConnection();
        
        public void InitializeConnectionString(string user = null, string pass = null)
        {
            if(user != null && pass != null)
            {
                sqlConnectionStringBuilder.UserID = user;
                sqlConnectionStringBuilder.Password = pass;
            }
            sqlConnectionStringBuilder.DataSource = "localhost";
            sqlConnectionStringBuilder.InitialCatalog = "Construct_database";
            sqlConnectionStringBuilder.Encrypt = true;
            sqlConnectionStringBuilder.TrustServerCertificate = true;
            sqlConnectionStringBuilder.PersistSecurityInfo = false;

            sqlConnection.Close();
            sqlConnection.ConnectionString = sqlConnectionStringBuilder.ConnectionString;
        }
        public void OpenConnection()
        {
            if (sqlConnection.ConnectionString == null)
                return;

            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }

    }
}
