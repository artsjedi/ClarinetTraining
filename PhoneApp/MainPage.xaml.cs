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

namespace ClarinetTraining
{
    public partial class MainPage : PhoneApplicationPage
    {
        private int currentScale = 0;
        private int currentUpper = 29;
        private int currentLower = 2;
		
		private int _delay = 2000; //ms
        private DispatcherTimer timer;
        private bool playing=false;
        bool soundOn = true;
        ClarinetSound sound;

		private int[] harmonicIntervals = new int[] { 2, 2, 1, 2, 2, 2, 1 };
        private int[] harmonicDistances = new int[] { 0, 2, 4, 5, 7, 9, 11, 12 };
            
        private int[] chromaticNot = new int[] { 0, 0, 1, 1, 2, 3, 3, 4, 4, 5, 5, 6 };
        private int[] chromaticSus = new int[] { 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1, 0 };

        //
        private string[] scalesNames = { "C maj", "D maj", "E maj", "F maj", "G maj", "A maj", "B maj" };
        private string[] intervalsText = { "1/2 s", "1s", "2s", "4s", "8s", "16s"};

        private int[] intervalsValues = { 500, 1000, 2000, 3000, 5000, 7000, 15000, 30000 };

        private Random rnd = new Random();

        public MainPage()
        {
            
            InitializeComponent();


            setlists();
            timer = new DispatcherTimer();
            timer.Tick += tick;
            setInterval(_delay);
            sound = new ClarinetSound();
        }

        private void setlists()
        {
            foreach (string i in scalesNames)
                ScaleList.Items.Add(i);

            foreach (string i in intervalsText)
                IntervalList.Items.Add(i);
        }

        private void setInterval(int delay){
           if(timer!=null) timer.Interval = new TimeSpan(0, 0,0,0,delay); 
        }

        private void startShowing()
        {
            timer.Start();
        }

        private void stopShowing()
        {
            timer.Stop();
            sheet.hideNote();
        }

        private void tick(Object sender, EventArgs e){
            var n = randomNote(currentScale, currentUpper, currentLower);
            displayNote(n);
            
        }


        private Note randomNote(int scale=-1,int upper=29, int lower=2){
            var n = new Note();

            //select note
            //harmonic Scale
            if (scale <=6 )
            {

                var chromaticId = (harmonicDistances[scale] + harmonicDistances[rnd.Next(7)]) % 12;
                n.note = chromaticNot[chromaticId];
                n.sus = chromaticSus[chromaticId]==1;
                n.scale = rnd.Next(4);

            }
            //Any Scale
            else
            {

                n.note = rnd.Next(7);
                n.scale = rnd.Next(4);
                var sus = rnd.Next(6);

                switch (sus)
                {
                    case 0: n.sus = true; break;
                    case 1: n.bmol = true; break;
                }

                System.Diagnostics.Debug.WriteLine("n:" + n.note + " s:" + n.scale);
                
            }

            //limit-it
            while (n.note + n.scale * 7 > upper) n.scale--;
            while (n.note + n.scale * 7 < lower) n.scale++;
          
            return n;
        }
        
        private void displayNote(Note n){
            
            sheet.showNote(n);
            if(!clarinet.showNote(n))tick(null, null);
            if(soundOn)sound.playNote(n);
        }

        #region callbacks

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            
            var on =    new Uri("/Assets/AppBar/sound on.png",UriKind.Relative);
            var off =   new Uri("/Assets/AppBar/sound off.png",UriKind.Relative);

            soundOn = !soundOn;
            (sender as ApplicationBarIconButton).IconUri = soundOn ? on : off;
        }

		private void hideLists(){
            try
            {
                //listsBackground.Visibility = System.Windows.Visibility.Collapsed;
                //ScaleList.Visibility = System.Windows.Visibility.Collapsed;
                //IntervalList.Visibility = System.Windows.Visibility.Collapsed;
                CloseList.Begin();
                ApplicationBar.IsVisible = true;
            }
            catch
            {
            }
		}

        private void showLists()
        {

        }

        private void ScaleButton_Click(object sender, System.EventArgs e)
        {
            ApplicationBar.IsVisible = false;
			ScaleList.Visibility = System.Windows.Visibility.Visible;
            IntervalList.Visibility = System.Windows.Visibility.Collapsed;
            RangeList.Visibility = System.Windows.Visibility.Collapsed;
            OpenList.Begin();
        }

        private void IntervalButton_Click(object sender, System.EventArgs e)
        {
            ApplicationBar.IsVisible = false;
			ScaleList.Visibility = System.Windows.Visibility.Collapsed;
            RangeList.Visibility = System.Windows.Visibility.Collapsed;
            IntervalList.Visibility = System.Windows.Visibility.Visible;
            OpenList.Begin();
        }


        private void RangeButton_Click(object sender, System.EventArgs e)
        {
            ApplicationBar.IsVisible = false;
            ScaleList.Visibility = System.Windows.Visibility.Collapsed;
            RangeList.Visibility = System.Windows.Visibility.Visible;
            IntervalList.Visibility = System.Windows.Visibility.Collapsed;
            OpenList.Begin();
        }

        private void IntervalList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        	hideLists();
            var i = (sender as ListBox).SelectedIndex;
            setInterval(intervalsValues[i]);

        }

        private void ScaleList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            var i = (sender as ListBox).SelectedIndex;
            currentScale = i;
        	hideLists();
        }

		private void RangeList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        	switch( (sender as ListBox).SelectedIndex){
				case 0: 
					currentLower = 2;
					currentUpper = 29;
					break;
				case 1: 
					currentUpper= 29;
					currentLower=18;
					break;
				case 2: 
					currentUpper= 17;
					currentLower=9;
					break;
				case 3: 
					currentUpper= 9;
					currentLower=2;
					break;
			}
			hideLists();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {

            if( ApplicationBar.IsVisible == false){
                hideLists();
                e.Cancel = true;
            }
            base.OnBackKeyPress(e);
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
        	
        }

        #endregion

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

        private void ApplicationBarMenuItem_Click_1(object sender, System.EventArgs e)
        {
            var uri = new Uri("/Dictionary.xaml", UriKind.Relative);
            NavigationService.Navigate(uri);
				
        }
    }
}
