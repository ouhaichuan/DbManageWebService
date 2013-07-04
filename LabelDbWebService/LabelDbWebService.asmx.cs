using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace LabelDbWebService
{
    /// <summary>
    /// LabelDbWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class LabelDbWebService : System.Web.Services.WebService
    {
        DBOperation dbOperation = new DBOperation();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(Description = "获取标签信息")]
        public string[] selectLabelList(string labelFlag)
        {
            return dbOperation.selectLabelList(labelFlag).ToArray();
        }
        [WebMethod(Description = "验证登录")]
        public string[] doLogin(string username, string password)
        {
            return dbOperation.doLogin(username, password).ToArray();
        }
        [WebMethod(Description = "获取标签详细信息")]
        public string[] selectLabelDetailed(int labelId)
        {
            return dbOperation.selectLabelDetailed(labelId).ToArray();
        }
    }
}