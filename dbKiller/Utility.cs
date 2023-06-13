using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dbKiller
{
    public static class Utility
    {
        public static void AddSqlParameter(SqlCommand command, string parameterName, SqlDbType dbType, int size, object value)
        {
            SqlParameter parameter = new SqlParameter(parameterName, dbType, size);
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

    }
}
