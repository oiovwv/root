using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSR_OrderTracking.Models
{
    public class PostDto<T>
    {
        public T data { get; set; }
    }
    public class ResponseDto<T>
    {
        public bool status { get; set; }
        public string msg { get; set; }
        public T result { get; set; }
    }

    //如果好用，请收藏地址，帮忙分享。
    public class ExteriorRouteListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 揽件完毕
        /// </summary>
        public string routeStep { get; set; }
        /// <summary>
        /// 快件已由硕放分拨揽件完毕！
        /// </summary>
        public string routeDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uploadDate { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string carInfo { get; set; }
        public string intransitInfo { get; set; }
        public string ref_d_01 { get; set; }
        public string ref_d_02 { get; set; }
        public string ref_d_03 { get; set; }
        public string ref_d_04 { get; set; }
        public string ref_d_05 { get; set; }
    }

    public class ExteriorRoute
    {
        public string timestamp { get; set; }
        public string omsNo { get; set; }

        public string clientOrderNo { get; set; }
        public string waybillNumber { get; set; }

        public string ref_h_01 { get; set; }
        public string ref_h_02 { get; set; }
        public string ref_h_03 { get; set; }
        public string ref_h_04 { get; set; }
        public string ref_h_05 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ExteriorRouteListItem> exteriorRouteList { get; set; }
    }

}