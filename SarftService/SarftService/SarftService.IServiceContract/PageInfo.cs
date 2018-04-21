using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SarftService.IServiceContract
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PageInfo<T>
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public long TotalCount { get; set; }
        /// <summary>
        /// 记录
        /// </summary>
        public List<T> Data { get; set; }
        public PageInfo()
        {
            Data = new List<T>();
        }
        public PageInfo(List<T> data)
        {
            Data = data;
        }
        public PageInfo(long total, List<T> data)
        {
            this.TotalCount = total;
            this.Data = data;
        }
    }
}
