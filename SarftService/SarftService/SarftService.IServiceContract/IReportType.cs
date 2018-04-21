using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.IServiceContract
{
    /// <summary>
    /// 举报类型信息
    /// </summary>
    public interface IReportType
    {
        /// <summary>
        /// 新增违章类别
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        int AddReportType(ReportTypeInfo model);
        /// <summary>
        /// 根据id查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ReportTypeInfo GetReportTypeById(long id);
        /// <summary>
        /// 修改违章类别
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        int UpdateReportType(ReportTypeInfo model);
        /// <summary>
        /// 删除违章类别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DelReportTypeInfo(long id);
        /// <summary>
        /// 分页获取违章类别列表
        /// </summary>
        /// <param name="pageIndex">每页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="userName">违章名</param>
        /// <returns></returns>
        PageInfo<ReportTypeInfo> GetReportTypeInfoList(int pageIndex, int pageSize, string ReportName);
        /// <summary>
        /// 获取违章类别列表
        /// </summary>
        /// <returns></returns>
        List<ReportTypeInfo> GetReportTypeList();
    }
}
