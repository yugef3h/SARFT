using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Upload_Photo
{
    public class Global : System.Web.HttpApplication
    {
        
        public static Upload_Photo.App_Start.SarftServiceClient ServiceClient = new App_Start.SarftServiceClient(ConfigHelper.ReadAppSettingsValue("SarftServiceAddress"));
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        #region 构造JsonResult

        public static JsonResult OutPutJsonResult(int result, string desc, object data = null)
        {
            ResultModel_Common resultInfo = new ResultModel_Common();

            resultInfo.Result = result;
            resultInfo.Desc = desc;
            resultInfo.Data = data;

            return OutPutJsonResult(resultInfo);
        }

        public static JsonResult OutPutJsonResult(ResultModel_Common resultInfo)
        {
            JsonResult jsonResult = new JsonResult();

            jsonResult.Data = resultInfo;

            return jsonResult;
        }
        public static JsonResult OutPutJsonResultByGet(ResultModel_Common resultInfo)
        {

            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jsonResult.Data = resultInfo;
            return jsonResult;
        }
        #endregion
    }
}
