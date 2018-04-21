using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.IServiceContract
{
    /// <summary>
    /// 系统错误信息
    /// </summary>
    public interface IErrorService
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">实体</param>
        /// <returns></returns>
        SysErrorInfo AddError(SysErrorInfo model);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">Error表ID</param>
        /// <returns></returns>
        SysErrorInfo GetErrorByGuid(string guid);
    }
}
