using SqlLauncherAPP.Utilites;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IScanner scanner = new Scanner();
            scanner.Start();
        }
    }
}