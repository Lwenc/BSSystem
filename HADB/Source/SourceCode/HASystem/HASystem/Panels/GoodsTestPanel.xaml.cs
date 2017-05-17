using System.Windows.Controls;
using System.Windows;

namespace HASystem.Panels
{
    /// <summary>
    /// GoodsTestPanel.xaml 的交互逻辑
    /// </summary>
    public partial class GoodsTestPanel : UserControl
    {
        public GoodsTestPanel()
        {
            InitializeComponent();
            InitCombo();
        }
        private void InitCombo()
        {
            string[] type = new string[] {"o1","o2" };
            comboType.ItemsSource = type;
            comboType.SelectedIndex = 0;
        }
        //开始按钮
        private void btnStart_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnStart.Visibility = Visibility.Collapsed;
            btnStop.Visibility = Visibility.Visible;
        }
        //结束按钮
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            btnStart.Visibility = Visibility.Visible;
            btnStop.Visibility = Visibility.Collapsed;
        }
    }
}
