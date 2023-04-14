using SqlLauncherAPP.Repositories;
using SqlLauncherAPP.Services;
using SqlLauncherAPP.Utilites;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISqlFileService scanner = new SqlFileService(new MySqlFileRepository());
            scanner.Launcher();
        }
    }
}