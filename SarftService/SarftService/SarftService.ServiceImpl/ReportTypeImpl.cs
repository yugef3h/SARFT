using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SarftService.IServiceContract;
using SarftService.ServiceCommon;
using SarftService.ServiceModel;
using Service.Data.Manager;
using Service.Data.Client;

namespace SarftService.ServiceImpl
{
    public class ReportTypeImpl : IServiceContract.IReportType
    {
        public int AddReportType(ReportTypeInfo model)
        {
            var db = DBClinetHelp.Client();
            var user = db.Get<ReportType>(s => s.ReportName == model.ReportName).FirstOrDefault();
            if (user == null)
            {
                ReportType info = new ReportType();
                info.ReportName = model.ReportName;
                info.Remark = model.Remark;
                if (db.Save<ReportType>(info) != null)
                {
                    //新增成功
                    return 2;
                }
                else
                {
                    //新增失败
                    return 3;
                }
            }
            else
            {
                //用户名已存在
                return 1;
            }
        }
        public ReportTypeInfo GetReportTypeById(long id)
        {
            var db = DBClinetHelp.Client();
            var Report = db.Get<ReportType>(s => s.Id == id).FirstOrDefault();
            if (Report == null)
                return null;
            else
                return ChangeReport(Report);
        }
        public int UpdateReportType(ReportTypeInfo model)
        {
            var db = DBClinetHelp.Client();
            var info = db.Get<ReportType>(s => s.Id == model.Id).FirstOrDefault();
            if (info != null)
            {
                info.UpdateTime = DateTime.Now;
                info.ReportName = model.ReportName;
                info.Remark = model.Remark;
                if (db.Update<ReportType>(info) != null)
                {
                    return 1;
                }
            }
            return 0;
        }
        public int DelReportTypeInfo(long id)
        {
            bool b = false;
            var db = DBClinetHelp.Client();
            ReportType ReportTypeInfo = db.Get<ReportType>(s => s.Id == id).FirstOrDefault();
            if (ReportTypeInfo != null)
            {
                b = db.Delete<ReportType>(ReportTypeInfo);
                if (b)
                {
                    return 1;
                }
                else
                    return 0;
            }
            return 0;
        }

        public PageInfo<ReportTypeInfo> GetReportTypeInfoList(int pageIndex, int pageSize, string ReportName)
        {
            List<ReportTypeInfo> lst = new List<ReportTypeInfo>();
            var service = DBClinetHelp.Client();
            PageList<ReportType> pageInfo = null;
            var query = new QueryOption();
            query.FieldType = typeof(int);
            query.NodeType = NodeType.AndAlso;
            query.OptionType = QueryExpression.Eq;
            query.FieldName = "1";
            query.OptionValue = 1;
            QueryParam qp = new QueryParam(typeof(ReportType));
            qp.AddOption(query);
            if (!string.IsNullOrEmpty(ReportName))
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(string);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Eq;
                query1.FieldName = "ReportName";
                query1.OptionValue = ReportName;
                qp.AndAlso(query1);
            }
            pageInfo = service.GetPage<ReportType>(qp, pageSize, pageIndex);
            if (pageInfo.Data.Count > 0)
            {
                foreach (var item in pageInfo.Data)
                {
                    lst.Add(ChangeReport(item));
                }
            }
            return new PageInfo<ReportTypeInfo>(pageInfo.Total, lst);
        }
        public List<ReportTypeInfo> GetReportTypeList()
        {
            List<ReportTypeInfo> ReportTypeList = new List<ReportTypeInfo>();
            var service = DBClinetHelp.Client();
            var ReportTypeInfo = service.Get<ReportType>().ToList();
            if (ReportTypeInfo.Count > 0)
            {
                foreach (var item in ReportTypeInfo)
                {
                    ReportTypeList.Add(ChangeReport(item));
                }
            }
            return ReportTypeList;
        }

        #region 帮助
        public ReportTypeInfo ChangeReport(ReportType item)
        {
            return new ReportTypeInfo()
            {
                Id = item.Id,
                ReportName = item.ReportName,
                Remark = item.Remark,
            };
        }
        public ReportType ChangeReports(ReportTypeInfo item)
        {
            return new ReportType()
            {
                Id = item.Id,
                ReportName = item.ReportName,
                Remark = item.Remark,
            };
        }
        #endregion
    }
}
