using System.Linq;
using System;
using System.Windows;
using System.Windows.Controls;
using System.IO.Ports;
using System.Windows.Threading;
using ssi = HASystem.StaticClass.StructSerialInfo;
using si = HASystem.StaticClass.SerialInfo;
using mi = HASystem.StaticClass.ModelInfo;

namespace HASystem.Panels
{
    /// <summary>
    /// GoodsTestPanel.xaml 的交互逻辑
    /// </summary>
    public partial class GoodsTestPanel : UserControl
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private string[] model = new string[] { };
        SerialPort sp = new SerialPort();

        public GoodsTestPanel()
        {
            InitializeComponent();
            InitCombo();
        }
        private void InitCombo()
        {
            string[] type = new string[] {"O1","OB" };
            comboType.ItemsSource = type;            
            comboType.SelectedIndex = 0;
            comboModel.ItemsSource = (from l in mi.list
                                      select l.model).Distinct();
        }
        //开始按钮
        private void btnStart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnStart.Visibility = Visibility.Collapsed;
            btnStop.Visibility = Visibility.Visible;
            btnReset.IsEnabled = false;
            comboType.IsEnabled = false;
            comboModel.IsEnabled = false;
            txtBarcode.Focus();
        }
        //读取测量数据
        private void ReadTestData()
        {
            if (txtBarcode.Text != "")
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
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
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

            sp.Close();
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
            }

        }

    }
}
