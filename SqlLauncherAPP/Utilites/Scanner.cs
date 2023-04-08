using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLauncherAPP.Utilites
{
    public class Scanner:IScanner
    {
        string mypath = "D:\\ДЗ С#\\hschool\\hschool_beggining_csh\\OOPLearning\\checking\\Scripts";
        private readonly string ConnectionString = "server=localhost;database=Scripts;uid=root;password=123qwe4r5t6YY;";
        //private readonly string SQL_CreateTable = @"create table if not exists TableScripts (
        //  Id INT auto_increment not null, 
        //  Name varchar(255) not null, 
        //  primary key(id) 
        //);";
        private readonly string SQL_selectItems = "select name from TableScripts;";
        private readonly string SQL_AddItem = "insert into TableScripts (name) values (@name1);";
        // метод, который считает все названия скриптов с базы данных
        public void Start()
        {


            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                try
                {
                    //MySqlConnection connection = Connection();
                    //MySqlCommand command = new MySqlCommand(SQL_AddDataBase, connection);
                    //command.CommandText = SQL_UseDatabase;
                    //command.CommandText = SQL_CreateTable;
                    //connection.Close();
                    //GetScriptsName();    // названия скриптов с БД
                    //ListNames();     //названия файлов с папки
                    CheckFileNames(ListNames(), GetScriptsName(connection), connection);
                    connection.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    Console.WriteLine("Добавлено в список ");
                    //connection.Close();
                }
            }
        }
        private List<string> GetScriptsName(MySqlConnection connection)   // тянем  все названия скриптов с базы данных
        {
            //MySqlConnection connection = Connection();
            if (connection == null) throw new Exception("connection error");
            try
            {
                List<string> scripts = new List<string>();
                MySqlCommand command = new MySqlCommand(SQL_selectItems, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    scripts.Add(reader.GetString(0));
                }
                return scripts;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
        }
        // метод считывания названий файлов в папке
        private List<string> ListNames()
        {
            List<string> listAllFileNames = new List<string>();
            string[] fileAll = Directory.GetFiles(mypath, "*.sql", SearchOption.TopDirectoryOnly);
            foreach (string str in fileAll)
            {
                listAllFileNames.Add(Path.GetFileName(str));                           // REPLACE****************************
            }
            return listAllFileNames;
        }

        // метод, который сравнит названия,если нет его в списке,то вызывает метод считывания и его выполнение, затем добавление записи в базу данных
        private void CheckFileNames(List<string> files, List<string> lists, MySqlConnection connection)
        {
            foreach (var st in files)
            {
                if (!lists.Contains(st))
                {
                    //метод считывания файла с командами и отработка
                    readSQLFile(st, connection);
                    Console.WriteLine(st);

                    //метод добавления названия скрипта в БД
                    AddScriptInTable(st, connection);
                }
            }
        }
        //метод добавления записи в базу данных
        private void AddScriptInTable(string scr, MySqlConnection connection)
        {
            var transaction = connection.BeginTransaction();
            if (scr == null) throw new ArgumentNullException(nameof(scr));
            if (connection == null) throw new Exception("Connection Error");
            try
            {
                MySqlCommand command = new MySqlCommand(SQL_AddItem, connection);
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@name1", scr);
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (MySqlException ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex);
                throw ex;
            }
            //finally
            //{
            //    connection.Close();
            //}
        }
        // метод считывания файла(скрипта) с папки и далее выполнение всех команд
        private void readSQLFile(string fileDirectory, MySqlConnection connection)
        {
            string result = File.ReadAllText($"{mypath}\\{fileDirectory}");
            ProcessLoader(result, connection);

        }
        // метод выполнения команд со считанного файла
        private void ProcessLoader(string text, MySqlConnection connection)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));
            //MySqlConnection connection = Connection();
            if (connection == null) throw new Exception("Connection Error");
            try
            {
                MySqlCommand command = new MySqlCommand(text, connection);
                //command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex);
                throw ex;
            }
            //finally
            //{
            //    connection.Close();
            //}
        }
        //private MySqlConnection Connection()
        //{
        //    try
        //    {
        //        MySqlConnection connection = new MySqlConnection(ConnectionString);
        //        connection.Open();
        //        return connection;
        //    }
        //    catch (MySqlException ex)
        //    {
        //        return null;
        //    }
        //}

    }
}
