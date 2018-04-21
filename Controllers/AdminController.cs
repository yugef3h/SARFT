using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SarftService.IServiceContract;
using Upload_Photo.Filter;

namespace Upload_Photo.Controllers
{
    [SarftServiceExceptionFilter()]
    [CheckLogin()]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Defult()
        {
            return View();
        }
        /// <summary>
        /// 系统错误页面
        /// </summary>
        /// <param name="ErrorCode"></param>
        /// <returns></returns>
        public ActionResult ErrorPage(string ErrorCode)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["error"]))
            {
                ErrorCode = Request.QueryString["error"];
            }
            if (!string.IsNullOrEmpty(ErrorCode))
            {
                SysErrorInfo errorInfo = Global.ServiceClient.GetErrorByGuid(ErrorCode);

                if (errorInfo != null)
                {
                    ViewData["ErrorInfo"] = errorInfo;
                }
            }

            return View();
        }
        /// <summary>
        /// 管理员首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        [CheckLogin()]
        public JsonResult Logout()
        {
            //Session.Clear();
            CookieHelper.Del("UsersID");
            CookieHelper.Del("SessionsID");
            ResultModel_Common resultModel = new ResultModel_Common();
            resultModel.Result = 1;
            resultModel.Desc = "退出成功";
            return Global.OutPutJsonResult(resultModel);
        }
    }
}