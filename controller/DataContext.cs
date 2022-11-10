using model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace controller
{
    public class DataContext : INotifyPropertyChanged
    {
        /* public List<IParticipant> Participants { get; set; }*/
        public string TrackName => Data.CurrentRace?.Track.Name;
        public List<string> PLaces =>  Data.CurrentRace.places.Select((d, i) => String.Format("{1}:{0}", d.Trim(), i + 1)).ToList();

        public List<string> FastestLapTimes =>  Data.CurrentRace.FastestRoundTime.Select((d) => String.Format("{1}:{0}", MathF.Round( d.Value,3).ToString(), d.Key.Name)).ToList();
        public List< string> LapTimes => Data.CurrentRace.RoundTime.Select((d) => String.Format("{1}:{0}", MathF.Round(d.Value, 3).ToString(), d.Key.TeamColor)).ToList();

        public List<string> broken =>Data.CurrentRace.Participants.Select((d) => String.Format("{0}:{1}", d.TeamColor, d.Equipment.isBroken ? "broken" : "ok")).ToList();

        public List<string> points => Data.competition.Points.OrderByDescending(x => x.Value ).Select((d) => String.Format("{0}:{1}", d.Key.TeamColor, d.Value)).ToList();

        public string TracksToGo => $"tracks to go:  {Data.competition.Tracks.Count.ToString()} ";
        public event PropertyChangedEventHandler PropertyChanged;

        public DataContext()
        {
            Data.CurrentRace.RaceTimer.Elapsed += OnDriverChanged;
            Data.InalizeVisualization  += OnNextRace;
            PropertyChanged += PropertyChanged;

           /* TrackName = Data.CurrentRace.Track.Name;*/
          /*  Participants = Data.CurrentRace.Participants;*/
        }

          public void OnNextRace(Race race)
        {
            race.RaceTimer.Elapsed += OnDriverChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

        private void OnDriverChanged(object sender,ElapsedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
        private void OnPropertyChanged(object sender,PropertyChangedEventArgs e)
        {
    /*        TrackName = Data.CurrentRace.Track.Name;*/
        }



    }
}
