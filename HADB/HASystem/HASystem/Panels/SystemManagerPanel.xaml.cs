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
    /// SystemManagerPanel.xaml 的交互逻辑
    /// </summary>
    public partial class SystemManagerPanel : UserControl
    {
        public SystemManagerPanel()
        {
            InitializeComponent();
        }
        //通讯设置
        private void btnCommunication_Click(object sender, RoutedEventArgs e)
        {
            btnCommunication.IsEnabled = false;
            btnpassageway.IsEnabled = true;
            communicationPanel.Visibility = Visibility.Visible;
            passagewayPanel.Visibility = Visibility.Collapsed;
        }

        private void btnpassageway_Click(object sender, RoutedEventArgs e)
        {
            btnpassageway.IsEnabled = false;
            communicationPanel.Visibility = Visibility.Collapsed;
            passagewayPanel.Visibility = Visibility.Visible;
            btnCommunication.IsEnabled = true;
        }
        //宽876.5//高528.8

    }
}
