using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using controller;
using model;
using Visualisation_Applications;

namespace ViewButBetter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

     
        public  Timer AnimationTimer;
        static int AnimationFrames = 4;

        private bool _active;
        private CompetitionStatistics _compStatistic;
        private RaceStatistics _raceStatistics;
        public MainWindow()
        {
            Data.Initialize();

            
            Data.InalizeVisualization += Visualisation.PreCalculateGrid;

            Data.NextRace();

            Data.CurrentRace.RaceTimer.Elapsed += CurrentRace_DriversChanged;

            _active = false;
            SetTimer();
            startTimer();

            InitializeComponent();
        }

        #region TimerFunctions
        public  void startTimer()
        {
            AnimationTimer.Start();
        }
        private  void SetTimer()
        {
            AnimationTimer = new System.Timers.Timer(100);
            AnimationTimer.AutoReset = true;
            AnimationTimer.Elapsed += CurrentRace_DriversChanged;


        }

       
        #endregion TimerFunctions


        public void CurrentRace_DriversChanged(object? sender, EventArgs e)
        {
           if(this.Image != null) { 
            this.Image.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() =>
            {
            this.Image.Source = null;
            this.Image.Source = Visualisation.DrawTrack(Data.CurrentRace.Track);
            }));}
           
        }
        public void ChangeRaceEvent(Race race)
        {
            
              
               

            
            race.RaceTimer.Elapsed += CurrentRace_DriversChanged;
            race.DriversChanged += CurrentRace_DriversChanged;
           
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Open_Race_Statis(object sender, RoutedEventArgs e)
        {
            _raceStatistics = new();
            _raceStatistics.Show();
        }
        private void Open_Racer_Statis(object sender, RoutedEventArgs e)
        {
            _compStatistic = new();
            _compStatistic.Show();
        }
    }
}
