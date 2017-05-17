using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using ui = HASystem.StaticClass.UserInfo;
using System;

namespace HASystem.Panels
{
    /// <summary>
    /// UserManagerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class UserManagerPanel : UserControl
    {
        private ObservableCollection<ui.UserResult> list;
        SqlConnection conn = new SqlConnection($"Server=localhost;Database=HADB;User id=sa;PWD=Lwenc");

        public UserManagerPanel()
        {
            InitializeComponent();
            GetUser();
        }
        //查看用户信息
        private void btnModelInfo_Click(object sender, RoutedEventArgs e)
        {
            btnUserlInfo.IsEnabled = false;
            btnUsereAdd.IsEnabled = true;
            btnPassWord.IsEnabled = true;
            gridData.Visibility = Visibility.Visible;
            panelAddUser.Visibility = Visibility.Collapsed;
            panelAlterPwd.Visibility = Visibility.Collapsed;
            panelAlterInfo.Visibility = Visibility.Collapsed;
        }
        //用户信息添加
        private void btnModleAdd_Click(object sender, RoutedEventArgs e)
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
            gridData.Visibility = Visibility.Collapsed;
            panelAlterInfo.Visibility = Visibility.Visible;
        }
        //获取用户信息
        private void GetUser()
        {
            list = ui.GetUserData();
            dgUserInfo.ItemsSource = list;
        }
        //刷新按钮
        private void menuRefresh_Click(object sender, RoutedEventArgs e)
        {
            GetUser();
        }
        //删除按钮
        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteList = dgUserInfo.SelectedItems;
            int num = deleteList.Count;
            string mess = string.Format("是否删除这" + num + "行数据？");
            MessageBoxResult result = MessageBox.Show(mess, "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string[] userlId = new string[num];
                //string[] modelbrand = new string[num];
                for (int i = 0; i < num; i++)
                {
                    try
                    {
                        userlId[i] = (((ui.UserResult)deleteList[i]).user).ToString();
                        //modelbrand[i] = (((ui.ModelResult)deleteList[i]).brand).ToString();
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandText = $"use HADB exec proc_delUserInfo '{userlId[i]}'";
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }
                    catch (Exception ex)
                    {
                        conn.Close();
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        return;
                    }
                }
                GetUser();
                MessageBox.Show("删除成功！");
            }
        }
    }
}
