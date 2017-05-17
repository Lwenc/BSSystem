using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HASystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            SetComboBox();
        }
        //初始化comboBox
        private void SetComboBox()
        {
            string[] str = new string[] { "普通用户", "管理员" };
            comboUserType.ItemsSource = str;
            comboUserType.SelectedIndex = 0;
        }
       
        //登陆按钮
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            //    if (txtPwassWord.Password == "" || txtUserId.Text == "")
            //    {
            //        MessageBox.Show("账号或密码不能为空！");
            //    }
            //    else
            //    {
            //        conn = new SqlConnection($"Server={"localhost"};uid={"sa"};pwd={"Lwenc"}");
            //        conn.Open();
            //        SqlCommand cmd = conn.CreateCommand();
            //        if (comboUserType.SelectedIndex == 0)
            //            cmd.CommandText = ($"use {DBName} exec proc_Loginuser '{txtUserId.Text}'");
            //        if (comboUserType.SelectedIndex == 1)
            //            cmd.CommandText = ($"use {DBName} exec proc_Loginmanager '{txtUserId.Text}'");

            //        var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            //        //不存在账号
            //        if (!reader.Read())
            //        {
            //            MessageBox.Show("账号或密码错误!");
            //        }
            //        else
            //        {
            //            List<object> list = new List<object>();
            //            //string[] info = new string[reader.FieldCount];
            //            for (int i = 0; i < reader.FieldCount; i++)
            //                list.Add(reader[i]);
            //            if ((SHA256Generator.GenerateSHA256String(txtPwassWord.Password)) == list[0].ToString())
            //            {
            //                SqlInfo.name = list[1].ToString();
            //                SqlInfo.account = txtUserId.Text;
            //                SqlInfo.byImage = list[2] as byte[];
            //                SqlInfo.type = list[3].ToString();
            //                SqlInfo.id = list[4].ToString();
            //                MainWindow mw = new MainWindow();
            //                this.Close();
            //                mw.ShowDialog();
            //            }
            //            else
            //            {
            //                MessageBox.Show("账号或密码错误!");
            //            }

            //        }

                //}
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
        }
        //用户文本框KeyDown
        private void txtUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
                txtPwassWord.Focus();
        }
        //密码文本框KeyDown
        private void txtPwassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
                btnLogin_Click(sender, e);
        }
        //用户类型KeyDown
        private void comboUserType_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
                btnLogin_Click(sender, e);
        }
    }
}
