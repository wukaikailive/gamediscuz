using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using GameDiscuz.Filter;
namespace GameDiscuz.Controllers
{
    public class AdminController : BaseController
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
        #region 增加小编
        public ActionResult AddEditer()
        {
            return View();
        }
        [AdminFilter]
        [HttpPost]
        public JsonResult AddEditer(Models.User model,string email)
        {
            string sex =Request.Form["sex"];
            BLL.User bll = new BLL.User();
            model.Sex = sex == "0" ? false : true;
            model.RoleID = (int?)Models.RoleType.EDITOR;
            model.RegisterDate = DateTime.Now;
            model.UserStatus = (int?)Models.UserStatus.NORMAL;
            try
            {
                if (bll.Exists(email))
                {
                    return Json(400, "邮箱已被使用", null);
                }
                bll.Add(model);
                return Json(200, "添加成功", null);
            }
            catch (Exception e)
            {
                return Json(500, e.Message, null);
            }

            
        }

        public JsonResult NeedLogin()
        {
            return Json(300, "需要登录", "/Account/Login");
        }
        #endregion
        #region 查看小编
        public ActionResult ShowEditer()
        {
            return View();
        }

        [AdminFilter]
        public JsonResult GetAllNormalEditors()
        {
            BLL.User bll = new BLL.User();
            try
            {
                var list = bll.GetAllNormalList();

                var jsonObj = from m in list
                              select new
                              {
                                  m.UserID,
                                  m.RoleID,
                                  m.Age,
                                  m.Email,
                                  m.NickName,
                                  RegisterDate = m.RegisterDate.Value.ToLongDateString(),
                                  Sex=m.Sex==true?"男":"女"
                              };
                return Json(200, "ok", jsonObj);
            }
            catch (Exception e)
            {
                return Json(500, e.Message, null);
            }
            
        }

        #endregion
        #region 查看已删除小编
        public ActionResult DeletedEditer()
        {
            return View();
        }
        [AdminFilter]
        public JsonResult GetAllDeleteList()
        {
            BLL.User bll=new BLL.User();
            try{
                var list = bll.GetAllDeleteList();

                var jsonObj = from m in list
                              select new
                              {
                                  m.UserID,
                                  m.RoleID,
                                  m.Age,
                                  m.Email,
                                  m.NickName,
                                  RegisterDate = m.RegisterDate.Value.ToLongDateString(),
                                  Sex = m.Sex == true ? "男" : "女"
                              };
                return Json(200, "ok", jsonObj);
            }
            catch (Exception e)
            {
                return Json(500, e.Message, null);
            }

        }
        #endregion
        #region 查看已停用小编
        public ActionResult StopEditer()
        {
            return View();
        }
        public JsonResult GetAllStopList()
        {
            BLL.User bll = new BLL.User();
            try
            {
                var List = bll.GetAllStopList();
                var jsonObj = from m in List
                              select new
                              {
                                  m.UserID,
                                  m.RoleID,
                                  m.Age,
                                  m.Email,
                                  m.NickName,
                                  RegisterDate = m.RegisterDate.Value.ToLongDateString(),
                                  Sex = m.Sex == true ? "男" : "女"
                              };
                return Json(200,"ok",jsonObj);
            }
            catch(Exception e)
            {
                return Json(500, e.Message, null);
            }
        }
        #endregion
        #region 修改管理员个人信息
        public ActionResult PersonalInfo()
        {
            return View();
        }
        [AdminFilter]
        public JsonResult GetAdminPersonalInfo()
        {
            int id =(int)Session["userId"];
            BLL.User bll = new BLL.User();
            try
            {
                var m=bll.GetModel(id);
                var jsonObj = new{
                                  m.UserID,
                                  m.RoleID,
                                  m.Age,
                                  m.Email,
                                  m.NickName,
                                  RegisterDate = m.RegisterDate.Value.ToLongDateString(),
                                  Sex = m.Sex == true ? "男" : "女"
                              };
                return Json(200, "", jsonObj);
            }
            catch (Exception e)
            {
                return Json(500,e.Message, null);
            }
            
        }
        [AdminFilter]
        public JsonResult RevisePersonalInfo(Models.User form)
        {
            int id = (int)Session["userId"];
            BLL.User bll = new BLL.User();
            try
            {
                Models.User model = bll.GetModel(id);
                model.Age = form.Age;
                model.NickName = form.NickName;
                string sex = Request.Form["sex"];
                model.Sex = sex == "0" ? false : true;
                bll.Update(model);
                return Json(200,"修改成功",null);
            }
            catch (Exception e)
            {
                return Json(500, e.Message, null);
            }
        }
        #endregion
        #region 修改密码
        public ActionResult RevisePassword()
        {
            return View();
        }
        public JsonResult PasswordAuthentication(Models.User Form)
        {
            int id =(int)Session["userId"];
            BLL.User bll = new BLL.User();
            try
            {
                Models.User model = bll.GetModel(id);
                if (Form.Password == Form.NickName)
                {
                    if (model.Password == Form.Email)
                    {
                        model.Password = Form.Password;
                        bll.Update(model);
                        return Json(200, "修改成功", null);
                    }
                    else
                    {
                        return Json(301, "密码错误", null); 
                    }
                }else {
                        return Json(302, "两次输入的密码不一", null);
                    }    
            }
            catch (Exception e)
            {
                return Json(500, e.Message, null);
            } 
        }
        #endregion
        #region 操作
        [AdminFilter]
        public JsonResult Stop(int id)
        {
            BLL.User bll = new BLL.User();
            try
            {
                bll.Stop(id);
                return Json(200, "success", null);
            }
            catch (Exception e)
            {
                return Json(500, e.Message, null);
            }

        }
        [AdminFilter]
        public JsonResult Delete(int id)
        {
            BLL.User bll = new BLL.User();
            try
            {
                bll.Delete(id);
                return Json(200, "删除成功", null);
            }
            catch (Exception e)
            {
                return Json(500, e.Message, null);
            }

        }
        public JsonResult Restore(int id)
        {
            BLL.User bll = new BLL.User();
            try
            {
                bll.Restore(id);
                return Json(200, "删除成功", null);
            }
            catch (Exception e)
            {
                return Json(500, e.Message, null);
            }
        }
        #endregion

    }
}
