using Service.Data.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SarftService.ServiceModel
{
    /// <summary>
    /// 基本信息
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// 惟一表示
        /// </summary>
        [EntityField(PrimaryKey = true, Identity = true)]
        public virtual long Id { get; set; }

        private DateTime _CreateTime = DateTime.Now;

        public virtual DateTime CreateTime
        {
            get
            {
                if (_CreateTime == default(DateTime) || _CreateTime < Convert.ToDateTime("2000-01-01"))
                    return DateTime.Now;

                return _CreateTime;

            }
            set { _CreateTime = value; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        private DateTime _UpdateTime = DateTime.Now;

        public virtual DateTime UpdateTime
        {
            get
            {

                if (_UpdateTime == default(DateTime) || _UpdateTime < Convert.ToDateTime("2000-01-01"))
                    return DateTime.Now;
                return _UpdateTime;

            }
            set { _UpdateTime = value; }
        }

        /// <summary>
        /// 创建者Id
        /// </summary>
        private long _CreaterId = 0;
        public virtual long CreaterId
        {
            get
            {
                return _CreaterId;
            }
            set
            {
                _CreaterId = value;
            }
        }
        /// <summary>
        /// 最后一次修改者ID
        /// </summary>
        public virtual long OperateId { get; set; }

    }
}
