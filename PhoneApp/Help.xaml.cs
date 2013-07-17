using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ClarinetTraining
{
    public partial class Help : PhoneApplicationPage
    {
        public Help()
        {
            InitializeComponent();

            var isLightTheme = (Visibility)Application.Current.Resources["PhoneLightThemeVisibility"] ==  System.Windows.Visibility.Visible;
          
           if (isLightTheme)
           {
               image1d.Visibility = System.Windows.Visibility.Collapsed;
               image2d.Visibility = System.Windows.Visibility.Collapsed;
           
               image1l.Visibility = System.Windows.Visibility.Visible;
               image2l.Visibility = System.Windows.Visibility.Visible;
           }
           else
           {
               image1d.Visibility = System.Windows.Visibility.Visible;
               image2d.Visibility = System.Windows.Visibility.Visible;
           
               image1l.Visibility = System.Windows.Visibility.Collapsed;
               image2l.Visibility = System.Windows.Visibility.Collapsed;
           }
        }

        private void Image_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Page1.Begin();
        }

        private void Image_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	NavigationService.GoBack();
        }

    }
}