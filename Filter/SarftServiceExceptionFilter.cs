using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using SarftService.IServiceContract;
using System.Web;



public class SarftServiceExceptionFilter : FilterAttribute, IExceptionFilter
{
    void IExceptionFilter.OnException(ExceptionContext filterContext)
    {
        if (filterContext.Exception.Message.IndexOf("无法在发送 HTTP 标头之后进行重定向") == -1)
        {
            SysErrorInfo errorInfo = new SysErrorInfo();
            errorInfo.GUID = Guid.NewGuid().ToString();
            if (CookieHelper.GetValue("UserID") != null)
            {
                long UserID = long.Parse(CookieHelper.GetValue("UserID"));
                UsersInfo users = Upload_Photo.Global.ServiceClient.GetUserInfoById(UserID);
                errorInfo.UserID = UserID;
                errorInfo.UserName = users.UserName;
                errorInfo.Message = filterContext.Exception.Message;
                errorInfo.RequestInfo = filterContext.RequestContext.HttpContext.Request.RawUrl;
                if (filterContext.RequestContext.HttpContext.Request.Headers != null)
                {
                    errorInfo.Headers = HttpUtility.UrlDecode(filterContext.RequestContext.HttpContext.Request.Headers.ToString(), System.Text.Encoding.UTF8);

                }
                errorInfo.StackTrace = filterContext.Exception.StackTrace;

                errorInfo.ExceptionTime = DateTime.Now;
                SysErrorInfo error = Upload_Photo.Global.ServiceClient.AddError(errorInfo);
                string errorCode = error.GUID;
                filterContext.Result = new RedirectResult(String.Format("/ErrorPage/?error={0}", errorCode));
                filterContext.ExceptionHandled = true;
            }

        }
    }
}
