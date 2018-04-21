using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.ServiceModel
{
    public class SysError:EntityBase
    {
        /// <summary>
        /// 惟一表示
        /// </summary>
        public virtual string GUID { get; set; }
        /// <summary>
        /// 系统用户ID
        /// </summary>
        public virtual long UserID { get; set; }
        /// <summary>
        /// 系统用户名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public virtual string Message { get; set; }
        /// <summary>
        /// 请求信息
        /// </summary>
        public virtual string RequestInfo { get; set; }
        /// <summary>
        /// 请求表头
        /// </summary>
        public virtual string Headers { get; set; }
        /// <summary>
        /// 堆栈
        /// </summary>
        public virtual string StackTrace { get; set; }
        /// <summary>
        /// 异常时间
        /// </summary>
        public virtual DateTime ExceptionTime { get; set; }
    }
}
