using System; 
using System.Windows;
using System.Windows.Controls;

namespace YkSchoolWpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //显示登陆界面，验证后返回。
            var  loginWindow = new Login();
            loginWindow.ShowDialog();
            if (loginWindow.DialogResult != Convert.ToBoolean(1))
            {
                Close();
            }
        }


        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!(sender is TreeView tvMenu)) return;
            if (!(tvMenu.SelectedItem is TreeViewItem tvItem)) return;
            var uid = tvItem.Uid;
            if (!string.IsNullOrWhiteSpace(uid))
            {
                MainFrame.Navigate(new Uri(uid, UriKind.Relative));
            }
        }
    }
}
