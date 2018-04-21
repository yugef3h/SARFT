using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NRails;
using NRails.Util;
using OSSClient;
using SarftService.IServiceContract;
using SarftService.ServiceCommon;
using CloudDogClient;
using TerminalClient;
using OSSFileStorageService.IServiceContract;
using TerminalDataService.IServiceContract;
using CloudDog.IServiceContract;
using System.Threading.Tasks;
using DES;

namespace SarftService.ServiceImpl
{
    public class ReportDataCollection
    {
        static DateTime startTime;
        static ReportDataCollection()
        {
           startTime = DateTime.Now.AddSeconds(-(ServiceConfig.tick / 1000));
           //startTime = Convert.ToDateTime("2016-05-18 17:01:30");
        }
        /// <summary>
        /// 采集数据
        /// </summary>
        private static void CollectData()
        {
           
            string oldTime = startTime.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                List<ReportDataInfo> reportList = new List<ReportDataInfo>();
                using (var client = new MediaClient(ServiceConfig.OSSFileAdress))
                {
                   var res = client.GDSearchMediaFileInfoThanRevTime(oldTime, 1, 4,ServiceConfig.CityCode);
                    
                    if (res != null)
                    {
                        if (Convert.ToDateTime(res.ReceiveTime) > startTime)
                        {
                            //有新数据
                            if (startTime != Convert.ToDateTime(res.ReceiveTime))
                            {
                                //根据返回时间更新查询时间
                                startTime = Convert.ToDateTime(res.ReceiveTime);
                                //两段闭区间查询
                                var retInfo = client.GDSearchMediaFileListByRange(oldTime, startTime.ToString("yyyy-MM-dd HH:mm:ss"), 1, 4, 1, 1000, 0, ServiceConfig.CityCode);
                                if (retInfo != null && retInfo.Total > 0)
                                {
                                    foreach (var model in retInfo.FileItems)
                                    {
                                        //过滤掉重复数据
                                        if (model.ReceiveTime == oldTime) continue;
                                        //过滤掉经纬度为0的无效数据
                                        if (model.Item.Lon == 0 && model.Item.Lat == 0) continue;
                                        else
                                        {
                                            ReportDataInfo info = new ReportDataInfo();
                                            info.ImgURL = model.FileUrl;
                                            info.Lat = model.Item.Lat;
                                            info.Lon = model.Item.Lon;
                                            info.ReportTime = model.ReceiveTime == "" ? oldTime : model.ReceiveTime;
                                            info.TextLocation = model.Item.TextLocation;
                                            info.TerminalKey = model.TerminalKey;
                                            using (var dogClient = new CloudDogServiceClient(ServiceConfig.CloudDogAdress))
                                            {
                                                //获取用户终端信息
                                                var terminalInfo = dogClient.GetUserTermianlByTerminalKey(model.TerminalKey);
                                                
                                                if (terminalInfo != null)
                                                {
                                                    info.UserName = terminalInfo.LoginName;
                                                    info.Phone = terminalInfo.Phone;
                                                    try
                                                    {
                                                        using (var terminalClient = new TerminalServiceClient(ServiceConfig.TerminalAdress))
                                                        {
                                                            Token tk = new Token();
                                                            tk.Content = model.TerminalKey;
                                                            string token = DES.DES.DesEncrypt(tk);
                                                            //获取终端信息
                                                            var terminalInfo2 = terminalClient.GetTermianlByISMI(new string[] { token });
                                                            //获取车牌号
                                                            info.UserCarLicense = terminalInfo2[0].Remark;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        NRails.LogHelper.Error(ex, "采集车牌信息异常!");
                                                    }
                                                   
                                                   
                                                }
                                            }
                                            reportList.Add(info);
                                        }

                                    }
                                    //入库操作
                                    if (reportList.Count > 0)
                                    {
                                        var service = ServiceContainer.ServiceHostContainer.Current.GetServiceItemObject<IReportData>();
                                        service.AddReportData(reportList.ToArray());
                                    }

                                }
                            }
                           
                        }
                      
                    }
                }
            }
            catch (Exception ex)
            {
                NRails.LogHelper.Error(ex, "采集举报信息异常!");
            }
        }
        /// <summary>
        /// 定时采集数据
        /// </summary>
        public static void CaptureData()
        {
            ThreadHelper.InvokeOnce(CollectData, ServiceConfig.tick, true);  
        }
       
    }
}
