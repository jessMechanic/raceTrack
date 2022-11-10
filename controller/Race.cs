using consoleProj;
using controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static System.Collections.Specialized.BitVector32;
using Timer = System.Timers.Timer;
namespace model
{
    public class Race
    {
        #region PublicVariables
        public Track Track;
        public List<IParticipant> Participants;
        public DateTime StartTime;
        public Timer RaceTimer;
        public int Rounds;
        public event EventHandler<DriversChangedEventArgs> DriversChanged;
        public event EventHandler<NextRaceEventArg> NextRaceEvent;
        public bool Busy = false;
        public Dictionary<IParticipant, float> FastestRoundTime;
        public List<string> places;
        //public delegate void OnTimedEvent();
        #endregion PublicVariables


        #region privateVar 
      

        public Dictionary<IParticipant, float> RoundTime;
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private Dictionary<IParticipant, int> _lapsRun;
        private Dictionary<IParticipant, bool> _playerMoved;
        #endregion privateVar             
        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            this.Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            _lapsRun = new Dictionary<IParticipant, int>();
            _playerMoved = new Dictionary<IParticipant, bool>();
            FastestRoundTime = new();
            RoundTime = new();
            places = new();
            Rounds = track.Rounds;
            foreach (IParticipant entry in Participants)
            {
                RoundTime.Add(entry, 0);
            }


