using System;
using System.Windows;
using Business.Models;
using Util;

namespace YkSchoolWpf
{
    public class UserInfo
    {

        public static User User
        {
            get => (User) Application.Current.Properties["User"];
            set => Application.Current.Properties["User"] = value;
        }

        public static string Account => User.Account.ToStr();

        public static string RealName => User.RealName.ToStr();

        public static string Password => User.Password.ToStr();

        public static DateTime LastLoginDate => User.LastLoginDate.ToDate();

        public static DateTime CreationTime => User.CreationTime.ToDate();




    }
}