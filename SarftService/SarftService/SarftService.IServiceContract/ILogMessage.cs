using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.IServiceContract
{
    /// <summary>
    /// 系统操作日志信息
    /// </summary>
    public interface ILogMessage
    {
        /// <summary>
        /// 新增日志
        /// </summary>
        /// <param name="model">日志实体</param>
        /// <returns></returns>
        LogMessageInfo AddLogMessage(LogMessageInfo model);
        /// <summary>
        /// 分页获取日志信息列表
        /// </summary>
        /// <param name="pageIndex">每页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="UserID">用户id</param>
        /// <param name="OperationType">日志类型</param>
        /// <returns></returns>
        PageInfo<LogMessageInfo> GetLogMessageInfoList(int pageIndex, int pageSize, long UserID, short OperationType);
    }
}
