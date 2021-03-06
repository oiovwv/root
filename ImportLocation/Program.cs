using Biz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportLocation
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = ConfigurationManager.AppSettings["Path"].ToString();
            var files = Directory.GetFiles(path, "*.xls");
            foreach(var filePath in files)
            {
                DataSet dsRes = CommonFunction.ReadExcelFileToDataSet(filePath, 1, 28);
                var sql = string.Format(@"select  max(l.serialkey) from wh2.loc l");
                EntDB dB = new EntDB("Provider=MSDAORA.1;Password=stagrape;User ID=wh2;Data Source=prod");
                var o = Convert.ToInt32(dB.GetObject(sql));
                var no = o;
                List<string> sqls = new List<string>();
                sqls.Clear();
                for(var i = 0; i < dsRes.Tables[0].Rows.Count; i++)
                {
                    
                    var dr = dsRes.Tables[0].Rows[i];
                    sql = string.Format(@"select * from wh2.loc where loc = '{0}'",dr[0].ToString());
                    var dt = dB.ExecuteToDataTable(sql);
                    if (dt.Rows.Count <= 0)
                    {
                        o++;
                        sql = string.Format(@"insert into wh2.loc
  (LOC,
   LOGICALLOCATION,
   SERIALKEY,
   WHSEID,
   CUBE,
   LENGTH,
   WIDTH,
   HEIGHT,
   LOCATIONTYPE,
   LOCATIONFLAG,
   LOCATIONHANDLING,
   LOCATIONCATEGORY,
   CUBICCAPACITY,
   WEIGHTCAPACITY,
   STATUS,
   LOSEID,
   FACILITY,
   SECTION,
   ABC,
   PICKZONE,
   PUTAWAYZONE,
   SECTIONKEY,
   PICKMETHOD,
   COMMINGLESKU,
   COMMINGLELOT,
   LOCLEVEL,
   XCOORD,
   YCOORD,
   ZCOORD,
   OPTLOC,
   STACKLIMIT,
   FOOTPRINT,
   LOCGROUPID,
   LOCSTATUS)

  (SELECT '{0}',
          '{1}',
          {2},
          a.WHSEID,
          a.CUBE,
          a.LENGTH,
          a.WIDTH,
          a.HEIGHT,
          a.LOCATIONTYPE,
          a.LOCATIONFLAG,
          a.LOCATIONHANDLING,
          a.LOCATIONCATEGORY,
          a.CUBICCAPACITY,
          a.WEIGHTCAPACITY,
          a.STATUS,
          a.LOSEID,
          a.FACILITY,
          a.SECTION,
          a.ABC,
          a.PICKZONE,
          a.PUTAWAYZONE,
          a.SECTIONKEY,
          a.PICKMETHOD,
          a.COMMINGLESKU,
          a.COMMINGLELOT,
          a.LOCLEVEL,
          a.XCOORD,
          a.YCOORD,
          a.ZCOORD,
          a.OPTLOC,
          a.STACKLIMIT,
          a.FOOTPRINT,
          a.LOCGROUPID,
          a.LOCSTATUS
   
     FROM wh2.LOC A
    WHERE A.serialkey = '{3}'
   
   )", dr[0].ToString(), dr[0].ToString(), o, no.ToString());


                        //                  sql = string.Format(@"insert into wh35.loc
                        //(LOC,
                        // LOGICALLOCATION,
                        // SERIALKEY,
                        // WHSEID,
                        // CUBE,
                        // LENGTH,
                        // WIDTH,
                        // HEIGHT,
                        // LOCATIONTYPE,
                        // LOCATIONFLAG,
                        // LOCATIONHANDLING,
                        // LOCATIONCATEGORY,
                        // CUBICCAPACITY,
                        // WEIGHTCAPACITY,
                        // STATUS,
                        // LOSEID,
                        // FACILITY,
                        // SECTION,
                        // ABC,
                        // PICKZONE,
                        // PUTAWAYZONE,
                        // SECTIONKEY,
                        // PICKMETHOD,
                        // COMMINGLESKU,
                        // COMMINGLELOT,
                        // LOCLEVEL,
                        // XCOORD,
                        // YCOORD,
                        // ZCOORD,
                        // OPTLOC,
                        // STACKLIMIT,
                        // FOOTPRINT,
                        // LOCGROUPID,
                        // LOCSTATUS)

                        //(SELECT '{0}',
                        //        a.LOGICALLOCATION,
                        //        '{1}',
                        //        a.WHSEID,
                        //        a.CUBE,
                        //        a.LENGTH,
                        // a.WIDTH,
                        // a.HEIGHT,
                        //        a.LOCATIONTYPE,
                        //        a.LOCATIONFLAG,
                        //        a.LOCATIONHANDLING,
                        //        a.LOCATIONCATEGORY,
                        //        a.CUBICCAPACITY,
                        // a.WEIGHTCAPACITY,
                        //        a.STATUS,
                        //        a.LOSEID,
                        //        a.FACILITY,
                        //        a.SECTION,
                        //        a.ABC,
                        //        a.PICKZONE,
                        //        a.PUTAWAYZONE,
                        //        a.SECTIONKEY,
                        //        a.PICKMETHOD,
                        //        a.COMMINGLESKU,
                        //        a.COMMINGLELOT,
                        //        a.LOCLEVEL,
                        //        a.XCOORD,
                        //        a.YCOORD,
                        //        a.ZCOORD,
                        //        a.OPTLOC,
                        //        a.STACKLIMIT,
                        //        a.FOOTPRINT,
                        //        a.LOCGROUPID,
                        //        a.LOCSTATUS

                        //   FROM WH35.LOC A
                        //  WHERE A.serialkey = '194935'

                        // )", dr[0].ToString(), o);


                        sqls.Add(sql);
                    }
                    
                }
                var res = dB.DoTran(sqls.ToArray());
                var aaa = string.Format("");
            }
        }
    }
}
