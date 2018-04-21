using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Web.Mvc;
using Upload_Photo.Filter;
using SarftService.IServiceContract;
using DES;
using System.Net.Http;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Upload_Photo.Controllers
{
    [SarftServiceExceptionFilter()]
    [CheckLogin()]
    public class ReportDataWebApiController : Controller
    {
        // GET: ReportDataWebApi
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取推荐信息列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="ReportType"></param>
        /// <param name="Status"></param>
        /// <param name="Stars"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public JsonResult GetBroadCastInfoList(int pageIndex, int pageSize, long ReportType, short CastFlag, short Status, string start, string end)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            if (!string.IsNullOrEmpty(start))
            {
                start += " 00:00:00";
            }
            if (!string.IsNullOrEmpty(end))
            {
                end += " 23:59:59";
            }
            PageInfo<ReportDataInfo> orderList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageIndex, start, end, Status, 0, 0, CastFlag, 0, ReportType, 1,0);
            if (orderList.Data.Count > 0)
            {
                resultModel.Data = orderList;
                resultModel.Result = 1;
                resultModel.Desc = "加载成功";
            }
            else
            {
                resultModel.Result = 0;
                resultModel.Desc = "加载失败";
            }
            return Global.OutPutJsonResult(resultModel);
        }
        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public JsonResult GetReportDataInfo(long ID)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            resultModel.Data = null;
            try
            {
                ReportDataInfo info = Global.ServiceClient.GetReportDataByID(ID);
                if (info != null)
                {
                    resultModel.Result = 1;
                    resultModel.Data = info;
                    //更新成已读状态
                    info.CastFlag = 2;
                    Global.ServiceClient.UpdateReportData(info);
                }
                else
                {
                    resultModel.Result = 0;
                    resultModel.Desc = "举报信息获取失败";
                }
            }
            catch (Exception ex)
            {
                resultModel.Result = 0;
                resultModel.Desc = ex.Message.ToString();
            }

            return Global.OutPutJsonResult(resultModel);
        }
        /// <summary>
        /// 编辑举报信息
        /// </summary>
        /// <param name="ConfigInfoJson"></param>
        /// <returns></returns>
        public JsonResult SaveCastReportDataContent(string ConfigInfoJson)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            bool ret = false;
            try
            {
                ReportDataInfo newData = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportDataInfo>(ConfigInfoJson);
                ReportDataInfo oldData = Global.ServiceClient.GetReportDataByID(newData.Id);
                oldData.CusRemark = newData.CusRemark;
                oldData.BroRemark = newData.BroRemark;
                oldData.ReportCarLicense = newData.ReportCarLicense;
                oldData.ReportType = newData.ReportType;
                //oldData.Stars = newData.Stars;
                ret = Global.ServiceClient.UpdateReportData(oldData);
                if (ret)
                {
                    DataHelper.AddLog(oldData.Id, 7);
                    resultModel.Desc = "保存成功";
                    resultModel.Result = 1;
                }
                else
                {
                    resultModel.Desc = "保存失败";
                    resultModel.Result = 0;
                }
            }
            catch (Exception ex)
            {
                resultModel.Desc = ex.Message;
                resultModel.Result = 0;
            }
            return Global.OutPutJsonResult(resultModel);
        }
        /// <summary>
        /// 查询标星信息数据
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
        public JsonResult GetHostManInfoList(int pageSize = 0, int pageIndex = 0, string startTime = "", string endTime = "", short Status = 2, short Stars = 7, short CusFlag = 0, short CastFlag = 0, short HostFlag = 0, long ReportType = 0, byte oderType = 1)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            try
            {
                if (!string.IsNullOrEmpty(startTime) && startTime.Length < 11)
                {
                    startTime += " 00:00:00";
                }
                if (!string.IsNullOrEmpty(endTime) && endTime.Length < 11)
                {
                    endTime += " 23:59:59";
                }
                SarftService.IServiceContract.PageInfo<ReportDataInfo> GetReportList = new SarftService.IServiceContract.PageInfo<ReportDataInfo>();
                GetReportList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageIndex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType,0);
                if (GetReportList.Data.Count > 0)
                {
                    resultModel.Data = GetReportList;
                    resultModel.Result = 1;
                    resultModel.Desc = "加载成功";
                }
                else
                {
                    resultModel.Result = 0;
                    resultModel.Desc = "加载失败";
                }
                return Global.OutPutJsonResult(resultModel);
            }
            catch (Exception)
            {

                resultModel.Result = 0;
                resultModel.Desc = "加载失败";
            }
            return Global.OutPutJsonResult(resultModel);
        }
        /// <summary>
        /// 导播评分
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="star"></param>
        /// <returns></returns>
        public JsonResult SaveCastScores(long Id, short star)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            bool ret = false;
            try
            {
                ReportDataInfo oldData = Global.ServiceClient.GetReportDataByID(Id);
                oldData.Stars = star;
                ret = Global.ServiceClient.UpdateReportData(oldData);
                if (ret)
                {
                    resultModel.Desc = "评价成功";
                    resultModel.Result = 1;
                }
                else
                {
                    resultModel.Desc = "评价失败";
                    resultModel.Result = 0;
                }
            }
            catch (Exception ex)
            {
                resultModel.Desc = ex.Message;
                resultModel.Result = 0;
            }
            return Global.OutPutJsonResult(resultModel);
        }
        /// <summary>
        /// 主持人已读
        /// </summary>
        /// <param name="ConfigJson"></param>
        /// <returns></returns>
        public ActionResult AlterReportHostFlag(long ID)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            ReportDataInfo ReportInfo = new ReportDataInfo();
            if (ID > 0)
            {
                try
                {
                    ReportInfo = Global.ServiceClient.GetReportDataByID(ID);
                    if (ReportInfo.Id == 0)
                    {
                        resultModel.Desc = "数据获取失败";
                        resultModel.Result = 0;
                        return Global.OutPutJsonResult(resultModel);
                    }
                    else
                    {
                        if (Global.ServiceClient.UpdateReportData(ReportInfo))
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
        /// 通知编辑
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult GetNotify(long ID)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            ReportDataInfo ReportInfo = new ReportDataInfo();
            ReportInfo = Global.ServiceClient.GetReportDataByID(ID);
            if (ReportInfo == null)
            {
                return Json(ReportInfo);
            }
            return Json(ReportInfo);
        }
        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetRewards(long Id, string AwardRemark)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            ReportDataInfo ReportInfo = new ReportDataInfo();
            if (Id > 0)
            {
                try
                {
                    ReportInfo = Global.ServiceClient.GetReportDataByID(Id);
                    if (ReportInfo.Stars == 2)
                    {
                        resultModel.Result = 1;
                        resultModel.Desc = "该条记录已通知过";
                        return Global.OutPutJsonResult(resultModel);
                    }
                    if (ReportInfo != null)
                    {
                        Token t1 = new Token();
                        t1.Content = ReportInfo.TerminalKey;
                        string tk1 = DES.DES.DesEncrypt(t1);
                        int commandType = 153;
                        CMDARG_SendMessage msg = new CMDARG_SendMessage();
                        if (!string.IsNullOrEmpty(AwardRemark))
                        {
                            msg.Message = AwardRemark;
                        }
                        else
                        {
                            msg.Message = "您举报" + ReportInfo.ReportCarLicense + "的车牌号已被采纳";
                        }
                        HttpClient client = new HttpClient();
                        string url = string.Format("http://weixin.ungps.com/command/SendCommand?terminalToken={0}&commandType={1}&json={2}", tk1, commandType, Newtonsoft.Json.JsonConvert.SerializeObject(msg));
                        try
                        {
                            var data = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
                            JObject obj = JObject.Parse(data);
                            string serial_number = obj["Success"].ToString();
                            if (serial_number == "True")
                            {
                                ReportInfo.Stars = 2;
                                ReportInfo.HostFlag = 2;
                                ReportInfo.AwardRemark = msg.Message;
                                if (Global.ServiceClient.UpdateReportData(ReportInfo))
                                {
                                    DataHelper.AddLog(Id, 8);
                                    resultModel.Result = 1;
                                    resultModel.Desc = "通知成功";
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            string mesg = e.Message;
                            resultModel.Result = 0;
                            resultModel.Desc = "通知失败";
                            return Global.OutPutJsonResult(resultModel);
                        }
                        //var data = client.GetAsync("http://aa.com?a=b&c=d").Result.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        resultModel.Result = 0;
                        resultModel.Desc = "通知失败";
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
        /// 播报
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetRecommend(long Id)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            ReportDataInfo ReportInfo = new ReportDataInfo();
            if (Id > 0)
            {
                try
                {
                    ReportInfo = Global.ServiceClient.GetReportDataByID(Id);
                    if (ReportInfo.Status == 4)
                    {
                        resultModel.Result = 1;
                        resultModel.Desc = "该条记录已播报过";
                        return Global.OutPutJsonResult(resultModel);
                    }
                    if (ReportInfo != null)
                    {
                        try
                        {
                            ReportInfo.Status = 4;
                            if (Global.ServiceClient.UpdateReportData(ReportInfo))
                            {
                                DataHelper.AddLog(Id, 9);
                                resultModel.Result = 1;
                                resultModel.Desc = "播报成功";
                            }
                        }
                        catch (Exception e)
                        {
                            string mesg = e.Message;
                            resultModel.Result = 0;
                            resultModel.Desc = "播报失败";
                            return Global.OutPutJsonResult(resultModel);
                        }
                    }
                    else
                    {
                        resultModel.Result = 0;
                        resultModel.Desc = "播报失败";
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
        /// 发送文本信息
        /// </summary>
        public class CMDARG_SendMessage
        {
            /// <summary>
            /// 文本信息
            /// </summary>
            public string Message;
            /// <summary>
            /// 文本信息类型
            /// </summary>
            public int MessageType = 0;
        }

        /// <summary>
        /// 导播推荐
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult SaveRecomand(long Id)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            bool ret = false;
            try
            {
                ReportDataInfo oldData = Global.ServiceClient.GetReportDataByID(Id);
                oldData.Status = 3;
                ret = Global.ServiceClient.UpdateReportData(oldData);
                if (ret)
                {
                    DataHelper.AddLog(Id, 5);
                    resultModel.Desc = "推荐成功";
                    resultModel.Result = 1;
                }
                else
                {
                    resultModel.Desc = "推荐失败";
                    resultModel.Result = 0;
                }
            }
            catch (Exception ex)
            {
                resultModel.Desc = ex.Message;
                resultModel.Result = 0;
            }
            return Global.OutPutJsonResult(resultModel);
        }
        /// <summary>
        /// 导播推荐
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public JsonResult CancelRecomand(long Id)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            bool ret = false;
            try
            {
                ReportDataInfo oldData = Global.ServiceClient.GetReportDataByID(Id);
                oldData.Status = 2;
                ret = Global.ServiceClient.UpdateReportData(oldData);
                if (ret)
                {
                    DataHelper.AddLog(Id, 10);
                    resultModel.Desc = "取消推荐成功";
                    resultModel.Result = 1;
                }
                else
                {
                    resultModel.Desc = "取消推荐失败";
                    resultModel.Result = 0;
                }
            }
            catch (Exception ex)
            {
                resultModel.Desc = ex.Message;
                resultModel.Result = 0;
            }
            return Global.OutPutJsonResult(resultModel);
        }

        /// <summary>
        /// 流量卡导出
        /// </summary>
        /// <returns></returns>
        public FileResult OutPutCards()
        {

            string start = "";
            string end = "";
            short Status = 7;
            short CastFlag = 0;
            long ReportType = 0;
            var pageIndex = 1;
            var pageSize = 100;
            if (!string.IsNullOrEmpty(Request.QueryString["start"]))
            {
                start = Request.QueryString["start"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["end"]))
            {
                end = Request.QueryString["end"];
            }
            if (!string.IsNullOrEmpty(start))
            {
                start += " 00:00:00";
            }
            if (!string.IsNullOrEmpty(end))
            {
                end += " 23:59:59";
            }
            if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
            {
                if (Request.QueryString["Status"] != "7")
                {
                    Status = Convert.ToInt16(Request.QueryString["Status"]);
                }
            }
            if (!string.IsNullOrEmpty(Request.QueryString["CastFlag"]))
            {
                CastFlag = Convert.ToInt16(Request.QueryString["CastFlag"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ReportType"]))
            {
                ReportType = Convert.ToInt64(Request.QueryString["ReportType"]);
            }
            var list = new List<ReportDataInfo>();


            PageInfo<ReportDataInfo> orderList = Global.ServiceClient.GetReortDataInfoList(1, 1, start, end, Status, 0, 0, CastFlag, 0, ReportType, 1,0);
            var count = orderList.TotalCount;
            int record = 0;

            while (orderList.TotalCount > record)
            {
                orderList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageIndex, start, end, Status, 0, 0, CastFlag, 0, ReportType, 1,0);
                list.AddRange(orderList.Data);
                record += orderList.Data.Count;
                pageIndex++;
            }
            if (list.Count > 0)
            {
                //创建Excel文件的对象
                NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
                //添加一个sheet
                NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

                //给sheet1添加第一行的头部标题
                NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
                // row1.CreateCell(0).SetCellValue("用户名");
                sheet1.SetColumnWidth(0, 20 * 256);
                ICell icell1top = row1.CreateCell(0);
                icell1top.CellStyle = IPOHelper.Getcellstyle(book, stylexls.头);
                icell1top.SetCellValue("举报人");


                //row1.CreateCell(1).SetCellValue("SIM");
                sheet1.SetColumnWidth(1, 20 * 256);
                ICell icell1top1 = row1.CreateCell(1);
                icell1top1.CellStyle = IPOHelper.Getcellstyle(book, stylexls.头);
                icell1top1.SetCellValue("举报人姓名");


                //row1.CreateCell(2).SetCellValue("产品型号");
                sheet1.SetColumnWidth(2, 20 * 256);
                ICell icell1top2 = row1.CreateCell(2);
                icell1top2.CellStyle = IPOHelper.Getcellstyle(book, stylexls.头);
                icell1top2.SetCellValue("举报人车牌号");

                //row1.CreateCell(3).SetCellValue("公众号");
                sheet1.SetColumnWidth(3, 20 * 256);
                ICell icell1top3 = row1.CreateCell(3);
                icell1top3.CellStyle = IPOHelper.Getcellstyle(book, stylexls.头);
                icell1top3.SetCellValue("被举报人车牌号");

                // row1.CreateCell(4).SetCellValue("续费金额(元)");
                sheet1.SetColumnWidth(4, 20 * 256);
                ICell icell1top4 = row1.CreateCell(4);
                icell1top4.CellStyle = IPOHelper.Getcellstyle(book, stylexls.头);
                icell1top4.SetCellValue("上报时间");


                // row1.CreateCell(5).SetCellValue("续费套餐");
                sheet1.SetColumnWidth(5, 25 * 256);
                ICell icell1top5 = row1.CreateCell(5);
                icell1top5.CellStyle = IPOHelper.Getcellstyle(book, stylexls.头);
                icell1top5.SetCellValue("位置信息");

                // row1.CreateCell(6).SetCellValue("续费时间");
                sheet1.SetColumnWidth(6, 25 * 256);
                ICell icell1top6 = row1.CreateCell(6);
                icell1top6.CellStyle = IPOHelper.Getcellstyle(book, stylexls.头);
                icell1top6.SetCellValue("信息说明");

                sheet1.SetColumnWidth(7, 25 * 256);
                ICell icell1top7 = row1.CreateCell(7);
                icell1top7.CellStyle = IPOHelper.Getcellstyle(book, stylexls.头);
                icell1top7.SetCellValue("电台反馈");

                sheet1.SetColumnWidth(8, 25 * 256);
                ICell icell1top8 = row1.CreateCell(8);
                icell1top8.CellStyle = IPOHelper.Getcellstyle(book, stylexls.头);
                icell1top8.SetCellValue("是否奖励");

                sheet1.SetColumnWidth(9, 25 * 256);
                ICell icell1top9 = row1.CreateCell(9);
                icell1top9.CellStyle = IPOHelper.Getcellstyle(book, stylexls.头);
                icell1top9.SetCellValue("奖励明细");



                ICellStyle style = IPOHelper.Getcellstyle(book, stylexls.默认);
                //将数据逐步写入sheet1各个行
                for (int i = 0; i < list.Count; i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    ICell icell = rowtemp.CreateCell(0);
                    icell.CellStyle = style;
                    rowtemp.CreateCell(0).SetCellValue((list[i].UserName == "" ? list[i].Phone : list[i].UserName));

                    ICell icell1 = rowtemp.CreateCell(1);
                    icell1.CellStyle = style;
                    rowtemp.CreateCell(1).SetCellValue(list[i].Name);
                    ICell icell2 = rowtemp.CreateCell(2);
                    icell2.CellStyle = style;
                    rowtemp.CreateCell(2).SetCellValue(list[i].UserCarLicense);
                    ICell icell3 = rowtemp.CreateCell(3);
                    icell3.CellStyle = style;
                    rowtemp.CreateCell(3).SetCellValue(list[i].ReportCarLicense);
                    ICell icell4 = rowtemp.CreateCell(4);
                    icell4.CellStyle = style;
                    rowtemp.CreateCell(4).SetCellValue(list[i].ReportTime);
                    ICell icell5 = rowtemp.CreateCell(5);
                    icell5.CellStyle = style;
                    rowtemp.CreateCell(5).SetCellValue(list[i].TextLocation);
                    ICell icell6 = rowtemp.CreateCell(6);
                    icell6.CellStyle = style;
                    rowtemp.CreateCell(6).SetCellValue(list[i].CusRemark);

                    ICell icell7 = rowtemp.CreateCell(7);
                    icell7.CellStyle = style;
                    rowtemp.CreateCell(7).SetCellValue(list[i].BroRemark);
                    ICell icell8 = rowtemp.CreateCell(8);
                    icell8.CellStyle = style;
                    if (list[i].Stars == 1)
                    {
                        rowtemp.CreateCell(8).SetCellValue("否");
                    }
                    else
                    {
                        rowtemp.CreateCell(8).SetCellValue("是");
                    }

                    ICell icell9 = rowtemp.CreateCell(9);
                    icell9.CellStyle = style;
                    rowtemp.CreateCell(9).SetCellValue(list[i].AwardRemark);

                }
                // 写入到客户端 
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                book.Write(ms);
                ms.Seek(0, SeekOrigin.Begin);
                return File(ms, "application/vnd.ms-excel", "举报信息.xls");
            }
            System.IO.MemoryStream msd = new System.IO.MemoryStream();
            return File(msd, "application/vnd.ms-excel", "举报信息.xls");
        }
    }
}