using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Data.SQLite;
using mi = HASystem.StaticClass.ModelInfo;

namespace HASystem.Panels
{
    /// <summary>
    /// ModelManagerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ModelManagerPanel : UserControl
    {
        private ObservableCollection<mi.ModelResult> list;
        private  SQLiteConnection conn = new SQLiteConnection("Data Source=DB\\BS.db");
        public ModelManagerPanel()
        {
            InitializeComponent();
            GetModel();
            panelAlterInfo.RequestBack += (s, e) => Return();
        }

        //查看型号信息
        private void btnModelInfo_Click(object sender, RoutedEventArgs e)
        {
            GetModel();
            btnModelInfo.IsEnabled = false;
            btnModleAdd.IsEnabled = true;
            panelAddModelInfo.Visibility = Visibility.Collapsed;
            gridInfo.Visibility = Visibility.Visible;
            panelAlterInfo.Visibility = Visibility.Collapsed;
        }
        private void Return()
        {
            btnModelInfo.IsEnabled = false;
            btnModleAdd.IsEnabled = true;
            panelAddModelInfo.Visibility = Visibility.Collapsed;
            gridInfo.Visibility = Visibility.Visible;
            panelAlterInfo.Visibility = Visibility.Collapsed;
            GetModel();
        }
        //型号信息添加
        private void btnModleAdd_Click(object sender, RoutedEventArgs e)
        {
            btnModleAdd.IsEnabled = false;
            btnModelInfo.IsEnabled = true;
            panelAddModelInfo.Visibility = Visibility.Visible;
            gridInfo.Visibility = Visibility.Collapsed;
            panelAlterInfo.Visibility = Visibility.Collapsed;
        }
        //型号信息修改按钮
        private void menuAlter_Click(object sender, RoutedEventArgs e)
        {
            gridInfo.Visibility = Visibility.Collapsed;
            panelAlterInfo.Visibility = Visibility.Visible;
            panelAddModelInfo.Visibility = Visibility.Collapsed;
            var alterList = dgModelInfo.SelectedItem;
            panelAlterInfo.AlterModel(alterList);
        }

        //获取型号信息
        private void GetModel()
        {
            if(LoginWindow.strRole=="普通用户")
            {
                btnModleAdd.Visibility = Visibility.Collapsed;
                menuDelete.Visibility = Visibility.Collapsed;
                menuAlter.Visibility = Visibility.Collapsed;
                sepSelect.Visibility = Visibility.Collapsed;
                sepDelete.Visibility = Visibility.Collapsed;
            }
            try
            {
                list = mi.GetModelData();
                dgModelInfo.ItemsSource = list;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }     
        }
        //刷新按钮
        private void menuRefresh_Click(object sender, RoutedEventArgs e)
         =>   GetModel();
        //删除按钮
        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteList = dgModelInfo.SelectedItems;
            int num = deleteList.Count;
            string mess = string.Format("是否删除这" + num+"行数据？");
            MessageBoxResult result = MessageBox.Show(mess,"提示",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if(result==MessageBoxResult.Yes)
            {
                string[] modelId = new string[num];
                for(int i=0;i<num;i++)
                {
                    try
                    {
                        modelId[i] = (((mi.ModelResult)deleteList[i]).model).ToString();
                        conn.Open();
                        SQLiteCommand cmd = conn.CreateCommand();
                        cmd.CommandText = $"delete from ModelInfo where model = '{modelId[i]}'";
                        cmd.ExecuteNonQuery();
                        conn.Close();       
                    }
                    catch(Exception ex)
                    {
                        conn.Close();
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        return;
                    }
                }
                GetModel();
                MessageBox.Show("删除成功！");
            }
        }
        //查找按钮
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if(txtSearch.Text!="")
            {
                list = mi.SearchModelData(txtSearch.Text);
                dgModelInfo.ItemsSource = list;
            }
            else
                MessageBox.Show("查询信息不能为空！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        //回车键出发查询事件
        private void txtSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        =>    btnSearch_Click(sender,e);
    }
}
