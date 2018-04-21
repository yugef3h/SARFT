using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace SarftService.IServiceContract
{
    public class ReportDataInfo
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 终端串号
        /// </summary>
        public string TerminalKey { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 用户车牌号
        /// </summary>
        public string UserCarLicense { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Lon { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Lat { get; set; }
        /// <summary>
        /// 位置信息
        /// </summary>
        public string TextLocation { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgURL { get; set; }
        /// <summary>
        /// 上报时间
        /// </summary>
        public string ReportTime { get; set; }
        /// <summary>
        /// 状态 1 未筛选信息 2 客服已推荐信息 3 导播已推荐（未播报） 4主持人已推荐（播报）   7 导播查询所有 8 主持人查询所有 
        /// </summary>

        public short Status { get; set; }
        /// <summary>
        /// 客服标志 1 未读 2已读
        /// </summary>

        public short CusFlag { get; set; }
        /// <summary>
        /// 导播标志 1 未读 2已读
        /// </summary>

        public short CastFlag { get; set; }
        /// <summary>
        /// 主持人标志 1 未读 2已读
        /// </summary>

        public short HostFlag { get; set; }
        /// <summary>
        /// 奖励 1未奖励 2 已奖励    7 主持人查询所有
        /// </summary>

        public short Stars { get; set; }
        /// <summary>
        /// 信息说明 (客服标注)
        /// </summary>
        public string CusRemark { get; set; }
        /// <summary>
        /// 举报车牌号
        /// </summary>
        public string ReportCarLicense { get; set; }
        /// <summary>
        /// 举报类型表ID
        /// </summary>
        public long ReportType { get; set; }
        /// <summary>
        /// 举报类型名称
        /// </summary>
        public string ReportTypeName { get; set; }
        /// <summary>
        /// 导播反馈
        /// </summary>
        public string BroRemark { get; set; }
        /// <summary>
        /// 举报人姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 奖励说明
        /// </summary>
        public string AwardRemark { get; set; }
        /// <summary>
        /// 是否开启
        /// </summary>
        public short Is_Lock { get; set; }

        public ReportDataInfo()
        {
            Status = 1;
            Stars = 1;
            CusFlag = 1;
            CastFlag = 1;
            HostFlag = 1;
        }
    }
}
