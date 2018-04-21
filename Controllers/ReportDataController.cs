using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Upload_Photo.Filter;
using SarftService.IServiceContract;

namespace Upload_Photo.Controllers
{
    [SarftServiceExceptionFilter()]
    [CheckLogin()]
    public class ReportDataController : Controller
    {
        // GET: ReportData
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 已推荐举报信息列表
        /// </summary>
        /// <returns></returns>
        public ActionResult BroadCastInfoList()
        {
            List<ReportTypeInfo> info = Global.ServiceClient.GetReportTypeList();
            ViewData.Add("info", info);
            return View();
        }
        /// <summary>
        /// 已标星举报信息列表
        /// </summary>
        /// <returns></returns>
        public ActionResult HostManInfoList()
        {
            return View();
        }
    }
}