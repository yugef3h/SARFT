using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.IServiceContract
{
    public class SysErrorInfo
    {
        /// <summary>
        /// 惟一表示
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 惟一表示
        /// </summary>
        public string GUID { get; set; }
        /// <summary>
        /// 系统用户ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 系统用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 请求信息
        /// </summary>
        public string RequestInfo { get; set; }
        /// <summary>
        /// 请求表头
        /// </summary>
        public string Headers { get; set; }
        /// <summary>
        /// 堆栈
        /// </summary>
        public string StackTrace { get; set; }
        /// <summary>
        /// 异常时间
        /// </summary>
        public DateTime ExceptionTime { get; set; }
    }
}
