using System;
using System.Data;
using System.Data.OleDb;
using Business.AccessHelper;
using Business.Models;
using Util;

namespace Business.Dal
{
    public class UserDal
    {



        public static User Login(string account,string password,ref string strResult)
        { 
            strResult = "1";
            if (string.IsNullOrEmpty(account))
            {
                strResult = "账号不能为空";
                return null;
            }
            if (string.IsNullOrEmpty(password))
            {
                strResult = "密码不能为空";
                return null;
            } 
            var sql = "select * from [user] where account=?";
            var parameters = new[]
            {
                new OleDbParameter("@account", OleDbType.VarChar, 50) {Value = account}
            };
            var ds = AccessDbUtil.ExecuteQuery(sql, parameters);
            if (ds.Tables[0].Rows.Count==0)
            {
                strResult= "账号不存在！";
                return null;
            }
            var user = RowToEntity(ds.Tables[0].Rows[0]); 
            password = Encrypt.Md5By32(password);
            if (user.Password!=password)
            {
                strResult="密码不正确！";
                return null;
            } 
            return user;
        }


        /// <summary>
        /// 验证是否存在
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool Exists(User user)
        {
            var strSql = "select 1 from user where account='" + user.Account + "'";
            return AccessDbUtil.Exists(strSql);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string Insert(User user)
        {
            var strError = Validator(user);
            if (strError!="") 
                return strError;
            if (!InsertUser(user))
                strError= "新增用户失败,请稍后重试！";
            return strError;
        }




        public static User Get(string where)
        {
            var sql = "select * from User where 1=1";
            if (!string.IsNullOrEmpty(where))
            {
                sql += where;
            }
            var ds = AccessDbUtil.ExecuteQuery(sql);
            return RowToEntity(ds.Tables["ds"].Rows[0]);
        }
         

        #region 私有方法

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static string Validator(User user)
        {
            var strError = "";
            if (string.IsNullOrEmpty(user.Account))
            {
                strError += "账号不能为空！";
            } 

            if (string.IsNullOrEmpty(user.Password))
            {
                strError += "密码不能为空！";
            } 
            return strError;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static bool InsertUser(User user)
        {
            var sql = @"INSERT INTO User
                (Account,RealName,Password,CreationTime)
                VALUES(?,?,?,?)";
            var parameters = new OleDbParameter[4];
            parameters[0] = new OleDbParameter("@Account", OleDbType.VarChar, 50) { Value = user.RealName };
            parameters[1] = new OleDbParameter("@RealName", OleDbType.Integer) { Value = user.RealName };
            parameters[2] = new OleDbParameter("@Password", OleDbType.Integer) { Value = Encrypt.Md5By32(user.Password) };
            parameters[3] = new OleDbParameter("@CreationTime", OleDbType.Date) { Value = DateTime.Now };
            return AccessDbUtil.ExecuteInsert(sql, parameters) == 1;
        } 

        private static User RowToEntity(DataRow row)
        {
            var user=new User
            {
                Account = row["Account"].ToStr(),
                RealName = row["RealName"].ToStr(),
                Password = row["Password"].ToStr(),
                CreationTime = row["CreationTime"].ToDate(),
                LastLoginDate = row["LastLoginDate"].ToDate()
            };
            return user;
        }


        #endregion




    }
}