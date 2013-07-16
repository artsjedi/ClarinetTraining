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
    public partial class Dictionary : PhoneApplicationPage
    {

        public string[] list;

        public Dictionary()
        {
            InitializeComponent();

            // list = new string[] {"a","b","c"};

            
            
            
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            DictionaryList1.DataContext = new string[] { "a", "b", "c" };
        }
    }
}