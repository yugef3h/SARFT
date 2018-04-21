using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.IServiceContract
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    public interface IUsers
    {
        /// <summary>
        /// 根据用户名密码查找用户
        /// </summary>
        /// <param name="loginName">登录名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        UsersInfo FindUserInfo(string loginName, string password);
        /// <summary>
        /// 根据id查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UsersInfo GetUserInfoById(long id);
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        int AddUser(UsersInfo model);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        int DelUser(long id);
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="oldpassword">原密码</param>
        /// <param name="password">新密码</param>
        /// <returns>0失败1原密码不匹配2修改成功</returns>
        int AlterUserPassword(string username, string oldpassword, string password);
        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="user">用户对象</param>
        int AlterUsers(UsersInfo user);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int AlterUserPass(long id);
        /// <summary>
        /// 分页获取用户信息列表
        /// </summary>
        /// <param name="pageIndex">每页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="userName">用户名</param>
        /// <param name="RoleID">用户角色</param>
        /// <returns></returns>
        PageInfo<UsersInfo> GetUserInfoList(int pageIndex, int pageSize, string userName, long RoleID);
    }
}
