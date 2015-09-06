using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameDiscuz.BLL
{
    public class User
    {
        GameDiscuz.DAL.User dal = new DAL.User();
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Models.User GetModel(String email, String password)
        {
            return dal.GetModel(m => m.Email == email && m.Password == password);
        }
        public Models.User GetModel(int id)
        {
            return dal.GetModel(id);
        }
        public bool Add(Models.User model)
        {
            return dal.Add(model);
        }
        /// <summary>
        /// 检查邮箱是否使用过
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool Exists(string email)
        {
            return dal.Exists(m => m.Email == email);
        }
        public IEnumerable<Models.User> GetAllList()
        {
            return dal.GetModelList(m => true);
        }

        public IEnumerable<Models.User> GetAllNormalList()
        {
            return dal.GetModelList(m => m.RoleID == (int)Models.RoleType.EDITOR && m.UserStatus == (int)Models.UserStatus.NORMAL);
        }
        public bool Update(Models.User model)
        {
            return dal.Update(model);
        }
        private bool ChangeStatus(int id, Models.UserStatus status)
        {
            Models.User model = dal.GetModel(id);
            model.UserStatus = (int)status;
            return Update(model);
        }
        public bool Delete(int id)
        {
            return ChangeStatus(id, Models.UserStatus.DELETED);
        }
        public IEnumerable<Models.User> GetAllDeleteList()
        {
            return dal.GetModelList(m => m.RoleID == (int)Models.RoleType.EDITOR && m.UserStatus == (int)Models.UserStatus.DELETED);
        }
        public bool Stop(int id)
        {
            return ChangeStatus(id, Models.UserStatus.STOPED);
        }
        public IEnumerable<Models.User> GetAllStopList()
        {
            return dal.GetModelList(m => m.RoleID == (int)Models.RoleType.EDITOR && m.UserStatus == (int)Models.UserStatus.STOPED);
        }
        public bool Restore(int id)
        {
            return ChangeStatus(id, Models.UserStatus.NORMAL);
        }
        public IEnumerable<Models.User> GetAdminPersonalInfo()
        {
            return dal.GetModelList(m => m.RoleID == (int)Models.RoleType.MANAGER && m.UserStatus == (int)Models.UserStatus.NORMAL);
        }
    }
}