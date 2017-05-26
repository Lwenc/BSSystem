using HASystem.StaticClass;
using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using mi = HASystem.StaticClass.ModelInfo;
using si = HASystem.StaticClass.SerialInfo;
using ssi = HASystem.StaticClass.StructSerialInfo;
using test = HASystem.StaticClass.TestResultInfo;
using ini = HASystem.StaticClass.IniConfigure;
using System.Windows.Controls.Primitives;

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
        private int sum = 0;//计算测试总数；
        private int pass = 0;//保存测试的合格数；
        private int npass = 0;//保存测试的不合格数；
        private int rapass = 0;//保存测试的合格率；
        private string strReset = "23 AA AA 41 37 41 31 33 46 33 45 0D";
        SerialPort sp = new SerialPort();
        string strModel;
        string strFrom_user1="";
        string strTesttype_1;
        string strPassageway_1="1";
        string strIspass_1;
        DateTime dtTime_1;
        string strFrom_user2 = "";
        string strTesttype_2;
        string strPassageway_2 = "1";
        string strIspass_2;
        string compensate=" ";
        DateTime dtTime_2;

        public GoodsTestPanel()
        {
            InitializeComponent();
            InitCombo();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(GetTestData);
            timer.Interval = new TimeSpan(0, 0, 0,1);
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
                btnReset.Visibility = Visibility.Collapsed;
                comboType.IsEnabled = false;
                comboModel.IsEnabled = false;
                cbCunFang.IsEnabled = false;
                txtBarcode.IsEnabled = true;
                IniSerial();
                SendData();
                txtBarcode.Focus();
            }
            else
                MessageBox.Show("请选择型号进行测试！");
        }
        //发送命令和设置上下限
        private void SendData()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                ini.path = @"..\\..\\IniConfigures\\VoltResisSetting.ini";
                string resis = ini.IniReadvalue("SectionResis", "key1");
                string volt = ini.IniReadvalue("SectionVolt", "key1");
                string strResistanceMax = labResistanceMax.Content.ToString().strResisRemove(resis);
                string strResistanceMin = labResistanceMin.Content.ToString().strResisRemove(resis);
                string strVoltMax = labVoltMax.Content.ToString().strVoltRemove(volt);
                string strVoltMin = labVoltMin.Content.ToString().strVoltRemove(volt);
                if (StringExtension.IsVoltRight == false&&StringExtension.IsResisRight==false)
                {
                    MessageBox.Show("电压挡位和电阻挡位设置不正确");
                    btnStop_Click(this, new RoutedEventArgs(ButtonBase.ClickEvent));
                    return;
                }
               if (StringExtension.IsVoltRight == false)
                {
                    MessageBox.Show("电压挡位设置不正确");
                    btnStop_Click(this, new RoutedEventArgs(ButtonBase.ClickEvent));
                    return;
                }
                if (StringExtension.IsResisRight == false)
                {
                    MessageBox.Show("电压挡位设置不正确");
                    btnStop_Click(this, new RoutedEventArgs(ButtonBase.ClickEvent));
                    return;
                }
                sb.Append("0");
                sb.Append(resis);
                sb.Append(volt);
                sb.Append(strResistanceMax.Substring(0, 1));
                sb.Append(strResistanceMax.Substring(1, 1));
                sb.Append(strResistanceMax.Substring(2, 1));
                sb.Append(strResistanceMax.Substring(3, 1));
                sb.Append(strResistanceMin.Substring(0, 1));
                sb.Append(strResistanceMin.Substring(1, 1));
                sb.Append(strResistanceMin.Substring(2, 1));
                sb.Append(strResistanceMin.Substring(3, 1));
                sb.Append(strVoltMax.Substring(0, 1));
                sb.Append(strVoltMax.Substring(1, 1));
                sb.Append(strVoltMax.Substring(2, 1));
                sb.Append(strVoltMax.Substring(3, 1));
                sb.Append(strVoltMin.Substring(0, 1));
                sb.Append(strVoltMin.Substring(1, 1));
                sb.Append(strVoltMin.Substring(2, 1));
                sb.Append(strVoltMin.Substring(3, 1));
                //string order = "#0121005000558250011";
                string order = "#" + sb.ToString();
                order = order.Convert();
                //string order = "23 30 31 32 31 30 30 35 30 30 30 35 35 38 32 35 30 30 30 30";
                string str = CRC32.GetCrc32(order.Replace(" ", ""));
                for (int index = 0; index < str.Length / 2; ++index)
                    order += " " + str.Substring(index * 2, 2);
                order += " " + "0D";

                //将命令转换成十六进制
                var strs = order.Split(' ');
                var bytes = new byte[strs.Length];
                for (int i = 0; i < strs.Length; i++)
                {
                    bytes[i] = byte.Parse(strs[i], System.Globalization.NumberStyles.HexNumber);
                }
                sp.Write(bytes, 0, bytes.Length);
                System.Threading.Thread.Sleep(6000);
                sp.DiscardInBuffer();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
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
                    timer.Start();
                 }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            
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
                   //tbResult.Text = "PASS";
                   //test.ispass = "PASS";
                   //tbResult.Foreground = new SolidColorBrush(Colors.GreenYellow);
                   txtBarcode.Clear();
                   timer.Stop();
                }     
            }
            else
            {
                timer.Stop();
                MessageBox.Show("请扫描电池条码");
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


            //面板显示控制，只能10条数据
            if (dgO1.Items.Count >= 10)
            {
                dgO1.Items.Clear();
            }
            if (dgOB.Items.Count >= 10)
            {
                dgOB.Items.Clear();
            }

            if (test.type.Equals("O1"))//执行O1测试
            {
                strModel = comboModel.Text;
                strFrom_user1 = "";
                strPassageway_1 = "1";
                strTesttype_1 = "O1";
                dtTime_1 = DateTime.Now;
                //面板数据处理
                if (test.volt >= double.Parse(labVoltMin.Content.ToString()) && test.volt <= double.Parse(labVoltMax.Content.ToString()) && test.resistance >= double.Parse(labResistanceMin.Content.ToString()) && test.resistance <= double.Parse(labResistanceMax.Content.ToString()))//与标准规格做判断，合格
                {
                    strIspass_1 = "PASS";
                    dgr = new DataGridRow() { Item = new {barcod = test.barcode, model = strModel, from_user1 = strFrom_user1, testtype_1=test.type, passageway_1=strPassageway_1,time_1=dtTime_1,volt_1=test.volt, resistance_1=test.resistance, ispass_1=strIspass_1 } };
                    dgO1.Items.Add(dgr);
                    //面板数据控制
                    sum++;
                    pass++;
                    rapass = pass / sum * 100;//通过率的计算
                    tbResult.Text = "PASS";
                    tbResult.Foreground = new SolidColorBrush(Colors.YellowGreen);
                }
                else
                {
                    strIspass_1 = "FAIL";
                    dgr = new DataGridRow() { Item = new { barcod = test.barcode, model = strModel, from_user1 = strFrom_user1, testtype_1 = test.type, passageway_1 = strPassageway_1, time_1 = dtTime_1, volt_1 = test.volt, resistance_1 = test.resistance, ispass_1 = strIspass_1 } };
                    dgr.Background = new SolidColorBrush(Colors.Red);
                    dgO1.Items.Add(dgr);
                    //面板数据控制
                    sum++;
                    npass++;
                    rapass = pass / sum * 100;//通过率的计算
                    tbResult.Text = "FAIL";
                    tbResult.Foreground = new SolidColorBrush(Colors.Red);
                }

                //数据存储操作
                //判断是否已经有电池的O1测试
                bool b = SaveTestData.CheckBarCode(test.barcode, "O1");
                if (b == true)//如果有，需要覆盖吗？
                {
                    if (MessageBox.Show(test.barcode + "型号的电池已经进行过O1测试，是否覆盖？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        SaveTestData.UpdateTsetData_O1(test.barcode, strFrom_user1, strTesttype_1, strPassageway_1, dtTime_1, (decimal)test.volt, (decimal)test.resistance, strIspass_1);
                    }
                    else//面板数据统计调整
                    {
                        if (strIspass_1.Equals("PASS"))
                        {
                            sum--;
                            pass--;
                        }
                        else if(strIspass_1.Equals("FAIL"))
                        {
                            sum--;
                            npass--;
                        }
                        if(sum>1)
                            rapass = pass / sum * 100;//通过率的计算
                    }
                }
                else
                {
                    SaveTestData.AddNewTsetData_O1(test.barcode, strModel, strFrom_user1, strTesttype_1, strPassageway_1, dtTime_1, (decimal)test.volt, (decimal)test.resistance, strIspass_1);
                }

            }
            else//下面执行OB测试的代码
            {
                strModel = comboModel.Text;
                strFrom_user2 = "";
                strPassageway_2 = "1";
                strTesttype_2 = "OB";
                dtTime_2 = DateTime.Now;
                //判断是否已经经过O1测试
                bool b = SaveTestData.CheckBarCode(test.barcode, "O1");
                if (b == false)
                {
                    MessageBox.Show(test.barcode+"电池没有进行O1测试，不可以进行OB测试！","提示",MessageBoxButton.OK,MessageBoxImage.Information);
                    return;
                }
                //判断电池O1测试是否合格
                bool b2 = SaveTestData.CheckBarCodePsaa1(test.barcode);
                if (b2 == false)//不合格
                {
                    MessageBox.Show(test.barcode+"电池的O1测试不合格，不可以进行OB测试！","提示",MessageBoxButton.OK,MessageBoxImage.Information);
                    return;
                }
                //计算K值
                test.kvalue = SaveTestData.getK(test.barcode, test.volt, dtTime_2);
                //面板数据处理
                if (test.volt >= double.Parse(labVoltMin.Content.ToString()) && test.volt <= double.Parse(labVoltMax.Content.ToString()) && test.resistance >= double.Parse(labResistanceMin.Content.ToString()) && test.resistance <= double.Parse(labResistanceMax.Content.ToString())&&test.kvalue>=double.Parse(labKMin.Content.ToString())&&test.kvalue<=double.Parse(labKMax.Content.ToString()))//判断为合格
                {
                    strIspass_2 = "PASS";
                    dgr = new DataGridRow() { Item = new { barcod = test.barcode, model = strModel, from_user2 = strFrom_user2, testtype_2 = test.type, passageway_2 = strPassageway_2, time_2 = dtTime_2, volt_2 = test.volt, resistance_2 = test.resistance, k_value_2=test.kvalue, ispass_2 = strIspass_2 } };
                    dgOB.Items.Add(dgr);
                    //面板数据控制
                    sum++;
                    pass++;
                    rapass = pass / sum * 100;//通过率的计算
                    tbResult.Text = "PASS";
                    tbResult.Foreground = new SolidColorBrush(Colors.YellowGreen);
                }
                else//判断为不合格
                {
                    strIspass_2 = "FAIL";
                    dgr = new DataGridRow() { Item = new { barcod = test.barcode, model = strModel, from_user2 = strFrom_user2, testtype_2 = test.type, passageway_2 = strPassageway_2, time_2 = dtTime_2, volt_2 = test.volt, resistance_2 = test.resistance, k_value_2=test.kvalue, ispass_2 = strIspass_2 } };
                    dgr.Background = new SolidColorBrush(Colors.Red);
                    dgOB.Items.Add(dgr);
                    //面板数据控制
                    sum++;
                    npass++;
                    rapass = pass / sum * 100;//通过率的计算
                    tbResult.Text = "FAIL";
                    tbResult.Foreground = new SolidColorBrush(Colors.Red);
                }

                //数据进行存储
                //数据存储操作
                //判断是否已经有电池的O1测试
                bool bb = SaveTestData.CheckBarCode(test.barcode, "OB");
                if (bb == true)//如果有，需要覆盖吗？
                {
                    if (MessageBox.Show(test.barcode + "型号的电池已经进行过OB测试，是否覆盖？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        SaveTestData.AddNewTsetData_OB(test.barcode, strFrom_user2, test.type, strPassageway_2, dtTime_2, (decimal)test.volt, (decimal)test.resistance, (decimal)test.kvalue, strIspass_2);
                    }
                    else
                    {
                        if (strIspass_2.Equals("PASS"))
                        {
                            sum--;
                            pass--;
                        }
                        else if(strIspass_2.Equals("FAIL"))
                        {
                            sum--;
                            npass--;
                        }
                        if(sum>1)
                            rapass = pass / sum * 100;//通过率的计算
                    }
                }
                else
                {
                    SaveTestData.AddNewTsetData_OB(test.barcode, strFrom_user2, test.type, strPassageway_2, dtTime_2, (decimal)test.volt, (decimal)test.resistance, (decimal)test.kvalue, strIspass_2);
                }
            }

            //面板计数显示
            labTotal.Content = sum;
            labQualified.Content = pass;
            labUnQualified.Content = npass;
            labQualifiedRate.Content = rapass;
            if (dgO1.Items.Count > 10)
            {
                dgO1.Items.Clear();
            }
            if (dgOB.Items.Count > 10)
            {
                dgOB.Items.Clear();
            }
        }
        //结束按钮
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnStart.Visibility = Visibility.Visible;
            btnStop.Visibility = Visibility.Collapsed;
            btnReset.Visibility = Visibility.Visible;
            comboType.IsEnabled = true;
            comboModel.IsEnabled = true;
            cbCunFang.IsEnabled = true;
            txtBarcode.IsEnabled = false;
            sp.Close();
            timer.Stop();
            //面板计数清除
            sum = 0;
            pass = 0;
            npass = 0;
            rapass = 0;
            labTotal.Content = "0";
            labQualified.Content = "0";
            labUnQualified.Content = "0";
            labQualifiedRate.Content = "0";
        }
        //复位按钮
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            comboModel.SelectedIndex = -1;
            btnStart.IsEnabled = true;
            labVoltMax.Content = 0;
            labVoltMin.Content = 0;
            labResistanceMax.Content = 0;
            labResistanceMin.Content = 0;
            labKMax.Content = 0;
            labKMin.Content = 0;
            labTotal.Content = 0;
            labUnQualified.Content = 0;
            labQualifiedRate.Content = 0;
            labQualified.Content = 0;
            sum = 0;
            pass = 0;
            npass = 0;
            rapass = 0;
            labTotal.Content = "0";
            labQualified.Content = "0";
            labUnQualified.Content = "0";
            labQualifiedRate.Content = "0";
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
            //
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
                compensate = (from l in mi.list
                              where l.model == selected
                              select l.volt_compensate).ToArray()[0];
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
                compensate = (from l in mi.list
                                     where l.model == selected
                                     select l.volt_compensate).ToArray()[0];
                test.type = "OB";
            }

        }

        private void txtBarcode_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key==System.Windows.Input.Key.Enter)
            {
                if (txtBarcode.Text.Length <= 3)
                {
                    MessageBox.Show("错误的电池编码！");
                    return;
                }
                else if (comboModel.Text.Substring(0, 3).Equals(txtBarcode.Text.Substring(0, 3)) == false)
                {
                    MessageBox.Show("电池型号不符合，无法测试！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (txtBarcode.Text.Length != 14)
                {
                    MessageBox.Show("电池编码为14位！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                ReadTestData();
            }
        }

        private void comboType_DropDownClosed(object sender, EventArgs e)//设置显示哪个dg
        {
            //设置dg显示
            if (comboType.Text.Equals("O1"))
            {
                dgO1.Visibility = Visibility.Visible;
                dgOB.Visibility = Visibility.Hidden;
            }
            else if (comboType.Text.Equals("OB"))
            {
                dgO1.Visibility = Visibility.Hidden;
                dgOB.Visibility = Visibility.Visible;
            }
        }
    }
}
