using Biz;
using Biz.Models;
using Biz.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrugInfoWebAPI.Controllers
{
    public class DrugInfoController : Controller
    {
        // GET: DrugInfo
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UpdateAAA()
        {
            ReponseDto<int> res = new ReponseDto<int>();
            EntDB dB = new EntDB();
            try
            {
                DrugInfo drugInfo = new DrugInfo(dB);

            }
            catch(Exception ex)
            {
                res.success = false;
                res.result = -1;
            }
            finally
            {
                dB.Close();
                dB = null;
            }
            return Json(res);
        }
    }
}