using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.IO.Ports;
using System.Windows.Media;
using System.Windows.Threading;
using System.Text;
using test = HASystem.StaticClass.TestResultInfo;
using ssi = HASystem.StaticClass.StructSerialInfo;
using si = HASystem.StaticClass.SerialInfo;
using mi = HASystem.StaticClass.ModelInfo;
using System.ComponentModel;

namespace HASystem.Panels
{
    /// <summary>
    /// GoodsTestPanel.xaml 的交互逻辑
    /// </summary>
    public partial class GoodsTestPanel : UserControl
    {
        DataGridRow dgr;
        private DispatcherTimer timer;
        private string[] model = new string[] { };
        private int sum = 0;
        private string strReset = "23 AA AA 41 37 41 31 33 46 33 45 0D";
        SerialPort sp = new SerialPort();
        string strModel;
        string strFrom_user1="";
        string strTesttype_1;
        string strPassageway_1="1";
        DateTime dtTime_1;


        public GoodsTestPanel()
        {
            InitializeComponent();
            InitCombo();
        }
        private void InitCombo()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            mi.GetModelData();
            string[] type = new string[] {"O1","OB" };
            string[] CunFang = new string[] { "本地", "网络" };
            comboType.ItemsSource = type;
            cbCunFang.ItemsSource = CunFang;     
            comboType.SelectedIndex = 0;
            comboModel.ItemsSource = (from l in mi.list
                                      select l.model).Distinct();
        }
        //开始按钮
        private void btnStart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (comboModel.SelectedIndex != -1)
            {
                btnStart.Visibility = Visibility.Collapsed;
                btnStop.Visibility = Visibility.Visible;
                btnReset.IsEnabled = false;
                comboType.IsEnabled = false;
                comboModel.IsEnabled = false;
                cbCunFang.IsEnabled = false;
                txtBarcode.IsEnabled = true;
                txtBarcode.Focus();
            }
            else
                MessageBox.Show("请选择型号进行测试！");
        }
        //初始化和打开串口
        private void IniSerial()
        {
            try
            {
                //从Ini文件读取串口信息
                si.GetSerialInfo();
                sp.PortName = ssi.portName;
                sp.BaudRate = int.Parse(ssi.baudRate);
                sp.StopBits = (StopBits)(double.Parse(ssi.stopBite));
                sp.Parity = (Parity)Enum.Parse(typeof(Parity), ssi.parity);
                sp.DataBits = int.Parse(ssi.dataBits);
                sp.Open();
            }
            catch { }
        }
        //读取测量数据
        private void ReadTestData()
        { 
                try
                {
                    IniSerial();              
                    //设置上下限
                    if(comboType.SelectedIndex==0)
                    {
                        //方法体
                        //将命令转换成十六进制
                        //var strs = strReset.Split(' ');
                        //var bytes = new byte[strs.Length];
                        //for (int i = 0; i < strs.Length; i++)
                        //{
                        //    bytes[i] = byte.Parse(strs[i], System.Globalization.NumberStyles.HexNumber);
                        //}
                        //sp.Write(bytes, 0, bytes.Length);

                        //try
                        //{
                        //    int lenght = sp.BytesToRead;
                        //    if (lenght != 0)
                        //    {
                        //        byte[] by = new byte[lenght];
                        //        sp.Read(by, 0, lenght);
                        //    }
                        //}
                        //catch { }

                        StartTimer();
                    }
                    else
                    {
                        //方法体
                        StartTimer();
                    }      
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            
        }
        //开始计时
        private void StartTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(GetTestData);
            timer.Interval = new TimeSpan(0,0,0,2);
          //  System.Threading.Thread.Sleep(2000);
            timer.Start();
        }
        //获得数据方法
        public void GetTestData(object sender, EventArgs e)
        {
            //System.Threading.Thread.Sleep(1000);
            if (txtBarcode.Text != "")
            {
                int lenght = sp.BytesToRead;
                if (lenght != 0)
                {
                   byte[] by = new byte[lenght];
                   sp.Read(by, 0, lenght);
                   SetData(by);

                   labTotal.Content = sum++;
                   //tbResult.Text = "PASS";
                   //test.ispass = "PASS";
                   //tbResult.Foreground = new SolidColorBrush(Colors.GreenYellow);
                   txtBarcode.Clear();
                   timer.Stop();
                }     
            }
            else
            {
                MessageBox.Show("请扫描电池条码");
                timer.Stop();
                //tbResult.Text = "FAIL";
                //test.ispass = "FAIL";
                //tbResult.Foreground = new SolidColorBrush(Colors.Red);
                //timer.Stop();
            }
        }
        private void SetData(byte[] by)
        {           
            string isRBit = ((char)by[2]).ToString();
            string isVBit = ((char)by[7]).ToString();
            string strResistance = ((char)by[3]).ToString()+ ((char)by[4]).ToString()+ ((char)by[5]).ToString()+ ((char)by[6]).ToString();
            string strVolt= ((char)by[8]).ToString() + ((char)by[9]).ToString() + ((char)by[10]).ToString() + ((char)by[11]).ToString();
            test.barcode = txtBarcode.Text;
            test.type = comboType.Text;
            
            if (isRBit == "0")
            {
                test.resistance = double.Parse(strResistance);
            }
            else
            {
                test.resistance = double.Parse(strResistance) / 10;
            }   

            if (isVBit == "2")
                test.volt = double.Parse(strVolt) / 100;
            else
                test.volt = double.Parse(strVolt) / 1000;

            if (test.type.Equals("O1"))//执行O1测试
            {
                if (test.volt >= double.Parse(labVoltMin.Content.ToString()) && test.volt <= double.Parse(labVoltMax.Content.ToString()) && test.resistance >= double.Parse(labResistanceMin.Content.ToString()) && test.resistance <= double.Parse(labResistanceMax.Content.ToString()))
                {
                    strModel = comboModel.Text;
                    strFrom_user1 = "";
                    strPassageway_1 = "1";
                    dtTime_1 = DateTime.Now;
                    dgr = new DataGridRow() { Item = new {barcod = test.barcode, model = strModel, from_user1 = strFrom_user1, testtype_1=test.type, passageway_1=strPassageway_1,time_1=dtTime_1,volt_1=test.volt, resistance_1=test.resistance, ispass_1="PASS" } };
                    dgO1.Items.Add(dgr);
                    
                }
                else
                {
                    strModel = comboModel.Text;
                    strFrom_user1 = "";
                    strPassageway_1 = "1";
                    dtTime_1 = DateTime.Now;
                    dgr = new DataGridRow() { Item = new { barcod = test.barcode, model = strModel, from_user1 = strFrom_user1, testtype_1 = test.type, passageway_1 = strPassageway_1, time_1 = dtTime_1, volt_1 = test.volt, resistance_1 = test.resistance, ispass_1 = "FAIL" } };
                    dgr.Background = new SolidColorBrush(Colors.Red);
                    dgO1.Items.Add(dgr);
                }
               
            }
            else//下面执行OB测试的代码
            {

            }

        }
        //结束按钮
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.Visibility = Visibility.Visible;
            btnStop.Visibility = Visibility.Collapsed;
            btnReset.IsEnabled = true;
            comboType.IsEnabled = true;
            comboModel.IsEnabled = true;
            cbCunFang.IsEnabled = true;
            txtBarcode.IsEnabled = false;
            sp.Close();
            timer.Stop();
        }
        //复位按钮
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            comboModel.SelectedIndex = -1;
            labVoltMax.Content = 0;
            labVoltMin.Content = 0;
            labResistanceMax.Content = 0;
            labResistanceMin.Content = 0;
            labKMax.Content = 0;
            labKMin.Content = 0;
            labTotal.Content = 0;
            labUnQualified.Content = 0;
            labUnQualifiedRate.Content = 0;
            labQualified.Content = 0;
            sum = 0;
            //方法体
            IniSerial();
            //将命令转换成十六进制
            var strs = strReset.Split(' ');
            var bytes = new byte[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                bytes[i] = byte.Parse(strs[i], System.Globalization.NumberStyles.HexNumber);
            }
            sp.Write(bytes, 0, bytes.Length);
            sp.Close();
        }
        //选择型号变更测试规格
        private void comboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = comboModel.SelectedValue?.ToString();
            if (selected == null)
                return;

            if(comboType.SelectedIndex==0)
            {
                labVoltMax.Content = (from l in mi.list
                                      where l.model == selected
                                      select l.voltMax1).ToArray()[0];
                labVoltMin.Content = (from l in mi.list
                                      where l.model == selected
                                      select l.voltMin1).ToArray()[0];
                labResistanceMax.Content = (from l in mi.list
                                            where l.model == selected
                                            select l.resistanceMax1).ToArray()[0];
                labResistanceMin.Content = (from l in mi.list
                                            where l.model == selected
                                            select l.resistanceMin1).ToArray()[0];
                labKMax.Content = 0;
                labKMin.Content = 0;
                test.type = "O1";

            }
            else
            {
                labVoltMax.Content = (from l in mi.list
                                      where l.model == selected
                                      select l.voltMax2).ToArray()[0];
                labVoltMin.Content = (from l in mi.list
                                      where l.model == selected
                                      select l.voltMin2).ToArray()[0];
                labResistanceMax.Content = (from l in mi.list
                                            where l.model == selected
                                            select l.resistanceMax2).ToArray()[0];
                labResistanceMin.Content = (from l in mi.list
                                            where l.model == selected
                                            select l.resistanceMin2).ToArray()[0];
                labKMax.Content = (from l in mi.list
                                   where l.model == selected
                                   select l.k_valueMax2).ToArray()[0];
                labKMin.Content = (from l in mi.list
                                   where l.model == selected
                                   select l.k_valueMin2).ToArray()[0];
                test.type = "OB";
            }

        }


 

        private void txtBarcode_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ReadTestData();
        }
    }
}
