using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Models
{
    class LSLRmModel
    {

    }
    public class ProductParamModel
    {
        public CONTROLModel CONTROL { get; set; }
        public List<ProductItem> DATA { get; set; }

    }
    public class CONTROLModel
    {
        public string SYSID { get { return "NONE"; } }
        public string IFID { get { return "MATNR_LGORT"; } }
        public string IFNO { get; set; }
        public string SUSER { get; set; }
        public string SDATE { get; set; }
        public string STIME { get; set; }
        public string SDATATYPE { get { return "JSON"; } }
        public string KEYDATA { get; set; }
    }
    public class ProductItem
    {
        public string S1 { get; set; }
        public string S2 { get; set; }
        public string S3 { get; set; }
        public string S4 { get; set; }
        public string S5 { get; set; }

    }


    public class CONTROL
    {
        /// <summary>
        /// 
        /// </summary>
        public string SYSID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IFID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IFNO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SCENEID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SUBSCENEID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SMTYPE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SUSER { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SDATE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string STIME { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SDATATYPE { get { return "JSON"; } }
        /// <summary>
        /// 
        /// </summary>
        public string KEYDATA { get; set; }
    }

    public class KNA1Item
    {
        /// <summary>
        /// 
        /// </summary>
        public string KTOKD { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BUKRS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KUNNR { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PERSON { get; set; }
        /// <summary>
        /// 溆浦县青蛙王子日化
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 唐利斌
        /// </summary>
        public string NAME2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NAME3 { get; set; }
        /// <summary>
        /// 青蛙王子
        /// </summary>
        public string SORT2 { get; set; }
        /// <summary>
        /// 溆浦县城北雅华楼
        /// </summary>
        public string CO_AD { get; set; }
        /// <summary>
        /// 唐利斌
        /// </summary>
        public string CO_NA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CO_TE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SPERR { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KDGRP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LPRIO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BEZEI { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KONDA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string WERKS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LGORT { get; set; }
        /// <summary>
        /// 长沙RDC
        /// </summary>
        public string LGOBE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LZONE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string STCD5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BZIRK { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VKBUR { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string VSBED { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ZTERM { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KNRZE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KVGR1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AUFSD { get; set; }
    }

    public class KNBKItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string BANKN { get; set; }
        /// <summary>
        /// 唐利斌
        /// </summary>
        public string KOINH { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string WAERS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BANKS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BANKL { get; set; }
        /// <summary>
        /// 中国农业银行股份有限公司怀化分行
        /// </summary>
        public string BANKA { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BKONT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SPERR { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BVTYP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CARTY { get; set; }
    }

    public class KNVPItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string PARVW { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string KUNN2 { get; set; }
        /// <summary>
        /// 溆浦县青蛙王子日化
        /// </summary>
        public string NAME1 { get; set; }
        /// <summary>
        /// 溆浦县城南菊花园马家路口
        /// </summary>
        public string STRAS { get; set; }
    }

    public class DATAItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string KEY { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string XBLCK { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<KNA1Item> KNA1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<KNBKItem> KNBK { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<KNVPItem> KNVP { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public CONTROL CONTROL { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DATAItem> DATA { get; set; }
    }
}
