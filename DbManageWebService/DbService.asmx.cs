using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace DbManageWebService
{
    /// <summary>
    /// Service1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class DbService : System.Web.Services.WebService
    {
        DBOperation dbOperation = new DBOperation();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod(Description = "验证登录")]
        public string[] doLogin(string userid, string password)
        {
            return dbOperation.doLogin(userid, password).ToArray();
        }

        [WebMethod(Description = "获取用户密码")]
        public string[] doFindPassWord(string userid)
        {
            return dbOperation.doFindPassWord(userid).ToArray();
        }

        [WebMethod(Description = "获取关注任务的信息")]
        public string[] selectWatchMissionInfo(int id)
        {
            return dbOperation.selectWatchMissionInfo(id).ToArray();
        }

        [WebMethod(Description = "获取我的任务的信息")]
        public string[] selectMyMissionInfo(int id)
        {
            return dbOperation.selectMyMissionInfo(id).ToArray();
        }

        [WebMethod(Description = "获取可见任务的信息")]
        public string[] selectCanSeeMissionInfo(int id, string rolename, string department_name)
        {
            return dbOperation.selectCanSeeMissionInfo(id, rolename, department_name).ToArray();
        }

        [WebMethod(Description = "获取统计信息")]
        public string[] doFindChartData(int userid, string rolename, string department_name)
        {
            return dbOperation.doFindChartData(userid, rolename, department_name).ToArray();
        }

        [WebMethod(Description = "获取我的任务的详细信息")]
        public string[] selectMyMissionDetailedInfo(int id, int userid)
        {
            return dbOperation.selectMyMissionDetailedInfo(id, userid).ToArray();
        }

        [WebMethod(Description = "获取我的关注任务的详细信息")]
        public string[] selectWatchMissionDetailedInfo(int id, int userid)
        {
            return dbOperation.selectWatchMissionDetailedInfo(id, userid).ToArray();
        }

        [WebMethod(Description = "获取我的关注任务的详细信息")]
        public string[] selectCanSeeMissionDetailedInfo(int id, int userid, string rolename, string department_name)
        {
            return dbOperation.selectCanSeeMissionDetailedInfo(id, userid, rolename, department_name).ToArray();
        }

        [WebMethod(Description = "增加一条任务信息")]
        public bool insertMissionInfo(int id, string bh)
        {
            return dbOperation.insertMissionInfo(id, bh);
        }

        [WebMethod(Description = "取消关注一条任务信息")]
        public string[] doAcessCancelReq(int userid, int missionid)
        {
            return dbOperation.doAcessCancelReq(userid, missionid).ToArray();
        }

        [WebMethod(Description = "关注一条任务信息")]
        public string[] doAcessOkReq(int userid, int missionid)
        {
            return dbOperation.doAcessOkReq(userid, missionid).ToArray();
        }

        [WebMethod(Description = "删除一条任务及其子任务信息")]
        public string[] doDeleteReq(int missionid)
        {
            return dbOperation.doDeleteReq(missionid).ToArray();
        }
        [WebMethod(Description = "获取我的申请车辆的信息")]
        public string[] selectMyCarAppInfo(int userId)
        {
            return dbOperation.selectMyCarAppInfo(userId).ToArray();
        }
        [WebMethod(Description = "删除一条申请信息")]
        public string[] doDeleteCarAppReq(int app_id)
        {
            return dbOperation.doDeleteCarAppReq(app_id).ToArray();
        }
        [WebMethod(Description = "获取申请详细信息")]
        public string[] selectCarAppDetailedInfo(int app_id)
        {
            return dbOperation.selectCarAppDetailedInfo(app_id).ToArray();
        }
        [WebMethod(Description = "更新申请详细信息")]
        public string[] doUpdateCarAppReq(int app_id, string begin_time, string end_time, string person_num, string reason, string destination, string remarks)
        {
            return dbOperation.doUpdateCarAppReq(app_id, begin_time, end_time, person_num, reason, destination, remarks).ToArray();
        }
        [WebMethod(Description = "查询车辆信息")]
        public string[] selectAllCars()
        {
            return dbOperation.selectAllCars().ToArray();
        }
        [WebMethod(Description = "添加申请信息")]
        public string[] doAddCarAppReq(int user_id, string car_num, int car_id, string begin_time, string end_time, string person_num, string reason, string destination, string remarks)
        {
            return dbOperation.doAddCarAppReq(user_id, car_num, car_id, begin_time, end_time, person_num, reason, destination, remarks).ToArray();
        }
        [WebMethod(Description = "获取完成情况信息")]
        public string[] selectReportInfo(int userId, int missionId)
        {
            return dbOperation.selectReportInfo(userId, missionId).ToArray();
        }
        [WebMethod(Description = "删除完成情况信息")]
        public string[] doDeleteReportReq(int report_id)
        {
            return dbOperation.doDeleteReportReq(report_id).ToArray();
        }
        [WebMethod(Description = "添加完成情况信息")]
        public string[] doAddReportReq(string report_info, string missionId, string userId)
        {
            return dbOperation.doAddReportReq(report_info, missionId, userId).ToArray();
        }
        [WebMethod(Description = "获取子任务信息")]
        public string[] selectChildMissionInfo(int intent_missionId)
        {
            return dbOperation.selectChildMissionInfo(intent_missionId).ToArray();
        }
        [WebMethod(Description = "获取统计任务信息")]
        public string[] selectChartMissionInfo(int userId, string datachart_index, string rolename, string department_name)
        {
            return dbOperation.selectChartMissionInfo(userId, datachart_index, rolename, department_name).ToArray();
        }
        [WebMethod(Description = "完成某个任务")]
        public string[] doCompleteTaskReq(int missionId, string userId)
        {
            return dbOperation.doCompleteTaskReq(missionId, userId).ToArray();
        }
        [WebMethod(Description = "审核某个任务")]
        public string[] doAuditTaskReq(int missionId, string userId)
        {
            return dbOperation.doAuditTaskReq(missionId, userId).ToArray();
        }
    }
}