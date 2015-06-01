using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SimpleFilter.Control
{
    public partial class Loading : UserControl
    {
        public Loading()
        {
            InitializeComponent();
            SetVisible(Visibility.Collapsed);
        }

        public void SetVisible(Visibility visible)
        {
            this.Visibility = visible;
            if (visible == Visibility.Visible) sbloading.Begin();
            else sbloading.Stop();
        }
    }
}
