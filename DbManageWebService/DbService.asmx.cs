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
        public string[] selectCanSeeMissionInfo(int id)
        {
            return dbOperation.selectCanSeeMissionInfo(id).ToArray();
        }

        [WebMethod(Description = "获取统计信息")]
        public string[] doFindChartData(int userid)
        {
            return dbOperation.doFindChartData(userid).ToArray();
        }

        [WebMethod(Description = "获取任务的详细信息")]
        public string[] selectDetailedMissionInfo(int id)
        {
            return dbOperation.selectDetailedMissionInfo(id).ToArray();
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
    }
}