﻿#pragma checksum "..\..\..\Panels\CommunicationPanel.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D123A1260F982851D163101984520B0A"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using HASystem.Panels;
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


namespace HASystem.Panels {
    
    
    /// <summary>
    /// CommunicationPanel
    /// </summary>
    public partial class CommunicationPanel : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 81 "..\..\..\Panels\CommunicationPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboModel;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\Panels\CommunicationPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboSerial;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\Panels\CommunicationPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comBoBaueRate;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\Panels\CommunicationPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comBoStopBit;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\Panels\CommunicationPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboParityBit;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\..\Panels\CommunicationPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comBoDataBit;
        
        #line default
        #line hidden
        
        
        #line 147 "..\..\..\Panels\CommunicationPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rdiSerial;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\Panels\CommunicationPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rdiNet;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\..\Panels\CommunicationPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
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
            System.Uri resourceLocater = new System.Uri("/HASystem;component/panels/communicationpanel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Panels\CommunicationPanel.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.comboModel = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.comboSerial = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.comBoBaueRate = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.comBoStopBit = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.comboParityBit = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.comBoDataBit = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.rdiSerial = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.rdiNet = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 9:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 150 "..\..\..\Panels\CommunicationPanel.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

