using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLauncherAPP.Utilites
{
    public static class AppSettingsHelper
    {
        private static string _connectionString;
        public static string ConnectionString => _connectionString ??= GetBuilder().GetConnectionString("MyDatabase");
        private static string _pathFolder;
        public static string PathFolder => _pathFolder ??= GetBuilder().GetValue<string>("PathFolder");

        private static IConfigurationRoot GetBuilder()
        {
            return new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("tsconfig1.json", optional: true, reloadOnChange: true)
                .Build();
        }
    }
}
