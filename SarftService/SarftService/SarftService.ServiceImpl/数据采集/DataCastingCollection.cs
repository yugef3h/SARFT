using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPS.Common;
using NRails;
using NRails.Service;
using NRails.Threading;
using NRails.Util;
using OSSClient;
using SarftService.IServiceContract;
using SarftService.ServiceCommon;
using CloudDogClient;
using TerminalClient;
using OSSFileStorageService.IServiceContract;
using TerminalDataService.IServiceContract;
using CloudDog.IServiceContract;
using DES;

namespace SarftService.ServiceImpl
{
    public class DataCastingCollection
    {
        //广电数据接收客户端
        static DatacastingClient<ClientAction> castingClient = null;
     
        //广电数据队列
        static DataProcessQueue<TerminalData> dataProcess = null;

        static DateTime _time = DateTime.Now;
       
        static DataCastingCollection()
        {
            //初始化队列 最大值10000  0.5s处理每次
            dataProcess = new DataProcessQueue<TerminalData>(HandleTerminalDatas, 500);
            //广电数据接收初始化
            InitializeCastingClient(ServiceConfig.userName);
            //定时检查时间
            CheckByTime();

        }
        /// <summary>
        /// 数据接收处理
        /// </summary>
        /// <param name="datas"></param>
        private static void HandleTerminalDatas(TerminalData[] datas)
        {
            List<ReportDataInfo> reportList = new List<ReportDataInfo>();
            foreach (var item in datas)
            {
                //数据解析
                switch (item.ClientAction)
                {
                    case ClientAction.指令应答:
                        {
                            if (item.ServerAction == ServerAction.一键拍照)
                            {
                                var DataInfo = item.Data as CMD_RES_一键拍照;
                                //为空时不操作
                                if (DataInfo == null) break;
                                //过滤掉不是一件拍照数据
                                if (DataInfo.StatusType != GPS.Common.StatusType.一键拍照) break;
                                //过滤掉经纬度为0的无效数据
                                if (DataInfo.Item.Lon == 0 && DataInfo.Item.Lat == 0) break;
                                else
                                {
                                    ReportDataInfo info = new ReportDataInfo();
                                    info.ImgURL = DataInfo.FileUrl;
                                    info.Lat = DataInfo.Item.Lat;
                                    info.Lon = DataInfo.Item.Lon;
                                    info.ReportTime = DataInfo.FileTime == null ? "" : DataInfo.FileTime.ToString("yyyy-MM-dd HH:mm:ss");
                                    info.TextLocation = DataInfo.Item.TextLocation;
                                    info.TerminalKey = DataInfo.TerminalKey;
                                    using (var dogClient = new CloudDogServiceClient(ServiceConfig.CloudDogAdress))
                                    {
                                        //获取用户终端信息
                                        var terminalInfo = dogClient.GetUserTermianlByTerminalKey(DataInfo.TerminalKey);

                                        if (terminalInfo != null)
                                        {
                                            info.UserName = terminalInfo.LoginName;
                                            info.Phone = terminalInfo.Phone;
                                            try
                                            {
                                                using (var terminalClient = new TerminalServiceClient(ServiceConfig.TerminalAdress))
                                                {
                                                    Token tk = new Token();
                                                    tk.Content = DataInfo.TerminalKey;
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
                            break;
                        } 
                }
            }
            //入库操作
            if (reportList.Count > 0)
            {
                var service = ServiceContainer.ServiceHostContainer.Current.GetServiceItemObject<IReportData>();
                service.AddReportData(reportList.ToArray());
            }
        }
       
        /// <summary>
        /// 广电数据接收初始化
        /// </summary>
        /// <param name="userName"></param>
        static void InitializeCastingClient(string userName)
        {
            string password = EncryptHelper.BuildKey(userName);
            if (castingClient != null)
                castingClient.Dispose();
            castingClient = new DatacastingClient<ClientAction>(ServiceConfig.DATServer, userName, password, ServiceConfig.TDSubmitChannelName);
            castingClient.AfterConnect += new DatacastingClient<ClientAction>.DatacastingClientConnectedEventHandler(castingClient_AfterConnect);
            castingClient.DisConnected += new DatacastingClient<ClientAction>.DatacastingClientActionHandler(castingClient_DisConnected);
            castingClient.RegeistedDataReceived += new DatacastingClient<ClientAction>.RegeistedDataReceivedEventHandler(castingClient_RegeistedDataReceived);
            //注册所有终端
            castingClient.Regist(RegistMode.ALL);

            castingClient.ConnectAsync(true);
        }
        /// <summary>
        /// 链接服务器成功后处理程序
        /// </summary>
        /// <param name="client"></param>
        /// <param name="success"></param>
        /// <param name="error"></param>
        static void castingClient_AfterConnect(DatacastingClient<ClientAction> client, bool success, System.Net.Sockets.SocketError error)
        {
            if (error == System.Net.Sockets.SocketError.Success)
                NRails.LogHelper.Log(NRails.LogType.Info, "连接到[{0}]成功!", ServiceConfig.DATServer);
            else
                NRails.LogHelper.Log(NRails.LogType.Info, "连接到[{0}]失败!", ServiceConfig.DATServer);
        }
        /// <summary>
        /// 从服务器断开后的处理程序
        /// </summary>
        /// <param name="client"></param>
        static void castingClient_DisConnected(DatacastingClient<ClientAction> client)
        {
            NRails.LogHelper.Log(NRails.LogType.Info, "到服务器[{0}]的连接被断开!", ServiceConfig.DATServer);
        }
        /// <summary>
        /// 接收到订阅数据时的处理程序
        /// </summary>
        /// <param name="client"></param>
        /// <param name="datas"></param>
        static void castingClient_RegeistedDataReceived(DatacastingClient<ClientAction> client, MSGDAT<ClientAction>[] datas)
        {
            _time = DateTime.Now;
            foreach (var data in datas)
            {
                try
                {
                    switch (data.dat)
                    {

                        case ClientAction.指令应答:
                            {
                                var td = (TerminalData)SerializeHelper.DeSerialize(data.buffer, data.offset);
                                dataProcess.Enqueue(td);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error(ex, "消息总线在处理数据时发生错误!数据类型:{0}".f(data.dat));
                }
            }
        }

        static void CheckRevTime()
        {
            //超过半小时无数据重新连接下消息总线
            if(_time.NowDistance(TimeDistanceType.Minutes) > ServiceConfig.TimeTick)
            {
                //广电数据接收初始化
                InitializeCastingClient(ServiceConfig.userName);
            }
        }
        /// <summary>
        /// 定时检查时间
        /// </summary>
        static void CheckByTime()
        {
            int tick = Convert.ToInt32( ServiceConfig.TimeTick.ToString())* 60 * 1000;
            ThreadHelper.InvokeOnce(CheckRevTime, tick, true);
        }
    }
}
