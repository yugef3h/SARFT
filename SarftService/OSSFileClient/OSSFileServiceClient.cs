using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSSFileClient
{
    public partial class OSSFileServiceClient : NRails.Service.ServiceClient
    {
        public OSSFileServiceClient(System.String serverAddress, NRails.Service.IRequestSerializer serializer = null, NRails.Net.IProto proto = null)
			: base(serverAddress, serializer, proto)
		{
        }

        public virtual string UploadFile(string terminalKey, string fileTime, double lon, double lat, byte fileType, byte statusType, int length, System.Byte[] fileBuffer, string textLocation, int timeout = 30000)
        {
            return (string)this.Invoke(18446744071580777681, "UploadFile(string,string,double,double,byte,byte,int,System.Byte[],string)", new object[] { terminalKey, fileTime, lon, lat, fileType, statusType, length, fileBuffer, textLocation }, timeout, "UploadFile(string,string,double,double,byte,byte,int,System.Byte[],string)");
        }
        public virtual void UploadFile(string terminalKey, string fileTime, double lon, double lat, byte fileType, byte statusType, int length, System.Byte[] fileBuffer, string textLocation, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744071580777681, "UploadFile(string,string,double,double,byte,byte,int,System.Byte[],string)", new object[] { terminalKey, fileTime, lon, lat, fileType, statusType, length, fileBuffer, textLocation }, "UploadFile(string,string,double,double,byte,byte,int,System.Byte[],string)", callback, tokens);
        }

        public virtual bool AddIMGFileInfo(OSSFileStorageService.IServiceContract.FileItem item, int timeout = 30000)
        {
            return (bool)this.Invoke(18446744072617269505, "AddIMGFileInfo(OSSFileStorageService.IServiceContract.FileItem)", new object[] { item }, timeout, "AddIMGFileInfo(OSSFileStorageService.IServiceContract.FileItem)");
        }
        public virtual void AddIMGFileInfo(OSSFileStorageService.IServiceContract.FileItem item, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072617269505, "AddIMGFileInfo(OSSFileStorageService.IServiceContract.FileItem)", new object[] { item }, "AddIMGFileInfo(OSSFileStorageService.IServiceContract.FileItem)", callback, tokens);
        }

        public virtual bool AddAudioFileInfo(OSSFileStorageService.IServiceContract.FileItem item, int timeout = 30000)
        {
            return (bool)this.Invoke(473451276, "AddAudioFileInfo(OSSFileStorageService.IServiceContract.FileItem)", new object[] { item }, timeout, "AddAudioFileInfo(OSSFileStorageService.IServiceContract.FileItem)");
        }
        public virtual void AddAudioFileInfo(OSSFileStorageService.IServiceContract.FileItem item, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(473451276, "AddAudioFileInfo(OSSFileStorageService.IServiceContract.FileItem)", new object[] { item }, "AddAudioFileInfo(OSSFileStorageService.IServiceContract.FileItem)", callback, tokens);
        }

        public virtual bool AddVedioFileInfo(OSSFileStorageService.IServiceContract.FileItem item, int timeout = 30000)
        {
            return (bool)this.Invoke(18446744071584305719, "AddVedioFileInfo(OSSFileStorageService.IServiceContract.FileItem)", new object[] { item }, timeout, "AddVedioFileInfo(OSSFileStorageService.IServiceContract.FileItem)");
        }
        public virtual void AddVedioFileInfo(OSSFileStorageService.IServiceContract.FileItem item, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744071584305719, "AddVedioFileInfo(OSSFileStorageService.IServiceContract.FileItem)", new object[] { item }, "AddVedioFileInfo(OSSFileStorageService.IServiceContract.FileItem)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.FileItemInfoRet GDSearchMediaFileInfoThanRevTime(string revTime, int mediaType, int statusType, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.FileItemInfoRet)this.Invoke(18446744073281316870, "GDSearchMediaFileInfoThanRevTime(string,int,int)", new object[] { revTime, mediaType, statusType }, timeout, "GDSearchMediaFileInfoThanRevTime(string,int,int)");
        }
        public virtual void GDSearchMediaFileInfoThanRevTime(string revTime, int mediaType, int statusType, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744073281316870, "GDSearchMediaFileInfoThanRevTime(string,int,int)", new object[] { revTime, mediaType, statusType }, "GDSearchMediaFileInfoThanRevTime(string,int,int)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.PageMediaFiles GDSearchMediaFileListByRange(string startTime, string endTime, int mediaType, int statusType, int pageIndex, int pageSize, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.PageMediaFiles)this.Invoke(1226451420, "GDSearchMediaFileListByRange(string,string,int,int,int,int,byte)", new object[] { startTime, endTime, mediaType, statusType, pageIndex, pageSize, ascOrDes }, timeout, "GDSearchMediaFileListByRange(string,string,int,int,int,int,byte)");
        }
        public virtual void GDSearchMediaFileListByRange(string startTime, string endTime, int mediaType, int statusType, int pageIndex, int pageSize, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1226451420, "GDSearchMediaFileListByRange(string,string,int,int,int,int,byte)", new object[] { startTime, endTime, mediaType, statusType, pageIndex, pageSize, ascOrDes }, "GDSearchMediaFileListByRange(string,string,int,int,int,int,byte)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.FileItemRet[] SearchMediaFiles(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.FileItemRet[])this.Invoke(18446744072032560498, "SearchMediaFiles(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, timeout, "SearchMediaFiles(string,string,string,int,byte)");
        }
        public virtual void SearchMediaFiles(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072032560498, "SearchMediaFiles(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, "SearchMediaFiles(string,string,string,int,byte)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.FileItemRet SearchLastMediaFile(string terminalKey, int mediaType, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.FileItemRet)this.Invoke(18446744072685735678, "SearchLastMediaFile(string,int)", new object[] { terminalKey, mediaType }, timeout, "SearchLastMediaFile(string,int)");
        }
        public virtual void SearchLastMediaFile(string terminalKey, int mediaType, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072685735678, "SearchLastMediaFile(string,int)", new object[] { terminalKey, mediaType }, "SearchLastMediaFile(string,int)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.FileItemRet[] SearchMediaFilesByTimes(string terminalKey, System.String[] times, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.FileItemRet[])this.Invoke(922226725, "SearchMediaFilesByTimes(string,System.String[],int,byte)", new object[] { terminalKey, times, mediaType, ascOrDes }, timeout, "SearchMediaFilesByTimes(string,System.String[],int,byte)");
        }
        public virtual void SearchMediaFilesByTimes(string terminalKey, System.String[] times, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(922226725, "SearchMediaFilesByTimes(string,System.String[],int,byte)", new object[] { terminalKey, times, mediaType, ascOrDes }, "SearchMediaFilesByTimes(string,System.String[],int,byte)", callback, tokens);
        }

        public virtual string SearchMediaLastTime(string terminalKey, int mediaType, int timeout = 30000)
        {
            return (string)this.Invoke(512746269, "SearchMediaLastTime(string,int)", new object[] { terminalKey, mediaType }, timeout, "SearchMediaLastTime(string,int)");
        }
        public virtual void SearchMediaLastTime(string terminalKey, int mediaType, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(512746269, "SearchMediaLastTime(string,int)", new object[] { terminalKey, mediaType }, "SearchMediaLastTime(string,int)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.MediaFileTime SearchMediaFileLastTime(string terminalKey, int mediaType, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.MediaFileTime)this.Invoke(727972783, "SearchMediaFileLastTime(string,int)", new object[] { terminalKey, mediaType }, timeout, "SearchMediaFileLastTime(string,int)");
        }
        public virtual void SearchMediaFileLastTime(string terminalKey, int mediaType, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(727972783, "SearchMediaFileLastTime(string,int)", new object[] { terminalKey, mediaType }, "SearchMediaFileLastTime(string,int)", callback, tokens);
        }

        public virtual System.String[] SearchMediaTimes(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (System.String[])this.Invoke(1238736252, "SearchMediaTimes(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, timeout, "SearchMediaTimes(string,string,string,int,byte)");
        }
        public virtual void SearchMediaTimes(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1238736252, "SearchMediaTimes(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, "SearchMediaTimes(string,string,string,int,byte)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.MediaFileTime[] SearchMediaTimeItems(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.MediaFileTime[])this.Invoke(18446744072648315849, "SearchMediaTimeItems(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, timeout, "SearchMediaTimeItems(string,string,string,int,byte)");
        }
        public virtual void SearchMediaTimeItems(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072648315849, "SearchMediaTimeItems(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, "SearchMediaTimeItems(string,string,string,int,byte)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.FileItemInfoRet SearchLastMediaFileInfo(string terminalKey, int mediaType, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.FileItemInfoRet)this.Invoke(318398053, "SearchLastMediaFileInfo(string,int)", new object[] { terminalKey, mediaType }, timeout, "SearchLastMediaFileInfo(string,int)");
        }
        public virtual void SearchLastMediaFileInfo(string terminalKey, int mediaType, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(318398053, "SearchLastMediaFileInfo(string,int)", new object[] { terminalKey, mediaType }, "SearchLastMediaFileInfo(string,int)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.FileItemInfoRet[] SearchMediaFileInfosByTimes(string terminalKey, System.String[] times, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.FileItemInfoRet[])this.Invoke(18446744072730422175, "SearchMediaFileInfosByTimes(string,System.String[],int,byte)", new object[] { terminalKey, times, mediaType, ascOrDes }, timeout, "SearchMediaFileInfosByTimes(string,System.String[],int,byte)");
        }
        public virtual void SearchMediaFileInfosByTimes(string terminalKey, System.String[] times, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072730422175, "SearchMediaFileInfosByTimes(string,System.String[],int,byte)", new object[] { terminalKey, times, mediaType, ascOrDes }, "SearchMediaFileInfosByTimes(string,System.String[],int,byte)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.FileItemInfoRet[] SearchMediaFileInfos(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.FileItemInfoRet[])this.Invoke(1341392733, "SearchMediaFileInfos(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, timeout, "SearchMediaFileInfos(string,string,string,int,byte)");
        }
        public virtual void SearchMediaFileInfos(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1341392733, "SearchMediaFileInfos(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, "SearchMediaFileInfos(string,string,string,int,byte)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.FileItemRet[] SearchMediaFilesFromReceiveTime(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.FileItemRet[])this.Invoke(293843246, "SearchMediaFilesFromReceiveTime(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, timeout, "SearchMediaFilesFromReceiveTime(string,string,string,int,byte)");
        }
        public virtual void SearchMediaFilesFromReceiveTime(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(293843246, "SearchMediaFilesFromReceiveTime(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, "SearchMediaFilesFromReceiveTime(string,string,string,int,byte)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.DeleteFileItemRet[] DeleteMediaFileByTimes(string terminalKey, System.String[] times, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.DeleteFileItemRet[])this.Invoke(1864730689, "DeleteMediaFileByTimes(string,System.String[],int,byte)", new object[] { terminalKey, times, mediaType, ascOrDes }, timeout, "DeleteMediaFileByTimes(string,System.String[],int,byte)");
        }
        public virtual void DeleteMediaFileByTimes(string terminalKey, System.String[] times, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1864730689, "DeleteMediaFileByTimes(string,System.String[],int,byte)", new object[] { terminalKey, times, mediaType, ascOrDes }, "DeleteMediaFileByTimes(string,System.String[],int,byte)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.DeleteFileItemRet[] DeleteMediaFiles(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.DeleteFileItemRet[])this.Invoke(1698055228, "DeleteMediaFiles(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, timeout, "DeleteMediaFiles(string,string,string,int,byte)");
        }
        public virtual void DeleteMediaFiles(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1698055228, "DeleteMediaFiles(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, "DeleteMediaFiles(string,string,string,int,byte)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.DeleteFileRetForever DeleMediaFilesForever(string startTime, string endTime, int mediaType, string password, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.DeleteFileRetForever)this.Invoke(880047183, "DeleMediaFilesForever(string,string,int,string)", new object[] { startTime, endTime, mediaType, password }, timeout, "DeleMediaFilesForever(string,string,int,string)");
        }
        public virtual void DeleMediaFilesForever(string startTime, string endTime, int mediaType, string password, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(880047183, "DeleMediaFilesForever(string,string,int,string)", new object[] { startTime, endTime, mediaType, password }, "DeleMediaFilesForever(string,string,int,string)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.DeleteFileItemRet[] SearchDeletedMediaFiles(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.DeleteFileItemRet[])this.Invoke(1874744220, "SearchDeletedMediaFiles(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, timeout, "SearchDeletedMediaFiles(string,string,string,int,byte)");
        }
        public virtual void SearchDeletedMediaFiles(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1874744220, "SearchDeletedMediaFiles(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, "SearchDeletedMediaFiles(string,string,string,int,byte)", callback, tokens);
        }

        public virtual OSSFileStorageService.IServiceContract.RecoverFileItemRet[] RecoverMediaFiles(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, int timeout = 30000)
        {
            return (OSSFileStorageService.IServiceContract.RecoverFileItemRet[])this.Invoke(18446744072553284069, "RecoverMediaFiles(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, timeout, "RecoverMediaFiles(string,string,string,int,byte)");
        }
        public virtual void RecoverMediaFiles(string terminalKey, string startTime, string endTime, int mediaType, byte ascOrDes, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072553284069, "RecoverMediaFiles(string,string,string,int,byte)", new object[] { terminalKey, startTime, endTime, mediaType, ascOrDes }, "RecoverMediaFiles(string,string,string,int,byte)", callback, tokens);
        }

        public virtual void SyncCommandMessage(string terminalKey, System.DateTime cmdTime, int cmdType, object arg, int timeout = 30000)
        {
            this.Invoke(184808122, "SyncCommandMessage(string,System.DateTime,int,object)", new object[] { terminalKey, cmdTime, cmdType, arg }, timeout, "SyncCommandMessage(string,System.DateTime,int,object)");
        }
        public virtual void SyncCommandMessage(string terminalKey, System.DateTime cmdTime, int cmdType, object arg, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(184808122, "SyncCommandMessage(string,System.DateTime,int,object)", new object[] { terminalKey, cmdTime, cmdType, arg }, "SyncCommandMessage(string,System.DateTime,int,object)", callback, tokens);
        }

        public virtual bool Set(string key, string value, System.DateTime expierTime, int timeout = 30000)
        {
            return (bool)this.Invoke(10623626, "Set(string,string,System.DateTime)", new object[] { key, value, expierTime }, timeout, "Set(string,string,System.DateTime)");
        }
        public virtual void Set(string key, string value, System.DateTime expierTime, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(10623626, "Set(string,string,System.DateTime)", new object[] { key, value, expierTime }, "Set(string,string,System.DateTime)", callback, tokens);
        }

        public virtual bool SetValue(string key, System.Byte[] value, System.DateTime expierTime, int timeout = 30000)
        {
            return (bool)this.Invoke(1558436963, "SetValue(string,System.Byte[],System.DateTime)", new object[] { key, value, expierTime }, timeout, "SetValue(string,System.Byte[],System.DateTime)");
        }
        public virtual void SetValue(string key, System.Byte[] value, System.DateTime expierTime, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1558436963, "SetValue(string,System.Byte[],System.DateTime)", new object[] { key, value, expierTime }, "SetValue(string,System.Byte[],System.DateTime)", callback, tokens);
        }

        public virtual System.Byte[] GetVlueBytes(string key, int timeout = 30000)
        {
            return (System.Byte[])this.Invoke(18446744072848485972, "GetVlueBytes(string)", new object[] { key }, timeout, "GetVlueBytes(string)");
        }
        public virtual void GetVlueBytes(string key, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072848485972, "GetVlueBytes(string)", new object[] { key }, "GetVlueBytes(string)", callback, tokens);
        }

        public virtual string GetValue(string key, int timeout = 30000)
        {
            return (string)this.Invoke(1366530652, "GetValue(string)", new object[] { key }, timeout, "GetValue(string)");
        }
        public virtual void GetValue(string key, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(1366530652, "GetValue(string)", new object[] { key }, "GetValue(string)", callback, tokens);
        }

        public virtual System.String[] GetValues(System.String[] keys, int timeout = 30000)
        {
            return (System.String[])this.Invoke(18446744073672441927, "GetValues(System.String[])", new object[] { keys }, timeout, "GetValues(System.String[])");
        }
        public virtual void GetValues(System.String[] keys, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744073672441927, "GetValues(System.String[])", new object[] { keys }, "GetValues(System.String[])", callback, tokens);
        }

        public virtual string GetShareUrl(string key, int timeout = 30000)
        {
            return (string)this.Invoke(18446744072103499235, "GetShareUrl(string)", new object[] { key }, timeout, "GetShareUrl(string)");
        }
        public virtual void GetShareUrl(string key, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072103499235, "GetShareUrl(string)", new object[] { key }, "GetShareUrl(string)", callback, tokens);
        }

        public virtual System.String[] GetShareUrls(System.String[] keys, int timeout = 30000)
        {
            return (System.String[])this.Invoke(18446744072135578348, "GetShareUrls(System.String[])", new object[] { keys }, timeout, "GetShareUrls(System.String[])");
        }
        public virtual void GetShareUrls(System.String[] keys, NRails.Service.InvokeServiceCallback callback, params object[] tokens)
        {
            this.Invoke(18446744072135578348, "GetShareUrls(System.String[])", new object[] { keys }, "GetShareUrls(System.String[])", callback, tokens);
        }
    }
}
