using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using mi = HASystem.StaticClass.ModelInfo;

namespace HASystem.UC
{
    /// <summary>
    /// PassageWayUC.xaml 的交互逻辑
    /// </summary>
    public partial class PassageWayUC : UserControl
    {
        private ObservableCollection<mi.ModelResult> list;
        public PassageWayUC()
        {
            
            InitializeComponent();
            SetResult();
        }

        private void SetResult()
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return;


            list = mi.GetModelData();
            dgDG.ItemsSource = list;
        }
    }
}
