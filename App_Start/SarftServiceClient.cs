using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Upload_Photo.App_Start
{
    public class SarftServiceClient : NRails.Service.ServiceClient
    {
        public SarftServiceClient(System.String serverAddress, NRails.Service.IRequestSerializer serializer = null, NRails.Net.IProto proto = null)
                : base(serverAddress, serializer, proto)
        {
        }

        public virtual SarftService.IServiceContract.UsersInfo FindUserInfo(string loginName, string password, int timeout = 30000)
        {
            return (SarftService.IServiceContract.UsersInfo)this.Invoke(391480114, "FindUserInfo(string,string)", new object[] { loginName, password }, timeout, "FindUserInfo(string,string)");
        }
        public virtual void FindUserInfo(string loginName, string password, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(391480114, "FindUserInfo(string,string)", new object[] { loginName, password }, "FindUserInfo(string,string)", callback, tokens);
        }

        public virtual SarftService.IServiceContract.UsersInfo GetUserInfoById(long id, int timeout = 30000)
        {
            return (SarftService.IServiceContract.UsersInfo)this.Invoke(1145518213, "GetUserInfoById(long)", new object[] { id }, timeout, "GetUserInfoById(long)");
        }
        public virtual void GetUserInfoById(long id, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1145518213, "GetUserInfoById(long)", new object[] { id }, "GetUserInfoById(long)", callback, tokens);
        }

        public virtual int AddUser(SarftService.IServiceContract.UsersInfo model, int timeout = 30000)
        {
            return (int)this.Invoke(1902170835, "AddUser(SarftService.IServiceContract.UsersInfo)", new object[] { model }, timeout, "AddUser(SarftService.IServiceContract.UsersInfo)");
        }
        public virtual void AddUser(SarftService.IServiceContract.UsersInfo model, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1902170835, "AddUser(SarftService.IServiceContract.UsersInfo)", new object[] { model }, "AddUser(SarftService.IServiceContract.UsersInfo)", callback, tokens);
        }

        public virtual int DelUser(long id, int timeout = 30000)
        {
            return (int)this.Invoke(18446744073552437846, "DelUser(long)", new object[] { id }, timeout, "DelUser(long)");
        }
        public virtual void DelUser(long id, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744073552437846, "DelUser(long)", new object[] { id }, "DelUser(long)", callback, tokens);
        }

        public virtual int AlterUserPassword(string username, string oldpassword, string password, int timeout = 30000)
        {
            return (int)this.Invoke(18446744073689372902, "AlterUserPassword(string,string,string)", new object[] { username, oldpassword, password }, timeout, "AlterUserPassword(string,string,string)");
        }
        public virtual void AlterUserPassword(string username, string oldpassword, string password, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744073689372902, "AlterUserPassword(string,string,string)", new object[] { username, oldpassword, password }, "AlterUserPassword(string,string,string)", callback, tokens);
        }

        public virtual int AlterUsers(SarftService.IServiceContract.UsersInfo user, int timeout = 30000)
        {
            return (int)this.Invoke(18446744073576941235, "AlterUsers(SarftService.IServiceContract.UsersInfo)", new object[] { user }, timeout, "AlterUsers(SarftService.IServiceContract.UsersInfo)");
        }
        public virtual void AlterUsers(SarftService.IServiceContract.UsersInfo user, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744073576941235, "AlterUsers(SarftService.IServiceContract.UsersInfo)", new object[] { user }, "AlterUsers(SarftService.IServiceContract.UsersInfo)", callback, tokens);
        }

        public virtual int AlterUserPass(long id, int timeout = 30000)
        {
            return (int)this.Invoke(18446744072872185771, "AlterUserPass(long)", new object[] { id }, timeout, "AlterUserPass(long)");
        }
        public virtual void AlterUserPass(long id, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072872185771, "AlterUserPass(long)", new object[] { id }, "AlterUserPass(long)", callback, tokens);
        }

        public virtual SarftService.IServiceContract.PageInfo<SarftService.IServiceContract.UsersInfo> GetUserInfoList(int pageIndex, int pageSize, string userName, long RoleID, int timeout = 30000)
        {
            return (SarftService.IServiceContract.PageInfo<SarftService.IServiceContract.UsersInfo>)this.Invoke(18446744073194624088, "GetUserInfoList(int,int,string,long)", new object[] { pageIndex, pageSize, userName, RoleID }, timeout, "GetUserInfoList(int,int,string,long)");
        }
        public virtual void GetUserInfoList(int pageIndex, int pageSize, string userName, long RoleID, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744073194624088, "GetUserInfoList(int,int,string,long)", new object[] { pageIndex, pageSize, userName, RoleID }, "GetUserInfoList(int,int,string,long)", callback, tokens);
        }

        public virtual void AddReportData(SarftService.IServiceContract.ReportDataInfo[] infos, int timeout = 30000)
        {
            this.Invoke(763442332, "AddReportData(SarftService.IServiceContract.ReportDataInfo[])", new object[] { infos }, timeout, "AddReportData(SarftService.IServiceContract.ReportDataInfo[])");
        }
        public virtual void AddReportData(SarftService.IServiceContract.ReportDataInfo[] infos, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(763442332, "AddReportData(SarftService.IServiceContract.ReportDataInfo[])", new object[] { infos }, "AddReportData(SarftService.IServiceContract.ReportDataInfo[])", callback, tokens);
        }
        public virtual void AddReportData(SarftService.IServiceContract.ReportDataInfo info, int timeout = 30000)
        {
            this.AddReportData(new SarftService.IServiceContract.ReportDataInfo[] { info }, timeout);
        }

        public virtual bool UpdateReportData(SarftService.IServiceContract.ReportDataInfo model, int timeout = 30000)
        {
            return (bool)this.Invoke(715335059, "UpdateReportData(SarftService.IServiceContract.ReportDataInfo)", new object[] { model }, timeout, "UpdateReportData(SarftService.IServiceContract.ReportDataInfo)");
        }
        public virtual void UpdateReportData(SarftService.IServiceContract.ReportDataInfo model, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(715335059, "UpdateReportData(SarftService.IServiceContract.ReportDataInfo)", new object[] { model }, "UpdateReportData(SarftService.IServiceContract.ReportDataInfo)", callback, tokens);
        }

        public virtual SarftService.IServiceContract.ReportDataInfo GetReportDataByID(long id, int timeout = 30000)
        {
            return (SarftService.IServiceContract.ReportDataInfo)this.Invoke(562279022, "GetReportDataByID(long)", new object[] { id }, timeout, "GetReportDataByID(long)");
        }
        public virtual void GetReportDataByID(long id, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(562279022, "GetReportDataByID(long)", new object[] { id }, "GetReportDataByID(long)", callback, tokens);
        }

        public virtual SarftService.IServiceContract.PageInfo<SarftService.IServiceContract.ReportDataInfo> GetReortDataInfoList(int pageSize, int pageIndex, string startTime, string endTime, short Status, short Stars, short CusFlag, short CastFlag, short HostFlag, long ReportType, byte oderType, short Is_Lock, int timeout = 30000)
        {
            return (SarftService.IServiceContract.PageInfo<SarftService.IServiceContract.ReportDataInfo>)this.Invoke(18446744071830602253, "GetReortDataInfoList(int,int,string,string,short,short,short,short,short,long,byte,short)", new object[] { pageSize, pageIndex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, Is_Lock }, timeout, "GetReortDataInfoList(int,int,string,string,short,short,short,short,short,long,byte,short)");
        }
        public virtual void GetReortDataInfoList(int pageSize, int pageIndex, string startTime, string endTime, short Status, short Stars, short CusFlag, short CastFlag, short HostFlag, long ReportType, byte oderType, short Is_Lock, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744071830602253, "GetReortDataInfoList(int,int,string,string,short,short,short,short,short,long,byte,short)", new object[] { pageSize, pageIndex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, Is_Lock }, "GetReortDataInfoList(int,int,string,string,short,short,short,short,short,long,byte,short)", callback, tokens);
        }

        public virtual int AddReportType(SarftService.IServiceContract.ReportTypeInfo model, int timeout = 30000)
        {
            return (int)this.Invoke(1930891296, "AddReportType(SarftService.IServiceContract.ReportTypeInfo)", new object[] { model }, timeout, "AddReportType(SarftService.IServiceContract.ReportTypeInfo)");
        }
        public virtual void AddReportType(SarftService.IServiceContract.ReportTypeInfo model, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1930891296, "AddReportType(SarftService.IServiceContract.ReportTypeInfo)", new object[] { model }, "AddReportType(SarftService.IServiceContract.ReportTypeInfo)", callback, tokens);
        }

        public virtual SarftService.IServiceContract.ReportTypeInfo GetReportTypeById(long id, int timeout = 30000)
        {
            return (SarftService.IServiceContract.ReportTypeInfo)this.Invoke(18446744072881684234, "GetReportTypeById(long)", new object[] { id }, timeout, "GetReportTypeById(long)");
        }
        public virtual void GetReportTypeById(long id, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072881684234, "GetReportTypeById(long)", new object[] { id }, "GetReportTypeById(long)", callback, tokens);
        }

        public virtual int UpdateReportType(SarftService.IServiceContract.ReportTypeInfo model, int timeout = 30000)
        {
            return (int)this.Invoke(451277676, "UpdateReportType(SarftService.IServiceContract.ReportTypeInfo)", new object[] { model }, timeout, "UpdateReportType(SarftService.IServiceContract.ReportTypeInfo)");
        }
        public virtual void UpdateReportType(SarftService.IServiceContract.ReportTypeInfo model, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(451277676, "UpdateReportType(SarftService.IServiceContract.ReportTypeInfo)", new object[] { model }, "UpdateReportType(SarftService.IServiceContract.ReportTypeInfo)", callback, tokens);
        }

        public virtual int DelReportTypeInfo(long id, int timeout = 30000)
        {
            return (int)this.Invoke(336794244, "DelReportTypeInfo(long)", new object[] { id }, timeout, "DelReportTypeInfo(long)");
        }
        public virtual void DelReportTypeInfo(long id, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(336794244, "DelReportTypeInfo(long)", new object[] { id }, "DelReportTypeInfo(long)", callback, tokens);
        }

        public virtual SarftService.IServiceContract.PageInfo<SarftService.IServiceContract.ReportTypeInfo> GetReportTypeInfoList(int pageIndex, int pageSize, string ReportName, int timeout = 30000)
        {
            return (SarftService.IServiceContract.PageInfo<SarftService.IServiceContract.ReportTypeInfo>)this.Invoke(18446744072706823792, "GetReportTypeInfoList(int,int,string)", new object[] { pageIndex, pageSize, ReportName }, timeout, "GetReportTypeInfoList(int,int,string)");
        }
        public virtual void GetReportTypeInfoList(int pageIndex, int pageSize, string ReportName, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072706823792, "GetReportTypeInfoList(int,int,string)", new object[] { pageIndex, pageSize, ReportName }, "GetReportTypeInfoList(int,int,string)", callback, tokens);
        }

        public virtual SarftService.IServiceContract.SysErrorInfo AddError(SarftService.IServiceContract.SysErrorInfo model, int timeout = 30000)
        {
            return (SarftService.IServiceContract.SysErrorInfo)this.Invoke(1176791765, "AddError(SarftService.IServiceContract.SysErrorInfo)", new object[] { model }, timeout, "AddError(SarftService.IServiceContract.SysErrorInfo)");
        }
        public virtual void AddError(SarftService.IServiceContract.SysErrorInfo model, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1176791765, "AddError(SarftService.IServiceContract.SysErrorInfo)", new object[] { model }, "AddError(SarftService.IServiceContract.SysErrorInfo)", callback, tokens);
        }

        public virtual SarftService.IServiceContract.SysErrorInfo GetErrorByGuid(string guid, int timeout = 30000)
        {
            return (SarftService.IServiceContract.SysErrorInfo)this.Invoke(1935778843, "GetErrorByGuid(string)", new object[] { guid }, timeout, "GetErrorByGuid(string)");
        }
        public virtual void GetErrorByGuid(string guid, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1935778843, "GetErrorByGuid(string)", new object[] { guid }, "GetErrorByGuid(string)", callback, tokens);
        }
        public virtual System.Collections.Generic.List<SarftService.IServiceContract.ReportTypeInfo> GetReportTypeList(int timeout = 30000)
        {
            return (System.Collections.Generic.List<SarftService.IServiceContract.ReportTypeInfo>)this.Invoke(18446744072537998538, "GetReportTypeList()", new object[] { }, timeout, "GetReportTypeList()");
        }
        public virtual void GetReportTypeList(NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072537998538, "GetReportTypeList()", new object[] { }, "GetReportTypeList()", callback, tokens);
        }
        public virtual SarftService.IServiceContract.LogMessageInfo AddLogMessage(SarftService.IServiceContract.LogMessageInfo model, int timeout)
        {
            return (SarftService.IServiceContract.LogMessageInfo)this.Invoke("AddLogMessage(SarftService.IServiceContract.LogMessageInfo)", new object[] { model }, timeout);
        }
        public virtual SarftService.IServiceContract.LogMessageInfo AddLogMessage(SarftService.IServiceContract.LogMessageInfo model)
        {
            return this.AddLogMessage(model, 3000);
        }
        public virtual void AddLogMessage(SarftService.IServiceContract.LogMessageInfo model, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke("AddLogMessage(SarftService.IServiceContract.LogMessageInfo)", new object[] { model }, callback, tokens);
        }
        public virtual SarftService.IServiceContract.PageInfo<SarftService.IServiceContract.LogMessageInfo> GetLogMessageInfoList(int pageIndex, int pageSize, long UserID, short OperationType, int timeout)
        {
            return (SarftService.IServiceContract.PageInfo<SarftService.IServiceContract.LogMessageInfo>)this.Invoke("GetLogMessageInfoList(int,int,long,short)", new object[] { pageIndex, pageSize, UserID, OperationType }, timeout);
        }
        public virtual SarftService.IServiceContract.PageInfo<SarftService.IServiceContract.LogMessageInfo> GetLogMessageInfoList(int pageIndex, int pageSize, long UserID, short OperationType)
        {
            return this.GetLogMessageInfoList(pageIndex, pageSize, UserID, OperationType, 3000);
        }
        public virtual void GetLogMessageInfoList(int pageIndex, int pageSize, long UserID, short OperationType, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke("GetLogMessageInfoList(int,int,long,short)", new object[] { pageIndex, pageSize, UserID, OperationType }, callback, tokens);
        }
    }
}