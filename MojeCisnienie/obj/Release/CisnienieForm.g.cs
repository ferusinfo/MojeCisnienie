﻿#pragma checksum "C:\Users\ferus\Documents\Visual Studio 2013\Projects\MojeCisnienie\MojeCisnienie\CisnienieForm.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "EFFF4D248C670F2BCB5888BDEB287A60"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MojeCisnienie {
    
    
    public partial class CisnienieForm : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBox RozkurczoweText;
        
        internal System.Windows.Controls.TextBox NotatkaText;
        
        internal System.Windows.Controls.TextBox SkurczoweText;
        
        internal System.Windows.Controls.TextBox TetnoText;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MojeCisnienie;component/CisnienieForm.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.RozkurczoweText = ((System.Windows.Controls.TextBox)(this.FindName("RozkurczoweText")));
            this.NotatkaText = ((System.Windows.Controls.TextBox)(this.FindName("NotatkaText")));
            this.SkurczoweText = ((System.Windows.Controls.TextBox)(this.FindName("SkurczoweText")));
            this.TetnoText = ((System.Windows.Controls.TextBox)(this.FindName("TetnoText")));
        }
    }
}

