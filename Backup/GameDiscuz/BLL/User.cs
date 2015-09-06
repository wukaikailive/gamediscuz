using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameDiscuz.BLL
{
    public class User
    {
        GameDiscuz.DAL.User dal = new DAL.User();

        public Models.User GetModel(String email, String password)
        {
            return dal.GetModel(m => m.Email == email && m.Password == password);
        }
        public bool Add(Models.User model)
        {
            return dal.Add(model);
        }
    }
}