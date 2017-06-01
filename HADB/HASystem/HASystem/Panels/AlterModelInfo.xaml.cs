using System.Windows.Controls;
using System.Data.SqlClient;
using mi = HASystem.StaticClass.ModelInfo;
using System.Windows;
using System;
using HASystem.StaticClass;

namespace HASystem.Panels
{
    /// <summary>
    /// AlterModelInfo.xaml 的交互逻辑
    /// </summary>
    public partial class AlterModelInfo : UserControl
    {
        private string _oldModel;
        public event EventHandler RequestBack;

        public AlterModelInfo()
        {
            InitializeComponent();
        }
        public void AlterModel(object list)
        {
            _oldModel = (((mi.ModelResult)list).model).ToString();
            txtModel.Text= (((mi.ModelResult)list).model).ToString();
            txtMaxVolt.Text= (((mi.ModelResult)list).voltMax1).ToString();
            txtMinVolt.Text = (((mi.ModelResult)list).voltMin1).ToString();
            txtMaxResistance.Text = (((mi.ModelResult)list).resistanceMax1).ToString();
            txtMinResistance.Text = (((mi.ModelResult)list).resistanceMin1).ToString();
            txtMaxVolt2.Text = (((mi.ModelResult)list).voltMax2).ToString();
            txtMinVolt2.Text = (((mi.ModelResult)list).voltMin2).ToString();
            txtMaxResistance2.Text = (((mi.ModelResult)list).resistanceMax2).ToString();
            txtMinResistance2.Text = (((mi.ModelResult)list).resistanceMin2).ToString();
            txtMaxK2.Text = (((mi.ModelResult)list).k_valueMax2).ToString();
            txtMinK2.Text = (((mi.ModelResult)list).k_valueMin2).ToString();
            txtCompence.Text = (((mi.ModelResult)list).volt_compensate).ToString();
        }
        //保存按钮
        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //格式检查
            bool b = true;
            bool b2 = true;
            b = txtMinVolt.getGeShi();
            if (b == false)
                b2 = false;
            b = txtMaxVolt.getGeShi();
            if (b == false)
                b2 = false;
            b = txtMinResistance.getGeShi();
            if (b == false)
                b2 = false;
            b = txtMaxResistance.getGeShi();
            if (b == false)
                b2 = false;
            b = txtMinVolt2.getGeShi();
            if (b == false)
                b2 = false;
            b = txtMaxVolt2.getGeShi();
            if (b == false)
                b2 = false;
            b = txtMinResistance2.getGeShi();
            if (b == false)
                b2 = false;
            b = txtMaxResistance2.getGeShi();
            if (b == false)
                b2 = false;
            b = txtCompence.getGeShi();
            if (b == false)
                b2 = false;
            b = txtMaxK2.getGeShi();
            if (b == false)
                b2 = false;
            b = txtMinK2.getGeShi();
            if (b == false)
                b2 = false;
            if (b2 == false)
            {
                MessageBox.Show("红色标记为错误数据，请修正，并注意所有数据不能为空！数值之间不能出现空格！");
                return;
            }
            try
            {
                ModelInfo.UpdateModeInfo(txtModel.Text, "O1", double.Parse(txtMaxVolt.Text), double.Parse(txtMinVolt.Text), double.Parse(txtMaxResistance.Text), double.Parse(txtMinResistance.Text), "OB", double.Parse(txtMaxVolt2.Text), double.Parse(txtMinVolt2.Text), double.Parse(txtMaxResistance2.Text), double.Parse(txtMinResistance2.Text), double.Parse(txtMaxK2.Text), double.Parse(txtMinK2.Text), double.Parse(txtCompence.Text), 0, "未启用", DateTime.Now);
                MessageBox.Show("修改成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //重置按钮
        private void btnReset_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txtMaxResistance2.Text = "";
            txtMaxVolt2.Text = "";
            txtMinResistance2.Text = "";
            txtMinVolt2.Text = "";
            txtMaxK2.Text = "";
            txtMaxResistance.Text = "";
            txtMaxVolt.Text = "";
            txtMinResistance.Text = "";
            txtMinVolt.Text = "";
            txtMinK2.Text = "";
            txtModel.Text = "";
            txtCompence.Text = "";
        }
        //返回按钮
        private void btnReturn_Click(object sender, RoutedEventArgs e)
         => RequestBack?.Invoke(sender, EventArgs.Empty);
    }

    public static class SetValue//创建TextBox的扩展方法
    {
        public static bool getGeShi(this TextBox tex)//扩展方法，设置输入框的格式
        {
            double Value;
            //检查是否是有效数值
            bool b = double.TryParse(tex.Text, out Value);
            bool b2 = true;
            int i;
            if (b == false)
            {
                tex.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                b2 = false;
                return b2;
            }

            //检查是否是五位数，数值范围是否正确     
            if (tex.Tag == "1")
                i = 100;
            else
                i = 10000;
            if (tex.Text.Trim().Length > 5)
            {
                tex.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                b2 = false;
            }
            else if (Value < 0 || Value >= i)
            {
                tex.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                b2 = false;
            }
            else
            {
                tex.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            }
            return b2;
        }
    }
}
