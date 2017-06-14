using HASystem.StaticClass;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace HASystem.Panels
{
    /// <summary>
    /// UserManagerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class UserManagerPanel : UserControl
    {   
        public UserManagerPanel()
        {
            InitializeComponent();
            IsRoleReturn();
        }
        //判断用户角色和修改返回按钮判断
        private void IsRoleReturn()
        {
            if(LoginWindow.strRole=="普通用户")
            {
                btnUsereAdd.Visibility = Visibility.Collapsed;
                menuDelete.Visibility = Visibility.Collapsed;
            }
            panelAlterInfo.RequestBack += (s, e) => ShowUserInfo();
        }
        private void ShowUserInfo()
        {
            btnUserInfo_Click(this,new RoutedEventArgs());
        }
        //查看用户信息
        private void btnUserInfo_Click(object sender, RoutedEventArgs e)
        {
            btnUserlInfo.IsEnabled = false;
            btnUsereAdd.IsEnabled = true;
            btnPassWord.IsEnabled = true;
            gridData.Visibility = Visibility.Visible;
            panelAddUser.Visibility = Visibility.Collapsed;
            panelAlterPwd.Visibility = Visibility.Collapsed;
            panelAlterInfo.Visibility = Visibility.Collapsed;

            //进入数据库查询用户信息
            DataSet Ds = UserInfo.getAllUserInfo();
            dgUserInfo.ItemsSource = Ds.Tables[0].DefaultView;
        }
        //用户信息添加
        private void btnUserAdd_Click(object sender, RoutedEventArgs e)
        {
            btnUserlInfo.IsEnabled = true;
            btnUsereAdd.IsEnabled = false;
            btnPassWord.IsEnabled = true;
            gridData.Visibility = Visibility.Collapsed;
            panelAddUser.Visibility = Visibility.Visible;
            panelAlterPwd.Visibility = Visibility.Collapsed;
            panelAlterInfo.Visibility = Visibility.Collapsed;
        }
        //修改密码
        private void btnPassWord_Click(object sender, RoutedEventArgs e)
        {
            btnUserlInfo.IsEnabled = true;
            btnUsereAdd.IsEnabled = true;
            btnPassWord.IsEnabled = false;
            gridData.Visibility = Visibility.Collapsed;
            panelAddUser.Visibility = Visibility.Collapsed;
            panelAlterPwd.Visibility = Visibility.Visible;
            panelAlterInfo.Visibility = Visibility.Collapsed;
        }
        //修改信息
        private void menuAlter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strId;
                string strUserName;
                string strtxtTelephone;
                string strcomBoRole;
                //获取选中行的用户Id
                DataRowView mySelectedElement = (DataRowView)dgUserInfo.SelectedItem;
                strId = mySelectedElement.Row[0].ToString();
                strUserName= mySelectedElement.Row[1].ToString();
                strtxtTelephone= mySelectedElement.Row[2].ToString();
                strcomBoRole= mySelectedElement.Row[3].ToString();
                panelAlterInfo.txtId.Text = strId;
                panelAlterInfo.txtUserName.Text = strUserName;
                panelAlterInfo.txtTelephone.Text = strtxtTelephone;
                if (strcomBoRole.Equals("普通用户") == true)
                    panelAlterInfo.comBoRole.SelectedIndex = 0;
                else
                    panelAlterInfo.comBoRole.SelectedIndex = 1;
                gridData.Visibility = Visibility.Collapsed;
                panelAlterInfo.Visibility = Visibility.Visible;
            }
            catch
            {
                MessageBox.Show("请先点击要修改的用户！");
            }
        }
        //刷新按钮
        private void menuRefresh_Click(object sender, RoutedEventArgs e)
        {
            //进入数据库查询用户信息
            DataSet Ds = UserInfo.getAllUserInfo();
            dgUserInfo.ItemsSource = Ds.Tables[0].DefaultView;
        }
        //删除按钮
        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dr = (DataRowView)dgUserInfo.SelectedItem;
            string strUserId = dr.Row[0].ToString();
            UserInfo.DelUserInfo(strUserId);
            UserInfo.DelURInfo(strUserId);
            MessageBox.Show("用户" + strUserId + "删除成功！");
            menuRefresh_Click(sender, e);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataSet Ds = UserInfo.getUserInfoByUserId(txtSearch.Text.Trim());
            dgUserInfo.ItemsSource = Ds.Tables[0].DefaultView;
        }
    }
}
