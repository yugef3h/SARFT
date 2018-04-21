using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.ServiceModel
{
    /// <summary>
    /// 日志表
    /// </summary>
    public class LogMessage : EntityBase
    {
        /// <summary>
        /// 操作用户id
        /// </summary>
        public virtual long UserID { get; set; }
        /// <summary>
        /// 操作类型 1客服编辑2删除3恢复4客服推荐5导播推荐6取消推荐7导播编辑8主持人通知9主持人播报10导播取消推荐
        /// </summary>
        public virtual short OperationType { get; set; }
        /// <summary>
        /// 操作数据id
        /// </summary>
        public virtual long ReportDataID { get; set; }
    }
}
