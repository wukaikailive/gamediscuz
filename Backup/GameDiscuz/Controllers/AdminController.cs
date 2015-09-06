using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace GameDiscuz.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Default()
        {
            return View();
        }
        public ActionResult AddEditer(Models.User model)
        {
            BLL.User bll = new BLL.User();
            model.RoleID = 1;
            model.RegisterDate = DateTime.Now;
            model.UserStatus = (int?)Models.UserStatus.NORMAL;
            try
            {
                bll.Add(model);
                return Redirect("/Admin/AddEditer");
            }
            catch (Exception e)
            {

            }

            
        }
    }
}
