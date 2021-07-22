using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Service
{
    public class DrugInfo
    {
        private EntDB m_OConn;
        public DrugInfo(EntDB _conn)
        {
            m_OConn = _conn;
        }

        public int UpdateInfo()
        {
            string[] sqls = new string[] { };
            return m_OConn.DoTran(sqls);
        }
        public void AAA()
        {

        }
    }
}
