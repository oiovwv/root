using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Models
{
    public class ReponseDto<T>
    {
        public bool success { get; set; }
        public T result { get; set; }
    }

    public class RequestOSRDto
    {
        public List<string> waybillNumbers { get; set; }
        public List<string> productCodes { get; set; }
    }
}
