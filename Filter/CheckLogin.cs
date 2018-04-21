using SarftService.IServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Upload_Photo.Filter
{
    public class CheckLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CookieHelper.GetValue("UsersID") != null && CookieHelper.GetValue("SessionsID") != null)
            {
                long UserID = long.Parse(CookieHelper.GetValue("UsersID"));
                UsersInfo users = Upload_Photo.Global.ServiceClient.GetUserInfoById(UserID);
                if (CookieHelper.GetValue("SessionsID") != users.UserName + users.Password)
                {
                    filterContext.Result = new RedirectResult("/Login");
                }
            }
            else
            {
                if (filterContext.RequestContext.HttpContext.Request.RawUrl.IndexOf("/Admin/index") > -1)
                {
                    filterContext.Result = new RedirectResult("/Login?timeout=1");
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Login?timeout=1&child=1");
                }
            }
        }

    }
}