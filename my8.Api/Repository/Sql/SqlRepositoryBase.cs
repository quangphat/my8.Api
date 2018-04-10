using Microsoft.Extensions.Options;
using my8.Api.Infrastructures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Sql
{
    public class SqlRepositoryBase
    {
        protected IDbConnection connection;
        private SqlServerConnection sqlConnection;
        public SqlRepositoryBase(IOptions<SqlServerConnection> setting)
        {
            sqlConnection = setting.Value;
            Connect(sqlConnection);
        }
        private void Connect(SqlServerConnection sqlConnection)
        {
            if (connection == null)
                connection = new SqlConnection($"server={sqlConnection.ServerURl};database={sqlConnection.Database};User={sqlConnection.UserName};password={sqlConnection.Password};");
        }
    }
}
