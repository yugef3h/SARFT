using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Upload_Photo.Filter;

namespace Upload_Photo.Controllers
{
    [SarftServiceExceptionFilter()]
    [CheckLogin()]
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult Index(long id = 0)
        {

            ViewBag.Id = id;
            return View();
        }
        public ActionResult Initial(long id = 0)
        {
            SarftService.IServiceContract.ReportDataInfo report = new SarftService.IServiceContract.ReportDataInfo();
            report = Global.ServiceClient.GetReportDataByID(id);
            if (report != null)
            {
                return Json(report);
            }
            return View();
        }
    }
}