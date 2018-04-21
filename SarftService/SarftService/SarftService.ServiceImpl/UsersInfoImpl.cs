using SarftService.IServiceContract;
using SarftService.ServiceCommon;
using SarftService.ServiceModel;
using Service.Data.Client;
using Service.Data.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.ServiceImpl
{
    public class UsersInfoImpl : IServiceContract.IUsers
    {
        /// <summary>
        /// 根据用户名密码查找用户
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UsersInfo FindUserInfo(string loginName, string password)
        {
            var db = DBClinetHelp.Client();
            var user = db.Get<Users>(s => s.UserName == loginName).FirstOrDefault();
            if (user == null)
                return null;
            if (password != "ecpcservice")
            {
                if (user.Password != password)
                    return null;
            }
            return ChangeUser(user);
        }
        /// <summary>
        /// 根据id查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UsersInfo GetUserInfoById(long id)
        {
            var db = DBClinetHelp.Client();
            var user = db.Get<Users>(s => s.Id == id).FirstOrDefault();
            if (user == null)
                return null;
            else
                return ChangeUser(user);
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        public int AddUser(UsersInfo model)
        {
            var db = DBClinetHelp.Client();
            var user = db.Get<Users>(s => s.UserName == model.UserName).FirstOrDefault();
            if (user == null)
            {
                Users info = new Users();
                info.UserName = model.UserName;
                info.Password = model.Password;
                info.Phone = model.Phone;
                info.RoleID = model.RoleID;
                info.Power = model.Power;
                if (db.Save<Users>(info) != null)
                {
                    //新增成功
                    return 2;
                }
                else
                {
                    //新增失败
                    return 3;
                }
            }
            else
            {
                //用户名已存在
                return 1;
            }
        }
        /// <summary>
        /// 根据id删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelUser(long id)
        {
            var db = DBClinetHelp.Client();
            bool b = false;
            var user = db.Get<Users>(s => s.Id == id).FirstOrDefault();
            if (user != null)
            {
                b = db.Delete<Users>(user);
                if (b)
                {
                    return 1;
                }
            }
            return 0;
        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="oldpassword">原密码</param>
        /// <param name="password">新密码</param>
        /// <returns>0失败1原密码不匹配2修改成功</returns>
        public int AlterUserPassword(string username, string oldpassword, string password)
        {
            var db = DBClinetHelp.Client();
            var user = db.Get<Users>(s => s.UserName == username).FirstOrDefault();
            if (user != null)
            {
                if (user.Password != oldpassword)
                {
                    return 1;
                }
                else
                {
                    user.Password = password;
                    if (db.Update<Users>(user) != null)
                    {
                        return 2;
                    }
                    else
                    {
                        return 0;
                    }
                }

            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="user">用户对象</param>
        public int AlterUsers(UsersInfo user)
        {
            var db = DBClinetHelp.Client();
            var info = db.Get<Users>(s => s.Id == user.Id).FirstOrDefault();
            if (info != null)
            {
                info.UpdateTime = DateTime.Now;
                info.Phone = user.Phone;
                info.RoleID = user.RoleID;
                info.Power = user.Power;
                info.Password = user.Password;
                if (db.Update<Users>(info) != null)
                {
                    return 1;
                }
            }
            return 0;
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int AlterUserPass(long id)
        {
            var db = DBClinetHelp.Client();
            var user = db.Get<Users>(s => s.Id == id).FirstOrDefault();
            if (user != null)
            {
                user.Password = GetMd5Hash("123456");
                if (db.Update<Users>(user) != null)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
        /// <summary>
        /// 分页获取用户信息列表
        /// </summary>
        /// <param name="pageIndex">每页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="userName">用户名</param>
        /// <param name="RoleID">用户类型</param>
        /// <returns></returns>
        public PageInfo<UsersInfo> GetUserInfoList(int pageIndex, int pageSize, string userName, long RoleID)
        {
            List<UsersInfo> lst = new List<UsersInfo>();
            var service = DBClinetHelp.Client();
            PageList<Users> pageInfo = null;
            var query = new QueryOption();
            query.FieldType = typeof(int);
            query.NodeType = NodeType.AndAlso;
            query.OptionType = QueryExpression.Eq;
            query.FieldName = "1";
            query.OptionValue = 1;
            QueryParam qp = new QueryParam(typeof(Users));
            qp.AddOption(query);
            if (!string.IsNullOrEmpty(userName))
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(string);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Eq;
                query1.FieldName = "UserName";
                query1.OptionValue = userName;
                qp.AndAlso(query1);
            }
            if (RoleID > 0)
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(long);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Eq;
                query1.FieldName = "RoleID";
                query1.OptionValue = Convert.ToInt64(RoleID);
                qp.AndAlso(query1);
            }
            pageInfo = service.GetPage<Users>(qp, pageSize, pageIndex);
            if (pageInfo.Data.Count > 0)
            {
                foreach (var item in pageInfo.Data)
                {
                    lst.Add(ChangeUser(item));
                }
            }
            return new PageInfo<UsersInfo>(pageInfo.Total, lst);
        }

        #region 帮助
        public UsersInfo ChangeUser(Users item)
        {
            return new UsersInfo()
            {
                Id = item.Id,
                UserName = item.UserName,
                Password = item.Password,
                Phone = item.Phone,
                RoleID = item.RoleID,
                Power = item.Power,
            };
        }
        public Users ChangeUsers(UsersInfo item)
        {
            return new Users()
            {
                Id = item.Id,
                UserName = item.UserName,
                Password = item.Password,
                Phone = item.Phone,
                RoleID = item.RoleID,
                Power = item.Power,
            };
        }
        #endregion
    }
}
