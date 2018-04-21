using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.ServiceModel
{
    public class ReportData : EntityBase
    {
        /// <summary>
        /// 终端串号
        /// </summary>
        public virtual string TerminalKey { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 用户车牌号
        /// </summary>
        public virtual string UserCarLicense { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public virtual double Lon { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public virtual double Lat { get; set; }
        /// <summary>
        /// 位置信息
        /// </summary>
        public virtual string TextLocation { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public virtual string ImgURL { get; set; }
        /// <summary>
        /// 上报时间
        /// </summary>
        public virtual DateTime ReportTime { get; set; }
        /// <summary>
        /// 状态 1 未筛选信息 2 客服已推荐信息 3 导播已推荐（未播报） 4主持人已推荐（播报）  7 导播查询所有 8 主持人查询所有 
        /// </summary>

        public virtual short Status { get; set; }

        /// <summary>
        /// 客服标志 1 未读 2已读
        /// </summary>

        public virtual short CusFlag { get; set; }
        /// <summary>
        /// 导播标志 1 未读 2已读
        /// </summary>

        public virtual short CastFlag { get; set; }
        /// <summary>
        /// 主持人标志 1 未读 2已读
        /// </summary>

        public virtual short HostFlag { get; set; }
        /// <summary>
        /// 奖励 1未奖励 2 已奖励    7 主持人查询所有
        /// </summary>

        public virtual short Stars { get; set; }
        /// <summary>
        /// 信息说明 (客服标注)
        /// </summary>
        public virtual string CusRemark { get; set; }
        /// <summary>
        /// 举报车牌号
        /// </summary>
        public virtual string ReportCarLicense { get; set; }
        /// <summary>
        /// 举报类型表ID
        /// </summary>
        public virtual long ReportType { get; set; }
        /// <summary>
        /// 导播反馈
        /// </summary>
        public virtual string BroRemark { get; set; }
        /// <summary>
        /// 举报人姓名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 奖励说明
        /// </summary>
        public virtual string AwardRemark { get; set; }
        /// <summary>
        /// 是否开启
        /// </summary>
        public virtual short Is_Lock { get; set; }

    }
}
