using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using test = HASystem.StaticClass.TestResultInfo;

namespace HASystem.UC
{
    /// <summary>
    /// PassageWayUC.xaml 的交互逻辑
    /// </summary>
    public partial class PassageWayUC : UserControl
    {
        private ObservableCollection<test.Result> list = new ObservableCollection<test.Result>();


        public PassageWayUC()
        {
            
            InitializeComponent();
        }

        public void SetResult()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            list = test.SetTestInfo();
            //list = mi.GetModelData();
            dgDG.ItemsSource = list;
        }
    }
}
