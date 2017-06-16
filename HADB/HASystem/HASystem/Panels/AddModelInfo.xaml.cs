using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System;
using HASystem.StaticClass;

namespace HASystem.Panels
{
    /// <summary>
    /// AddModelInfo.xaml 的交互逻辑
    /// </summary>
    public partial class AddModelInfo : UserControl
    {
        public AddModelInfo()
        {
            InitializeComponent();
        }
        //保存按钮
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtModel.Text.Trim() == "" || txtModel.Text.Trim() == null)
            {
                MessageBox.Show("请输入型号！");
                return;
            }
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
            b = txtMaxK2.getKGetShi();
            if (b == false)
                b2 = false;
            b = txtMinK2.getKGetShi();
            if (b == false)
                b2 = false;
            if (b2 == false)
            {
                MessageBox.Show("红色标记为错误数据，请修正，并注意所有数据不能为空！数值之间不能出现空格！");
                return;
            }
            //下面判断上限是否小于下限
            b = txtMaxVolt.CompaVolues(txtMinVolt);
            if (b == false)
                b2 = false;
            b = txtMaxResistance.CompaVolues(txtMinResistance);
            if (b == false)
                b2 = false;
            b = txtMaxVolt2.CompaVolues(txtMinVolt2);
            if (b == false)
                b2 = false;
            b = txtMaxResistance2.CompaVolues(txtMinResistance2);
            if (b == false)
                b2 = false;
            b = txtMaxK2.CompaVolues(txtMinK2);
            if (b == false)
                b2 = false;
            if (b2 == false)
            {
                MessageBox.Show("绿色标记处出错，上限值小于下限值！");
                return;
            }
            //进行数据保存
            try
            {
                ModelInfo.AddNewModeInfo(txtModel.Text, "O1", double.Parse(txtMaxVolt.Text), double.Parse(txtMinVolt.Text), double.Parse(txtMaxResistance.Text), double.Parse(txtMinResistance.Text), "OB", double.Parse(txtMaxVolt2.Text), double.Parse(txtMinVolt2.Text), double.Parse(txtMaxResistance2.Text), double.Parse(txtMinResistance2.Text), double.Parse(txtMaxK2.Text), double.Parse(txtMinK2.Text), double.Parse(txtCompence.Text), 0, LoginWindow.strUserId, DateTime.Now);
                MessageBox.Show("型号：" + txtModel.Text + "添加成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //重置按钮
        private void btnReset_Click(object sender, RoutedEventArgs e)
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
    }
}
