using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SarftService.IServiceContract;
using SarftService.ServiceCommon;
using SarftService.ServiceModel;

namespace SarftService.ServiceImpl
{
    public class ErrorServiceImpl:IErrorService
    {
        /// <summary>
        /// 新增系统错误
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        public SysErrorInfo AddError(SysErrorInfo model)
        {
            var db = DBClinetHelp.Client();

            SysError info = new SysError();
            info.GUID = model.GUID;
            info.Message = model.Message;
            info.RequestInfo = model.RequestInfo;
            info.StackTrace = model.StackTrace;
            info.UserID = model.UserID;
            info.UserName = model.UserName;
            info.ExceptionTime = model.ExceptionTime;
            info.UpdateTime = DateTime.Now;
            info.CreateTime = DateTime.Now;
            info.Headers = model.Headers;

            SysError result = db.Save<SysError>(info);
            if (result != null)
            {
                //新增成功
                return change(result);
            }
            else
            {
                //新增失败
                return null;
            }
        }
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">comOEM表ID</param>
        /// <returns></returns>
        public SysErrorInfo GetErrorByGuid(string guid)
        {
            var db = DBClinetHelp.Client();
            var SysError = db.Get<SysError>(s => s.GUID == guid).FirstOrDefault();
            if (SysError != null)
            {
                return change(SysError);
            }
            else
            {
                return null;
            }
        }
        SysErrorInfo change(SysError model)
        {
            SysErrorInfo info = new SysErrorInfo();
            info.GUID = model.GUID;
            info.Message = model.Message;
            info.RequestInfo = model.RequestInfo;
            info.StackTrace = model.StackTrace;
            info.UserID = model.UserID;
            info.UserName = model.UserName;
            info.ExceptionTime = model.ExceptionTime;
            info.Headers = model.Headers;
            return info;
        }

    }
}
