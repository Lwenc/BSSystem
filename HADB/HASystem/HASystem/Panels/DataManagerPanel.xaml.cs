using HASystem.StaticClass;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using mi = HASystem.StaticClass.ModelInfo;

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
        }
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
        =>
            dgTestInfo.ItemsSource = GetTestInfo();

        //操作日志
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            dgTestInfo.Visibility = Visibility.Collapsed;
            dgLogInfo.Visibility = Visibility.Visible;
        }
        //查找按钮
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                list = new ObservableCollection<TestResult>();
                list.Clear();
                GC.Collect();
                try
                {
                    conn.Open();
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = $"select * from TestInfo where barcod like '%{txtSearch.Text}%' or model like '%{txtSearch.Text}%' "
                       + $"or (testtype_1 = 'O1' and ispass_1 = '{txtSearch.Text}') or testtype_1 = '{txtSearch.Text}' or testtype_2 = '{txtSearch.Text}'";
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
                    dgTestInfo.ItemsSource = list;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    conn.Close();
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
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
                btnTestInfo_Click(sender,e);
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
            {
                ExcelExportHelper.Export(dgTestInfo, sfd.FileName, "Sheet1");
                //MessageBox.Show("数据保存成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }                  
        }
        //加载行号
        private void dgTestInfo_LoadingRow(object sender, DataGridRowEventArgs e)
        =>    e.Row.Header = e.Row.GetIndex() + 1;

        //日期查询
        private void dpGetData_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DateTime dt;
            if (DateTime.TryParse(dpGetData.Text, out dt)==false) 
                return;
            list = new ObservableCollection<TestResult>();
            list.Clear();
            GC.Collect();
            try
            {
                string selectedModel = comboModel.SelectedValue?.ToString();
                string selectedType = comboType.SelectedValue?.ToString();
                if (selectedModel == null)
                {
                    MessageBox.Show("请选择型号进行时间查询！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }                   
                if (selectedType == null)
                {
                    MessageBox.Show("请选择类型进行时间查询！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }     
                DateTime dtTime = DateTime.Parse(dpGetData.Text);
                DateTime dtTime2 = DateTime.Parse(dpGetData.Text).AddDays(1);
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
                dgTestInfo.ItemsSource = list;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        //按回车键触发查询事件
        private void txtSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        =>    btnSearch_Click(sender,e);
        //型号下拉框触发事件
        private void comboModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedModel = comboModel.SelectedValue?.ToString();
            string selectedType = comboType.SelectedValue?.ToString();
            if (selectedModel == null || selectedType == null)
                return;
            dpGetData_CalendarClosed(sender, e);
        }
        //类型下拉框触发事件
        private void comboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        => comboModel_SelectionChanged(sender, e);
    }
}
