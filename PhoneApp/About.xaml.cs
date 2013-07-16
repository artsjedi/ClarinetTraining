using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
 

namespace PinNotes
{
    public partial class About : PhoneApplicationPage
    {
        public About()
        {
            InitializeComponent();
        }

        #region About bts


        private void RateBt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new MarketplaceReviewTask().Show();
        }

        private void FacebookBt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new WebBrowserTask() { URL = "http://www.facebook.com/Lirum.Labs" }.Show();
        }

        private void TwitterBt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new WebBrowserTask() { URL = "http://twitter.com/lirumlabs" }.Show();
        }

        private void WebBt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new WebBrowserTask() { URL = "http://www.lirumlabs.com" }.Show();
        }

        private void LirumBt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new MarketplaceSearchTask() { SearchTerms = "Lirum" }.Show();
        }

        //private void Panorama_SelectionChanged_1(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //{
        //    int i;
        //    if (sender is Pivot) i = (sender as Pivot).SelectedIndex;
        //    else i = (sender as Panorama).SelectedIndex;
        //
        //    if (i == 0)
        //        ApplicationBar.IsVisible = true;
        //    else
        //        ApplicationBar.IsVisible = false;
        //}
        #endregion
    }
}