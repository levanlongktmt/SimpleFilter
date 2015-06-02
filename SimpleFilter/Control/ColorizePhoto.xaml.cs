using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;

namespace SimpleFilter.Control
{
    public partial class ColorizePhoto : PhoneApplicationPage
    {
        WriteableBitmap sourceImage;
        WriteableBitmap displayImage;
        bool isProcess = false;
        public ColorizePhoto()
        {
            InitializeComponent();
        }

        private void imgClear_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void imgShare_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void imgSave_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }

        private void imgNew_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
    }
}