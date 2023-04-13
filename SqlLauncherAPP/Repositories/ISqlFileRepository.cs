using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SqlLauncherAPP.Repositories
{
    public interface ISqlFileRepository //<T> where T : DbConnection
    {
        void CheckAndCreateTable(MySqlConnection connection);
        List<string> GetAllFilesFromDB(MySqlConnection connection);
        void AddNames(MySqlConnection connection, string text);          
    }
}
