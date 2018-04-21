using NRails.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SarftService.ServiceCommon
{
    public class ServiceConfig : ParameterSetting
    {
        public static string ConnectionString = "";
        public static int HttpPort = 7001; //http接口
        public static int NWcfPort = 7003; //NWcfPort接口
        public static bool Mocker = false;
        public static bool UnPack = false;
        public static string CloudDogAdress = "";//云狗服务地址
        public static string OSSFileAdress = "";//文件存储服务地址
        public static string CityCode = "";//采集数据的城市代码
        public static int tick = 0;//促发时间
        public static string TerminalAdress = "";//终端服务地址
        public static string DATServer = "";//消息总线服务地址
        public static string TDSubmitChannelName = "";//管道名称
        public static string userName = "";//管道用户名
        public static double TimeTick = 0;//检查时间间隔
    }
}
