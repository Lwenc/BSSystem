using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System;

namespace HASystem.Panels
{
    /// <summary>
    /// AddModelInfo.xaml 的交互逻辑
    /// </summary>
    public partial class AddModelInfo : UserControl
    {
        SqlConnection conn = new SqlConnection("Server=localhost;Database=HADB;User id=sa;PWD=Lwenc");
        public AddModelInfo()
        {
            InitializeComponent();
        }
        //保存按钮
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"use HADB exec proc_addModel '{txtModel.Text}'," +
                    $"'{txtMaxVolt.Text}','{txtMinVolt.Text}','{txtMaxResistance.Text}','{txtMinResistance.Text}'," +
                   $"'{txtMaxVolt2.Text}','{txtMinVolt2.Text}','{txtMaxResistance2.Text}','{txtMinResistance2.Text}','{txtMaxK2.Text}','{txtMinK2.Text}'";
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("添加成功！");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                conn.Close();
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
        }
    }
}
