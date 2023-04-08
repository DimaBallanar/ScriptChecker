using MySqlConnector;
using SqlLauncherAPP.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLauncherAPP.Repositories
{
    public class MySqlFileRepository:ISqlFileRepository
    {
        private MySqlConnection Connection()
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(AppSettingsHelper.ConnectionString);
                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                return null;
            }
        }
    }
}
