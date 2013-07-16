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
    public partial class DictionaryItem : UserControl
    {
        public DictionaryItem()
        {
            InitializeComponent();
        }

        public void setNote(string n){
            sheet.showNote(new Note(n));
            clarinet.showNote(n);
        }
    }
}
