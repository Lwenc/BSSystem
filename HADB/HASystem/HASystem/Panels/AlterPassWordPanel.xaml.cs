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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HASystem.StaticClass;

namespace HASystem.Panels
{
    /// <summary>
    /// AlterPassWordPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AlterPassWordPanel : UserControl
    {
        public AlterPassWordPanel()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtUserId.Text = UserInfo.strUserId.Trim();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.FindUserPassword(txtUserId.Text, pwdOldPassWord.Password) == false)
            {
                pwdOldPassWord.Background = new SolidColorBrush(Colors.Red);
                MessageBox.Show("输入旧密码错误！");  
                return;
            }
            else
            {
                pwdOldPassWord.Background = new SolidColorBrush(Colors.White);
            }
            if (pwdPassWord.Password.Equals(pwdComfirmPassWord.Password) == false)
            {
                pwdComfirmPassWord.Background = new SolidColorBrush(Colors.Red);
                pwdPassWord.Background = new SolidColorBrush(Colors.Red);
                MessageBox.Show("两次输入的新密码不一致！");
                return;
            }
            else
            {
                pwdComfirmPassWord.Background = new SolidColorBrush(Colors.White);
                pwdPassWord.Background = new SolidColorBrush(Colors.White);
            }

            //修改密码保存到数据库
            UserInfo.AlterPassword(txtUserId.Text.Trim(), pwdPassWord.Password);
            MessageBox.Show("修改成功！");
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            pwdOldPassWord.Password = "";
            pwdComfirmPassWord.Password = "";
            pwdPassWord.Password = "";
            pwdOldPassWord.Background = new SolidColorBrush(Colors.White);
            pwdComfirmPassWord.Background = new SolidColorBrush(Colors.White);
            pwdPassWord.Background = new SolidColorBrush(Colors.White);
        }
    }

}
