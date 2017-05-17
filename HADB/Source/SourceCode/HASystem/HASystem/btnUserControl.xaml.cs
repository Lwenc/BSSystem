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

namespace DPCMS
{
    /// <summary>
    /// btnUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class btnUserControl : UserControl
    {
        public event EventHandler<EventArgs> Click;
        public btnUserControl()
        {
            InitializeComponent();
            btnClick.Click += (s, e) => Click?.Invoke(this,EventArgs.Empty);
        }

        /// <summary>
        /// 新加的属性，用来给Button里Image的Source属性做绑定源
        /// </summary>
        public ImageSource AppearanceSource
        {
            get { return (ImageSource)GetValue(AppearanceSourceProperty); }
            set { SetValue(AppearanceSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AppearanceSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AppearanceSourceProperty =
            DependencyProperty.Register("AppearanceSource", typeof(ImageSource), typeof(btnUserControl), new PropertyMetadata(null));

        private void rootCtrl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!btnClick.IsChecked.Value)
                VisualStateManager.GoToElementState(rootCtrl, "MouseEnter", false);
        }

        private void rootCtrl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!btnClick.IsChecked.Value)
                VisualStateManager.GoToElementState(rootCtrl, "OriginalState", false);
        }

        private void btnClick_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnClick_Unchecked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToElementState(rootCtrl, "OriginalState", false);
        }
    }
}
