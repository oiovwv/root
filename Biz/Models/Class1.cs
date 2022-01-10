using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Models
{
    class Class1
    {
    }

    public class EntityDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid idx { get; set; } = Guid.NewGuid();
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool isDelete { get; set; } = false;
        /// <summary>
        /// 记录创建时间
        /// </summary>
        //[JsonConverter(typeof(CustomDateTimeConverter))]
        [Display(Name = "创建时间")]
        public DateTime? addTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 记录编辑时间
        /// </summary>
        //[JsonConverter(typeof(CustomDateTimeConverter))]
        [Display(Name = "编辑时间")]
        public DateTime? editTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 操作人ID号
        /// </summary>
        public Guid operatorId { get; set; }
        /// <summary>
        /// 操作人姓名
        /// </summary>
        [Display(Name = "操作人姓名")]
        public string operatorName { get; set; }
    }
    public class EdiLogDto : EntityDto
    {
        /// <summary>
        /// 类路径，主要用于重新导入时，反射调用
        /// </summary>
        [Display(Name = "类路径")]
        public string classPath { get; set; }
        /// <summary>
        /// 货主代码
        /// </summary>
        [Display(Name = "货主代码")]
        public string storerCode { get; set; }
        /// <summary>
        /// EDI 类型：产品 、 客户 、 订单 通常等于接口函数名
        /// </summary>
        [Display(Name = "接口类型")]
        public string ediType { get; set; }
        /// <summary>
        /// Inbound / Outbound
        /// </summary>
        [Display(Name = "传输类型")]
        public string transferType { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        [Display(Name = "文件名")]
        public string fileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [Display(Name = "文件路径")]
        public string filePath { get; set; }
        /// <summary>
        /// 处理状态
        /// </summary>
        [Display(Name = "处理状态")]
        public string processStatus { get; set; }
        /// <summary>
        /// 处理消息
        /// </summary>
        [Display(Name = "处理消息")]
        public string processMessage { get; set; }
    }
}
