using System.Windows.Controls;
using System.Data.SqlClient;
using mi = HASystem.StaticClass.ModelInfo;
using System.Windows;
using System;

namespace HASystem.Panels
{
    /// <summary>
    /// AlterModelInfo.xaml 的交互逻辑
    /// </summary>
    public partial class AlterModelInfo : UserControl
    {
        private string _oldModel;
        SqlConnection conn=new SqlConnection("Server=localhost;Database=HADB;User id=sa;PWD=Lwenc");
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
        }
        //确认按钮
        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"use HADB exec proc_alterModel  '{_oldModel}','{txtModel.Text}'," +
                    $"'{txtMaxVolt.Text}','{txtMinVolt.Text}','{txtMaxResistance.Text}','{txtMinResistance.Text}'," +
                   $"'{txtMaxVolt2.Text}','{txtMinVolt2.Text}','{txtMaxResistance2.Text}','{txtMinResistance2.Text}','{txtMaxK2.Text}','{txtMinK2.Text}'";
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("修改成功！");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                conn.Close();
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
        }
        //返回按钮
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            RequestBack?.Invoke(sender, EventArgs.Empty);
        }
    }
}
