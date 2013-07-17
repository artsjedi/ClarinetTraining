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
        ClarinetSound sound = new ClarinetSound();
        Note currentNote;
        public DictionaryItem()
        {
            InitializeComponent();
        }

        public void setNote(string n){
            var note = new Note(n);
            currentNote = note;
            sheet.showNote(currentNote);
            clarinet.showNote(n);
            NoteName.Text = note.ToString("");
        }

        private void playbt_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            sound.playNote(currentNote);
        }
    }
}
