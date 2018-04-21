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
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ActionResult UsersList(int pageIndex = 0, int pageSize = 0, string userName = "", long RoleID = 0)
        {
            try
            {
                var user = Global.ServiceClient.GetUserInfoById(Convert.ToInt64(CookieHelper.GetValue("UsersID")));
                if (user == null)
                {
                    return Json(100);
                }
                SarftService.IServiceContract.PageInfo<UsersInfo> GetUsersList = new SarftService.IServiceContract.PageInfo<UsersInfo>();
                GetUsersList = Global.ServiceClient.GetUserInfoList(pageIndex, pageSize, userName, RoleID);
                if (GetUsersList.Data.Count < 1)
                {
                    return Json(100);
                }
                return Json(GetUsersList);
            }
            catch (Exception)
            {

                return Json(100);
            }

        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DelUser(long id)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            resultModel.Result = 1;
            resultModel.Desc = "删除成功";
            int num = Global.ServiceClient.DelUser(id);
            if (num < 1)
            {
                resultModel.Result = 0;
                resultModel.Desc = "删除失败";
            }
            return Global.OutPutJsonResult(resultModel);
        }
        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RefreshUser(long id)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            resultModel.Result = 1;
            resultModel.Desc = "重置成功";
            int num = Global.ServiceClient.AlterUserPass(id);
            if (num < 1)
            {
                resultModel.Result = 0;
                resultModel.Desc = "重置失败";
            }
            return Global.OutPutJsonResult(resultModel);
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UpdateUser(long id = 0)
        {
            if (id > 0)
            {
                ViewBag.Id = id;
                //为编辑
                ViewBag.OpType = "Edit";
                UsersInfo userinfo = Global.ServiceClient.GetUserInfoById(id);
                if (userinfo != null)
                {
                    ViewBag.ID = userinfo.Id;
                    ViewBag.Password = userinfo.Password;
                    ViewBag.UserName = userinfo.UserName;
                    ViewBag.Phone = userinfo.Phone;
                    ViewBag.RoleID = userinfo.RoleID;
                    ViewData["PowerList"] = userinfo.Power;
                }
            }
            else
            {
                //为新增
                ViewBag.OpType = "Add";
            }


            return View();
        }
        public ActionResult AlterUsers(string ConfigJson)
        {
            ResultModel_Common resultModel = new ResultModel_Common();
            UsersInfo userInfo = new UsersInfo();
            if (!string.IsNullOrEmpty(ConfigJson))
            {
                try
                {
                    userInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<UsersInfo>(ConfigJson);
                    //为新增
                    if (userInfo.Id == 0)
                    {
                        if (userInfo != null)
                        {
                            if (Global.ServiceClient.AddUser(userInfo) > 0)
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
                    else
                    {
                        if (Global.ServiceClient.AlterUsers(userInfo) > 0)
                        {
                            resultModel.Result = 1;
                            resultModel.Desc = "修改成功";
                        }
                        else
                        {
                            resultModel.Result = 0;
                            resultModel.Desc = "修改失败";
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
    }
}