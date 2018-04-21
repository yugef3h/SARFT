using System;
using NRails;
using NRails.Collections;
using NRails.Configuration;
using SarftService.ServiceCommon;
using ServiceContainer;
using Service.Data.Client;
using SarftService.DBProvider;
using SarftService.IServiceContract;
using SarftService.ServiceImpl;

namespace SarftService.ServiceMain
{
    public class StartUp : NRails.Service.IServiceBase
    {
        public override void Initialize(ParameterProvider provider = null)
        {
            new ServiceConfig().Initialize(provider);
            base.Initialize(provider);
            #region 服务日志
            this.LOGS.Add(MyInvoker.Log);
            #endregion
            ServiceItemConfig config = new ServiceItemConfig();
            config.SetServiceImpl(typeof(IUsers), new UsersInfoImpl());
            config.SetServiceImpl(typeof(IReportData), new ReportDataImpl());
            config.SetServiceImpl(typeof(IReportType), new ReportTypeImpl());
            config.SetServiceImpl(typeof(IErrorService), new ErrorServiceImpl());
            config.SetServiceImpl(typeof(ILogMessage), new LogMessageImpl());
            ServiceHostContainer shc = new ServiceHostContainer("ServiceName", new MyInvoker());
            shc.LoadServiceItem(config);
            if (ServiceConfig.UnPack)
                shc.AddHost(new HttpServiceHost_V2(ServiceConfig.HttpPort, "http ServiceName", HttpServiceHost_V2.RunType.ReturnUnPack));
            else
                shc.AddHost(new HttpServiceHost_V2(ServiceConfig.HttpPort, "nwcf ServiceName", HttpServiceHost_V2.RunType.ReturnPack));
            var nwcf = new NWcfServiceHost(ServiceConfig.NWcfPort, "ServiceName");
            shc.AddHost(nwcf);
            shc.Start();
            var i = new CustomMenuItem("复制代码");
            MyInvoker.Log.MenuItems.Add(i);
            nwcf.CopySoSourceCode(i);
            //初始化采集时间
            //  ReportDataCollection aa = new ReportDataCollection();
            //定时采集数据
            // ReportDataCollection.CaptureData();
            //开启总线接收数据
           // DataCastingCollection bb = new DataCastingCollection();
            MyInvoker.Log.Write("http port:" + ServiceConfig.HttpPort);
            MyInvoker.Log.Write("Nwcf port:" + ServiceConfig.NWcfPort);
        }

        public class MyInvoker : ServiceInvoker
        {
            public static ContentList Log = new ContentList("ServiceName", 500, true, new ContentColumn[]
            {
                new ContentColumn()
                {
                    Name = "接口名称", Width = 130
                },
                new ContentColumn()
                {
                    Name = "IP地址", Width = 90
                },
                new ContentColumn()
                {
                    Name = "SessionId", Width = 170
                }
            });
            protected override void OnAfterInvokeService(InvokeServiceContext context)
            {
                //提交事务
                DBClient client = (DBClient)context.Session["tran"];
                if (client != null)
                    client.CommitTransaction();

            }

            protected override void OnBeforeInvokeService(InvokeServiceContext context)
            {
                Log.Write(context.Service.MetohdName, context.Session.Ip, context.Session.SessionId);
                //创建事务
                DBClient client = DBClientFactory.GetDBClient(ServiceConfig.ConnectionString);
                client.BeginTransaction();
                context.Session["tran"] = client;
            }
        }


    }
}
