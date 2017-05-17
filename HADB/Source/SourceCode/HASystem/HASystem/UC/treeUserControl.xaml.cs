using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HASystem.UC
{
    /// <summary>
    /// treeUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class treeUserControl : UserControl
    {
        public event EventHandler<EventArgs> Click;
        public treeUserControl()
        {
            InitializeComponent();
            btnClick.Click += (s, e) => Click?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 新加的属性，用来给Button里Image的Source属性做绑定源
        /// </summary>
        public ImageSource treeAppearanceSource
        {
            get { return (ImageSource)GetValue(AppearanceSourceProperty); }
            set { SetValue(AppearanceSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AppearanceSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AppearanceSourceProperty =
            DependencyProperty.Register("treeAppearanceSource", typeof(ImageSource), typeof(treeUserControl), new PropertyMetadata(null));

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

