using HASystem.StaticClass;
using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
using ini = HASystem.StaticClass.IniConfigure;
using mi = HASystem.StaticClass.ModelInfo;
using si = HASystem.StaticClass.SerialInfo;
using ssi = HASystem.StaticClass.StructSerialInfo;
using test = HASystem.StaticClass.TestResultInfo;
using System.Collections.Generic;

namespace HASystem.Panels
{
    /// <summary>
    /// GoodsTestPanel.xaml 的交互逻辑
    /// </summary>
    public partial class GoodsTestPanel : UserControl
    {
        DataGridRow dgr;
        private DispatcherTimer timer;
        private DispatcherTimer pbTimer;
        private string[] model = new string[] { };
        private double sum = 0;//计算测试总数；
        private double pass = 0;//保存测试的合格数；
        private double npass = 0;//保存测试的不合格数；
        private double rapass = 0;//保存测试的合格率；
        private string strReset = "23 AA AA 41 37 41 31 33 46 33 45 0D";
        SerialPort sp = new SerialPort();
        string strModel;
        string strFrom_user1="";
        string strTesttype_1;
        string strPassageway_1="1";
        string strIspass_1;
        string strRemark_1;
        DateTime dtTime_1;
        string strFrom_user2 = "";
        string strTesttype_2;
        string strPassageway_2 = "1";
        string strIspass_2;
        string strRemark_2;
        string strcompensate=" ";//电压补偿的值
        DateTime dtTime_2;

        //网络测试需要用到的变量
        string strMachinName;
        string strBatCode;
        string strTestType;
        double dVolt;
        double dResistance;
        string strRemark;

