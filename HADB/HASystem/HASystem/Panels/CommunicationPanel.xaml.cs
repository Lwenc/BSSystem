using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;
using ini = HASystem.StaticClass.IniConfigure;

namespace HASystem.Panels
{
    /// <summary>
    /// CommunicationPanel.xaml 的交互逻辑
    /// </summary>
    public partial class CommunicationPanel : UserControl
    {
        public CommunicationPanel()
        {
            InitializeComponent();
            InitComboBox();
        }
        //初始化串口
        private void InitComboBox()
        {
            //赋值停止位
            double[] stopBits = new double[] { 1, 1.5, 2 };
            comBoStopBit.ItemsSource = stopBits;
            comBoStopBit.SelectedIndex = 0;
            //赋值波特率
            int[] bauRate = new int[] { 110, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 56000, 57600, 115200, 128000, 128000, 256000 };
            comBoBaueRate.ItemsSource = bauRate;
            comBoBaueRate.SelectedIndex = 12;
            //赋值数据位
            int[] dataBits = new int[] { 5, 6, 7, 8 };
            comBoDataBit.ItemsSource = dataBits;
            comBoDataBit.SelectedIndex = 3;
            //赋值校验位
            string[] parity = new string[] { "None", "Odd", "Even", "Mark", "Space" };
            comboParityBit.ItemsSource = parity;
            comboParityBit.SelectedIndex = 0;
            //赋值串口号
            string[] spnumber = SerialPort.GetPortNames();
            comboSerial.ItemsSource = spnumber;
            comboSerial.SelectedIndex = 0;
        }
        //保存按钮
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ini.path = @"..\\..\\IniConfigures\\SerialSetting.ini";
            ini.IniWritevalue("Section1", "Key1",comboSerial.Text);
            ini.IniWritevalue("Section1", "Key2", comBoBaueRate.Text);
            ini.IniWritevalue("Section1", "Key3", comBoStopBit.Text);
            ini.IniWritevalue("Section1", "Key4", comboParityBit.Text);
            ini.IniWritevalue("Section1", "Key5", comBoDataBit.Text);
            MessageBox.Show("保存成功！");
        }
    }
}
