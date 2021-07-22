using Biz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jda_product_lot
{
    class Program
    {
        
        static void Main(string[] args)
        {
            EntDB dB = new EntDB();
            try
            {
                string filePath = CommonFunction.ChooseFile();
                DataSet ds = CommonFunction.ReadUperExcelFileToDataSet(filePath, 1, 50, "Sheet1");
                string sql2 = string.Empty;
                List<string> sqlList = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sql2 = ((!isExist(dr[0].ToString())) ? $"insert into oms_client_lotvalidation_jda(client_c,batch_number,\r\n                batch_date,EXPIRY_DATE,INV_ATTR_STR1,INV_ATTR_STR2,INV_ATTR_STR3,INV_ATTR_STR4,INV_ATTR_STR5,\r\n                INV_ATTR_STR6,INV_ATTR_STR7,INV_ATTR_STR8,INV_ATTR_STR9,INV_ATTR_STR10,STORAGE_AGE,status) values('{dr[0].ToString()}','{dr[1].ToString()}','{dr[2].ToString()}','{dr[3].ToString()}','{dr[4].ToString()}','{dr[5].ToString()}','{dr[6].ToString()}','{dr[7].ToString()}','{dr[8].ToString()}','{dr[9].ToString()}','{dr[10].ToString()}','{dr[11].ToString()}','{dr[12].ToString()}','{dr[13].ToString()}','{dr[14].ToString()}','{dr[15].ToString()}')" : $"update oms_client_lotvalidation_jda set batch_number='{dr[1].ToString()}',batch_date='{dr[2].ToString()}',EXPIRY_DATE='{dr[3].ToString()}',INV_ATTR_STR1='{dr[4].ToString()}',INV_ATTR_STR2='{dr[5].ToString()}',INV_ATTR_STR3='{dr[6].ToString()}',INV_ATTR_STR4='{dr[7].ToString()}',INV_ATTR_STR5='{dr[8].ToString()}',INV_ATTR_STR6='{dr[9].ToString()}',INV_ATTR_STR7='{dr[10].ToString()}',INV_ATTR_STR8='{dr[11].ToString()}',INV_ATTR_STR9='{dr[12].ToString()}',INV_ATTR_STR10='{dr[13].ToString()}',STORAGE_AGE='{dr[14].ToString()}',status='{dr[15].ToString()}' where client_c='{dr[0].ToString()}'");
                    sqlList.Add(sql2);
                }
                int res = dB.DoTran(sqlList.ToArray());
                if (res > 0)
                {
                    Console.WriteLine("导入完成");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("导入失败，原因：" + ex.Message);
            }

            try
            {
                string filePath = CommonFunction.ChooseFile();
                DataSet ds = CommonFunction.ReadUperExcelFileToDataSet(filePath, 1, 20, "Sheet2");
                string sql2 = string.Empty;
                List<string> sqlList = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sql2 = ((!isExist(dr[0].ToString(), dr[15].ToString())) ? $"insert into oms_client_product_lot_jda(client_c,batch_number,\r\n                batch_date,EXPIRY_DATE,INV_ATTR_STR1,INV_ATTR_STR2,INV_ATTR_STR3,INV_ATTR_STR4,INV_ATTR_STR5,\r\n                INV_ATTR_STR6,INV_ATTR_STR7,INV_ATTR_STR8,INV_ATTR_STR9,INV_ATTR_STR10,STORAGE_AGE,product_no,status) values('{dr[0].ToString()}','{dr[1].ToString()}','{dr[2].ToString()}','{dr[3].ToString()}','{dr[4].ToString()}','{dr[5].ToString()}','{dr[6].ToString()}','{dr[7].ToString()}','{dr[8].ToString()}','{dr[9].ToString()}','{dr[10].ToString()}','{dr[11].ToString()}','{dr[12].ToString()}','{dr[13].ToString()}','{dr[14].ToString()}','{dr[15].ToString()}','{dr[16].ToString()}')" : $"update oms_client_product_lot_jda set batch_number='{dr[1].ToString()}',batch_date='{dr[2].ToString()}',EXPIRY_DATE='{dr[3].ToString()}',INV_ATTR_STR1='{dr[4].ToString()}',INV_ATTR_STR2='{dr[5].ToString()}',INV_ATTR_STR3='{dr[6].ToString()}',INV_ATTR_STR4='{dr[7].ToString()}',INV_ATTR_STR5='{dr[8].ToString()}',INV_ATTR_STR6='{dr[9].ToString()}',INV_ATTR_STR7='{dr[10].ToString()}',INV_ATTR_STR8='{dr[11].ToString()}',INV_ATTR_STR9='{dr[12].ToString()}',INV_ATTR_STR10='{dr[13].ToString()}',STORAGE_AGE='{dr[14].ToString()}',status='{dr[16].ToString()}' where client_c='{dr[0].ToString()}' and product_no='{dr[15].ToString()}'");
                    sqlList.Add(sql2);
                }
                int res = dB.DoTran(sqlList.ToArray());
                if (res > 0)
                {
                    Console.WriteLine("导入完成");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("导入失败，原因：" + ex.Message);
            }
        }

        private static bool isExist(string client_c)
        {
            EntDB dB = new EntDB();
            string sql = $"SELECT COUNT(*) FROM oms_client_lotvalidation_jda WHERE CLIENT_C='{client_c}'";
            int count = Convert.ToInt32(dB.GetObject(sql));
            return count > 0;
        }

        private static bool isExist(string client_c, string product_no)
        {
            EntDB dB = new EntDB();
            string sql = $"SELECT COUNT(*) FROM oms_client_product_lot_jda WHERE CLIENT_C='{client_c}' and product_no='{product_no}'";
            int count = Convert.ToInt32(dB.GetObject(sql));
            return count > 0;
        }
    }
}
