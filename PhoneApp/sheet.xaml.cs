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

        private bool isInScale = false;

        //lines
        private UIElement[] Sheetlines;
        private UIElement[] SheetupperLines;
        private UIElement[] SheetlowerLines;


        //private int[] hamonicScaleIntervals = new int[] {2,2,1,2,2,2};
        // from E1 to A4 

        public Sheet()
        {
            InitializeComponent();
            hideNote();

            Sheetlines = new UIElement[] {sheet_line1,sheet_line2,sheet_line3,sheet_line4,sheet_line5};
            SheetupperLines  = new UIElement[] {sheet_upper1,sheet_upper2,sheet_upper3,sheet_upper4};
            SheetlowerLines = new UIElement[] { sheet_lower1, sheet_lower2, sheet_lower3 };
            
        }
		
		public void getCurrentNote(){
            throw (new NotImplementedException());
		}

        /// <summary>
        /// hides a note from the sheet
        /// </summary>
        public void hideNote()
        {
            Canvas.SetTop(Sus, -1000);
            Canvas.SetTop(Bmol, -1000);
            Canvas.SetTop(noteSymbol, -1000);
             

        }

        /// <summary>
        /// sets scale within the note will be show
        /// </summary>
        /// <param name="scale">scale</param>
        public void setScale(int scale)
        {
            isInScale = (scale <= 6);
            UIElement[] scalesSymbols = new UIElement[] { null, scale_D, scale_E, scale_F, scale_G, scale_A, scale_B };
          
            for (var i = 1; i < scalesSymbols.Length; i++)
                if (scale == i) scalesSymbols[i].Visibility = System.Windows.Visibility.Visible;
                else scalesSymbols[i].Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// show note on sheet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="note"></param>
        /// <param name="scale"></param>
		public void showNote(Note n){
            
            int noteValue = n.note + n.octave * scaleCount;
            int topValue = totalnotes - noteValue;

            NoteIn.Begin();

            var noteTop= topValue  * spacing + initialoffset ;

            Canvas.SetTop(Sus, noteTop- 15);
            Canvas.SetTop(Bmol,noteTop - 15);
            Canvas.SetTop(noteSymbol ,noteTop);

            if (!isInScale)
            {
                Sus.Visibility = n.sus ? Visibility.Visible : Visibility.Collapsed;
                Bmol.Visibility = n.bmol ? Visibility.Visible : Visibility.Collapsed;
            }

            

            //show/hide upper/lower lines 
            foreach (var l in SheetupperLines)
                if (Canvas.GetTop(l) < noteTop -15)
                    l.Visibility = System.Windows.Visibility.Collapsed;
                else
                    l.Visibility = System.Windows.Visibility.Visible;

            foreach (var l in SheetlowerLines)
                if (Canvas.GetTop(l) > noteTop)
                    l.Visibility = System.Windows.Visibility.Collapsed;
                else
                    l.Visibility = System.Windows.Visibility.Visible;
            
		}
        

    }
}
