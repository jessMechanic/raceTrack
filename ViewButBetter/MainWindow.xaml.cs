using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public EventHandler driverChange;
        public MainWindow()
        {
            Data.Initialize();

            
            Data.InalizeVisualization += Visualisation.PreCalculateGrid;

            Data.NextRace();

            Data.CurrentRace.RaceTimer.Elapsed += CurrentRace_DriversChanged;
            InitializeComponent();
        }

        public void CurrentRace_DriversChanged(object? sender, EventArgs e)
        {
            this.Image.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() =>
            {
            this.Image.Source = null;
            this.Image.Source = Visualisation.DrawTrack(Data.CurrentRace.Track);
            }));
        }
        public void ChangeRaceEvent(Race race)
        {
            
              
               

            
            race.RaceTimer.Elapsed += CurrentRace_DriversChanged;
            race.DriversChanged += CurrentRace_DriversChanged;
           
        }


    }
}
