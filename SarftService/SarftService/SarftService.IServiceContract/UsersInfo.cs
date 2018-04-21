using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.IServiceContract
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UsersInfo
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 角色ID默认1客服2导播 3 主持人 4 管理员
        /// </summary>
        public long RoleID { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string Power { get; set; }
    }
}
