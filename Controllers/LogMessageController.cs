using SarftService.IServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Upload_Photo.Controllers
{
    public class LogMessageController : Controller
    {
        // GET: LogMessage
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LogMessageList(int pageIndex = 0, int pageSize = 0, short OperationType = 0)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            try
            {
                SarftService.IServiceContract.PageInfo<LogMessageInfo> GetLogMessageList = new SarftService.IServiceContract.PageInfo<LogMessageInfo>();
                List<object> list = new List<object>();
                //GetLogMessageList = Global.ServiceClient.GetLogMessageInfoList(pageIndex, pageSize, Convert.ToInt64(CookieHelper.GetValue("UsersID")), OperationType);
                GetLogMessageList = Global.ServiceClient.GetLogMessageInfoList(pageIndex, pageSize, 0, OperationType);
                if (GetLogMessageList.Data.Count > 0)
                {
                    string OperationTypeName = string.Empty;
                    foreach (var item in GetLogMessageList.Data)
                    {
                        if (item.OperationType == 1)
                        {
                            OperationTypeName = "客服编辑";
                        }
                        else if (item.OperationType == 2)
                        {
                            OperationTypeName = "客服删除";
                        }
                        else if (item.OperationType == 3)
                        {
                            OperationTypeName = "客服恢复";
                        }
                        else if (item.OperationType == 4)
                        {
                            OperationTypeName = "客服推荐";
                        }
                        else if (item.OperationType == 5)
                        {
                            OperationTypeName = "导播推荐";
                        }
                        else if (item.OperationType == 6)
                        {
                            OperationTypeName = "客服取消推荐";
                        }
                        else if (item.OperationType == 7)
                        {
                            OperationTypeName = "导播编辑";
                        }
                        else if (item.OperationType == 8)
                        {
                            OperationTypeName = "主持人通知";
                        }
                        else if (item.OperationType == 9)
                        {
                            OperationTypeName = "主持人播报";
                        }
                        else if (item.OperationType == 10)
                        {
                            OperationTypeName = "导播取消推荐";
                        }
                        list.Add(new
                        {
                            UserName = Global.ServiceClient.GetUserInfoById(item.UserID).UserName,
                            ReportData = Global.ServiceClient.GetReportDataByID(item.ReportDataID),
                            OperationType = OperationTypeName,
                            Data= GetLogMessageList.Data.Count,
                            TotalCount= GetLogMessageList.TotalCount,
                            CreateTime = item.CreateTime.ToString("yyyy-MM-hh"),
                        });
                    }
                    resultModel.Data = list;
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
    }
}