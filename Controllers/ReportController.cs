using SarftService.IServiceContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Upload_Photo.Filter;

namespace Upload_Photo.Controllers
{
    [SarftServiceExceptionFilter()]
    [CheckLogin()]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询举报类型
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult ReportList(int pageIndex = 0, int pageSize = 0, string ReportName = "")
        {
            try
            {
                var user = Global.ServiceClient.GetUserInfoById(Convert.ToInt64(CookieHelper.GetValue("UsersID")));
                if (user == null)
                {
                    return Json(100);
                }
                SarftService.IServiceContract.PageInfo<ReportTypeInfo> GetReportList = new SarftService.IServiceContract.PageInfo<ReportTypeInfo>();
                GetReportList = Global.ServiceClient.GetReportTypeInfoList(pageIndex, pageSize, ReportName);
                if (GetReportList.Data.Count < 1)
                {
                    return Json(100);
                }
                return Json(GetReportList);
            }
            catch (Exception)
            {

                return Json(100);
            }

        }
        /// <summary>
        /// 删除举报类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DelReport(long id)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            resultModel.Result = 1;
            resultModel.Desc = "删除成功";
            int num = Global.ServiceClient.DelReportTypeInfo(id);
            if (num < 1)
            {
                resultModel.Result = 0;
                resultModel.Desc = "删除失败";
            }
            return Global.OutPutJsonResult(resultModel);
        }
        /// <summary>
        /// 编辑举报类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateReport(long id = 0)
        {
            if (id > 0)
            {
                ViewBag.Id = id;
                //为编辑
                ViewBag.OpType = "Edit";
                ReportTypeInfo ReportInfo = Global.ServiceClient.GetReportTypeById(id);
                if (ReportInfo != null)
                {
                    ViewBag.ID = ReportInfo.Id;
                    ViewBag.ReportName = ReportInfo.ReportName;
                    ViewBag.Remark = ReportInfo.Remark;
                }
            }
            else
            {
                //为新增
                ViewBag.OpType = "Add";
            }
            return View();
        }
        public ActionResult AlterReport(string ConfigJson)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            ReportTypeInfo ReportInfo = new ReportTypeInfo();
            if (!string.IsNullOrEmpty(ConfigJson))
            {
                try
                {
                    ReportInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportTypeInfo>(ConfigJson);
                    //为新增
                    if (ReportInfo.Id == 0)
                    {
                        if (ReportInfo != null)
                        {
                            if (Global.ServiceClient.AddReportType(ReportInfo) > 0)
                            {
                                resultModel.Result = 1;
                                resultModel.Desc = "保存成功";
                            }
                            else
                            {
                                resultModel.Result = 0;
                                resultModel.Desc = "保存失败";
                            }

                        }
                    }
                    else
                    {
                        if (Global.ServiceClient.UpdateReportType(ReportInfo) > 0)
                        {
                            resultModel.Result = 1;
                            resultModel.Desc = "修改成功";
                        }
                        else
                        {
                            resultModel.Result = 0;
                            resultModel.Desc = "修改失败";
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultModel.Desc = ex.Message;
                    resultModel.Result = 0;
                    return Global.OutPutJsonResult(resultModel);
                }
            }
            else
            {
                resultModel.Desc = "数据获取失败";
                resultModel.Result = 0;
                return Global.OutPutJsonResult(resultModel);
            }
            return Global.OutPutJsonResult(resultModel);
        }


        /// <summary>
        /// 查询举报信息数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetReortDataList()
        {
            return View();
        }
        /// <summary>
        /// 查询举报信息数据
        /// </summary>
        /// <param name="pageSize">页码大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="Status">状态</param>
        /// <param name="Stars">标星</param>
        /// <param name="ReportType">举报类型</param>
        /// <param name="oderType">排序</param>
        /// <returns></returns>
        public ActionResult GetReortDataInfoList(int pageSize = 0, int pageIndex = 0, string startTime = "", string endTime = "", short Status = 0, short Stars = 0, short CusFlag = 0, short CastFlag = 0, short HostFlag = 0, long ReportType = 0, byte oderType = 1)
        {
            try
            {
                var user = Global.ServiceClient.GetUserInfoById(Convert.ToInt64(CookieHelper.GetValue("UsersID")));
                if (user == null)
                {
                    return Json(100);
                }
                SarftService.IServiceContract.PageInfo<ReportDataInfo> GetReportList = new SarftService.IServiceContract.PageInfo<ReportDataInfo>();
                GetReportList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageIndex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType,0);
                if (GetReportList.Data.Count < 1)
                {
                    return Json(100);
                }
                return Json(GetReportList);
            }
            catch (Exception)
            {

                return Json(100);
            }

        }
        /// <summary>
        /// 导出推荐违章信息统计列表
        /// </summary>
        /// <returns></returns>
        public FileResult Reportex()
        {
            string startTime = Request.QueryString["startTime"];
            string endTime = Request.QueryString["endTime"];
            short Status = short.Parse(Request.QueryString["Status"]);
            short Stars = short.Parse(Request.QueryString["Stars"]);
            short CusFlag = short.Parse(Request.QueryString["CusFlag"]);
            short CastFlag = short.Parse(Request.QueryString["CastFlag"]);
            short HostFlag = short.Parse(Request.QueryString["HostFlag"]);
            short ReportType = short.Parse(Request.QueryString["ReportType"]);
            byte oderType = byte.Parse(Request.QueryString["oderType"]);
            int pageindex = 1;
            int pageSize = 1;
            int count = 0;
            int pageCount = 100;
            List<ReportDataInfo> carlist = new List<ReportDataInfo>();
            SarftService.IServiceContract.PageInfo<ReportDataInfo> GetReportList = new SarftService.IServiceContract.PageInfo<ReportDataInfo>();
            GetReportList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageindex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType,0);

            if (GetReportList.TotalCount > 0)
            {
                while (GetReportList.TotalCount > count)
                {
                    GetReportList = Global.ServiceClient.GetReortDataInfoList(pageCount, pageindex,startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType,0);
                    count += GetReportList.Data.Count;
                    carlist.AddRange(GetReportList.Data);
                    pageindex++;
                }
            }
            if (carlist.Count > 0)
            {
                //创建Excel文件的对象
                NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                //添加一个sheet
                NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

                //给sheet1添加第一行的头部标题
                NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
                row1.CreateCell(0).SetCellValue("用户名");
                row1.CreateCell(2).SetCellValue("用户车牌号");
                row1.CreateCell(1).SetCellValue("举报类型");
                row1.CreateCell(3).SetCellValue("举报车牌号");
                row1.CreateCell(4).SetCellValue("位置信息");
                row1.CreateCell(5).SetCellValue("筛选说明");
                row1.CreateCell(6).SetCellValue("上报时间");
                row1.CreateCell(7).SetCellValue("电台反馈");
                row1.CreateCell(8).SetCellValue("推荐时间");
                //将数据逐步写入sheet1各个行
                for (int i = 0; i < carlist.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    rowtemp.CreateCell(0).SetCellValue(carlist[i].UserName!=null? carlist[i].UserName.ToString():"");
                    rowtemp.CreateCell(1).SetCellValue(carlist[i].UserCarLicense != null ? carlist[i].UserCarLicense.ToString() : "");
                    rowtemp.CreateCell(2).SetCellValue(carlist[i].ReportTypeName!= null ? carlist[i].ReportTypeName.ToString() : "");
                    rowtemp.CreateCell(3).SetCellValue(carlist[i].ReportCarLicense != null ? carlist[i].ReportCarLicense.ToString() : "");
                    rowtemp.CreateCell(4).SetCellValue(carlist[i].TextLocation != null ? carlist[i].TextLocation.ToString() : "");
                    rowtemp.CreateCell(5).SetCellValue(carlist[i].CusRemark != null ? carlist[i].CusRemark.ToString() : "");
                    rowtemp.CreateCell(6).SetCellValue(carlist[i].ReportTime != null ? carlist[i].ReportTime.ToString() : "");
                    rowtemp.CreateCell(7).SetCellValue(carlist[i].BroRemark != null ? carlist[i].BroRemark.ToString() : "");
                    rowtemp.CreateCell(8).SetCellValue(carlist[i].ReportTime != null ? carlist[i].ReportTime.ToString() : "");
                }
                // 写入到客户端 
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                book.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/vnd.ms-excel", "推荐违章信息统计列表.xls");
            }
            System.IO.MemoryStream msd = new System.IO.MemoryStream();
            return File(msd, "application/vnd.ms-excel", "推荐违章信息统计列表.xls");
        }
    }
}