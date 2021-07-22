using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Service
{
    public class User
    {
        private EntDB m_oConn;
        public User(EntDB conn)
        {
            m_oConn = conn;
        }

        public bool isTrue()
        {
            return true;
        }
    }
}
