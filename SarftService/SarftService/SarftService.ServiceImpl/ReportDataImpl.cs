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
    public class ReportDataImpl : IServiceContract.IReportData
    {
        public void AddReportData(ReportDataInfo[] infos)
        {
            var db = DBClinetHelp.Client();
            foreach (var info in infos)
            {
                db.Save<ReportData>(ChangeToReportData(info));
            }
        }

        ReportData ChangeToReportData(ReportDataInfo info)
        {
            ReportData model = new ReportData();
            model.Id = info.Id;
            model.TerminalKey = info.TerminalKey;
            model.BroRemark = info.BroRemark;
            model.CusRemark = info.CusRemark;
            model.ImgURL = info.ImgURL;
            model.Lat = info.Lat;
            model.Lon = info.Lon;
            model.Phone = info.Phone;
            model.ReportCarLicense = info.ReportCarLicense;
            model.ReportTime = Convert.ToDateTime(info.ReportTime);
            model.ReportType = info.ReportType;
            model.Stars = info.Stars;
            model.Status = info.Status;
            model.TextLocation = info.TextLocation;
            model.UserCarLicense = info.UserCarLicense;
            model.UserName = info.UserName;
            model.CusFlag = info.CusFlag;
            model.CastFlag = info.CastFlag;
            model.HostFlag = info.HostFlag;
            model.Name = info.Name;
            model.AwardRemark = info.AwardRemark;
            model.Is_Lock = info.Is_Lock;
            return model;
        }
        ReportDataInfo ChangeToReportDataInfo(ReportData info)
        {
            ReportDataInfo model = new ReportDataInfo();
            model.Id = info.Id;
            model.TerminalKey = info.TerminalKey;
            model.BroRemark = info.BroRemark;
            model.CusRemark = info.CusRemark;
            model.ImgURL = info.ImgURL;
            model.Lat = info.Lat;
            model.Lon = info.Lon;
            model.Phone = info.Phone;
            model.ReportCarLicense = info.ReportCarLicense;
            model.ReportTime = info.ReportTime.ToString();
            model.ReportType = info.ReportType;
            model.Stars = info.Stars;
            model.Status = info.Status;
            model.TextLocation = info.TextLocation;
            model.UserCarLicense = info.UserCarLicense;
            model.UserName = info.UserName;
            model.CusFlag = info.CusFlag;
            model.CastFlag = info.CastFlag;
            model.HostFlag = info.HostFlag;
            model.Name = info.Name;
            model.AwardRemark = info.AwardRemark;
            model.Is_Lock = info.Is_Lock;
            return model;
        }

        public bool UpdateReportData(ReportDataInfo model)
        {
            var db = DBClinetHelp.Client();
            var packages = db.Get<ReportData>(t => t.Id == model.Id).FirstOrDefault();
            if (packages == null)
                throw new ServiceContainer.MessageException("举报数据不存在");
            var info = ChangeToReportData(model);
            db.Update<ReportData>(info);
            return true;
        }

        public ReportDataInfo GetReportDataByID(long id)
        {
            var db = DBClinetHelp.Client();
            var packages = db.Get<ReportData>(t => t.Id == id).FirstOrDefault();
            if (packages != null)
            {
                var dataInfo = ChangeToReportDataInfo(packages);
                var dataType = db.Get<ReportType>(t => t.Id == dataInfo.ReportType).FirstOrDefault();
                if (dataType != null)
                {
                    dataInfo.ReportTypeName = dataType.ReportName;
                }
                return dataInfo;
            }
            else return null;
        }

        public PageInfo<ReportDataInfo> GetReortDataInfoList(int pageSize, int pageIndex, string startTime, string endTime, short Status, short Stars, short CusFlag, short CastFlag, short HostFlag, long ReportType, byte oderType = 1, short Is_Lock = 0)
        {

            List<ReportDataInfo> lst = new List<ReportDataInfo>();
            var service = DBClinetHelp.Client();
            PageList<ReportData> pageInfo = null;
            QueryParam qp = new QueryParam(typeof(ReportData));
            var query = new QueryOption();
            query.FieldType = typeof(int);
            query.NodeType = NodeType.AndAlso;
            query.OptionType = QueryExpression.Eq;
            query.FieldName = "1";
            query.OptionValue = 1;
            qp.AddOption(query);


            var querynew = new QueryOption();
            querynew.FieldType = typeof(short);
            querynew.NodeType = NodeType.AndAlso;
            querynew.OptionType = QueryExpression.Eq;
            querynew.FieldName = "Is_Lock";
            querynew.OptionValue = Is_Lock;
            qp.AndAlso(querynew);

            if (!string.IsNullOrEmpty(startTime))
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(DateTime);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Gt;
                query1.FieldName = "ReportTime";
                query1.OptionValue = Convert.ToDateTime(startTime);
                qp.AndAlso(query1);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(DateTime);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Le;
                query1.FieldName = "ReportTime";
                query1.OptionValue = Convert.ToDateTime(endTime);
                qp.AndAlso(query1);
            }
            if (Status != 0)
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(short);
                query1.NodeType = NodeType.AndAlso;
                if (Status == 7)
                {
                    query1.OptionType = QueryExpression.Ge;
                    query1.OptionValue = 2;
                }
                else if (Status == 8)
                {
                    query1.OptionType = QueryExpression.Ge;
                    query1.OptionValue = 3;
                }
                else if (Status == 2)
                {
                    query1.OptionType = QueryExpression.Ge;
                    query1.OptionValue = 2;
                }
                else
                {
                    query1.OptionType = QueryExpression.Eq;
                    query1.OptionValue = Status;
                }

                query1.FieldName = "Status";

                qp.AndAlso(query1);
            }
            else
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(short);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Neq;
                query1.OptionValue = 2;
                query1.FieldName = "Status";
                qp.AndAlso(query1);
            }
            if (Stars != 0)
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(short);
                query1.NodeType = NodeType.AndAlso;
                if (Stars == 7)
                {
                    query1.OptionType = QueryExpression.Ge;
                    query1.OptionValue = 0;
                }
                else
                {
                    query1.OptionType = QueryExpression.Eq;
                    query1.OptionValue = Stars;
                }
                query1.FieldName = "Stars";
                qp.AndAlso(query1);
            }
            if (CusFlag != 0)
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(short);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Eq;
                query1.FieldName = "CusFlag";
                query1.OptionValue = CusFlag;
                qp.AndAlso(query1);
            }
            if (CastFlag != 0)
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(short);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Eq;
                query1.FieldName = "CastFlag";
                query1.OptionValue = CastFlag;
                qp.AndAlso(query1);
            }
            if (HostFlag != 0)
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(short);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Eq;
                query1.FieldName = "HostFlag";
                query1.OptionValue = HostFlag;
                qp.AndAlso(query1);
            }
            if (ReportType != 0)
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(ushort);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Eq;
                query1.FieldName = "ReportType";
                query1.OptionValue = ReportType;
                qp.AndAlso(query1);
            }
            if (oderType == 0)
            {
                var query2 = new OrderOption();
                query2.FieldName = "ReportTime";
                query2.OrderType = OrderExpression.asc;
                qp.AddOrderOption(query2);
            }
            else
            {
                var query2 = new OrderOption();
                query2.FieldName = "ReportTime";
                query2.OrderType = OrderExpression.desc;
                qp.AddOrderOption(query2);
            }
            pageInfo = service.GetPage<ReportData>(qp, pageSize, pageIndex);
            if (pageInfo.Data.Count > 0)
            {
                foreach (var item in pageInfo.Data)
                {
                    var dataInfo = ChangeToReportDataInfo(item);
                    var dataType = service.Get<ReportType>(t => t.Id == dataInfo.ReportType).FirstOrDefault();
                    if (dataType != null)
                    {
                        dataInfo.ReportTypeName = dataType.ReportName;
                    }
                    lst.Add(dataInfo);
                }
            }
            return new PageInfo<ReportDataInfo>(pageInfo.Total, lst);
        }
    }
}
