using HASystem.EnumInfo;
using HASystem.UC;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace HASystem
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region//功能面板切换
        //故事板输入和输出
        private Storyboard sbIn, sbOut;
        //切换类型
        private enum SwitchingType : byte
        {
            In,Out
        }
        //将toggleButton按钮与其实际功能关联
        private static Dictionary<string, OperatingType> opType = new Dictionary<string, OperatingType>
        {
            [nameof(rdiTest)] = OperatingType.GoodsTest,
            [nameof(rdiData)]=OperatingType.DataManager,
            [nameof(rdiModel)]=OperatingType.ModelManager,
            [nameof(rdiUser)]=OperatingType.UserManager,
            [nameof(rdiSystem)]=OperatingType.SystemManager
        };
        //面板转换
        private void Switching(object sender, EventArgs e)
        {
            btnUserControl rdiCliked = sender as btnUserControl;
            OperatingType _opType = opType[rdiCliked.Name];
            //高528.8//宽1008
            switch (_opType)
            {
                case OperatingType.GoodsTest:
                    SetContentAreaVisible(panelGoodsTest);
                    break;
                case OperatingType.DataManager:
                    SetContentAreaVisible(panelDataManager);
                    break;
                case OperatingType.ModelManager:
                    SetContentAreaVisible(panelModelManager);
                    break;
                case OperatingType.UserManager:
                    SetContentAreaVisible(panelUserManager);
                    break;
                case OperatingType.SystemManager:
                    SetContentAreaVisible(panelSystemManager);
                    break;
                default:
                    throw new InvalidOperationException("操作不存在或存在错误！");
            }
        }
        private async void SetContentAreaVisible(FrameworkElement element)
        {
            await BeginSwitchingAnimationAsync(SwitchingType.Out);
            foreach (FrameworkElement item in contentDisplay.Children)
                item.Visibility = Visibility.Collapsed;

            element.Visibility = Visibility.Visible;
            await BeginSwitchingAnimationAsync(SwitchingType.In);
        }
        private Task BeginSwitchingAnimationAsync(SwitchingType type)
        {
            SwitchingAnimationInitialize(type);
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            switch (type)
            {
                case SwitchingType.In:
                    sbIn.Completed += (s, e) => task.SetResult(true);
                    sbIn.Freeze();
                    sbIn.Begin();
                    break;
                case SwitchingType.Out:
                    sbOut.Completed += (s, e) => task.SetResult(true);
                    sbOut.Freeze();
                    sbOut.Begin();
                    break;
            }

            return task.Task;
        }
        private void SwitchingAnimationInitialize(SwitchingType type)
        {
            Duration dur = new Duration(TimeSpan.FromMilliseconds(400));
            switch (type)
            {
                case SwitchingType.In:
                    sbIn = new Storyboard();
                    DoubleAnimation daIn = new DoubleAnimation(0, dur);
                    QuadraticEase easeIn = new QuadraticEase();
                    easeIn.EasingMode = EasingMode.EaseOut;
                    contentDisplay.Opacity = 0.8;
                    daIn.From = -contentDisplay.ActualWidth;
                    daIn.To = 0;
                    daIn.EasingFunction = easeIn;
                    Storyboard.SetTarget(daIn, trans);
                    Storyboard.SetTargetProperty(daIn, new PropertyPath("X"));
                    sbIn.Children.Add(daIn);
                    break;
                case SwitchingType.Out:
                    sbOut = new Storyboard();
                    DoubleAnimation daOut = new DoubleAnimation(340, dur);
                    QuadraticEase easeOut = new QuadraticEase();
                    contentDisplay.Opacity = 0.5;
                    easeOut.EasingMode = EasingMode.EaseIn;
                    daOut.EasingFunction = easeOut;
                    Storyboard.SetTarget(daOut, trans);
                    Storyboard.SetTargetProperty(daOut, new PropertyPath("X"));
                    sbOut.Children.Add(daOut);
                    break;
            }
          }
        #endregion
    }
}
