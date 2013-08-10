using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Threading;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace ClarinetTraining
{
    public partial class MainPage : PhoneApplicationPage
    {
        private int currentScale = 0;
        private int currentUpper = 29;
        private int currentLower = 2;
        private bool currentNameVisibility = true;
        private int currentTranspose = 0;

		private int _delay = 2000; //ms
        private DispatcherTimer timer;
        private bool playing=false;
        bool soundOn = true;
        ClarinetSound sound;

        private int[] harmonicIntervals = new int[] { 0, 2, 2, 1, 2, 2, 02, 01 };
        private int[] harmonicDistances = new int[] { 0, 2, 4, 5, 7, 9, 11, 12 };
            
        private int[] chromaticNot = new int[] { 0, 0, 1, 1, 2, 3, 3, 4, 4, 5, 5, 6 };
        private int[] chromaticSus = new int[] { 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0 };

        private int[] intervalsValues = { 250, 500, 1000, 1500, 2000, 3000, 5000, 7000, 15000, 30000 };

        private int[] notesId = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };

        private Random rnd = new Random();


        //trial check
        private bool formLoaded = false;
        private bool unlocked = false;
        
#if DEBUG
        private bool trialfake = true;
#endif


        public MainPage()
        {
            
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += tick;
            setInterval(_delay);
            sound = new ClarinetSound();
            PageIn.Begin();


        }

        private void verifyFirstTime(){
            bool visited;
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("visited", out visited);

            if (!visited)
            {
                IsolatedStorageSettings.ApplicationSettings.Add("visited", true);
                IsolatedStorageSettings.ApplicationSettings.Save();

                var uri = new Uri("/Help.xaml", UriKind.Relative);
                NavigationService.Navigate(uri);
            }



        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            loadCondiguration();

            verifyFirstTime();

            formLoaded = true;
        }

        private void setNameVisibility(bool visible)
        {
            currentNameVisibility =visible;
            if (visible)
                NoteName.Opacity = 1;
            else
                NoteName.Opacity = 0;
        }

        private void setInterval(int delay)
        {
            if (timer != null) timer.Interval = new TimeSpan(0, 0, 0, 0, delay);
        }
        
        private void setSound(bool value)
        {
            soundOn = value;

            if (!soundOn)
            {
                soundOnIcon.Visibility = System.Windows.Visibility.Collapsed;
                SoundOffIcon.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                soundOnIcon.Visibility = System.Windows.Visibility.Visible;
                SoundOffIcon.Visibility = System.Windows.Visibility.Collapsed;
            }


            //var on =    new Uri("/Assets/AppBar/sound on.png",UriKind.Relative);
            //var off =   new Uri("/Assets/AppBar/sound off.png",UriKind.Relative);

            //(ApplicationBar.Buttons[0] as ApplicationBarIconButton).IconUri = value ? on : off;
        }

        private void setTranspose(int tranpose)
        {
            this.currentTranspose = tranpose;
        }

        private void startShowing()
        {
            tick(null, null);
            timer.Start();
        }

        private void stopShowing()
        {
            timer.Stop();
        }

        /// <summary>
        /// Shows a note 
        /// </summary>
        /// <param name="n"></param>
        private void displayNote(Note n)
        {
            sheet.showNote(n);
            clarinet.showNote(n);
            NoteName.Text = n.ToString("");
            if (soundOn) sound.playNote(n,currentTranspose);
        }

        /// <summary>
        /// Display a Rando note
        /// </summary>
        /// <param name="n"></param>
        private void displayRandomNote()
        {
            Note n;
            do
                n = randomNote(currentScale, currentUpper, currentLower);
            while (!clarinet.showNote(n));
                
            sheet.showNote(n);
            NoteName.Text = n.ToString("");
            if (soundOn) sound.playNote(n, currentTranspose);
        }

        /// <summary>
        /// timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tick(Object sender, EventArgs e){
            displayRandomNote();
        }

        #region /////////////////////////////////////////////////// Saved Configuration //////////////////////////

        /// <summary>
        /// Loads user configuration and configures UI
        /// </summary>
        private void loadCondiguration()
        {
            bool inverted = false;
            bool sound = true;
            int timeInterval = 3;
            int scale = 0;
            int rangeMin = 0;
            int rangeMax = 0;
            bool NameVisibility = false;
            int tunning = 1;
            
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("inverted", out inverted);
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("scale", out scale);

            if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue("tunning", out tunning))
                tunning = 1;

            if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue("rangeMin", out rangeMin))
                rangeMin = 8;

            if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue("rangeMax", out rangeMax))
                rangeMax = 18;
            
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue("sound", out sound))
                setSound(sound);

            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue("nameVisibility", out NameVisibility))
                setNameVisibility(NameVisibility);

            if (!IsolatedStorageSettings.ApplicationSettings.TryGetValue("time", out timeInterval)) 
                timeInterval = 3;

            RangeMin.SelectedIndex = rangeMin;
            RangeMax.SelectedIndex = rangeMax;
            IntervalList.SelectedIndex = timeInterval;
            ScaleList.SelectedIndex = scale;
            clarinet.setInverted(inverted);
            VoiceList.SelectedIndex = tunning;
        }
        #endregion

        #region future another class
        /////////////////////////////////////////////////// this below shoud be in another class /////////
        
        /// <summary>
        /// sets the Current scale
        /// </summary>
        /// <param name="scale"></param>
        private void setScale(int scale)
        {
            
            this.currentScale = scale;
            if (sheet == null) return;
            sheet.setScale(scale);
        }

        /// <summary>
        /// Get the variant note in a specific scale.
        /// </summary>
        /// <param name="scale">Scale</param>
        /// <param name="note">Notes</param>
        /// <returns>Return a new Note and if it is Bmol or Sustenide</returns>
        private Note getNoteVariantOnScale(int scale, int note)
        {                                                                                      
                                                                                                    //n s
            var n = new Note();                                                                     //3 5 
                                                                                                    //3 5 -2|5 note on Scale
            var noteOnScale = ((note - scale)+7) % 7;                                               //
                                                                                                    //4 5 5 
            var chromaticId = (harmonicDistances[scale] + harmonicDistances[noteOnScale]) % 12;     //3 5 5 18|6
            var diff = chromaticId - harmonicDistances[note];                                    //3 5 5 6 5
                                                                                                    //
            n.note = note;                                                                       //
                                                                                                    //
            if (diff == 1) n.sus = true;                                                            //
            if (diff == -1) n.bmol = true;                                                          //
                                                                                                    //
            return n;                                                                               //
        }

        /// <summary>
        /// Select a random note in a scale(or not)
        /// </summary>
        /// <param name="scale">Scale</param>
        /// <param name="upper">upper limit</param>
        /// <param name="lower">lower limit</param>
        /// <returns>a rando note according with parameters.</returns>
        private Note randomNote(int scale=-1,int upper=29, int lower=2){
            
            var n = new Note();
            var noteId = lower + rnd.Next(upper+1 - lower);

            var note = noteId % 7;
            var octave = Convert.ToInt16(noteId/7);

            //select note
            //harmonic Scale
            if (scale <=6 )
            {
                n = getNoteVariantOnScale(scale,note);
                n.octave = octave;
            }
            //Any Scale
            else
            {
                var sus = rnd.Next(6);
                n.note = note;
                n.octave = octave;

                switch (sus)
                {
                    case 0: n.sus = true; break;
                    case 1: n.bmol = true; break;
                }

                //System.Diagnostics.Debug.WriteLine("n:" + n.note + " s:" + n.scale);
                
            }

            //limit-it
            // while (n.note + n.octave * 7 > upper) n.octave--;
            // while (n.note + n.octave * 7 < lower) n.octave++;
          
            return n;
        }
    #endregion

        #region callbacks

        private void SoundOnOffClick(object sender, EventArgs e)
        {
            
            setSound(!soundOn); 


            IsolatedStorageSettings.ApplicationSettings["sound"] = soundOn;
            IsolatedStorageSettings.ApplicationSettings.Save();

        }

		private void hideLists(){
            try
            {
                //listsBackground.Visibility = System.Windows.Visibility.Collapsed;
                CloseList.Begin();
                ApplicationBar.IsVisible = true;
            }
            catch
            {
            }
		}

        private void ScaleButton_Click(object sender, System.EventArgs e)
        {
            ///ApplicationBar.IsVisible = false;
            listsBackground.Visibility = System.Windows.Visibility.Visible;
			ScaleList.Visibility = System.Windows.Visibility.Visible;
            IntervalList.Visibility = System.Windows.Visibility.Collapsed;
            RangeList.Visibility = System.Windows.Visibility.Collapsed;
            VoiceList.Visibility = System.Windows.Visibility.Collapsed;
            OpenList.Begin();


        }

        private void IntervalButton_Click(object sender, System.EventArgs e)
        {
            ///ApplicationBar.IsVisible = false;
            listsBackground.Visibility = System.Windows.Visibility.Visible;
			ScaleList.Visibility = System.Windows.Visibility.Collapsed;
            RangeList.Visibility = System.Windows.Visibility.Collapsed;
            IntervalList.Visibility = System.Windows.Visibility.Visible;
            VoiceList.Visibility = System.Windows.Visibility.Collapsed;
            OpenList.Begin();
        }

        private void RangeButton_Click(object sender, System.EventArgs e)
        {
            ///ApplicationBar.IsVisible = false;
            listsBackground.Visibility = System.Windows.Visibility.Visible;
            ScaleList.Visibility = System.Windows.Visibility.Collapsed;
            RangeList.Visibility = System.Windows.Visibility.Visible;
            IntervalList.Visibility = System.Windows.Visibility.Collapsed;
            VoiceList.Visibility = System.Windows.Visibility.Collapsed;
            OpenList.Begin();
        }

        private void VoiceList_Click(object sender, System.EventArgs e)
        {
            ///ApplicationBar.IsVisible = false;
            listsBackground.Visibility = System.Windows.Visibility.Visible;
            ScaleList.Visibility = System.Windows.Visibility.Collapsed;
            RangeList.Visibility = System.Windows.Visibility.Collapsed;
            IntervalList.Visibility = System.Windows.Visibility.Collapsed;
            VoiceList.Visibility = System.Windows.Visibility.Visible;
            OpenList.Begin();
        }

        private void VoiceList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        	hideLists();
            int i = (sender as ListBox).SelectedIndex;
            int transpose = 0;
            switch (i)
            {
                case 0: transpose = 0; TuneName.Text = "Natural"; break;  //Natural
                case 1: transpose = -2; TuneName.Text = "Bb"; break; //Bb
                case 2: transpose = +3;  TuneName.Text = "Eb";break; //Eb
            }

            setTranspose(transpose);

            IsolatedStorageSettings.ApplicationSettings["tunning"] = i;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void IntervalList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        	hideLists();
            var i = (sender as ListBox).SelectedIndex;
            setInterval(intervalsValues[i]);

            IsolatedStorageSettings.ApplicationSettings["time"] = i;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void ScaleList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (trialVerify()) return;

            var i = (sender as ListBox).SelectedIndex;
            setScale(i);
        	hideLists();


            IsolatedStorageSettings.ApplicationSettings["scale"] = i;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void RangeListMin_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (trialVerify()) return;

            var listIndex = (sender as ListBox).SelectedIndex;
            //hideLists();

            if (RangeMax == null) return;
            for (var i = 0; i < RangeMax.Items.Count; i++)
            {
                if (i <= listIndex)
                {
                    (RangeMax.Items[i] as ListBoxItem).IsEnabled = false;
                    (RangeMax.Items[i] as ListBoxItem).IsSelected = false;
                }
                else
                {
                    (RangeMax.Items[i] as ListBoxItem).IsEnabled = true;
                }
            }

            currentLower = notesId[listIndex];

            IsolatedStorageSettings.ApplicationSettings["rangeMin"] = listIndex;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void RangeListMax_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (trialVerify()) return;

            var listIndex = (sender as ListBox).SelectedIndex;
			//hideLists();
            if (RangeMin == null) return;
            for (var i = 0; i < RangeMin.Items.Count; i++)
            {
                if (i >= listIndex)
                {

                    (RangeMin.Items[i] as ListBoxItem).IsEnabled = false;
                    (RangeMin.Items[i] as ListBoxItem).IsSelected = false;
                }
                else
                {
                    (RangeMin.Items[i] as ListBoxItem).IsEnabled = true;
                }
            }

            currentUpper = notesId[listIndex];

            IsolatedStorageSettings.ApplicationSettings["rangeMax"] = listIndex;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {

            if (listsBackground.Visibility == System.Windows.Visibility.Visible)
            {
                hideLists();
                e.Cancel = true;
            }
            base.OnBackKeyPress(e);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            playing = !playing;
            if (playing)
            {
                startShowing();

                PlaySymbol.Visibility = System.Windows.Visibility.Collapsed;
                PauseSymbol1.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                stopShowing();
                PlaySymbol.Visibility = System.Windows.Visibility.Visible;
                PauseSymbol1.Visibility = System.Windows.Visibility.Collapsed;

            }
        }


        /// Menus ///////////////////////////////////////////////////////////////////////////////
        
     
        private void About_Click(object sender, EventArgs e)
        {
            var uri = new Uri("/About.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void Dictionary_Click(object sender, EventArgs e)
        {
            var uri = new Uri("/Dictionary.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }

        private void Invert_Click(object sender, EventArgs e)
        {
            clarinet.setInverted(!clarinet.getInverted());

            IsolatedStorageSettings.ApplicationSettings["inverted"] = clarinet.getInverted();
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void NoteName_Tap(object sender, EventArgs e)
        {
            setNameVisibility(!currentNameVisibility);
            IsolatedStorageSettings.ApplicationSettings["nameVisibility"] = currentNameVisibility;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private void listsBackground_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	hideLists();
        }

        private void Help_Click(object sender, System.EventArgs e)
        {
        	var uri = new Uri("/Help.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
        }


        #endregion

        private bool trialVerify()
        {
            if (!formLoaded) return false;
            //verify license
            Microsoft.Phone.Marketplace.LicenseInformation li = new Microsoft.Phone.Marketplace.LicenseInformation();

            var isTrial = true;
            
            #if DEBUG
            isTrial = true;
            #else
            IsolatedStorageSettings.ApplicationSettings["nameVisibility"] = currentNameVisibility;
            isTrial = li.IsTrial();
            #endif


            if (isTrial)
            {
                var m = MessageBox.Show("This settings is only avaliable in full version. Would you like to purchase the full version?", "Trial Version", MessageBoxButton.OKCancel);
                if (m == MessageBoxResult.OK)
                    new Microsoft.Phone.Tasks.MarketplaceDetailTask().Show();
                return true;
            }
            return false;

        }
    }
}
