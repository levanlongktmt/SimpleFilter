﻿#pragma checksum "D:\Work\WindowsPhone\SimpleFilter\SimpleFilter\Control\FilterImage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6EEFC4C427EB73D930B7840FD81485AF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace SimpleFilter.Control {
    
    
    public partial class FilterImage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Image btnRotation;
        
        internal System.Windows.Controls.Image btnRotation2;
        
        internal System.Windows.Controls.Image btnFlipHorizontal;
        
        internal System.Windows.Controls.Image btnFlipVertical;
        
        internal System.Windows.Controls.Image processImage;
        
        internal System.Windows.Controls.TextBlock tbEmpty;
        
        internal System.Windows.Controls.ListBox lsbFilterList;
        
        internal System.Windows.Controls.Image imgNew;
        
        internal System.Windows.Controls.Image imgSave;
        
        internal System.Windows.Controls.Image imgShare;
        
        internal System.Windows.Controls.Image imgClear;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SimpleFilter;component/Control/FilterImage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.btnRotation = ((System.Windows.Controls.Image)(this.FindName("btnRotation")));
            this.btnRotation2 = ((System.Windows.Controls.Image)(this.FindName("btnRotation2")));
            this.btnFlipHorizontal = ((System.Windows.Controls.Image)(this.FindName("btnFlipHorizontal")));
            this.btnFlipVertical = ((System.Windows.Controls.Image)(this.FindName("btnFlipVertical")));
            this.processImage = ((System.Windows.Controls.Image)(this.FindName("processImage")));
            this.tbEmpty = ((System.Windows.Controls.TextBlock)(this.FindName("tbEmpty")));
            this.lsbFilterList = ((System.Windows.Controls.ListBox)(this.FindName("lsbFilterList")));
            this.imgNew = ((System.Windows.Controls.Image)(this.FindName("imgNew")));
            this.imgSave = ((System.Windows.Controls.Image)(this.FindName("imgSave")));
            this.imgShare = ((System.Windows.Controls.Image)(this.FindName("imgShare")));
            this.imgClear = ((System.Windows.Controls.Image)(this.FindName("imgClear")));
        }
    }
}

