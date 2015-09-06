using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameDiscuz.Filter
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["roleId"] == null || ((int)filterContext.HttpContext.Session["roleId"])!=0)
            {
                filterContext.HttpContext.Response.Redirect("/Admin/NeedLogin");
            }
        }
    }
}