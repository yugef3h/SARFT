using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Upload_Photo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        //登录页面
        public ActionResult Login()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["timeout"]))
            {
                ViewBag.TimeOut = Request.QueryString["timeout"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["child"]))
            {
                ViewBag.Child = Request.QueryString["child"];
            }
            return View();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult UserLogin(string userName, string password, string url, bool b)
        {

            CookieHelper.Del("UsersID");
            ResultModel_Common resultModel = new ResultModel_Common();
            resultModel.Result = 1;
            resultModel.Desc = "登录成功";
            SarftService.IServiceContract.UsersInfo userInfo = Global.ServiceClient.FindUserInfo(userName, password);
            if (userInfo != null)
            {
                if ((!password.Equals(userInfo.Password)))
                {
                    resultModel.Result = 0;
                    resultModel.Desc = "用户名或密码错误";
                    return Global.OutPutJsonResult(resultModel);
                }
            }
            else
            {
                resultModel.Result = 0;
                resultModel.Desc = "用户名或密码错误";
                return Global.OutPutJsonResult(resultModel);
            }
            NRails.LogHelper.Info(userInfo.Id.ToString());
            if (resultModel.Result == 1)
            {           
                CookieHelper.SetObj("UsersID", 60 * 60, userInfo.Id.ToString());
                CookieHelper.SetObj("SessionsID", 60 * 60, (userInfo.UserName + userInfo.Password));
                if (b)
                {
                    CookieHelper.SetObj("UsersName", 60 * 60 * 7, userName);
                    CookieHelper.SetObj(userName + "_Passwords", 60 * 60 * 7, password);
                    CookieHelper.SetObj("remenbermes", 60 * 60 * 7, "1");
                }
                else
                {
                    CookieHelper.Del("UsersName");
                    CookieHelper.Del("Passwords");
                    CookieHelper.SetObj("remenbermes", 60 * 60 * 7, "0");
                }
                resultModel.Data = userInfo;
            }
            return Global.OutPutJsonResult(resultModel);
        }
        
    }
}