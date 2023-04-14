using MySqlConnector;
using SqlLauncherAPP.Utilites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLauncherAPP.Repositories
{
    public class MySqlFileRepository : ISqlFileRepository//<MySqlConnection>
    {
        private readonly string SQL_CreateTable = @"create table if not exists TableScripts (
          Id INT auto_increment not null, 
          Name varchar(255) not null, 
          primary key(id) 
        );";
        private readonly string SQL_selectItems = "select name from TableScripts;";
        private readonly string SQL_AddItem = "insert into TableScripts (name) values (@name1);";
        public void AddNames(MySqlConnection connection, string text)
        {
            if (connection == null) throw new Exception("Connection Error");
            if (text == null) throw new ArgumentNullException(nameof(text));
            var transaction = connection.BeginTransaction();                 
            try
            {
                MySqlCommand command = new MySqlCommand(SQL_AddItem, connection);
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@name1", text);
                command.ExecuteNonQuery();
                transaction.Commit();
                Console.WriteLine("Скрипты добавлены в БД ");
            }
            catch (MySqlException ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex);
                throw ex;
            }
            finally
            {                
                connection.Close();                
            }
        }

        public void CheckAndCreateTable(MySqlConnection connection)
        {
            if (connection == null) throw new Exception("Сonnection error");
            try
            {               
                MySqlCommand command = new MySqlCommand(SQL_CreateTable, connection);
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

        public List<string> GetAllFilesFromDB(MySqlConnection connection)
        {
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
            finally
            {
                connection.Close();
            }
        }
        
       
    }

}
