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

namespace LabelDbWebService
{
    /// <summary>
    /// 一个操作数据库的类，所有对SQLServer的操作都写在这个类中，使用的时候实例化一个然后直接调用就可以
    /// </summary>
    public class DBOperation : IDisposable
    {
        public static SqlConnection sqlCon;  //用于连接数据库

        //将下面的引号之间的内容换成上面记录下的属性中的连接字符串
        // private String ConServerStr = @"Data Source=HCOU\SQLEXPRESS;Initial Catalog=LablePrinter;Integrated Security=True";
        //private String ConServerStr = @"data source=127.0.0.1;uid=pswz;pwd=pswz@163.com;database=LablePrinter";

        private String ConServerStr = @"server=.;uid=jackbest;pwd=jackbest@163.com;database=LablePrinter";

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
        /// 查询标签信息
        /// </summary>
        /// <returns>标签信息</returns>
        public List<string> selectLabelList(string labelFlag)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select top 20 id,lablename,lablecode,isprint from tab_lable where 1=1";

                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中
                    list.Add(reader[0].ToString());//id
                    list.Add(reader[1].ToString());//name
                    list.Add(reader[2].ToString());//code
                    list.Add(reader[3].ToString());//isprint
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
        public List<string> doLogin(string username, string password)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "select username,pwd from tab_user where username='" + username + "'";
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
        /// 获取标签的详细信息
        /// </summary>
        /// <returns>标签的详细信息</returns>
        public List<string> selectLabelDetailed(int labelId)
        {
            List<string> list = new List<string>();

            try
            {
                string sql = "select lablename,lablelevel,isnull(lablearea,'  '),lablespeed,lablerote,labletime,lablecode from tab_lable where id=" + labelId;

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