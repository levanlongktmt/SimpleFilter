﻿#pragma checksum "D:\Work\WindowsPhone\SimpleFilter\SimpleFilter\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "715E47923D7A88B8C3AA81C49517F55C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using SimpleFilter.Control;
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


namespace SimpleFilter {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard showControlFilter;
        
        internal System.Windows.Media.Animation.Storyboard hidecontrolFilter;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid StartLayout;
        
        internal SimpleFilter.Control.FilterImage controlFilter;
        
        internal SimpleFilter.Control.Loading controlLoading;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SimpleFilter;component/MainPage.xaml", System.UriKind.Relative));
            this.showControlFilter = ((System.Windows.Media.Animation.Storyboard)(this.FindName("showControlFilter")));
            this.hidecontrolFilter = ((System.Windows.Media.Animation.Storyboard)(this.FindName("hidecontrolFilter")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.StartLayout = ((System.Windows.Controls.Grid)(this.FindName("StartLayout")));
            this.controlFilter = ((SimpleFilter.Control.FilterImage)(this.FindName("controlFilter")));
            this.controlLoading = ((SimpleFilter.Control.Loading)(this.FindName("controlLoading")));
        }
    }
}

