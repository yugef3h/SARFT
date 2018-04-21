using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.IServiceContract
{
    public class LogMessageInfo
    {
        /// <summary>
        /// 唯一值id
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 操作用户id
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 操作类型 1客服编辑2删除3恢复4客服推荐5导播推荐6取消推荐7导播编辑8主持人通知9主持人播报10导播取消推荐
        /// </summary>
        public short OperationType { get; set; }
        /// <summary>
        /// 操作数据id
        /// </summary>
        public long ReportDataID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
