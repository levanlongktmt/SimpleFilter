using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SimpleFilter.Resources;
using SimpleFilter.Control;

namespace SimpleFilter
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            InitEventHandler();
        }


        public void InitEventHandler()
        {
            controlFilter.OnBeginProcess += controlFilter_OnBeginProcess;
            controlFilter.OnCompletedProcess += controlFilter_OnCompletedProcess;
            controlFilter.OnPressBack += controlFilter_OnPressBack;
        }

        void controlFilter_OnPressBack(object sender, EventArgs e)
        {
            hidecontrolFilter.Begin();
            hidecontrolFilter.Completed += (s, x) =>
            {
                controlFilter.SetVisible(Visibility.Collapsed);
                hidecontrolFilter.Stop();

            };
        }

        void controlFilter_OnCompletedProcess(object sender, EventArgs e)
        {
            controlLoading.SetVisible(Visibility.Collapsed);
        }

        void controlFilter_OnBeginProcess(object sender, EventArgs e)
        {
            controlLoading.SetVisible(Visibility.Visible);
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NinePatchImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if((sender as NinePatchImage).Tag.ToString() == "Filter")
            {
                controlFilter.SetVisible(Visibility.Visible);
                showControlFilter.Begin();
                showControlFilter.Completed += (s, x) => { showControlFilter.Stop(); };
            }
            
        }
        
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            if(controlFilter.Visibility==Visibility.Visible)
            {
                hidecontrolFilter.Begin();
                hidecontrolFilter.Completed += (s, x) =>
                    {
                        controlFilter.SetVisible(Visibility.Collapsed);
                        hidecontrolFilter.Stop();
                        
                    };
                return;
            }
            else
            {
                App.Current.Terminate();
            }
        }
    }
}