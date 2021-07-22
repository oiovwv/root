using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz
{
    public class EntDB : CDB
    {
        public EntDB(string connStr)
        {
            CreateConnection(connStr,Cfg.LogPath);
        }
        public EntDB()
        {
            CreateConnection("Provider=MSDAORA.1;Password=o!m#s$@;User ID=oms;Data Source=10g",Cfg.LogPath);
        }
    }
}
