using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace DbManageWebService
{
    /// <summary>
    /// 一个操作数据库的类，所有对SQLServer的操作都写在这个类中，使用的时候实例化一个然后直接调用就可以
    /// </summary>
    public class DBOperation : IDisposable
    {
        public static SqlConnection sqlCon;  //用于连接数据库

        //将下面的引号之间的内容换成上面记录下的属性中的连接字符串
        // private String ConServerStr = @"Data Source=HCOU\SQLEXPRESS;Initial Catalog=DB_PM;Integrated Security=True";
        // private String ConServerStr = @"data source=127.0.0.1;uid=pswz;pwd=pswz@163.com;database=DB_PM";

        private String ConServerStr = @"server=.;uid=jackbest;pwd=jackbest@163.com;database=DB_PM";

        //默认构造函数
        public DBOperation()
        {
            if (sqlCon == null)
            {
                sqlCon = new SqlConnection();
                sqlCon.ConnectionString = ConServerStr;
                sqlCon.Open();
            }
        }

        //关闭/销毁函数，相当于Close()
        public void Dispose()
        {
            if (sqlCon != null)
            {
                sqlCon.Close();
                sqlCon = null;
            }
        }

        /// <summary>
        /// 获取关注任务的信息
        /// </summary>
        /// <returns>关注任务信息</returns>
        public List<string> selectWatchMissionInfo(int id)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select id,createUserName,Title,beginTime,endtime,status,importance,bfb as counts from Tab_Mission t where id in (select mission_id from tab_attent where userid_id=" + id + ") order by id";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[7].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 获取我的任务的信息
        /// </summary>
        /// <returns>我的任务信息</returns>
        public List<string> selectMyMissionInfo(int createUserId)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select id,createUserName,Title,beginTime,endtime,status,importance,bfb as counts,zrrName,ysrName from Tab_Mission t where ysrName=(select username from tab_user where id=" + createUserId + ") or zrrName=(select username from tab_user where id=" + createUserId + ")  order by id";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());//重要性
                    list.Add(reader[7].ToString());
                    list.Add(reader[8].ToString());//责任人
                    list.Add(reader[9].ToString());//验收人
                }
                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 获取我的任务的详细信息
        /// </summary>
        /// <returns>任务的详细信息</returns>
        public List<string> selectMyMissionDetailedInfo(int missionid, int userid)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select t.*,ss=case status when 1 then '进行中' when 2 then '提交待审' when 3 then '已完成' when 4 then '删除待审' when 5 then '即将超时' when 6 then '超时完成' when 7 then '超时' end,child=(select count(*) from Tab_Mission where parentid=t.id),tbzq=case zq when 1 then '每天' when 2 then '每周' when 3 then '每月' end,mjl=case mj when 1 then '一级' when 2 then '二级' else '三级' end,id,isnull((select top 1 id from Tab_Mission where id>t.id order by id),0) next_id,isnull((select top 1 id from Tab_Mission where id<t.id order by id desc),0) for_id from Tab_Mission t where t.id=" + missionid + " and (t.ysrName=(select username from tab_user where id=" + userid + ") or t.zrrName=(select username from tab_user where id=" + userid + "))";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[26].ToString());
                    list.Add(reader[24].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[25].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[32].ToString());
                    list.Add(reader[29].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[31].ToString());
                    list.Add(reader[17].ToString());
                    list.Add(reader[30].ToString());
                    list.Add(reader[33].ToString());
                    list.Add(reader[34].ToString());
                    list.Add(reader[35].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 获取我的关注的任务详细信息
        /// </summary>
        /// <returns>关注任务的详细信息</returns>
        public List<string> selectWatchMissionDetailedInfo(int missionid, int createUserId)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select t.*,ss=case status when 1 then '进行中' when 2 then '提交待审' when 3 then '已完成' when 4 then '删除待审' when 5 then '即将超时' when 6 then '超时完成' when 7 then '超时' end,child=(select count(*) from Tab_Mission where parentid=t.id),tbzq=case zq when 1 then '每天' when 2 then '每周' when 3 then '每月' end,mjl=case mj when 1 then '一级' when 2 then '二级' else '三级' end,id,isnull((select top 1 id from Tab_Mission where id>t.id order by id),0) next_id,isnull((select top 1 id from Tab_Mission where id<t.id order by id desc),0) for_id from Tab_Mission t where t.id=" + missionid + " and id in (select mission_id from tab_attent where userid_id=" + createUserId + ")";

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[26].ToString());
                    list.Add(reader[24].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[25].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[32].ToString());
                    list.Add(reader[29].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[31].ToString());
                    list.Add(reader[17].ToString());
                    list.Add(reader[30].ToString());
                    list.Add(reader[33].ToString());
                    list.Add(reader[34].ToString());
                    list.Add(reader[35].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 获取我的任务的详细信息
        /// </summary>
        /// <returns>任务的详细信息</returns>
        public List<string> selectCanSeeMissionDetailedInfo(int missionid, int createUserId, string rolename, string department_name)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "";
                if (rolename == "普通员工" || rolename == "主管")
                {
                    sql = "select t.*,ss=case status when 1 then '进行中' when 2 then '提交待审' when 3 then '已完成' when 4 then '删除待审' when 5 then '即将超时' when 6 then '超时完成' when 7 then '超时' end,child=(select count(*) from Tab_Mission where parentid=t.id),tbzq=case zq when 1 then '每天' when 2 then '每周' when 3 then '每月' end,mjl=case mj when 1 then '一级' when 2 then '二级' else '三级' end,id,isnull((select top 1 id from Tab_Mission where id>t.id order by id),0) next_id,isnull((select top 1 id from Tab_Mission where id<t.id order by id desc),0) for_id from Tab_Mission t where t.id=" + missionid + " and t.ysrName!=(select username from tab_user where id=" + createUserId + ") and t.zrrName!=(select username from tab_user where id=" + createUserId + ") and deptName='" + department_name + "'";
                }
                else if (rolename == "部门经理" || rolename == "副总经理" || rolename == "总经理")
                {
                    sql = "select t.*,ss=case status when 1 then '进行中' when 2 then '提交待审' when 3 then '已完成' when 4 then '删除待审' when 5 then '即将超时' when 6 then '超时完成' when 7 then '超时' end,child=(select count(*) from Tab_Mission where parentid=t.id),tbzq=case zq when 1 then '每天' when 2 then '每周' when 3 then '每月' end,mjl=case mj when 1 then '一级' when 2 then '二级' else '三级' end,id,isnull((select top 1 id from Tab_Mission where id>t.id order by id),0) next_id,isnull((select top 1 id from Tab_Mission where id<t.id order by id desc),0) for_id from Tab_Mission t where t.id=" + missionid + " and t.ysrName!=(select username from tab_user where id=" + createUserId + ") and t.zrrName!=(select username from tab_user where id=" + createUserId + ")";
                }

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[26].ToString());
                    list.Add(reader[24].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[25].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[32].ToString());
                    list.Add(reader[29].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[31].ToString());
                    list.Add(reader[17].ToString());
                    list.Add(reader[30].ToString());
                    list.Add(reader[33].ToString());
                    list.Add(reader[34].ToString());
                    list.Add(reader[35].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 获取可见任务的信息
        /// </summary>
        /// <returns>可见任务的信息</returns>
        public List<string> selectCanSeeMissionInfo(int createUserId, string rolename, string department_name)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "";
                if (rolename == "普通员工" || rolename == "主管")
                {
                    sql = "select t.*,(select count(*) from tab_attent where userid_id='" + createUserId + "' and mission_id=t.id) counts from Tab_Mission t where t.ysrName!=(select username from tab_user where id=" + createUserId + ") and t.zrrName!=(select username from tab_user where id=" + createUserId + ") and deptName='" + department_name + "' order by id";
                }
                else if (rolename == "部门经理" || rolename == "副总经理" || rolename == "总经理")
                {
                    sql = "select t.*,(select count(*) from tab_attent where userid_id='" + createUserId + "' and mission_id=t.id) counts from Tab_Mission t where t.ysrName!=(select username from tab_user where id=" + createUserId + ") and t.zrrName!=(select username from tab_user where id=" + createUserId + ") order by id";
                }

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[8].ToString());
                    list.Add(reader[27].ToString());//重要性
                    list.Add(reader[29].ToString());
                }
                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 验证登录的信息
        /// </summary>
        /// <returns>登录结果</returns>
        public List<string> doLogin(string userid, string password)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "select id,userpwd,(select rolename from tab_role where tab_user.roleid=id) as rolename,(select departmentname from tab_department where tab_user.departid=id) as departmentname,username from Tab_User where userid = '" + userid + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[1].ToString() == password)
                    {
                        list.Add(reader[0].ToString());
                        list.Add("true");
                        list.Add(reader[2].ToString());//角色名称
                        list.Add(reader[3].ToString());//部门名称
                        list.Add(reader[4].ToString());//用户名称
                    }
                    else
                    {
                        list.Add(reader[0].ToString());
                        list.Add("false");
                        list.Add(reader[2].ToString());//角色名称
                        list.Add(reader[3].ToString());//部门名称
                        list.Add(reader[4].ToString());//用户名称
                    }
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            if (list.Count <= 0)
            {
                list.Add("非法用户");
                list.Add("false");
                list.Add("非法用户");
                list.Add("非法用户");
                list.Add("非法用户");
            }
            return list;
        }

        /// <summary>
        /// 关注
        /// </summary>
        /// <returns>关注结果</returns>
        public List<string> doAcessOkReq(int userid, int missionid)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "insert into Tab_Attent(userid_id,mission_id) values ('" + userid + "','" + missionid + "')";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);

                cmd.ExecuteNonQuery();
                cmd.Dispose();

                list.Add("true");
            }
            catch (Exception)
            {
                list.Add("false");
            }
            return list;
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <returns>取消结果</returns>
        public List<string> doAcessCancelReq(int userid, int missionid)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "delete from Tab_Attent where userid_id=" + userid + " and mission_id=" + missionid;
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                list.Add("true");
            }
            catch (Exception)
            {
                list.Add("false");
            }
            return list;
        }

        /// <summary>
        /// 获取用户密码
        /// </summary>
        /// <returns>用户密码</returns>
        public List<string> doFindPassWord(string userid)
        {
            List<string> list = new List<string>();
            try
            {
                //更新数据库密码
                Random r = new Random();
                string newpass = r.Next(100000, 999999).ToString();
                string sql = "update tab_user set userpwd='" + newpass + "' where userid='" + userid + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                int rows = cmd.ExecuteNonQuery();

                //得到用户手机号码
                string sql_select = "select phone from tab_user where userid='" + userid + "'";
                SqlCommand cmd_select = new SqlCommand(sql_select, sqlCon);
                SqlDataReader reader = cmd_select.ExecuteReader();
                string phone = "";
                while (reader.Read())
                {
                    phone = reader[0].ToString();
                }

                /*
                //调用存储过程发送短信
                SqlCommand cmd_pro = new SqlCommand("PRO_SendMessage_878", sqlCon);
                cmd_pro.CommandType = CommandType.StoredProcedure;
                cmd_pro.Parameters.AddWithValue("@code", "");
                cmd_pro.Parameters.AddWithValue("@phone", phone);
                cmd_pro.Parameters.AddWithValue("@msg", newpass);
                cmd_pro.ExecuteNonQuery();
                */

                list.Add(rows + "");

                reader.Dispose();
                cmd_select.Dispose();
                cmd.Dispose();
            }
            catch (Exception)
            {
                list.Add("0");
            }

            return list;
        }

        /// <summary>
        /// 删除一条任务信息
        /// </summary>
        /// <param name="id">任务ID</param>
        public List<string> doDeleteReq(int id)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "delete from Tab_Mission where id=" + id;
                string sql2 = "delete from Tab_Mission where ID in (select id from GetChild(" + id + "))";

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand(sql2, sqlCon);
                cmd2.ExecuteNonQuery();

                cmd.Dispose();
                cmd2.Dispose();

                list.Add("true");
            }
            catch (Exception)
            {
                list.Add("false");
            }

            return list;
        }

        /// <summary>
        /// 获取统计信息
        /// </summary>
        /// <returns>获取统计信息</returns>
        public List<string> doFindChartData(int createUserId, string rolename, string department_name)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "";
                if (rolename == "普通员工" || rolename == "主管")
                {
                    sql = "select (select COUNT(*) from Tab_Mission where status in ('3','6') and (ysrName=(select username from tab_user where id=" + createUserId + ") or zrrName=(select username from tab_user where id=" + createUserId + "))) as completeCounts,(select COUNT(*) from Tab_Mission where status = 7 and (ysrName=(select username from tab_user where id=" + createUserId + ") or zrrName=(select username from tab_user where id=" + createUserId + "))) as overTime,(select COUNT(*) from Tab_Mission where status in ('1','2','5') and (ysrName=(select username from tab_user where id=" + createUserId + ") or zrrName=(select username from tab_user where id=" + createUserId + "))) as executingCounts";
                }
                else if (rolename == "部门经理")
                {
                    sql = "select (select COUNT(*) from Tab_Mission where status in ('3','6') and deptName = '" + department_name + "') as completeCounts,(select COUNT(*) from Tab_Mission where status = 7 and deptName = '" + department_name + "') as overTime,(select COUNT(*) from Tab_Mission where status in ('1','2','5') and deptName = '" + department_name + "') as executingCounts";
                }
                else if (rolename == "副总经理" || rolename == "总经理")
                {
                    sql = "select (select COUNT(*) from Tab_Mission where status in ('3','6')) as completeCounts,(select COUNT(*) from Tab_Mission where status = 7) as overTime,(select COUNT(*) from Tab_Mission where status in ('1','2','5')) as executingCounts";
                }

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 获取我的申请车辆信息
        /// </summary>
        /// <returns>我的申请车辆信息</returns>
        public List<string> selectMyCarAppInfo(int userId)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "SELECT id,CarID,(select username from tab_user where tab_user.id = AppUserID) username,CarNum,(case when isnull(Destination,'')='' then ' ' else Destination end) Destination,Status,convert(varchar, AddTime, 102) addtime FROM CarApp where AppUserID=" + userId;

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());//申请号
                    list.Add(reader[1].ToString());//车辆号
                    list.Add(reader[2].ToString());//申请人
                    list.Add(reader[3].ToString());//车牌号
                    list.Add(reader[4].ToString());//目的地
                    list.Add(reader[5].ToString());//状态
                    list.Add(reader[6].ToString());//添加时间
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 删除一条申请信息
        /// </summary>
        /// <param name="app_id">申请ID</param>
        public List<string> doDeleteCarAppReq(int app_id)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "delete from CarApp where id=" + app_id;

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                list.Add("true");
            }
            catch (Exception)
            {
                list.Add("false");
            }

            return list;
        }

        /// <summary>
        /// 获取我的申请的详细信息
        /// </summary>
        /// <returns>申请的详细信息</returns>
        public List<string> selectCarAppDetailedInfo(int app_id)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select convert(varchar, BeginTime, 102) BeginTime,convert(varchar, EndTime, 102) EndTime,(case when isnull(PersonNum,'')='' then ' ' else PersonNum end) PersonNum,(case when isnull(Reason,'')='' then ' ' else Reason end) Reason,(case when isnull(Destination,'')='' then ' ' else Destination end) Destination,(case when isnull(remark,'')='' then ' ' else remark end) remark from CarApp where id=" + app_id;
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 更新申请信息
        /// </summary>
        /// <returns>更新申请信息结果/returns>
        public List<string> doUpdateCarAppReq(int app_id, string begin_time, string end_time, string person_num, string reason, string destination, string remarks)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "update CarApp set BeginTime=(select convert(datetime,'" + begin_time + "')),EndTime=(select convert(datetime,'" + end_time + "')),PersonNum=" + person_num + ",Reason='" + reason + "',Destination='" + destination + "',Remark='" + remarks + "' where id=" + app_id;

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                list.Add("true");
            }
            catch (Exception)
            {
                list.Add("false");
            }

            return list;
        }

        /// <summary>
        /// 查询所有的车辆
        /// </summary>
        /// <returns>所有的车辆</returns>
        public List<string> selectAllCars()
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select Num + '$' + convert(varchar,id) as idAndNum from CarList";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 添加申请信息
        /// </summary>
        /// <returns>添加申请信息结果/returns>
        public List<string> doAddCarAppReq(int user_id, string car_num, int car_id, string begin_time, string end_time, string person_num, string reason, string destination, string remarks)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "insert into CarApp(AppUserID,carnum,carid,BeginTime,EndTime,PersonNum,Reason,Destination,Remark,Status,AddTime) values(" + user_id + ",'" + car_num + "'," + car_id + ",convert(datetime,'" + begin_time + "'),convert(datetime,'" + end_time + "')," + person_num + ",'" + reason + "','" + destination + "','" + remarks + "',1,getdate())";

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                list.Add("true");
            }
            catch (Exception)
            {
                list.Add("false");
            }

            return list;
        }

        /// <summary>
        /// 获取填报的完成情况
        /// </summary>
        /// <returns>填报的完成情况</returns>
        public List<string> selectReportInfo(int userId, int missionId)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "SELECT id,des,addtime from tab_misssion_report where addUserID = " + userId + " and  mid=" + missionId;

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());//ID
                    list.Add(reader[1].ToString());//描述
                    list.Add(reader[2].ToString());//添加时间
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (Exception)
            {
            }
            return list;
        }
        /// <summary>
        /// 删除一条完成情况信息
        /// </summary>
        /// <param name="id">完成情况ID</param>
        public List<string> doDeleteReportReq(int report_id)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "delete from Tab_Misssion_report where id=" + report_id;

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                list.Add("true");
            }
            catch (Exception)
            {
                list.Add("false");
            }

            return list;
        }

        /// <summary>
        /// 添加完成情况
        /// </summary>
        /// <returns>添加完成情况结果/returns>
        public List<string> doAddReportReq(string report_info, string missionId, string userId)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "insert into Tab_Misssion_report(mid,des,addUserID,addUserName,addtime) select '" + missionId + "','" + report_info + "'," + userId + ",(select username from tab_user where id='" + userId + "'),getDate()";

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                list.Add("true");
            }
            catch (Exception)
            {
                list.Add("false");
            }

            return list;
        }

        /// <summary>
        /// 获取子任务的信息
        /// </summary>
        /// <returns>子任务信息</returns>
        public List<string> selectChildMissionInfo(int intent_missionId)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select t.*,bfb as counts from Tab_Mission t where parentid=" + intent_missionId + " order by id";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[8].ToString());
                    list.Add(reader[27].ToString());//重要性
                    list.Add(reader[29].ToString());
                }
                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 获取统计任务信息
        /// </summary>
        /// <returns>获取统计任务信息</returns>
        public List<string> selectChartMissionInfo(int userId, string datachart_index, string rolename, string department_name)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "";
                if (datachart_index == "0")//完成
                {
                    sql = "select t.*,bfb as counts from Tab_Mission t where status in (3,6) ";
                }
                else if (datachart_index == "1")//超时
                {
                    sql = "select t.*,bfb as counts from Tab_Mission t where status = 7 ";
                }
                else if (datachart_index == "2")//进行中
                {
                    sql = "select t.*,bfb as counts from Tab_Mission t where status in (1,2,5) ";
                }

                if (rolename == "普通员工" || rolename == "主管")
                {
                    sql += "and (ysrName=(select username from tab_user where id=" + userId + ") or zrrName=(select username from tab_user where id=" + userId + "))";
                }
                else if (rolename == "部门经理")
                {
                    sql += "and deptName='" + department_name + "'";
                }
                else if (rolename == "副总经理" || rolename == "总经理")
                {
                }

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());
                    list.Add(reader[3].ToString());
                    list.Add(reader[4].ToString());
                    list.Add(reader[5].ToString());
                    list.Add(reader[6].ToString());
                    list.Add(reader[8].ToString());
                    list.Add(reader[27].ToString());//重要性
                    list.Add(reader[29].ToString());
                }
                reader.Close();
                cmd.Dispose();

            }
            catch (Exception)
            {
            }
            return list;
        }

        /// <summary>
        /// 完成某个任务
        /// </summary>
        /// <returns>完成某个任务结果/returns>
        public List<string> doCompleteTaskReq(int missionId, string userId)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "update tab_mission set status='2' where id=" + missionId;
                string instr_sql = "insert into tab_missionjournal(journalName,doTime,AddUser,AddUserId) select '用户提交任务待审'+'" + missionId + "',getDate(),(select username from tab_user where id='" + userId + "'),'" + userId + "'";

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand(instr_sql, sqlCon);
                cmd2.ExecuteNonQuery();

                cmd.Dispose();
                cmd2.Dispose();

                list.Add("true");
            }
            catch (Exception)
            {
                list.Add("false");
            }

            return list;
        }

        /// <summary>
        /// 审核某个任务
        /// </summary>
        /// <returns>审核某个任务结果/returns>
        public List<string> doAuditTaskReq(int missionId, string userId)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "update tab_mission set status=(case when endtime<getDate() then '6' else '3' end),overtime=getDate() where id=" + missionId;
                string instr_sql = "insert into tab_missionjournal(journalName,doTime,AddUser,AddUserId) select '审核任务'+'" + missionId + "',getDate(),(select username from tab_user where id='" + userId + "'),'" + userId + "'";

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand(instr_sql, sqlCon);
                cmd2.ExecuteNonQuery();

                cmd.Dispose();
                cmd2.Dispose();

                list.Add("true");
            }
            catch (Exception)
            {
                list.Add("false");
            }

            return list;
        }
    }

}