            RandomizeEquipment();
            SetTimer();
        }
        #region TimerFunctions
        public void startTimer()
        {
            RaceTimer.Start();
        }
        private void SetTimer()
        {
            RaceTimer = new System.Timers.Timer(1000);
            RaceTimer.AutoReset = true;
            RaceTimer.Elapsed += OnTimedEvent;
        }
        public void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            PlayersFinished();
            MovePlayers();
            AddLapTime();
            if (Participants.Count <= 0)
            {
                RaceTimer.Stop();
                NextRaceEvent?.Invoke(this, new model.NextRaceEventArg(Track));
            }
            BreakEquepment();
        }
        #endregion TimerFunctions
        #region gettersAndSetters
        public override string ToString()
        {
            return Track.Name;
        }
        public Dictionary<Section, SectionData> GetPositions()
        {
            return _positions;
        }
        public SectionData GetSectionData(Section sectionIn)
        {
            if (!_positions.ContainsKey(sectionIn))
            {
                _positions.Add(sectionIn, new SectionData());
            }
            return _positions[sectionIn];
        }
        int addLaps(IParticipant participant)
        {
            if (!_lapsRun.ContainsKey(participant))
            {
                _lapsRun.Add(participant, 0);
            }
            _lapsRun[participant] += 1;
            return _lapsRun[participant];
        }
        bool GetDriverMoved(IParticipant participant)
        {
            if (!_playerMoved.ContainsKey(participant))
            {
                _playerMoved.Add(participant, false);
            }
            return _playerMoved[participant];
        }
        void UpdateDriverMoved(IParticipant participant, bool status)
        {
            if (!_playerMoved.ContainsKey(participant))
            {
                _playerMoved.Add(participant, status);
            }
            _playerMoved[participant] = status;
        }
        #endregion gettersAndSetters
        #region Participants



        public void PlaceParticipants()
        {
            debugLines.writeLine($"placed participants for {Track.Name} : {Participants.Count}");
            LinkedList<Section> trackSections = Track.Sections;
            int startgridPosition = 0;
            foreach (var item in trackSections.Select((value, index) => new { index, value }))
            {
                if (item.value.SectionType == SectionTypes.StartGrid)
                {
                    startgridPosition = item.index;
                    break;
                }
            }
            for (int j = 0; j < Participants.Count; j++)
            {
                int i = startgridPosition - (j / 2);
                i += i < 0 ? trackSections.Count * 2 : 0;
                if (!_positions.ContainsKey(trackSections.ElementAt(i / 2)))
                {
                    _positions.Add(trackSections.ElementAt(i / 2), new SectionData());
                }
                SectionData sectionData = _positions[trackSections.ElementAt(i / 2)];
                if (j % 2 == 0)
                {
                    sectionData.Left = Participants.ElementAt(j);
                }
                else
                {
                    sectionData.Right = Participants.ElementAt(j);
                }
                _positions[trackSections.ElementAt(i / 2)] = sectionData;
            }
        }
        public void MovePlayers()
        {
            places = new();
            int trackLenght = 99;
            for (int i = Track.Sections.Count; i-- > 0;)
            {
                SectionData curData = GetSectionData(Track.Sections.ElementAt(i));
                SectionData nextData;
                try
                {
                    nextData = GetSectionData(Track.Sections.ElementAt(i + 1));
                }
                catch (Exception ex)
                {
                    nextData = GetSectionData(Track.Sections.First());
                }
                if (curData.Left != null)
                {
                    places.Add(curData.Left.Name);
                    UpdateDriverMoved(curData.Left, false);
                    if (!curData.Left.Equipment.isBroken)
                    {
                        curData.DistanceLeft += curData.Left.speed();
                    }
                    if (curData.DistanceLeft > trackLenght)
                    {
                        if (nextData.Left == null || nextData.Right == null)
                        {
                            UpdateDriverMoved(curData.Left, true);

                        }
                        if (nextData.Left == null)
                        {
                            nextData.Left = curData.Left;
                            curData.Left = null;
                            curData.DistanceLeft = 0;
                        }
                        if (nextData.Right == null)
                        {
                            nextData.Right = curData.Left;
                            curData.Left = null;
                            curData.DistanceLeft = 0;
                        }
                    }
                }
                if (curData.Right != null)
                {
                    places.Add(curData.Right.Name);
                    UpdateDriverMoved(curData.Right, false);
                    if (!curData.Right.Equipment.isBroken)
                    {
                        curData.DistanceRight += curData.Right.speed();
                    }
                    if (curData.DistanceRight > trackLenght)
                    {
                        if (nextData.Left == null || nextData.Right == null)
                        {
                            UpdateDriverMoved(curData.Right, true);
                        }
                        if (nextData.Left == null)
                        {
                            nextData.Left = curData.Right;
                            curData.Right = null;
                            curData.DistanceRight = 0;
                        }
                        else if (nextData.Right == null)
                        {
                            nextData.Right = curData.Right;
                            curData.Right = null;
                            curData.DistanceRight = 0;
                        }
                    }
                }
            }
        }
        #endregion Participants
       public void PlayersFinished()
        {
            foreach (KeyValuePair<Section, SectionData> entry in _positions)
            {
                if (entry.Value.Left != null && GetDriverMoved(entry.Value.Left))
                {
                    if (entry.Key.SectionType == SectionTypes.StartGrid || entry.Key.SectionType == SectionTypes.Finish)
                    {
                        UpdateLapTime(entry.Value.Left);

                        if (addLaps(entry.Value.Left) > Rounds)
                        {
                            Data.competition.AddPoints(entry.Value.Left, Participants.Count);
                            Participants.Remove(entry.Value.Left);
                            entry.Value.Left = null;
                        }
                    }
                }
                if (entry.Value.Right != null && GetDriverMoved(entry.Value.Right))
                {
                    if (entry.Key.SectionType == SectionTypes.StartGrid || entry.Key.SectionType == SectionTypes.Finish)
                    {
                        UpdateLapTime(entry.Value.Right);
                        if (addLaps(entry.Value.Right) > Rounds)
                        {
                            Data.competition.AddPoints(entry.Value.Right, Participants.Count);
                            Participants.Remove(entry.Value.Right);
                            entry.Value.Right = null;
                        }
                    }
                }
            }
        }

        public void AddLapTime()
        {
            float tickTime = 0.6f;
            foreach (IParticipant entry in Participants)
            {
                RoundTime[entry] += tickTime;
            }
        }
        public void UpdateLapTime(IParticipant participant)
        {
            if (!_lapsRun.ContainsKey(participant))
            {
                return; 
            }
            if (_lapsRun[participant] < 1) { return; }

            float time;
            RoundTime.TryGetValue(participant, out time);
            if (!FastestRoundTime.ContainsKey(participant))
                FastestRoundTime.Add(participant, time);
            else
            {
                if(FastestRoundTime[participant] > time)
                {
                    FastestRoundTime[participant] = time;
                }
            }
                
            RoundTime[participant] = 0;
        }
        public void BreakEquepment()
        {
            var rand = new Random();
            Participants.ForEach(x =>
           {
               IEquipment equepment = x.Equipment;
               if (equepment.isBroken)
               {
                   equepment.isBroken = equepment.FixChange < rand.Next(0, 100);
               }
               equepment.isBroken = (equepment.Quality) < rand.Next(0, 100);
           });
        }
        public void RandomizeEquipment()
        {
            int minPerf = 30;
            int maxPerf = 100;
            int minQual = 30;
            int maxQual = 100;
            foreach (var participant in Participants)
            {
                participant.Equipment.Performance = _random.Next(minPerf, maxPerf);
                participant.Equipment.Quality = _random.Next(minQual, maxQual);
            }
        }
    }
}
