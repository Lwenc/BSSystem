using HASystem.StaticClass;
using System.Windows;
using System.Windows.Input;

namespace HASystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        //public static string strRole="";
        //public static string strUserId = "";
        public LoginWindow()
        {
            InitializeComponent();
            SetComboBox();
        }
        //初始化comboBox
        private void SetComboBox()
        {
            string[] str = new string[] { "普通用户", "管理员" };
            comboUserType.ItemsSource = str;
            comboUserType.SelectedIndex = 0;
        }
       
        //登陆按钮
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string strRoleId;
            if (comboUserType.Text.Equals("管理员"))
            {
                strRoleId = "001";
            }
            else
            {
                strRoleId = "002";
            }
            if (txtUserId.Text.Trim() == "" || txtUserId.Text.Trim() == null)
            {
                MessageBox.Show("请输入用户名！");
                return;
            }
            else if (txtPwassWord.Password == "" || txtPwassWord.Password == null)
            {
                MessageBox.Show("请输入密码！");
                return;
            }
            else if (UserInfo.FindUserInfo(txtUserId.Text.Trim()) == false)
            {
                MessageBox.Show("账号不存在！");
                return;
            }
            else if (UserInfo.FindUserPassword(txtUserId.Text.Trim(), txtPwassWord.Password) == false)
            {
                MessageBox.Show("密码错误！");
                return;
            }
            else if (UserInfo.FindUserPower(txtUserId.Text.Trim(), strRoleId) == false)
            {
                MessageBox.Show("角色选择错误！");
                return;
            }
            else
            {
                this.DialogResult = true;
                UserInfo.strRole = comboUserType.Text;
                UserInfo.strUserId = txtUserId.Text;
            }
        }
        //用户文本框KeyDown
        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
                txtPwassWord.Focus();
        }
        //密码文本框KeyDown
        private void txtPwassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
                btnLogin_Click(sender, e);
        }
        //用户类型KeyDown
        private void comboUserType_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
                btnLogin_Click(sender, e);
        }
    }
}
