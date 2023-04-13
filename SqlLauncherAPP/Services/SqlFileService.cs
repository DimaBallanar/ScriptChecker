using MySqlConnector;
using SqlLauncherAPP.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SqlLauncherAPP.Services
{
   public class SqlFileService:ISqlFileService
    {
        string mypath = "D:\\Scripts";
        private readonly ISqlFileRepository n_Repository;
        public SqlFileService( ISqlFileRepository repository)
        { 
            n_Repository = repository?? throw new ArgumentNullException(nameof(repository));
        }
        public List<string> GetFilesFromFolder()
        {
            List<string> listAllFileNames = new List<string>();
            string[] fileAll = Directory.GetFiles(mypath, "*.sql", SearchOption.TopDirectoryOnly);
            foreach (string str in fileAll)
            {
                listAllFileNames.Add(Path.GetFileName(str));                           
            }
            return listAllFileNames;
        }
        public void RunSqlFileScripts()
        {
            string result = File.ReadAllText($"{mypath}\\{fileDirectory}");
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (connection == null) throw new Exception("Connection Error");
            try
            {
                MySqlCommand command = new MySqlCommand(result, connection);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
