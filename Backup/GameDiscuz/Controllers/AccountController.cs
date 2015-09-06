using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameDiscuz.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            BLL.User bll = new BLL.User();
            Models.User model = bll.GetModel(email, password);
            if (model == null)
            {
                return Redirect("/Account/Login?message=邮箱或密码不正确");
            }
            switch (model.RoleID)
            {
                case 0:
                    Session.Add("user", model);
                    Session.Add("userId", model.UserID);
                    Session.Add("userNickName", model.NickName);
                    return Redirect("/Admin/Index");
                case 1:
                    return Redirect("/Account/Login?message=还木有编辑功能");
                case 2:
                    return Redirect("/Account/Login?message=还木有用户功能");
                default:
                    return Redirect("/Account/Login?message=未知");
            }

        }
    }
}
