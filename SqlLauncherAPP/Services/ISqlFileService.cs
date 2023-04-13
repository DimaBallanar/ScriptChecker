using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlLauncherAPP.Services
{
    public interface ISqlFileService
    {
        public List<string> GetFilesFromFolder();
        public void RunSqlFileScripts();

    }
}
