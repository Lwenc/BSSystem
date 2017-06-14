using HASystem.StaticClass;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HASystem.Panels
{
    /// <summary>
    /// DataManagerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DataManagerPanel : UserControl
    {
        public static ObservableCollection<TestResult> list;
        public static SQLiteConnection conn = new SQLiteConnection("Data Source=DB\\BS.db");

        public DataManagerPanel()
        {
            InitializeComponent();
            InitCombo();
        }
        //初始化下拉框
        private void InitCombo()
        {
            string[] type = new string[] { "O1", "OB" };
            comboType.ItemsSource = type;
            comboModel.ItemsSource = (from l in GetTestInfo()
                                      select l.model).Distinct();
            //string[] s = new string[] { "112345698745", "456575698745" };
            //comboBarCode.ItemsSource = s;
            //comboBarCode.SelectedIndex = 0;
        }
        //获得全部测试数据
        private ObservableCollection<TestResult> GetTestInfo()
        {
            list = new ObservableCollection<TestResult>();
            list.Clear();
            GC.Collect();
            try
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select * from TestInfo";
                SQLiteDataReader reader = cmd.ExecuteReader();
                TestResult result = default(TestResult);
                foreach (var item in reader)
                {
                    result.model = reader["model"].ToString();
                    result.barcod = reader["barcod"].ToString();
                    result.from_user1 = reader["from_user1"].ToString();
                    result.testtype_1 = reader["testtype_1"].ToString();
                    result.passageway_1 = reader["passageway_1"].ToString();
                    result.time_1 = reader["time_1"].ToString();
                    result.volt_1 = reader["volt_1"].ToString();
                    result.resistance_1 = reader["resistance_1"].ToString();
                    result.ispass_1 = reader["ispass_1"].ToString();
                    result.remark_1 = reader["remark_1"].ToString();
                    result.from_user2 = reader["from_user2"].ToString();
                    result.testtype_2 = reader["testtype_2"].ToString();
                    result.passageway_2 = reader["passageway_2"].ToString();
                    result.time_2 = reader["time_2"].ToString();
                    result.volt_2 = reader["volt_2"].ToString();
                    result.resistance_2 = reader["resistance_2"].ToString();
                    result.k_value_2 = reader["k_value_2"].ToString();
                    result.ispass_2 = reader["ispass_2"].ToString();
                    result.remark_2 = reader["remark_2"].ToString();
                    list.Add(result);
                }
                conn.Close();
                return list;
            }
            catch (Exception ex)
            {
                conn.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }
        //存储测试数据结构
        public struct TestResult
        {
            public string barcod { get; set; }
            public string model { get; set; }
            public string from_user1 { get; set; }
            public string testtype_1 { get; set; }
            public string passageway_1 { get; set; }
            public string time_1 { get; set; }
            public string volt_1 { get; set; }
            public string resistance_1 { get; set; }
            public string ispass_1 { get; set; }
            public string remark_1 { get; set; }
            public string from_user2 { get; set; }
            public string testtype_2 { get; set; }
            public string passageway_2 { get; set; }
            public string time_2 { get; set; }
            public string volt_2 { get; set; }
            public string resistance_2 { get; set; }
            public string k_value_2 { get; set; }
            public string ispass_2 { get; set; }
            public string remark_2 { get; set; }
        }
        //测试数据
        private void btnTestInfo_Click(object sender, RoutedEventArgs e)
        => dgTestInfo.ItemsSource = GetTestInfo();

        //操作日志
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            dgTestInfo.Visibility = Visibility.Collapsed;
            dgLogInfo.Visibility = Visibility.Visible;
        }
        //查找按钮
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (cbBarcode.IsChecked == true)
            {
                if (txtSearch.Text != "")
                    BarCodeSearch();
                else
                    MessageBox.Show("条码不能为空！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if(cbMultiBarcode.IsChecked==true)
            {

            }
            else
                dpGetData_CalendarClosed();
        }
        //单条码查询
        private void BarCodeSearch()
        {
            string selectedmodel = comboModel.SelectedValue?.ToString();
            if (selectedmodel == null)
            {
                MessageBox.Show("请选择型号进行条码查询！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            list = new ObservableCollection<TestResult>();
            list.Clear();
            GC.Collect();
            try
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select * from TestInfo where barcod like '%{txtSearch.Text}%' and model like '%{selectedmodel}'";
                SQLiteDataReader reader = cmd.ExecuteReader();
                TestResult result = default(TestResult);
                foreach (var item in reader)
                {
                    result.model = reader["model"].ToString();
                    result.barcod = reader["barcod"].ToString();
                    result.from_user1 = reader["from_user1"].ToString();
                    result.testtype_1 = reader["testtype_1"].ToString();
                    result.passageway_1 = reader["passageway_1"].ToString();
                    result.time_1 = reader["time_1"].ToString();
                    result.volt_1 = reader["volt_1"].ToString();
                    result.resistance_1 = reader["resistance_1"].ToString();
                    result.ispass_1 = reader["ispass_1"].ToString();
                    result.remark_1 = reader["remark_1"].ToString();
                    result.from_user2 = reader["from_user2"].ToString();
                    result.testtype_2 = reader["testtype_2"].ToString();
                    result.passageway_2 = reader["passageway_2"].ToString();
                    result.time_2 = reader["time_2"].ToString();
                    result.volt_2 = reader["volt_2"].ToString();
                    result.resistance_2 = reader["resistance_2"].ToString();
                    result.k_value_2 = reader["k_value_2"].ToString();
                    result.ispass_2 = reader["ispass_2"].ToString();
                    result.remark_2 = reader["remark_2"].ToString();
                    list.Add(result);
                }
                if (list.Count == 0)
                    MessageBox.Show("没有查询结果！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                dgTestInfo.ItemsSource = list;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        //日期查询
        private void dpGetData_CalendarClosed()
        {
            DateTime dt;
            if (DateTime.TryParse(dpGetDataStart.Text, out dt) == false)
            {
                MessageBox.Show("请选择开始时间！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (DateTime.TryParse(dpGetDataEnd.Text, out dt) == false)
            {
                MessageBox.Show("请选择开始时间！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string selectedModel = comboModel.SelectedValue?.ToString();
            string selectedType = comboType.SelectedValue?.ToString();
            DateTime dtTime = DateTime.Parse(dpGetDataStart.Text);
            DateTime dtTime2 = DateTime.Parse(dpGetDataEnd.Text);
            if (selectedModel == null)
            {
                MessageBox.Show("请选择型号！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (selectedType == null)
            {
                MessageBox.Show("请选择测试类型！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (dtTime > dtTime2)
            {
                MessageBox.Show("开始时间大于结束时间，请重新选择！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                list = new ObservableCollection<TestResult>();
                list.Clear();
                GC.Collect();
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                if (selectedType == "O1")
                    cmd.CommandText = $"select * from TestInfo where time_1>='{dtTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}' and time_1<'{dtTime2.ToString("yyyy-MM-dd HH:mm:ss.fff")}' and model='{selectedModel}'";
                else
                    cmd.CommandText = $"select * from TestInfo where time_2>='{dtTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}' and time_2<'{dtTime2.ToString("yyyy-MM-dd HH:mm:ss.fff")}' and model='{selectedModel}'";
                SQLiteDataReader reader = cmd.ExecuteReader();
                TestResult result = default(TestResult);
                foreach (var item in reader)
                {
                    result.model = reader["model"].ToString();
                    result.barcod = reader["barcod"].ToString();
                    result.from_user1 = reader["from_user1"].ToString();
                    result.testtype_1 = reader["testtype_1"].ToString();
                    result.passageway_1 = reader["passageway_1"].ToString();
                    result.time_1 = reader["time_1"].ToString();
                    result.volt_1 = reader["volt_1"].ToString();
                    result.resistance_1 = reader["resistance_1"].ToString();
                    result.ispass_1 = reader["ispass_1"].ToString();
                    result.remark_1 = reader["remark_1"].ToString();
                    result.from_user2 = reader["from_user2"].ToString();
                    result.testtype_2 = reader["testtype_2"].ToString();
                    result.passageway_2 = reader["passageway_2"].ToString();
                    result.time_2 = reader["time_2"].ToString();
                    result.volt_2 = reader["volt_2"].ToString();
                    result.resistance_2 = reader["resistance_2"].ToString();
                    result.k_value_2 = reader["k_value_2"].ToString();
                    result.ispass_2 = reader["ispass_2"].ToString();
                    result.remark_2 = reader["remark_2"].ToString();
                    list.Add(result);
                }
                if (list.Count == 0)
                    MessageBox.Show("没有查询结果！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                dgTestInfo.ItemsSource = list;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        //删除按钮
        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteList = dgTestInfo.SelectedItems;
            int num = deleteList.Count;
            string mess = string.Format("是否删除这" + num + "行数据？");
            MessageBoxResult result = MessageBox.Show(mess, "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string[] strmodel = new string[num];
                string[] strbarcod = new string[num];
                for (int i = 0; i < num; i++)
                {
                    try
                    {
                        strmodel[i] = (((TestResult)deleteList[i]).model).ToString();
                        strbarcod[i] = (((TestResult)deleteList[i]).barcod).ToString();
                        conn.Open();
                        SQLiteCommand cmd = conn.CreateCommand();
                        cmd.CommandText = $"delete from TestInfo where model = '{strmodel[i]}' and barcod='{strbarcod[i]}'";
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
                btnTestInfo_Click(sender, e);
                MessageBox.Show("删除成功！");
            }
        }
        //刷新按钮
        private void menuRefresh_Click(object sender, RoutedEventArgs e)
        {
            btnSearch_Click(sender, e);
        }
        //导出按钮
        private void menuTestExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel文件|*.xlsx";
            if (sfd.ShowDialog().Value == true)
                ExcelExportHelper.Export(dgTestInfo, sfd.FileName, "Sheet1");
        }
        //浏览按钮
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TXT文件|*.txt";
            if (ofd.ShowDialog().Value == true)
                MessageBox.Show("导入成功！");
        }
        //加载行号
        private void dgTestInfo_LoadingRow(object sender, DataGridRowEventArgs e)
        => e.Row.Header = e.Row.GetIndex() + 1;

        //按回车键触发查询事件
        private void txtSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        => btnSearch_Click(sender, e);
        //是否启用条码框
        private void cbBarcode_Click(object sender, RoutedEventArgs e)
        {
            if (cbBarcode.IsChecked == true)
            {
                txtSearch.IsEnabled = true;
                comboType.IsEnabled = false;
                comboBarCode.IsEnabled = false;
                btnView.IsEnabled = false;
                dpGetDataStart.IsEnabled = false;
                dpGetDataEnd.IsEnabled = false;
                cbMultiBarcode.IsChecked = false;
            }
            else
            {
                txtSearch.IsEnabled = false;
                comboType.IsEnabled = true;
                dpGetDataStart.IsEnabled = true;
                dpGetDataEnd.IsEnabled = true;
            }
        }
        //多条码查询
        private void cbMultiBarcode_Click(object sender, RoutedEventArgs e)
        {
            if (cbMultiBarcode.IsChecked == true)
            {
                txtSearch.IsEnabled = false;
                comboType.IsEnabled = false;
                comboBarCode.IsEnabled = true;
                cbBarcode.IsEnabled = true;
                btnView.IsEnabled = true;
                dpGetDataStart.IsEnabled = false;
                dpGetDataEnd.IsEnabled = false;
                cbBarcode.IsChecked = false;
            }
            else
            {
                btnView.IsEnabled = false;
                comboType.IsEnabled = true;
                comboBarCode.IsEnabled = false;
                dpGetDataStart.IsEnabled = true;
                dpGetDataEnd.IsEnabled = true;
            }
        }
    }
}
