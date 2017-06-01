using HASystem.StaticClass;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace HASystem.Panels
{
    /// <summary>
    /// DataManagerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DataManagerPanel : UserControl
    {
       // private bool _isTestInfo = true;
        public static ObservableCollection<TestResult> list;
        public static SQLiteConnection conn = new SQLiteConnection("Data Source=DB\\BS.db");

        public DataManagerPanel()
        {
            InitializeComponent();
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
        {
            //dgTestInfo.Visibility = Visibility.Visible;
            //dgLogInfo.Visibility = Visibility.Collapsed;
            //_isTestInfo = true;

            list = new ObservableCollection<TestResult>();
            try
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"select model,barcod,barcod,testtype_1,passageway_1,time_1,volt_1,resistance_1,ispass_1,remark_1,"
                  + $"from_user2,testtype_2,passageway_2,time_2,volt_2,resistance_2,k_value_2,ispass_2,remark_2 from TestInfo";
                SQLiteDataReader reader = cmd.ExecuteReader();
                TestResult result = default(TestResult);
                foreach (var item in reader)
                {
                    result.model = reader["model"].ToString();
                    result.barcod = reader["barcod"].ToString();
                    //result.from_user1 = reader["from_user1"].ToString();
                    result.testtype_1 = reader["testtype_1"].ToString();
                    result.passageway_1 = reader["passageway_1"].ToString();
                    result.time_1 = reader["time_1"].ToString();
                    result.volt_1 = reader["volt_1"].ToString();
                    result.resistance_1 = reader["resistance_1"].ToString();
                    result.ispass_1 = reader["ispass_1"].ToString();
                    result.remark_1 = reader["remark_1"].ToString();
                   // result.from_user2 = reader["from_user2"].ToString();
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

        //操作日志
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            dgTestInfo.Visibility = Visibility.Collapsed;
            dgLogInfo.Visibility = Visibility.Visible;
            //_isTestInfo = false;
        }
        //查找按钮
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                conn.Open();
                string CommandText = $"select * from TestInfo where barcod like '%{txtSearch.Text}%' or model like '%{txtSearch.Text}%' "
                    +$"or (testtype_1 = 'O1' and ispass_1 = '{txtSearch.Text}') or testtype_1 = '{txtSearch.Text}' or testtype_2 = '{txtSearch.Text}'";
                SQLiteDataAdapter da = new SQLiteDataAdapter(CommandText, conn);
                DataSet Ds = new DataSet();
                da.Fill(Ds);
                dgTestInfo.ItemsSource = Ds.Tables[0].DefaultView;
                conn.Close();

            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(ae.Message + " \n\n" + ae.Source + "\n\n" + ae.StackTrace);
            }
            catch (Exception ex)
            {   
                //Do　any　logging　operation　here　if　necessary  
                throw new Exception(ex.Message);
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
            btnTestInfo_Click(sender, e);
        }

        private void menuTestExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataGridExtensions.WriteExcel(dgTestInfo);
                MessageBox.Show("数据保存成功！","提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //加载行号
        private void dgTestInfo_LoadingRow(object sender, DataGridRowEventArgs e)
        =>    e.Row.Header = e.Row.GetIndex() + 1;

        private void dpGetDataO1_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DateTime dt;
            if (DateTime.TryParse(dpGetDataO1.Text, out dt)==false) 
            {
                MessageBox.Show("输入日期有误！");
                return;
            }
            DataSet Ds = new DataSet();
            Ds = SaveTestData.getTestDataByDate(DateTime.Parse(dpGetDataO1.Text), "O1");
            dgTestInfo.ItemsSource = Ds.Tables[0].DefaultView;
        }

        private void dpGetDataOB_CalendarClosed(object sender, RoutedEventArgs e)
        {
            DateTime dt;
            if (DateTime.TryParse(dpGetDataOB.Text, out dt) == false) 
            {
                MessageBox.Show("输入日期有误！");
                return;
            }
            DataSet Ds = new DataSet();
            Ds = SaveTestData.getTestDataByDate(DateTime.Parse(dpGetDataOB.Text), "OB");
            dgTestInfo.ItemsSource = Ds.Tables[0].DefaultView;
        }
    }
}
