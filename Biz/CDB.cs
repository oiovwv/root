using System;
using System.Data;
using System.Data.OleDb;

namespace Biz
{
    public class CDB
    {
        private OleDbConnection m_oConn;
        private string m_strConn;
        private string m_logPath;

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        protected void CreateConnection(string strConn, string logPath)
        {
            m_oConn = new OleDbConnection(strConn);
            m_logPath = logPath;
        }

        private void WriteLog(string logText)
        {
            Cfg.WriteDBLog(m_logPath, logText);
        }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return m_strConn;
            }
            set
            {
                m_strConn = value;
                m_oConn.ConnectionString = m_strConn;
            }
        }
        public OleDbConnection Conn { get { return m_oConn; } }
        public bool ConnStatus
        {
            get { return m_oConn.State == ConnectionState.Open ? true : false; }
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns>true：成功   false：失败</returns>
        public bool Open()
        {
            try
            {
                if (m_oConn.State == ConnectionState.Open)
                {
                    m_oConn.Close();
                }
                m_oConn.Open();
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                throw new Exception("DBConnectFial:" + ex.Message);
            }
        }

        public object GetObject(string sql)
        {
            OleDbCommand cmd;
            object obj = null;
            try
            {
                if (Open())
                {
                    WriteLog(sql);
                    cmd = new OleDbCommand(sql, m_oConn);
                    obj = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                WriteLog(sql + "+|+" + ex.Message);
                throw ex;
            }
            finally
            {
                cmd = null;
                Close();
            }
            return obj;
        }



        /// <summary>
        /// 执行SQL语句并返回DataSet记录集
        /// </summary>
        /// <param name="strSQL">执行SQL语句</param>
        /// <returns>DataSet记录集</returns>
        public DataSet ExecuteToDataSet(string strSQL)
        {
            OleDbCommand cmd;
            OleDbDataAdapter dap;
            DataSet data;
            try
            {
                WriteLog(strSQL);
                cmd = new OleDbCommand(strSQL, m_oConn);
                dap = new OleDbDataAdapter(cmd);
                data = new DataSet();
                dap.Fill(data);
                return data;
            }
            catch (Exception ex)
            {
                WriteLog(strSQL + "+|+" + ex.Message);
                throw new Exception("GetDataSetFail");
            }
            finally
            {
                cmd = null;
                dap = null;
                data = null;
            }
        }
        /// <summary>
        /// 执行SQL语句并返回DataTable记录集
        /// </summary>
        /// <param name="strSQL">执行SQL语句</param>
        /// <returns>DataTable 记录集</returns>
        public DataTable ExecuteToDataTable(string strSQL)
        {
            OleDbCommand cmd;
            OleDbDataAdapter dap;
            DataSet data;
            try
            {
                //WriteLog(strSQL);

                cmd = new OleDbCommand(strSQL, m_oConn);
                dap = new OleDbDataAdapter(cmd);
                data = new DataSet();
                dap.Fill(data);
                return data.Tables[0];
            }
            catch (Exception ex)
            {
                //WriteLog(strSQL + "+|+" + ex.Message);
                throw new Exception("GetDataTableFail");
            }
            finally
            {
                cmd = null;
                dap = null;
                data = null;
            }
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="strSQL">执行SQL语句</param>
        /// <returns>true：成功   false：失败</returns>
        public bool Execute(string strSQL)
        {
            Open();
            OleDbCommand cmd;
            try
            {
                WriteLog(strSQL);

                cmd = new OleDbCommand(strSQL, m_oConn);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(strSQL + "+|+" + ex.Message);
                throw new Exception("ExecuteSQLFail");
            }
            finally
            {
                cmd = null;
            }
        }

        public int ExecuteSql(string strSQL)
        {
            int result = 0;
            OleDbCommand cmd;
            try
            {
                WriteLog(strSQL);

                cmd = new OleDbCommand(strSQL, m_oConn);
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                WriteLog(strSQL + "+|+" + ex.Message);
                result = -1;
                throw new Exception("ExecuteSQLFail");
            }
            finally
            {
                cmd = null;
            }
            return result;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public OleDbCommand DBCommand()
        {
            OleDbCommand cmd;
            try
            {
                cmd = new OleDbCommand();
                cmd.Connection = m_oConn;
                return cmd;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return null;
            }
            finally
            {
                cmd = null;
            }
        }
        /// <summary>
        /// 开始一个事务
        /// </summary>
        /// <returns>返回事务处理对象</returns>
        public OleDbTransaction BeginTrans()
        {
            OleDbTransaction trans;
            try
            {
                trans = m_oConn.BeginTransaction();
                return trans;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            try
            {
                if (m_oConn.State == ConnectionState.Open)
                {
                    m_oConn.Close();
                }
            }
            catch
            {

            }
        }


        /// <summary>
        /// 将一组SQL在一个事务中执行
        /// </summary>
        /// <param name="SQLs"></param>
        /// <returns></returns>
        public int DoTran(string[] SQLs)
        {
            Open();
            int i = 0;
            OleDbTransaction trans = this.BeginTrans();
            string strSQL = "";
            try
            {
                OleDbCommand cmd = this.DBCommand();
                cmd.Transaction = trans;
                i = 0;
                foreach (string str in SQLs)
                {
                    strSQL = str;
                    WriteLog(strSQL);
                    if (str.Length > 0)
                    {
                        cmd.CommandText = str;
                        i += cmd.ExecuteNonQuery();
                    }
                }
                trans.Commit();
                cmd.Dispose();
                Close();
                cmd = null;
            }
            catch (Exception ex)
            {
                WriteLog(strSQL + "+|+" + ex.Message);
                if (trans != null)
                {
                    trans.Rollback();
                    throw new Exception("执行SQL语句组失败");
                }
                else
                {
                    i = -1;
                }
            }
            finally
            {
                Close();
            }

            return i;
        }
        public string GetTableIDX(string strTableName)
        {
            string strSQL2 = "UPDATE OMS.TABLE_SEQUENCE SET IDX_NUM = IDX_NUM + 1 WHERE TABLE_NAME = '" + strTableName + "'";
            OleDbTransaction oTrans2 = BeginTrans();
            OleDbCommand oCmd2 = DBCommand();
            oCmd2.Transaction = oTrans2;
            try
            {
                oCmd2.CommandText = strSQL2;
                oCmd2.ExecuteNonQuery();
                strSQL2 = (oCmd2.CommandText = "SELECT IDX_NUM,IDX_MAX FROM OMS.TABLE_SEQUENCE WHERE TABLE_NAME = '" + strTableName + "'");
                OleDbDataAdapter oDap = new OleDbDataAdapter(oCmd2);
                DataSet data = new DataSet();
                oDap.Fill(data);
                string strKey = data.Tables[0].Rows[0]["IDX_NUM"].ToString();
                if (long.Parse(strKey) > long.Parse(data.Tables[0].Rows[0]["IDX_MAX"].ToString()))
                {
                    throw new Exception("获取序列号出错，已超过" + strTableName + "表最大序列号");
                }
                oTrans2.Commit();
                return strKey;
            }
            catch (Exception ex)
            {
                oTrans2.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                oTrans2 = null;
                oCmd2 = null;
            }
        }
    }
}
