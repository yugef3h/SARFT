using SarftService.IServiceContract;
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
    public class NotScreenMsgController : Controller
    {
        // GET: NotScreenMsg
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询未筛选信息数据
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
        public JsonResult GetReortDataInfoList(int pageSize = 0, int pageIndex = 0, string startTime = "", string endTime = "", short Status = 0, short Stars = 0, short CusFlag = 0, short CastFlag = 0, short HostFlag = 0, long ReportType = 0, byte oderType = 1)
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
                GetReportList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageIndex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, 0);
                if (pageIndex > 1 && GetReportList.Data.Count < 1)
                {
                    pageIndex = pageIndex - 1;
                    GetReportList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageIndex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, 0);
                    resultModel.Data = GetReportList;
                    resultModel.Result = 1;
                    resultModel.Desc = "加载成功";
                }
                else {
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
        /// 编辑
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult GetReportInfo(long ID)
        {
            SarftService.IServiceContract.ReportDataInfo GetReportList = new SarftService.IServiceContract.ReportDataInfo();
            var user = Global.ServiceClient.GetUserInfoById(Convert.ToInt64(CookieHelper.GetValue("UsersID")));
            if (user == null)
            {
                return Json(GetReportList);
            }
            GetReportList = Global.ServiceClient.GetReportDataByID(ID);
            return Json(GetReportList);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="ConfigJson"></param>
        /// <returns></returns>
        public ActionResult AlterReport(string ConfigJson)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            ReportDataInfo ReportInfo = new ReportDataInfo();
            ReportDataInfo ReportInfo1 = new ReportDataInfo();
            if (!string.IsNullOrEmpty(ConfigJson))
            {
                try
                {
                    ReportInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportDataInfo>(ConfigJson);
                    ReportInfo1 = Global.ServiceClient.GetReportDataByID(ReportInfo.Id);
                    if (ReportInfo.Id == 0)
                    {
                        resultModel.Desc = "数据获取失败";
                        resultModel.Result = 0;
                        return Global.OutPutJsonResult(resultModel);
                    }
                    else
                    {
                        ReportInfo1.CusRemark = ReportInfo.CusRemark;
                        ReportInfo1.ReportType = ReportInfo.ReportType;
                        ReportInfo1.ReportCarLicense = ReportInfo.ReportCarLicense;
                        ReportInfo1.CusFlag = 2;
                        if (Global.ServiceClient.UpdateReportData(ReportInfo1))
                        {
                            DataHelper.AddLog(ReportInfo1.Id,1);
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
        /// 客服已读
        /// </summary>
        /// <param name="ConfigJson"></param>
        /// <returns></returns>
        public ActionResult AlterReportCusFlag(string ConfigJson)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            ReportDataInfo ReportInfo = new ReportDataInfo();
            ReportDataInfo ReportInfo1 = new ReportDataInfo();
            if (!string.IsNullOrEmpty(ConfigJson))
            {
                try
                {
                    ReportInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportDataInfo>(ConfigJson);
                    ReportInfo1 = Global.ServiceClient.GetReportDataByID(ReportInfo.Id);
                    if (ReportInfo.Id == 0)
                    {
                        resultModel.Desc = "数据获取失败";
                        resultModel.Result = 0;
                        return Global.OutPutJsonResult(resultModel);
                    }
                    else
                    {
                        ReportInfo1.CusFlag = 2;
                        if (Global.ServiceClient.UpdateReportData(ReportInfo1))
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
        /// 推荐
        /// </summary>
        /// <param name="ConfigJson"></param>
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
                    if (ReportInfo != null)
                    {
                        //if (!string.IsNullOrEmpty(ReportInfo.ReportCarLicense) && ReportInfo.ReportType > 0)
                        if (ReportInfo.ReportType > 0)
                        {
                            ReportInfo.Status = 2;
                            if (Global.ServiceClient.UpdateReportData(ReportInfo))
                            {
                                DataHelper.AddLog(Id, 4);
                                resultModel.Result = 1;
                                resultModel.Desc = "推荐成功";
                            }
                            else
                            {
                                resultModel.Result = 0;
                                resultModel.Desc = "推荐失败";
                            }
                        }
                        else
                        {
                            resultModel.Result = 0;
                            resultModel.Desc = "请先编辑完善信息";
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
        /// 删除
        /// </summary>
        /// <param name="ConfigJson"></param>
        /// <returns></returns>
        public ActionResult DelReport(long ID)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            ReportDataInfo ReportInfo = new ReportDataInfo();
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
                    ReportInfo.Is_Lock = 1;
                    if (Global.ServiceClient.UpdateReportData(ReportInfo))
                    {
                        DataHelper.AddLog(ID, 2);
                        resultModel.Result = 1;
                        resultModel.Desc = "删除成功";
                        return Global.OutPutJsonResult(resultModel);
                    }
                    else
                    {
                        resultModel.Result = 0;
                        resultModel.Desc = "删除失败";
                        return Global.OutPutJsonResult(resultModel);
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
        /// <summary>
        /// 回收箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Trash()
        {
            return View();
        }
        /// <summary>
        /// 查询回收箱信息数据
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
        public JsonResult GetReortDataInfoTrashList(int pageSize = 0, int pageIndex = 0, string startTime = "", string endTime = "", short Status = 0, short Stars = 0, short CusFlag = 0, short CastFlag = 0, short HostFlag = 0, long ReportType = 0, byte oderType = 1)
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
                GetReportList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageIndex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, 1);
                if (pageIndex > 1 && GetReportList.Data.Count < 1)
                {
                    pageIndex = pageIndex - 1;
                    GetReportList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageIndex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, 1);
                    resultModel.Data = GetReportList;
                    resultModel.Result = 1;
                    resultModel.Desc = "加载成功";
                }
                else {
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
        /// 回收箱恢复
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult RecoverReport(long ID)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            ReportDataInfo ReportInfo = new ReportDataInfo();
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
                    ReportInfo.Is_Lock = 0;
                    if (Global.ServiceClient.UpdateReportData(ReportInfo))
                    {
                        DataHelper.AddLog(ID, 3);
                        resultModel.Result = 1;
                        resultModel.Desc = "恢复成功";
                        return Global.OutPutJsonResult(resultModel);
                    }
                    else
                    {
                        resultModel.Result = 0;
                        resultModel.Desc = "恢复失败";
                        return Global.OutPutJsonResult(resultModel);
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

        /// <summary>
        /// 推荐栏
        /// </summary>
        /// <returns></returns>
        public ActionResult Recommend()
        {
            return View();
        }
        /// <summary>
        /// 查询推荐栏信息数据
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
        public JsonResult GetReortDataInfoRecommendList(int pageSize = 0, int pageIndex = 0, string startTime = "", string endTime = "", short Status = 0, short Stars = 0, short CusFlag = 0, short CastFlag = 0, short HostFlag = 0, long ReportType = 0, byte oderType = 1)
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
                GetReportList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageIndex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, 0);
                if (pageIndex > 1 && GetReportList.Data.Count < 1)
                {
                    pageIndex = pageIndex - 1;
                    GetReportList = Global.ServiceClient.GetReortDataInfoList(pageSize, pageIndex, startTime, endTime, Status, Stars, CusFlag, CastFlag, HostFlag, ReportType, oderType, 0);
                    resultModel.Data = GetReportList;
                    resultModel.Result = 1;
                    resultModel.Desc = "加载成功";
                }
                else {
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
        /// 取消推荐
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult RecommendReport(long ID)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            ReportDataInfo ReportInfo = new ReportDataInfo();
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
                    ReportInfo.Status = 1;
                    if (Global.ServiceClient.UpdateReportData(ReportInfo))
                    {
                        DataHelper.AddLog(ID, 6);
                        resultModel.Result = 1;
                        resultModel.Desc = "取消推荐成功";
                        return Global.OutPutJsonResult(resultModel);
                    }
                    else
                    {
                        resultModel.Result = 0;
                        resultModel.Desc = "取消推荐失败";
                        return Global.OutPutJsonResult(resultModel);
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
    }
}