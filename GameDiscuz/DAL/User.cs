using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity;
using GameDiscuz.Models;
using System.Text;
using System.Linq.Expressions;
using GameDiscuz.Common;

namespace GameDiscuz.DAL
{
    public class User
    {

        GameDiscuzEntities mEntities = new GameDiscuzEntities();


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return mEntities.Users.Any(o => o.UserID == id);
        }

        /// <summary>
        /// 是否存在满足条件的记录
        /// </summary>
        public bool Exists(Expression<Func<Models.User, bool>> predicate)
        {
            return mEntities.Users.Any(predicate);
        }

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Models.User model)
        {

            using (GameDiscuzEntities entities = new GameDiscuzEntities())
            {
                entities.Users.Add(model);
                try
                {
                    entities.SaveChanges();
                    return true;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Models.User model)
        {
            using (GameDiscuzEntities entities = new GameDiscuzEntities())
            {
                entities.Users.Attach(model);
                entities.Entry(model).State = EntityState.Modified;
                try
                {
                    entities.SaveChanges();
                    return true;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (GameDiscuzEntities entities = new GameDiscuzEntities())
            {
                Models.User model = entities.Users.SingleOrDefault(m => m.UserID == id);
                entities.Users.Remove(model);
                try
                {
                    entities.SaveChanges();
                    return true;
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 获取一个实体对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Models.User GetModel(int id)
        {
            Models.User model = mEntities.Users.Where(m => m.UserID == id).SingleOrDefault();
            if (model != null)
            {
                mEntities.Entry(model).State = EntityState.Detached;
            }
            return model;
        }
        public Models.User GetModel(Expression<Func<Models.User,bool>> predicate)
        {
            Models.User model = mEntities.Users.Where(predicate).SingleOrDefault();
            if (model != null)
            {
                mEntities.Entry(model).State = EntityState.Detached;
            }
            return model;
        }

        /// <summary>
        ///获取实体的集合
        /// </summary>
        /// <typeparam name="T">List的类型</typeparam>
        /// <param name="selector">指定投影的字段，使用lambda表达式</param>
        /// <returns></returns>
        public IEnumerable<Models.User> GetModelList(Expression<Func<Models.User, bool>> predicate)
        {
            return mEntities.Users.Where(predicate);
        }


        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <typeparam name="Tkey">谓词参数</typeparam>
        /// <param name="predicate">筛选谓词</param>
        /// <param name="orderBy">排序条件</param>
        /// <param name="isDesc">是否降序</param>
        /// <param name="page">当前页</param>
        /// <param name="pageNum">每页要显示的记录数目</param>
        /// <returns></returns>
        public IQueryable<Models.User> GetListByPage<Tkey>(Expression<Func<Models.User, bool>> predicate, Expression<Func<Models.User, Tkey>> orderBy, bool isDesc, int page, int pageNum)
        {
            IQueryable<Models.User> _model;

            _model = (predicate != null) ? mEntities.Users.Where(predicate) : null;
            if (orderBy != null)
            {
                if (!isDesc)
                {
                    _model = (_model == null) ? mEntities.Users.OrderBy(orderBy) : _model.OrderBy(orderBy);
                }
                else
                {
                    _model = _model = (_model == null) ? mEntities.Users.OrderByDescending(orderBy) : _model.OrderByDescending(orderBy);
                }
            }
            else
            {
                _model = _model.OrderBy(o => o.UserID);
            }

            _model = _model.Skip((page - 1) * pageNum).Take(pageNum);
            return _model;
        }


    }
}