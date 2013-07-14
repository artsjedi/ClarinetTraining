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

    public partial class Sheet : UserControl
    {
		private int spacing = 16;
		private int initialoffset  = 4;
        private int totalnotes = 26; // A4 
        private int scaleCount = 7;

        //private int[] hamonicScaleIntervals = new int[] {2,2,1,2,2,2};
        // from E1 to A4 

        public Sheet()
        {
            InitializeComponent();
            hideNote();
        }
		
		public void getCurrentNote(){
		}

        /// <summary>
        /// Shows the keys
        /// </summary>
        /// <param name="key">Key value. </param>
        public void showKey(int key)
        {
        }

        /// <summary>
        /// hides a note from the sheet
        /// </summary>
        public void hideNote()
        {
            Canvas.SetTop(Sus, -1000);
            Canvas.SetTop(Bmol, -1000);
            Canvas.SetTop(noteSymbol, -1000);
            NoteName.Text = "";

        }

        /// <summary>
        /// show note on sheet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="note"></param>
        /// <param name="scale"></param>
		public void showNote(Note n){
            
            int noteValue = n.note + n.scale * scaleCount;
            int topValue = totalnotes - noteValue;

            NoteIn.Begin();

            Canvas.SetTop(Sus, topValue * spacing + initialoffset - 15);
            Canvas.SetTop(Bmol, topValue * spacing + initialoffset - 15);

            Canvas.SetTop(noteSymbol ,topValue  * spacing + initialoffset );

            Sus.Visibility = n.sus ? Visibility.Visible : Visibility.Collapsed;
            Bmol.Visibility = n.bmol ? Visibility.Visible : Visibility.Collapsed;

            NoteName.Text = n.ToString();
		}
  
        
    }
}
