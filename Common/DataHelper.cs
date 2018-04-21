using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using SarftService.IServiceContract;
using Upload_Photo;

public class DataHelper
{
    

    /// <summary>
    /// 根据用户查找型号
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static List<ReportTypeInfo> ReportTypeList()
    {
        return Global.ServiceClient.GetReportTypeList();
    }
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <param name="dtData"></param>
    /// <param name="FileName"></param>
    public static void DataTable2Excel(System.Data.DataTable dtData, String FileName)
    {
        System.Web.UI.WebControls.GridView dgExport = null;
        //当前对话 
        System.Web.HttpContext curContext = System.Web.HttpContext.Current;
        //IO用于导出并返回excel文件 
        System.IO.StringWriter strWriter = null;
        System.Web.UI.HtmlTextWriter htmlWriter = null;
        if (dtData != null && dtData.Rows.Count > 0)
        {
            curContext.Response.Clear();
            curContext.Response.ClearHeaders();
            curContext.Response.Buffer = true;

            //设置编码和附件格式 
            //System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8)作用是方式中文文件名乱码
            curContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            curContext.Response.ContentType = "application nd.ms-excel";//EXCEL 2003 
            //curContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";//EXCEL 2007
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "GB2312";
            curContext.Response.Write("<meta http-equiv='content-type' content='application/ms-excel;charset=UTF-8'/>");
            //导出Excel文件 
            strWriter = new System.IO.StringWriter();
            htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);
            //为了解决dgData中可能进行了分页的情况,需要重新定义一个无分页的GridView 
            dgExport = new System.Web.UI.WebControls.GridView();
            dgExport.DataSource = dtData.DefaultView;
            dgExport.AllowPaging = false;
            dgExport.DataBind();
            //下载到客户端 
            dgExport.RenderControl(htmlWriter);
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();
        }
    }
    public static void DataSet2Excel(System.Data.DataSet dsData, String FileName)
    {
        System.Web.UI.WebControls.GridView dgExport = null;
        //当前对话 
        System.Web.HttpContext curContext = System.Web.HttpContext.Current;
        //IO用于导出并返回excel文件 
        System.IO.StringWriter strWriter = null;
        System.Web.UI.HtmlTextWriter htmlWriter = null;
        if (dsData != null && dsData.Tables.Count > 0)
        {
            curContext.Response.Clear();
            curContext.Response.ClearHeaders();
            curContext.Response.Buffer = true;

            //设置编码和附件格式 
            //System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8)作用是方式中文文件名乱码
            curContext.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
            curContext.Response.ContentType = "application nd.ms-excel";//EXCEL 2003 
            //curContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";//EXCEL 2007
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "GB2312";
            curContext.Response.Write("<meta http-equiv='content-type' content='application/ms-excel;charset=UTF-8'/>");
            //导出Excel文件 
            strWriter = new System.IO.StringWriter();
            htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);
            //为了解决dgData中可能进行了分页的情况,需要重新定义一个无分页的GridView 
            foreach (DataTable dt in dsData.Tables)
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    dgExport = new System.Web.UI.WebControls.GridView();
                    dgExport.DataSource = dt;
                    dgExport.AllowPaging = false;
                    dgExport.DataBind();
                    //下载到客户端 
                    dgExport.RenderControl(htmlWriter);
                    htmlWriter.InnerWriter.Write("<br/><br/>");
                }
            }
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();
        }
    }
    //public void DataTableToExcel2(IList<TheCalendar> iListCalendar)
    //{
    //    DataTable dts = new DataTable("cart");
    //    DataColumn dc1 = new DataColumn("工号", Type.GetType("System.String"));
    //    DataColumn dc2 = new DataColumn("姓名", Type.GetType("System.String"));
    //    DataColumn dc3 = new DataColumn("上班打卡时间", Type.GetType("System.String"));
    //    DataColumn dc4 = new DataColumn("下班打卡时间", Type.GetType("System.String"));
    //    DataColumn dc5 = new DataColumn("开始时间", Type.GetType("System.String"));
    //    DataColumn dc6 = new DataColumn("结束时间", Type.GetType("System.String"));
    //    DataColumn dc7 = new DataColumn("部门", Type.GetType("System.String"));
    //    DataColumn dc8 = new DataColumn("异常", Type.GetType("System.String"));
    //    DataColumn dc9 = new DataColumn("已审核状态", Type.GetType("System.String"));
    //    DataColumn dc10 = new DataColumn("以前审核状态", Type.GetType("System.String"));
    //    DataColumn dc11 = new DataColumn("总时长", Type.GetType("System.String"));
    //    DataColumn dc12 = new DataColumn("有效时长", Type.GetType("System.String"));
    //    DataColumn dc13 = new DataColumn("考勤日期", Type.GetType("System.String"));
    //    dts.Columns.Add(dc1);
    //    dts.Columns.Add(dc2);
    //    dts.Columns.Add(dc3);
    //    dts.Columns.Add(dc4);
    //    dts.Columns.Add(dc5);
    //    dts.Columns.Add(dc6);
    //    dts.Columns.Add(dc7);
    //    dts.Columns.Add(dc8);
    //    dts.Columns.Add(dc9);
    //    dts.Columns.Add(dc10);
    //    dts.Columns.Add(dc11);
    //    dts.Columns.Add(dc12);
    //    dts.Columns.Add(dc13);
    //    for (int i = 0; i < iListCalendar.Count; i++)
    //    {
    //        DataRow dr = dts.NewRow();
    //        dr["工号"] = iListCalendar[i].WorkWorkNumber;
    //        dr["姓名"] = ProductAudit.GetUsernmaeByWorkNumber(iListCalendar[i].WorkWorkNumber);
    //        dr["上班打卡时间"] = iListCalendar[i].PunchInTime;
    //        dr["下班打卡时间"] = iListCalendar[i].PunchOutTime;
    //        dr["开始时间"] = iListCalendar[i].AbnormalStartTime;
    //        dr["结束时间"] = iListCalendar[i].AbnormalEndTime;
    //        dr["部门"] = ProductAudit.GetDepartmentFullNameByDepartmentID(ProductAudit.GetDepartmentIDByWorkNumber(iListCalendar[i].WorkWorkNumber));
    //        dr["异常"] = iListCalendar[i].ExceptionsOfForget + " " + iListCalendar[i].ExceptionsOfLateArrival;
    //        dr["已审核状态"] = "";
    //        dr["以前审核状态"] = "";
    //        dr["总时长"] = iListCalendar[i].TheTotalLength;
    //        dr["有效时长"] = iListCalendar[i].TheEffectiveTime;
    //        dr["考勤日期"] = iListCalendar[i].ShowDate;
    //        dts.Rows.Add(dr);
    //    }
    //    Global.DataTable2Excel(dts, "其他异常" + DateTime.Now.ToString("yyyyMMdd") + "");
    //}
    /// <summary>
    /// 时间戳转为C#格式时间
    /// </summary>
    /// <param name=”timeStamp”></param>
    /// <returns></returns>
    public static DateTime GetTimeByTimeStamp(int timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp.ToString() + "0000000");
        TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
    }

    /// <summary>
    /// DateTime时间格式转换为Unix时间戳格式(精确到秒级）
    /// </summary>
    /// <param name=”time”></param>
    /// <returns></returns>
    public static int ConvertDateTimeInt(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (int)(time - startTime).TotalSeconds;
    }
    /// <summary>
    /// DateTime时间格式转换为Unix时间戳格式(精确微秒级）
    /// </summary>
    /// <param name=”time”></param>
    /// <returns></returns>
    public static long ConvertDateTimeMiliionSeonds(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (long)(time - startTime).TotalMilliseconds;
    }
    /// <summary>
    /// IP筛选
    /// </summary>
    /// <param name="regeditIP"></param>
    /// <param name="endIP"></param>
    /// <returns></returns>
    public static string GetIP(string regeditIP, string endIP)
    {
        if (!String.IsNullOrEmpty(endIP))
        {
            return endIP;
        }
        return regeditIP;
    }
    /// <summary>
    /// 获取今天是一年中第几天
    /// </summary>
    /// <param name="day"></param>
    /// <returns></returns>
    public static int WeekOfYear(System.DateTime day)
    {
        int weeknum;
        System.DateTime fDt = DateTime.Parse(day.Year.ToString() + "-01-01");
        int k = Convert.ToInt32(fDt.DayOfWeek);
        //得到该年的第一天是周几         
        if (k == 0)
        {
            k = 7;
        }
        int l = Convert.ToInt32(day.DayOfYear);
        //得到当天是该年的第几天         
        l = l - (7 - k + 1);
        if (l <= 0)
        {
            weeknum = 1;
        }
        else
        {
            if (l % 7 == 0)
            {
                weeknum = l / 7 + 1;
            }
            else
            {
                weeknum = l / 7 + 2;
                //不能整除的时候要加上前面的一周和后面的一周            
            }
        } return weeknum - 1;
    }
    /// <summary>
    /// 读取excel里面的工作表集合
    /// </summary>
    /// <param name="excelFile"></param>
    /// <returns></returns>
    public static List<string> GetExcelSheetNames(string excelFile)
    {
        OleDbConnection objConn = null;
        System.Data.DataTable dt = null;

        try
        {
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + excelFile + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'"; //此連接可以操作.xls與.xlsx文件
            objConn = new OleDbConnection(strConn);
            objConn.Open();
            dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dt == null)
            {
                return null;
            }
            List<string> excelSheets = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                excelSheets.Add(row["TABLE_NAME"].ToString());
            }

            return excelSheets;
        }
        catch
        {
            return null;
        }
        finally
        {
            if (objConn != null)
            {
                objConn.Close();
                objConn.Dispose();
            }
            if (dt != null)
            {
                dt.Dispose();
            }
        }
    }

    /// <summary>
    /// 根据工作表名称和路径读取excel数据
    /// </summary>
    /// <param name="FileFullPath"></param>
    /// <param name="SheetName"></param>
    /// <returns></returns>
    public static DataTable GetExcelToDataTableBySheet(string FileFullPath, string SheetName)
    {
        string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + FileFullPath + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'"; //此連接可以操作.xls與.xlsx文件
        OleDbConnection conn = new OleDbConnection(strConn);
        conn.Open();
        DataSet ds = new DataSet();
        OleDbDataAdapter odda = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]", SheetName), conn);                    //("select * from [Sheet1$]", conn);
        odda.Fill(ds, SheetName);
        conn.Close();
        return ds.Tables[0];
    }

    /// <summary>
    /// 获取周一日期
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="weekday"></param>
    /// <param name="Number"></param>
    /// <returns></returns>
    public static DateTime getWeekUpOfDate(DateTime dt, DayOfWeek weekday, int Number)
    {
        int wd1 = (int)weekday;
        int wd2 = (int)dt.DayOfWeek;
        return wd2 == wd1 ? dt.AddDays(7 * Number) : dt.AddDays(7 * Number - wd2 + wd1);
    }

    public static LogMessageInfo AddLog(long ReportDataID,short OperationType)
    {
        LogMessageInfo log = new LogMessageInfo();
        log.UserID = Convert.ToInt64(CookieHelper.GetValue("UsersID"));
        log.ReportDataID = ReportDataID;
        log.OperationType = OperationType;
       return Global.ServiceClient.AddLogMessage(log);
    }
}
