using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Business.Dal;

namespace YkSchoolWpf
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            #region 检查文本框

            if (txtUserName.Text.Trim().Length <= 0 || pwdPassword.Password.Trim().Length <= 0)  //为了解决用户名和密码都不填写的时候，不能同时显示两个提示
            {
                if (txtUserName.Text.Length <= 0)
                {
                    //显示Label
                    labUserName.Visibility = Visibility.Visible;
                    //添加背景颜色
                    labUserName.Foreground = Brushes.Red;
                    //验证信息
                    labUserName.Content = "账号不能为空";

                }
                else
                {
                    //内容不为空，就隐藏
                    labUserName.Visibility = Visibility.Hidden;
                }
                if (pwdPassword.Password.Length <= 0)
                {
                    labPassword.Visibility = Visibility.Visible;

                    labPassword.Foreground = Brushes.Red;
                    labPassword.Content = "密码不能为空";

                }
                else
                {
                    labPassword.Visibility = Visibility.Hidden;
                }
                return;
            }

            labUserName.Visibility = Visibility.Hidden;
            labPassword.Visibility = Visibility.Hidden;

            #endregion

            //获取窗体用户名和密码
            var userName = txtUserName.Text;
            var password = pwdPassword.Password;
            var strResult = "";
            var user = UserDal.Login(userName, password, ref strResult);
            if (strResult != "1")
            {
                MessageBox.Show(strResult,"提示");
                return;
            }
            MessageBox.Show("登录成功！");
            DialogResult = true;
            UserInfo.User = user;



        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
