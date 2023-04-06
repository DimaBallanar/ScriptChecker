using SqlLauncherAPP.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLauncherAPP.Services
{
    internal class SqlFileService:ISqlFileService
    {
        private readonly ISqlFileRepository n_Repository;
        public SqlFileService( ISqlFileRepository repository)
        { 
            n_Repository = repository?? throw new ArgumentNullException(nameof(repository));
        }

        public void RunSqlFileScripts()
        {
            throw new NotImplementedException();
        }
    }
}
