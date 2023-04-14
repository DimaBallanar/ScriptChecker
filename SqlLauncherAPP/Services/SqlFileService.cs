using MySqlConnector;
using SqlLauncherAPP.Repositories;
using SqlLauncherAPP.Utilites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SqlLauncherAPP.Services
{
    public class SqlFileService : ISqlFileService
    {
        private readonly string ConnectionString = "server=localhost;database=Scripts;uid=root;password=123qwe4r5t6YY;";   // костыль

        string mypath = "D:\\Scripts";
        private readonly ISqlFileRepository n_Repository;
        public SqlFileService(ISqlFileRepository repository)
        {
            n_Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public void Launcher()
        {
            CheckFileNames(GetFilesFromFolder(), n_Repository.GetAllFilesFromDB(Connection()),Connection());

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
        public void RunSqlFileScripts(string fileDirectory)
        {
            string result = File.ReadAllText($"{mypath}\\{fileDirectory}");
            if (result == null) throw new ArgumentNullException(nameof(result));
            MySqlConnection connection = Connection();
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

        private void CheckFileNames(List<string> files, List<string> lists, MySqlConnection connection)
        {
            foreach (var st in files)
            {
                if (!lists.Contains(st))
                {
                    //метод считывания файла с командами и отработка
                    RunSqlFileScripts(st);
                    
                    //метод добавления названия скрипта в БД
                    n_Repository.AddNames(connection, st);
                    Console.WriteLine(st.ToString());
                }
            }
        }

        private MySqlConnection Connection()
        {
            try
            {
                //MySqlConnection connection = new MySqlConnection(AppSettingsHelper.ConnectionString);
                MySqlConnection connection = new MySqlConnection(ConnectionString);

                connection.Open();
                return connection;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("connection error");
            }
        }
    }
}
