using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ClarinetTraining
{
    public partial class Clarinet : UserControl
    {



        private Note currentNote;

        public String Value
        {
            set
            {
                
            }
            get
            {
                return "";
            }
        }
        
        public Note NoteValue
        {
            set
            {
                this.currentNote = value;
                showNote(currentNote);
            }
            get
            {
                return currentNote;
            }
        }

		private bool inverted = false;

        private Dictionary<String, Boolean[]> clarinetDictionary;
        private Path[] keyShapes;

        public Clarinet()
        {
            InitializeComponent();

            keyShapes = new Path[] { kl0, kl1, kl2, kl3, kl4, kl5, kl6, kl7, kl8, kl9, kl10, kc1, h1, h2, h3, kc2, h4, h5, h6, kr1, kr2, kr3, kr4, kr5 };
            defineDictionary();
       }


        /// <summary>
        /// Shows a note in the Clarinet
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool showNote(string note){
            bool[] keys;
            if (clarinetDictionary.TryGetValue(note, out keys))
                activateKeys(keys);
            else
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + note);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Shows a note in the Clarinet
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool showNote(Note n, bool alternative=false){

            n = n.normalize();
            var str = n.ToString("s");
            if (alternative) str += "+";

            return showNote(str);
        }

        /// <summary>
        /// Activate all keys shapes
        /// </summary>
        /// <param name="keys">keys values array</param>
        private void activateKeys(bool[] keyValues)
        {
            for (int i = 0; i < keyShapes.Length; i++)
                activateKey(keyShapes[i], keyValues[i]);
            NoteIn.Begin();
        }


        /// <summary>
        /// activate or desactivete a single key shape
        /// </summary>
        /// <param name="key">key shape</param>
        /// <param name="activated">value</param>
        private void activateKey(Path key, bool activated)
        {
           
            var phoneAccentBrush = new SolidColorBrush((App.Current.Resources["PhoneAccentBrush"] as SolidColorBrush).Color);
            var phoneForeground = new SolidColorBrush((App.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush).Color);
            var phonechrome = new SolidColorBrush((App.Current.Resources["PhoneChromeBrush"] as SolidColorBrush).Color);
            var phoneBackground = new SolidColorBrush((App.Current.Resources["PhoneBackgroundBrush"] as SolidColorBrush).Color);

            if (activated)
            {
                //key.Fill = phoneForeground;
                //key.Stroke = phoneBackground;
                //key.StrokeThickness = 3;
                key.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                key.Visibility = System.Windows.Visibility.Collapsed;
                //key.Fill = phonechrome;
                //key.Stroke = phoneBackground;
                //key.StrokeThickness = 3;
            }

        }
         
        /// <summary>
        /// Defines the dictionary
        /// </summary>
        private void defineDictionary()
        {
            clarinetDictionary = new Dictionary<String, Boolean[]>();
            clarinetDictionary["E1"] = new bool[] { false, true, false, false, false, false, false, false, false, false, true, false, true, true, true, false, true, true, true, false, false, false, true, false };
            clarinetDictionary["E1+"] = new bool[] { false, true, false, false, false, false, false, false, true, false, false, false, true, true, true, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["F1"] = new bool[] { false, true, false, false, false, false, false, false, false, false, true, false, true, true, true, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["F1+"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, true, true, true, false, false, true, false, false };
            clarinetDictionary["F#1"] = new bool[] { false, true, false, false, false, false, false, false, false, false, true, false, true, true, true, false, true, true, true, false, false, false, false, true };
            clarinetDictionary["F#1+"] = new bool[] { false, true, false, false, false, false, false, true, false, false, false, false, true, true, true, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["G1"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["G#1"] = new bool[] { false, true, false, false, false, false, false, false, false, true, false, false, true, true, true, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["A1"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, true, true, false, false, false, false, false, false };
            clarinetDictionary["A#1"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, true, false, false, false, false, false, false, false };
            clarinetDictionary["B1"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, false, true, false, false, false, false, false, false };
            clarinetDictionary["B1+"] = new bool[] { false, true, false, false, false, false, true, false, false, false, false, false, true, true, true, false, true, false, false, false, false, false, false, false };
            clarinetDictionary["C2"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["C#2"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, true, true, true, true, false, false, false, false, false, false, false, false };
            clarinetDictionary["D2"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["D#2"] = new bool[] { false, true, false, false, false, true, false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["D#2+"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false, true, false, false, false };
            clarinetDictionary["E2"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["F2"] = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["F#2"] = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["F#2+"] = new bool[] { false, true, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["G2"] = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["G#2"] = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false };
            clarinetDictionary["A2"] = new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["A#2"] = new bool[] { true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["A#2+"] = new bool[] { false, false, false, true, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["B2"] = new bool[] { true, true, false, false, false, false, false, false, false, false, true, false, true, true, true, false, true, true, true, false, false, false, true, false };
            clarinetDictionary["B2+"] = new bool[] { true, true, false, false, false, false, false, false, true, false, false, false, true, true, true, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["C3"] = new bool[] { true, true, false, false, false, false, false, false, false, false, true, false, true, true, true, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["C3+"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, true, true, true, false, false, true, false, false };
            clarinetDictionary["C#3"] = new bool[] { true, true, false, false, false, false, false, false, false, false, true, false, true, true, true, false, true, true, true, false, false, false, false, true };
            clarinetDictionary["C#3+"] = new bool[] { true, true, false, false, false, false, false, true, false, false, false, false, true, true, true, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["D3"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["D#3"] = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, true, true, true, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["E3"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, true, true, false, false, false, false, false, false };
            clarinetDictionary["F3"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, true, false, false, false, false, false, false, false };
            clarinetDictionary["F#3"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, false, true, false, false, false, false, false, false };
            clarinetDictionary["F#3+"] = new bool[] { true, true, false, false, false, false, true, false, false, false, false, false, true, true, true, false, true, false, false, false, false, false, false, false };
            clarinetDictionary["G3"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, true, true, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["G#3"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, true, true, true, false, false, false, false, false, false, false, false };
            clarinetDictionary["A3"] = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["A#3"] = new bool[] { true, true, false, false, false, true, false, false, false, false, false, false, true, true, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["A#3+"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, true, false, false, false, false, false, false, true, false, false, false };
            clarinetDictionary["B3"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["C4"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["C#4"] = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, true, true, false, true, true, false, false, false, false, false, false };
            clarinetDictionary["C#4+"] = new bool[] { true, true, false, false, true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["D4"] = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, false, true, true, false, true, false, false, false, false, false, false, false };
            clarinetDictionary["D4+"] = new bool[] { true, true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["D#4"] = new bool[] { true, true, false, false, false, false, true, false, false, true, false, false, false, true, true, false, true, false, false, false, false, false, false, false };
            clarinetDictionary["D#4+"] = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, false, true, true, false, false, true, false, false, false, false, false, false };
            clarinetDictionary["E4"] = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, false, true, true, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["F4"] = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, false, true, true, true, false, false, false, false, false, false, false, false };
            clarinetDictionary["F4+"] = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, true, true, true, true, true, true, true, false, false, false, false, false };
            clarinetDictionary["F#4"] = new bool[] { true, true, false, false, false, false, false, false, true, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false };
            clarinetDictionary["F#4+"] = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, true, true, false, false, true, true, true, false, false, false, false, false };
            clarinetDictionary["G4"] = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, false, true, false, false, true, true, false, false, false, false, false, false };
            clarinetDictionary["G4+"] = new bool[] { true, false, false, false, false, false, false, false, false, true, false, false, false, true, false, false, true, true, false, false, false, false, false, false };
            clarinetDictionary["G#4"] = new bool[] { true, true, false, false, false, false, true, false, false, true, false, false, false, true, true, false, true, false, false, false, false, false, false, false };
            clarinetDictionary["G#4+"] = new bool[] { true, true, false, false, false, false, false, false, false, false, true, false, false, true, true, false, true, true, true, false, false, false, false, true };
            clarinetDictionary["A4"] = new bool[] { true, true, false, false, false, false, false, false, false, true, false, false, false, true, true, false, false, false, false, false, false, false, false, true };
  
        }


        public bool getInverted(){
            return inverted;
        }

        public void setInverted(bool inverted)
        {
            this.inverted = inverted;
            if (inverted)
                VisualStateManager.GoToState(this, "Inverted", true);
            else
                VisualStateManager.GoToState(this, "Normal", true);


            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings["inverted"] = this.getInverted();
            System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void Layer_1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            setInverted(!inverted);
        }


        ////

       
    }
}