        public GoodsTestPanel()
        {
            InitializeComponent();
            InitCombo();
            timer = new DispatcherTimer();
            pbTimer = new DispatcherTimer();
            timer.Tick += new EventHandler(GetTestData);
            timer.Interval = new TimeSpan(0, 0, 0,1);
            pbTimer.Interval = new TimeSpan(0,0,0,1);
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
                comboType2.IsEnabled = false;
                comboModel2.IsEnabled = false;
                cbCunFang.IsEnabled = false;
                txtBarcode.IsEnabled = true;
                IniSerial();
                SendData();
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
                string strResistanceMax;
                string strResistanceMin;
                string strVoltMax;
                string strVoltMin;
                try
                {
                    strResistanceMax = labResistanceMax.Content.ToString().strResisRemove(resis).ThrowIfResistWrong();
                    strResistanceMin = labResistanceMin.Content.ToString().strResisRemove(resis).ThrowIfResistWrong();
                    strVoltMax = labVoltMax.Content.ToString().strVoltRemove(volt).ThrowIfVoltageWrong();
                    strVoltMin = labVoltMin.Content.ToString().strVoltRemove(volt).ThrowIfVoltageWrong();
                }
                catch (ResultWrongException rwe)
                {
                    MessageBox.Show(rwe.Message);
                    btnStop_Click(this, new RoutedEventArgs(ButtonBase.ClickEvent));
                    return;
                }

                sb.Append("0");
                sb.Append(ini.IniReadvalue("SectionResis", "key1"));
                sb.Append(ini.IniReadvalue("SectionVolt", "key1"));
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
                ProcessBarVisity();
                sp.DiscardInBuffer();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        //开始进程条
        private async void ProcessBarVisity()
        {
            gridPB.Visibility = Visibility.Visible;
            gridTestInfo.Opacity = 0.5;
            gridTestInfo.IsEnabled = false;
            pb.Value = 0;
            for (int i = 1; i < 11; i++)
            {
                this.pb.Dispatcher?.Invoke(() => this.pb.Value = i);
                this.tbProgressBar.Dispatcher.Invoke(() => this.tbProgressBar.Text = "正在操作：" + i + "0" + "%");
                await Task.Delay(1000);
            }
            await Task.Delay(500);
            gridPB.Visibility = Visibility.Collapsed;
            gridTestInfo.Opacity = 1;
            gridTestInfo.IsEnabled = true;
            txtBarcode.Focus();
            int lenght = sp.BytesToRead;
            if (lenght != 0)
            {
                byte[] by = new byte[lenght];
                sp.Read(by, 0, lenght);
            }
        }
        //复位进程条
        private async void ResetProcessBarVisity()
        {
            gridPB.Visibility = Visibility.Visible;
            gridTestInfo.Opacity = 0.5;
            gridTestInfo.IsEnabled = false;
            pb.Value = 0;
            for (int i = 1; i < 6; i++)
            {
                this.pb.Dispatcher?.Invoke(() => this.pb.Value = i * 2);
                this.tbProgressBar.Dispatcher.Invoke(() => this.tbProgressBar.Text = "正在操作：" + i * 2 + "0" + "%");
                await Task.Delay(1000);
            }
            gridPB.Visibility = Visibility.Collapsed;
            gridTestInfo.Opacity = 1;
            gridTestInfo.IsEnabled = true;
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
            if (cbCunFang.Text.Equals("本地"))
            {
                test.type = comboType.Text;
            }
            else
            {
                test.type = comboType2.Text;
            }
           
            
            if (isRBit == "0")
            {
                test.resistance = double.Parse(strResistance);
            }
            else
            {
                test.resistance = double.Parse(strResistance) / 10;
            }

            if (isVBit == "2")
                test.volt = double.Parse(strVolt) / 100 + double.Parse(strcompensate);
            else
                test.volt = double.Parse(strVolt) / 1000 + double.Parse(strcompensate);


            //面板显示控制，只能10条数据
            if (dgO1.Items.Count >= 10)
            {
                dgO1.Items.Clear();
            }
            if (dgOB.Items.Count >= 10)
            {
                dgOB.Items.Clear();
            }

            if (test.type.Equals("O1"))//判断是否执行O1测试
            {
                strModel = comboModel.Text;
                strFrom_user1 = "";
                strPassageway_1 = "1";
                strTesttype_1 = "O1";
                dtTime_1 = DateTime.Now;
                //面板数据处理
                if (test.volt >= double.Parse(labVoltMin.Content.ToString()) && test.volt <= double.Parse(labVoltMax.Content.ToString()) && test.resistance >= double.Parse(labResistanceMin.Content.ToString()) && test.resistance <= double.Parse(labResistanceMax.Content.ToString()))//与标准规格做判断，合格
                {
                    strRemark_1 = "测试通过！";
                    strIspass_1 = "PASS";
                    dgr = new DataGridRow() { Item = new { barcod = test.barcode, model = strModel, from_user1 = strFrom_user1, testtype_1 = test.type, passageway_1 = strPassageway_1, time_1 = dtTime_1, volt_1 = test.volt, resistance_1 = test.resistance, ispass_1 = strIspass_1, remark_1 = strRemark_1 } };
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
                    strRemark_1 = "";
                    if (test.volt < double.Parse(labVoltMin.Content.ToString()))
                        strRemark_1 = "测试电压低于下限设定值！ ";
                    if (test.volt > double.Parse(labVoltMax.Content.ToString()))
                        strRemark_1 += "测试电压高于上限设定值！ ";
                    if (test.resistance < double.Parse(labResistanceMin.Content.ToString()))
                        strRemark_1 += "测试内阻低于下限设定值！ ";
                    if (test.resistance > double.Parse(labResistanceMax.Content.ToString()))
                        strRemark_1 += "测试内阻高于上限设定值！ ";
                    strIspass_1 = "FAIL";
                    dgr = new DataGridRow() { Item = new { barcod = test.barcode, model = strModel, from_user1 = strFrom_user1, testtype_1 = test.type, passageway_1 = strPassageway_1, time_1 = dtTime_1, volt_1 = test.volt, resistance_1 = test.resistance, ispass_1 = strIspass_1, remark_1 = strRemark_1 } };
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
                        SaveTestData.UpdateTsetData_O1(test.barcode, strFrom_user1, strTesttype_1, strPassageway_1, dtTime_1, (decimal)test.volt, (decimal)test.resistance, strIspass_1, strRemark_1);
                    }
                    else//面板数据统计调整
                    {
                        if (strIspass_1.Equals("PASS"))
                        {
                            sum--;
                            pass--;
                        }
                        else if (strIspass_1.Equals("FAIL"))
                        {
                            sum--;
                            npass--;
                        }
                        if (sum >= 1)
                            rapass = pass / sum * 100;//通过率的计算
                        else rapass = 0;
                    }
                }
                else
                {
                    SaveTestData.AddNewTsetData_O1(test.barcode, strModel, strFrom_user1, strTesttype_1, strPassageway_1, dtTime_1, (decimal)test.volt, (decimal)test.resistance, strIspass_1, strRemark_1);
                }

            }
            else if (test.type.Equals("OB"))//下面执行OB测试的代码
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
                    MessageBox.Show(test.barcode + "电池没有进行O1测试，不可以进行OB测试！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                //判断电池O1测试是否合格
                bool b2 = SaveTestData.CheckBarCodePsaa1(test.barcode);
                if (b2 == false)//不合格
                {
                    MessageBox.Show(test.barcode + "电池的O1测试不合格，不可以进行OB测试！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                //计算K值
                try
                {
                    test.kvalue = Math.Round(SaveTestData.getK(test.barcode, test.volt, dtTime_2), 4);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                //面板数据处理
                if (test.volt >= double.Parse(labVoltMin.Content.ToString()) && test.volt <= double.Parse(labVoltMax.Content.ToString()) && test.resistance >= double.Parse(labResistanceMin.Content.ToString()) && test.resistance <= double.Parse(labResistanceMax.Content.ToString()) && test.kvalue >= double.Parse(labKMin.Content.ToString()) && test.kvalue <= double.Parse(labKMax.Content.ToString()))//判断为合格
                {
                    strRemark_2 = "测试通过！";
                    strIspass_2 = "PASS";
                    dgr = new DataGridRow() { Item = new { barcod = test.barcode, model = strModel, from_user2 = strFrom_user2, testtype_2 = test.type, passageway_2 = strPassageway_2, time_2 = dtTime_2, volt_2 = test.volt, resistance_2 = test.resistance, k_value_2 = test.kvalue, ispass_2 = strIspass_2, remark_2 = strRemark_2 } };
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
                    strRemark_2 = "";
                    if (test.volt < double.Parse(labVoltMin.Content.ToString()))
                        strRemark_2 = "测试电压低于下限设定值！ ";
                    if (test.volt > double.Parse(labVoltMax.Content.ToString()))
                        strRemark_2 += "测试电压高于上限设定值！ ";
                    if (test.resistance < double.Parse(labResistanceMin.Content.ToString()))
                        strRemark_2 += "测试内阻低于下限设定值！ ";
                    if (test.resistance > double.Parse(labResistanceMax.Content.ToString()))
                        strRemark_2 += "测试内阻高于上限设定值！ ";
                    if (test.kvalue < double.Parse(labKMin.Content.ToString()))
                        strRemark_2 += "测试K值低于下限设定值！ ";
                    if (test.kvalue > double.Parse(labKMax.Content.ToString()))
                        strRemark_2 += "测试K值高于上限设定值！ ";
                    strIspass_2 = "FAIL";
                    dgr = new DataGridRow() { Item = new { barcod = test.barcode, model = strModel, from_user2 = strFrom_user2, testtype_2 = test.type, passageway_2 = strPassageway_2, time_2 = dtTime_2, volt_2 = test.volt, resistance_2 = test.resistance, k_value_2 = test.kvalue, ispass_2 = strIspass_2, remark_2 = strRemark_2 } };
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
                //判ELHG
                bool bb = SaveTestData.CheckBarCode(test.barcode, "OB");
                if (bb == true)//如果有，需要覆盖吗？
                {
                    if (MessageBox.Show(test.barcode + "型号的电池已经进行过OB测试，是否覆盖？", "警告", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        SaveTestData.AddNewTsetData_OB(test.barcode, strFrom_user2, test.type, strPassageway_2, dtTime_2, (decimal)test.volt, (decimal)test.resistance, (decimal)test.kvalue, strIspass_2, strRemark_2);
                    }
                    else
                    {
                        if (strIspass_2.Equals("PASS"))
                        {
                            sum--;
                            pass--;
                        }
                        else if (strIspass_2.Equals("FAIL"))
                        {
                            sum--;
                            npass--;
                        }
                        if (sum >= 1)
                            rapass = pass / sum * 100;//通过率的计算
                        else
                            rapass = 0;
                    }
                }
                else
                {
                    SaveTestData.AddNewTsetData_OB(test.barcode, strFrom_user2, test.type, strPassageway_2, dtTime_2, (decimal)test.volt, (decimal)test.resistance, (decimal)test.kvalue, strIspass_2, strRemark_2);
                }
            }
            else if (test.type.Equals("OF"))//下面的开始进行联网版测试
            {
                strBatCode = txtBarcode.Text.Trim();
                strMachinName = MiddleService.DataSwap.GetComputerName();
                strTestType = "OF";
                dVolt = test.volt;
                dResistance = test.resistance;
                string strTip = MiddleService.DataSwap.uploadData(strBatCode,dVolt.ToString(),dResistance.ToString(),strTestType,strMachinName);
                if (strTip == "-1")
                {
                    MessageBox.Show("条码错误！");
                    tbResult.Text = "";
                }
                else if (strTip == "0")
                {
                    strRemark = "数据上传成功！";
                    dgr = new DataGridRow() { Item = new { CellName = strBatCode, TestType = strTestType, Machine = strMachinName, Ocv1 = dVolt, Imp1 = dResistance, Remark = strRemark } };
                    dgNet.Items.Add(dgr);
                    //面板数据控制
                    sum++;
                    pass++;
                    rapass = pass / sum * 100;//通过率的计算
                    tbResult.Text = "PASS";
                    tbResult.Foreground = new SolidColorBrush(Colors.YellowGreen);
                }
                else if (strTip == "1")
                {
                    strRemark = "OCV坏品";
                    dgr = new DataGridRow() { Item = new { CellName = strBatCode, TestType = strTestType, Machine = strMachinName, Ocv1 = dVolt, Imp1 = dResistance, Remark = strRemark } };
                    dgr.Background = new SolidColorBrush(Colors.Red);
                    dgNet.Items.Add(dgr);
                    //面板数据控制
                    sum++;
                    npass++;
                    rapass = pass / sum * 100;//通过率的计算
                    tbResult.Text = "FAIL";
                    tbResult.Foreground = new SolidColorBrush(Colors.Red);
                }
                else if (strTip == "2")
                {
                    strRemark = "K，Drop坏品";
                    dgr = new DataGridRow() { Item = new { CellName = strBatCode, TestType = strTestType, Machine = strMachinName, Ocv1 = dVolt, Imp1 = dResistance, Remark = strRemark } };
                    dgr.Background = new SolidColorBrush(Colors.Red);
                    dgNet.Items.Add(dgr);
                    //面板数据控制
                    sum++;
                    npass++;
                    rapass = pass / sum * 100;//通过率的计算
                    tbResult.Text = "FAIL";
                    tbResult.Foreground = new SolidColorBrush(Colors.Red);
                }
                else if (strTip == "3")
                {
                    strRemark = "Imp坏品";
                    dgr = new DataGridRow() { Item = new { CellName = strBatCode, TestType = strTestType, Machine = strMachinName, Ocv1 = dVolt, Imp1 = dResistance, Remark = strRemark } };
                    dgr.Background = new SolidColorBrush(Colors.Red);
                    dgNet.Items.Add(dgr);
                    //面板数据控制
                    sum++;
                    npass++;
                    rapass = pass / sum * 100;//通过率的计算
                    tbResult.Text = "FAIL";
                    tbResult.Foreground = new SolidColorBrush(Colors.Red);
                }
                else if (strTip == "4")
                {
                    MessageBox.Show("超时或者缺失项目");
                    tbResult.Text = "";
                }
                else if (strTip == "5")
                {
                    MessageBox.Show("不能插入数据或已存在该数据");
                    tbResult.Text = "";
                }
                else if (strTip == "6")
                {
                    MessageBox.Show("未知");
                    tbResult.Text = "";
                }
                else if (strTip == "7")
                {
                    MessageBox.Show("MI不匹配");
                    tbResult.Text = "";
                }
                else if (strTip == "8")
                {
                    MessageBox.Show("做货类类型不匹配");
                    tbResult.Text = "";

                }
                else if (strTip == "9")
                {
                    MessageBox.Show("已打包");
                    tbResult.Text = "";
                }
                else if (strTip == "10")
                {
                    MessageBox.Show("条码检测有问题");
                    tbResult.Text = "";
                }
                if (strTip == "11")
                {
                    MessageBox.Show("无权限");
                    tbResult.Text = "";
                }
            }

            //面板计数显示
            labTotal.Content = sum;
            labQualified.Content = pass;
            labUnQualified.Content = npass;
            labQualifiedRate.Content = Math.Round(rapass, 2);
          
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
            comboType2.IsEnabled = true;
            comboModel2.IsEnabled = true;
            cbCunFang.IsEnabled = true;
            txtBarcode.IsEnabled = false;
            sp.Close();
            timer.Stop();
        }
        //复位按钮
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            dgO1.Items.Clear();
            dgOB.Items.Clear();
            txtBarcode.Text = "";
            comboModel.SelectedIndex = -1;
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
            //mi.GetModelData();
            //方法体
            try
            {
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
                btnStart.IsEnabled = true;
                ResetProcessBarVisity();
            }
            catch
            {
                MessageBox.Show("暂无插入串口！");
            }
        }
        //选择型号变更测试规格
        private void comboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = comboModel.SelectedValue?.ToString();
            if (selected == null)
                return;
            mi.GetModelData();
            if (comboType.SelectedIndex==0)
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
                strcompensate = (from l in mi.list
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
                strcompensate = (from l in mi.list
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
                    MessageBox.Show("错误的电池编码！","提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (comboModel.Text.Substring(0, 3).Equals(txtBarcode.Text.Substring(0, 3)) == false)
                {
                    MessageBox.Show("电池型号与条码不符合，无法测试！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else if (txtBarcode.Text.Trim().Length != 12)
                {
                    MessageBox.Show("电池编码为12位！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
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
        //加载行号
        private void dgO1_LoadingRow(object sender, DataGridRowEventArgs e)
         =>   e.Row.Header = e.Row.GetIndex() + 1;

        private void comboModel2_DropDownClosed(object sender, EventArgs e)
        {
            if (comboModel2.SelectedIndex == -1)
            {
                return;
            }
            string str = MiddleService.DataSwap.GetOCVIMPmiSpec(comboModel2.Text, comboType2.Text);
            string[] Values = str.Split('&');
            labVoltMin.Content = Values[0];
            labVoltMax.Content = Values[1];
            labResistanceMin.Content = Values[2];
            labResistanceMax.Content = Values[3];
        }

        private void cbCunFang_DropDownClosed(object sender, EventArgs e)
        {
            if (cbCunFang.Text.Equals("本地"))
            {
                comboModel.Visibility = Visibility.Visible;
                comboType.Visibility = Visibility.Visible;
                comboModel2.Visibility = Visibility.Hidden;
                comboType2.Visibility = Visibility.Hidden;
                dgNet.Visibility = Visibility.Hidden;

            }
            else if (cbCunFang.Text.Equals("网络"))
            {
                comboModel.Visibility = Visibility.Hidden;
                comboType.Visibility = Visibility.Hidden;
                comboModel2.Visibility = Visibility.Visible;
                comboType2.Visibility = Visibility.Visible;
                dgNet.Visibility = Visibility.Visible;

                //下拉框初始化
                try
                {
                    //测试类型下拉款初始化
                    List<string> listTestType = new List<string>();
                    listTestType.Add(MiddleService.DataSwap.GetOCVTestType(MiddleService.DataSwap.GetComputerName()));
                    comboType2.ItemsSource = listTestType;
                    comboType2.SelectedIndex = 0;
                    //型号管理下拉框初始化
                    string str = MiddleService.DataSwap.GetMIList();
                    string[] strMode = str.Split('#');
                    comboModel2.ItemsSource = strMode;
                    comboModel2.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
