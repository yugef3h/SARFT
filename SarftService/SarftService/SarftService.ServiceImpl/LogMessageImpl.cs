using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SarftService.IServiceContract;
using SarftService.ServiceCommon;
using SarftService.ServiceModel;
using Service.Data.Client;
using Service.Data.Manager;

namespace SarftService.ServiceImpl
{
    public class LogMessageImpl : IServiceContract.ILogMessage
    {
        public LogMessageInfo AddLogMessage(LogMessageInfo model)
        {
            var service = DBClinetHelp.Client();
            LogMessage Log = service.Get<LogMessage>(L => L.Id == model.ID && L.OperationType == model.OperationType && L.UserID == model.UserID).FirstOrDefault();
            if (Log == null)
            {
                Log = new LogMessage();
                Log.MapFrom(model);
                Log = service.Save(Log);
            }
            else
            {
                Log.MapFrom<LogMessage>(model, L => L.Id);
                Log = service.Update(Log);
            }
            return model.MapFrom(Log);
        }

        public PageInfo<LogMessageInfo> GetLogMessageInfoList(int pageIndex, int pageSize, long UserID, short OperationType)
        {
            List<LogMessageInfo> lst = new List<LogMessageInfo>();
            var service = DBClinetHelp.Client();
            PageList<LogMessage> pageInfo = null;
            QueryParam qp = new QueryParam(typeof(LogMessage));
            var querynewe = new QueryOption();
            querynewe.FieldType = typeof(int);
            querynewe.NodeType = NodeType.AndAlso;
            querynewe.OptionType = QueryExpression.Eq;
            querynewe.FieldName = "1";
            querynewe.OptionValue = 1;
            qp.AddOption(querynewe);
            
            if (UserID>0)
            {
                var query = new QueryOption();
                query.FieldType = typeof(long);
                query.NodeType = NodeType.AndAlso;
                query.OptionType = QueryExpression.Eq;
                query.FieldName = "UserID";
                query.OptionValue = UserID;
                qp.AddOption(query);
            }
            if (OperationType != 0)
            {
                var query1 = new QueryOption();
                query1.FieldType = typeof(short);
                query1.NodeType = NodeType.AndAlso;
                query1.OptionType = QueryExpression.Eq;
                query1.FieldName = "OperationType";
                query1.OptionValue = OperationType;
                qp.AndAlso(query1);
            }
            pageInfo = service.GetPage<LogMessage>(qp, pageSize, pageIndex);
            if (pageInfo.Data.Count > 0)
            {
                foreach (var item in pageInfo.Data)
                {
                    LogMessageInfo info = new LogMessageInfo();
                    lst.Add(info.MapFrom(item));
                }
            }
            return new PageInfo<LogMessageInfo>() { Data = lst, TotalCount = pageInfo.Total };
        }
    }
}
