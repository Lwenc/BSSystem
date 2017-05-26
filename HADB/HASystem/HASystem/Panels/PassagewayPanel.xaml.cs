using System.Windows;
using System.Windows.Controls;
using ini = HASystem.StaticClass.IniConfigure;

namespace HASystem.Panels
{
    /// <summary>
    /// PassagewayPanel.xaml 的交互逻辑
    /// </summary>
    public partial class PassagewayPanel : UserControl
    {
  
        public PassagewayPanel()
        {
            InitializeComponent();
            GetDataRai();
        }
        //读取数据
        private void GetDataRai()
        {
            ini.path = @"..\\..\\IniConfigures\\VoltResisSetting.ini";
            if (ini.IniReadvalue("SectionVolt", "key1") == "2")
                rdiVoltTwo.IsChecked = true;
            else
                rdiVoltThree.IsChecked = true;

            if (ini.IniReadvalue("SectionResis", "key1") == "0")
                rdiResisZero.IsChecked = true;
            else
                rdiResisOne.IsChecked = true;
        }
        //保存按钮
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ini.path = @"..\\..\\IniConfigures\\VoltResisSetting.ini";
            if(rdiVoltTwo.IsChecked==true)
                ini.IniWritevalue("SectionVolt", "Key1", "2");
            if(rdiVoltThree.IsChecked==true)
                ini.IniWritevalue("SectionVolt", "Key1", "3");
            if (rdiResisOne.IsChecked == true)
                ini.IniWritevalue("SectionResis", "Key1", "1");
            if (rdiResisZero.IsChecked == true)
                ini.IniWritevalue("SectionResis", "Key1", "0");
            //ini.IniWritevalue("SectionVolt", "Key2", comBoBaueRate.Text);
            //ini.IniWritevalue("SectionResistance", "Key3", comBoStopBit.Text);
            //ini.IniWritevalue("Section1", "Key4", comboParityBit.Text);
            //ini.IniWritevalue("Section1", "Key5", comBoDataBit.Text);
            MessageBox.Show("保存成功！");
        }

        
    }
}
