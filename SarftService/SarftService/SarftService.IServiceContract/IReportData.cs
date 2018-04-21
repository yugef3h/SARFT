using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.IServiceContract
{
    /// <summary>
    /// 举报信息
    /// </summary>
    public interface IReportData
    {
        /// 新增举报数据
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        void AddReportData(ReportDataInfo[] infos);
        /// <summary>
        /// 更新举报数据
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        bool UpdateReportData(ReportDataInfo model);

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">ComOEM表ID</param>
        /// <returns></returns>
        ReportDataInfo GetReportDataByID(long id);

        /// <summary>
        /// 获取举报信息分页数据集合
        /// </summary>
        /// <param name="pageSize">页码大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="Status">状态</param>
        /// <param name="Stars">标星</param>
        /// <param name="ReportType">举报类型</param>
        /// <param name="oderType">排序</param>
        /// <returns></returns>
        PageInfo<ReportDataInfo> GetReortDataInfoList(int pageSize, int pageIndex, string startTime, string endTime, short Status, short Stars, short CusFlag, short CastFlag, short HostFlag, long ReportType, byte oderType = 1, short Is_Lock = 0);
    }
}
