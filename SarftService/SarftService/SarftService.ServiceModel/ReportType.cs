using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SarftService.ServiceModel
{
    public class ReportType:EntityBase
    {
        /// <summary>
        /// 违章名
        /// </summary>
        public virtual string ReportName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

    }
}
