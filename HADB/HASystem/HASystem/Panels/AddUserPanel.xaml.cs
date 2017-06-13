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
    /// AddUserPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AddUserPanel : UserControl
    {
        public AddUserPanel()
        {
            InitializeComponent();
            InitRole();
        }
        private void InitRole()
        {
            string[] role = new string[] {"普通用户","管理员"};
            comBoRole.ItemsSource = role;
            comBoRole.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //判断输入是否为空
            if (txtId.Text == "" || txtId.Text == null)
            {
                MessageBox.Show("用户账号不能为空！");
                return;
            }
            else if (txtUserName.Text == "" || txtUserName.Text == null)
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            else if (pwdPassWord.Password == "" || pwdPassWord.Password == null)
            {
                MessageBox.Show("密码不能为空！");
                return;
            }

            //检查两次密码是否相同
            if (pwdPassWord.Password.Trim().Equals(confirmPassWord.Password.Trim()) == false)
            {
                pwdPassWord.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                confirmPassWord.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                MessageBox.Show("两次输入的密码不相同！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                pwdPassWord.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                confirmPassWord.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            }
            //数据存储
            string strRole_id="";
            try
            {
                bool b = UserInfo.FindUserInfo(txtId.Text.Trim());
                if (b == true)
                {
                    MessageBox.Show("用户已经存在！");
                    return;
                }
                else
                {
                    UserInfo.AddNewUserInfo(txtId.Text.Trim(),pwdPassWord.Password,txtUserName.Text.Trim(),txtTelephone.Text.Trim());
                    if (comBoRole.Text.Trim().Equals("普通用户") == true)
                    {
                        strRole_id = "002";
                    }
                    else if (comBoRole.Text.Trim().Equals("管理员") == true)
                    {
                        strRole_id = "001";
                    }
                    else
                    {
                        MessageBox.Show("判断不出用户角色！");
                    }
                    UserInfo.AddNewURInfo(txtId.Text.Trim(), strRole_id);
                    MessageBox.Show("添加成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtId.Text = "";
            txtUserName.Text = "";
            pwdPassWord.Password = "";
            confirmPassWord.Password = "";
            txtTelephone.Text = "";
        }
    }
}
