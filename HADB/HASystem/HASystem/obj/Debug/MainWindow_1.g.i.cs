﻿#pragma checksum "..\..\MainWindow_1.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EA93B267C837F12333D98E39635E349B"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using HASystem;
using HASystem.Panels;
using HASystem.UC;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace HASystem {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 33 "..\..\MainWindow_1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HASystem.UC.btnUserControl rdiTest;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\MainWindow_1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HASystem.UC.btnUserControl rdiData;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\MainWindow_1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HASystem.UC.btnUserControl rdiUser;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\MainWindow_1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid contentDisplay;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\MainWindow_1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.TranslateTransform trans;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\MainWindow_1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HASystem.Panels.GoodsTestPanel panelGoodsTest;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\MainWindow_1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HASystem.Panels.DataManagerPanel panelDataManager;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\MainWindow_1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HASystem.Panels.ModelManagerPanel panelModelManager;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\MainWindow_1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HASystem.Panels.UserManagerPanel panelUserManager;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\MainWindow_1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HASystem.Panels.SystemManagerPanel panelSystemManager;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HASystem;component/mainwindow_1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow_1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.rdiTest = ((HASystem.UC.btnUserControl)(target));
            return;
            case 2:
            this.rdiData = ((HASystem.UC.btnUserControl)(target));
            return;
            case 3:
            this.rdiUser = ((HASystem.UC.btnUserControl)(target));
            return;
            case 4:
            this.contentDisplay = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.trans = ((System.Windows.Media.TranslateTransform)(target));
            return;
            case 6:
            this.panelGoodsTest = ((HASystem.Panels.GoodsTestPanel)(target));
            return;
            case 7:
            this.panelDataManager = ((HASystem.Panels.DataManagerPanel)(target));
            return;
            case 8:
            this.panelModelManager = ((HASystem.Panels.ModelManagerPanel)(target));
            return;
            case 9:
            this.panelUserManager = ((HASystem.Panels.UserManagerPanel)(target));
            return;
            case 10:
            this.panelSystemManager = ((HASystem.Panels.SystemManagerPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

