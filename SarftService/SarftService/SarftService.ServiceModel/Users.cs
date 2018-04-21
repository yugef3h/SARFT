using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.ServiceModel
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class Users : EntityBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 角色ID默认1客服2导播 3 主持人 4 管理员
        /// </summary>
        public virtual long RoleID { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public virtual string Power { get; set; }

    }
}
