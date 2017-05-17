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

namespace HASystem.Panels
{
    /// <summary>
    /// AddUserPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AddUserPanel : UserControl
    {
        public AddUserPanel()
        {
            InitializeComponent();
            InitRole();
        }
        private void InitRole()
        {
            string[] role = new string[] {"普通用户","管理员","超级管理员" };
        }
    }
}
