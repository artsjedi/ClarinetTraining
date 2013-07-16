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

            list = new string[] { "E1", "E1+", "F1", "F1+", "F#1", "F#1+", "G1", "G#1", "A1", "A#1", "B1", "B1+", "C2", "C#2", "D2", "D#2", "D#2+", "E2", "F2", "F#2", "F#2+", "G2", "G#2", "A2", "A#2", "A#2+", "B2", "B2+", "C3", "C3+", "C#3", "C#3+", "D3", "D#3", "E3", "F3", "F#3", "F#3+", "G3", "G#3", "A3", "A#3", "A#3+", "B3", "C4", "C#4", "C#4+", "D4", "D4+", "D#4", "D#4+", "E4", "F4", "F4+", "F#4", "F#4+", "G4", "G4+", "G#4", "G#4+", "A4" };

            
            
            
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            //DictionaryList1.DataContext = new string[] { "a", "b", "c" };
            foreach (var i in list)
            {
                var di = new DictionaryItem();

                itensContainer.Children.Add(di);
                di.setNote(i);
            }
            wait_text.Visibility = System.Windows.Visibility.Collapsed; 
            itensContainer.UpdateLayout();
        }
    }
}