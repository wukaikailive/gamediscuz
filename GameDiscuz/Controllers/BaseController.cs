using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace GameDiscuz.Controllers
{
    public class BaseController :Controller
    {
        public JsonResult Json(int code,String message,Object data)
        {
            return Json(new { code = code, message = message, data = data },JsonRequestBehavior.AllowGet);
        }
    }
}