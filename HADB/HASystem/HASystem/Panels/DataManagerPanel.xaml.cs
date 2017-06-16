using HASystem.StaticClass;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HASystem.Panels
{
    /// <summary>
    /// DataManagerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DataManagerPanel : UserControl
    {
        private ObservableCollection<TestResult> list;
        private ObservableCollection<string> txt;
        private SQLiteConnection conn = ModelInfo.conn;

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
        //获得全部测试数据，以便获得型号
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
        => GetTest();
        private void GetTest()
        {
            try
            {
                conn.Open();
                string CommandText = "select * from TestInfo;";
                SQLiteDataAdapter da = new SQLiteDataAdapter(CommandText, conn);
                DataSet Ds = new DataSet();
                da.Fill(Ds);
                dgTestInfo.ItemsSource = Ds.Tables[0].DefaultView;

                if (Ds.Tables[0].Rows.Count == 0)
                    MessageBox.Show("没有查询结果！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.Message);
            }
        }
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
            if (cbMultiBarcode.IsChecked == true)
            {
                if (comboBarCode.Text != "")
                    MultiBarCodeSearch();
                else
                    MessageBox.Show("尚未导入条码！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (cbBarcode.IsChecked == false && cbMultiBarcode.IsChecked == false)
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
                string CommandText = $"select * from TestInfo where barcod like '%{txtSearch.Text}%' and model = '{selectedmodel}'";
                SQLiteDataAdapter da = new SQLiteDataAdapter(CommandText, conn);
                DataSet Ds = new DataSet();
                da.Fill(Ds);
                if (Ds.Tables[0].Rows.Count > 10000)
                {
                    MessageBox.Show("查找结果大于10000条数据，请缩小范围进行查找！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                dgTestInfo.ItemsSource = Ds.Tables[0].DefaultView;

                if (Ds.Tables[0].Rows.Count == 0)
                    MessageBox.Show("没有查询结果！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        //多条码查询
        private void MultiBarCodeSearch()
        {
            string selectedmodel = comboModel.SelectedValue?.ToString();
            string CommandText = "";
            if (selectedmodel == null)
            {
                MessageBox.Show("请选择型号进行条码查询！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            GC.Collect();
            try
            {
                conn.Open();
                SQLiteDataAdapter da;
                DataSet Ds = new DataSet();
                for (int i = 0; i < txt.Count; i++)
                {
                    CommandText = $"select * from TestInfo where barcod like '{txt[i]}' and model = '{selectedmodel}'";
                    da = new SQLiteDataAdapter(CommandText, conn);
                    da.Fill(Ds);
                }
                if (Ds.Tables[0].Rows.Count > 10000)
                {
                    MessageBox.Show("查找结果大于10000条数据，请缩小范围进行查找！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                dgTestInfo.ItemsSource = Ds.Tables[0].DefaultView;

                if (Ds.Tables[0].Rows.Count == 0)
                    MessageBox.Show("没有查询结果！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                //MessageBox.Show("请选择开始时间！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (DateTime.TryParse(dpGetDataEnd.Text, out dt) == false)
            {
                //MessageBox.Show("请选择结束时间！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string selectedModel = comboModel.SelectedValue?.ToString();
            string selectedType = comboType.SelectedValue?.ToString();
            string CommandText = "";
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
                conn.Open();
                if (selectedType == "O1")
                    CommandText = $"select * from TestInfo where time_1>='{dtTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}' and time_1<'{dtTime2.ToString("yyyy-MM-dd HH:mm:ss.fff")}' and model='{selectedModel}'";
                else
                    CommandText = $"select * from TestInfo where time_2>='{dtTime.ToString("yyyy-MM-dd HH:mm:ss.fff")}' and time_2<'{dtTime2.ToString("yyyy-MM-dd HH:mm:ss.fff")}' and model='{selectedModel}'";
                SQLiteDataAdapter da = new SQLiteDataAdapter(CommandText, conn);
                DataSet Ds = new DataSet();
                da.Fill(Ds);
                if (Ds.Tables[0].Rows.Count > 10000)
                {
                    MessageBox.Show("查找结果大于10000条数据，请缩小范围进行查找！","提示",MessageBoxButton.OK,MessageBoxImage.Error);
                    return;
                }
                    
                dgTestInfo.ItemsSource = Ds.Tables[0].DefaultView;

                if (Ds.Tables[0].Rows.Count == 0)
                    MessageBox.Show("没有查询结果！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            int num = dgTestInfo.SelectedItems.Count;
            DataRowView[] drv = new DataRowView[num];
            
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
                        drv[i] = dgTestInfo.SelectedItems[i] as DataRowView;
                        strmodel[i] = drv[i].Row[1].ToString();
                        strbarcod[i] = drv[i].Row[0].ToString();
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
            DataGridExtensions.WriteExcel(dgTestInfo);
        }
        //浏览按钮
        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            txt = new ObservableCollection<string>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TXT文件|*.txt";
            if (ofd.ShowDialog() == true)
            {
                using (StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default))
                {
                    while (sr.Peek() > 0)
                    {
                        string temp = sr.ReadLine();
                        txt.Add(temp);
                    }
                    comboBarCode.ItemsSource = txt;
                    comboBarCode.SelectedIndex = 0;
                }
            }
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
                txtSearch.Visibility = Visibility.Visible;
                comboType.IsEnabled = false;
                dpGetDataStart.IsEnabled = false;
                dpGetDataEnd.IsEnabled = false;
                cbMultiBarcode.IsChecked = false;
                btnView.Visibility = Visibility.Collapsed;
            }
            else
            {
                comboType.IsEnabled = true;
                dpGetDataStart.IsEnabled = true;
                dpGetDataEnd.IsEnabled = true;
            }
        }
        //是否启用多条码查询
        private void cbMultiBarcode_Click(object sender, RoutedEventArgs e)
        {
            if (cbMultiBarcode.IsChecked == true)
            {
                txtSearch.Visibility = Visibility.Collapsed;
                comboType.IsEnabled = false;
                comboBarCode.Visibility = Visibility.Visible;
                btnView.Visibility = Visibility.Visible;
                dpGetDataStart.IsEnabled = false;
                dpGetDataEnd.IsEnabled = false;
                cbBarcode.IsChecked = false;
            }
            else
            {
                txtSearch.Visibility = Visibility.Visible;
                btnView.Visibility = Visibility.Collapsed;
                comboType.IsEnabled = true;
                comboBarCode.Visibility = Visibility.Collapsed;
                dpGetDataStart.IsEnabled = true;
                dpGetDataEnd.IsEnabled = true;
            }
        }
    }
}
