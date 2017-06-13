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
    /// AlterUserPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AlterUserPanel : UserControl
    {
        public AlterUserPanel()
        {
            InitializeComponent();
            List<string> list = new List<string>() { "普通用户", "管理员" };
            comBoRole.ItemsSource = list;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //判断输入是否为空
            if (txtUserName.Text == "" || txtUserName.Text == null)
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            //
            string strRoleId = "";
            if (comBoRole.Text.Trim().Equals("管理员"))
                strRoleId = "001";
            else
                strRoleId = "002";
            try
            {
                UserInfo.AlterUserInfo(txtId.Text.Trim(), txtUserName.Text.Trim(), txtTelephone.Text.Trim());
                UserInfo.DelURInfo(txtId.Text.Trim());
                UserInfo.AddNewURInfo(txtId.Text.Trim(),strRoleId);
                MessageBox.Show("用户" + txtId.Text.Trim() + "信息修改成功！","提示",MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
