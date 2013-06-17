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
        private String ConServerStr = @"Data Source=HCOU\SQLEXPRESS;Initial Catalog=DB_PM;Integrated Security=True";
        //private String ConServerStr = @"data source=127.0.0.1;uid=pswz;pwd=pswz@163.com;database=DB_PM";

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
                string sql = "select t.*, bfb as counts from Tab_Mission t where id in (select mission_id from tab_attent where userid_id=" + id + ")";
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
                    list.Add(reader[27].ToString());
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
                string sql = "select t.*,bfb as counts from Tab_Mission t where createUserID = " + createUserId;
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
                    list.Add(reader[27].ToString());
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
        public List<string> selectMyMissionDetailedInfo(int missionid, int createUserId)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select t.*,ss=case status when 0 then '起草' when 1 then '进行中' when 2 then '待审核' when 3 then '已完成' end,child=(select count(*) from Tab_Mission where parentid=t.id),tbzq=case zq when 1 then '每天' when 2 then '每周' when 3 then '每月' end,mjl=case mj when 1 then '一级' when 2 then '二级' else '三级' end,id,isnull((select top 1 id from Tab_Mission where id>t.id order by id),0) next_id,isnull((select top 1 id from Tab_Mission where id<t.id order by id desc),0) for_id from Tab_Mission t where t.id=" + missionid + " and createUserID=" + createUserId;
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
                    list.Add(reader[30].ToString());
                    list.Add(reader[27].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[29].ToString());
                    list.Add(reader[17].ToString());
                    list.Add(reader[28].ToString());
                    list.Add(reader[31].ToString());
                    list.Add(reader[32].ToString());
                    list.Add(reader[33].ToString());
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
                string sql = "select t.*,ss=case status when 0 then '起草' when 1 then '进行中' when 2 then '待审核' when 3 then '已完成' end,child=(select count(*) from Tab_Mission where parentid=t.id),tbzq=case zq when 1 then '每天' when 2 then '每周' when 3 then '每月' end,mjl=case mj when 1 then '一级' when 2 then '二级' else '三级' end,id,isnull((select top 1 id from Tab_Mission where id>t.id order by id),0) next_id,isnull((select top 1 id from Tab_Mission where id<t.id order by id desc),0) for_id from Tab_Mission t where t.id=" + missionid + " and id in (select mission_id from tab_attent where userid_id=" + createUserId + ")";

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
                    list.Add(reader[30].ToString());
                    list.Add(reader[27].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[29].ToString());
                    list.Add(reader[17].ToString());
                    list.Add(reader[28].ToString());
                    list.Add(reader[31].ToString());
                    list.Add(reader[32].ToString());
                    list.Add(reader[33].ToString());
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
        public List<string> selectCanSeeMissionDetailedInfo(int missionid, int createUserId)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select t.*,ss=case status when 0 then '起草' when 1 then '进行中' when 2 then '待审核' when 3 then '已完成' end,child=(select count(*) from Tab_Mission where parentid=t.id),tbzq=case zq when 1 then '每天' when 2 then '每周' when 3 then '每月' end,mjl=case mj when 1 then '一级' when 2 then '二级' else '三级' end,id,isnull((select top 1 id from Tab_Mission where id>t.id order by id),0) next_id,isnull((select top 1 id from Tab_Mission where id<t.id order by id desc),0) for_id from Tab_Mission t where t.id=" + missionid + " and createUserID!=" + createUserId;
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
                    list.Add(reader[30].ToString());
                    list.Add(reader[27].ToString());
                    list.Add(reader[10].ToString());
                    list.Add(reader[29].ToString());
                    list.Add(reader[17].ToString());
                    list.Add(reader[28].ToString());
                    list.Add(reader[31].ToString());
                    list.Add(reader[32].ToString());
                    list.Add(reader[33].ToString());
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
        public List<string> selectCanSeeMissionInfo(int createUserId)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select t.*,(select count(*) from tab_attent where userid_id='" + createUserId + "' and mission_id=t.id) counts from Tab_Mission t where createUserID != " + createUserId;

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
                    list.Add(reader[27].ToString());
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
                string sql = "select id,userpwd from Tab_User where userid = '" + userid + "'";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[1].ToString() == password)
                    {
                        list.Add(reader[0].ToString());
                        list.Add("true");
                    }
                    else
                    {
                        list.Add(reader[0].ToString());
                        list.Add("false");
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
        /// 增加一条任务信息
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="bh">任务编号</param>
        public bool insertMissionInfo(int id, string bh)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "insert into Tab_Mission (id,bh) values ('" + id + "'," + bh + ")";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
        public List<string> doFindChartData(int createUserId)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select (select COUNT(*) from Tab_Mission where status=3 and overTime<=endtime and createUserID = " + createUserId + ") as completeCounts,(select COUNT(*) from Tab_Mission where endtime<overTime and status = 3 and createUserID = " + createUserId + ") as overComplete,(select COUNT(*) from Tab_Mission where   status!=3 and createUserID = " + createUserId + ")executingCounts";
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
    }
}