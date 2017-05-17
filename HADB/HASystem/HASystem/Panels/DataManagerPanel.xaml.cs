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
    /// DataManagerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DataManagerPanel : UserControl
    {
        private bool _isTestInfo = true;
        public DataManagerPanel()
        {
            InitializeComponent();
        }
        //测试数据
        private void btnTestInfo_Click(object sender, RoutedEventArgs e)
        {
            dgTestInfo.Visibility = Visibility.Visible;
             dgLogInfo.Visibility = Visibility.Collapsed;
            _isTestInfo = true;
        }
        //操作日志
        private void btnLog_Click(object sender, RoutedEventArgs e)
        {
            dgTestInfo.Visibility = Visibility.Collapsed;
            dgLogInfo.Visibility = Visibility.Visible;
            _isTestInfo = false;
        }
    }
}
