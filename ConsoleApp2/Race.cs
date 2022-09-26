using System;
using System.Collections.Generic;
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

        //public delegate void OnTimedEvent();

        #endregion PublicVariables

        #region privateVar
        private Random _random;
        private Dictionary<Section, SectionData> _positions;
        private Dictionary<IParticipant, int> _lapsRun;


        #endregion privateVar             
        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            this.Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
            _lapsRun = new Dictionary<IParticipant, int>();
            Rounds = track.Rounds;

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
            CheckFinish();
            DriversChanged();


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


        #endregion gettersAndSetters

        #region Participants
        public void PlaceParticipants()
        {
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
            int spacesReserved = (startgridPosition + 1) * 2;
            if (spacesReserved >= Participants.Count)
            {

                for (int i = spacesReserved - Participants.Count, j = 0; i < spacesReserved; i++, j++)
                {
                    if (!_positions.ContainsKey(trackSections.ElementAt(i / 2)))
                    {
                        _positions.Add(trackSections.ElementAt(i / 2), new SectionData());
                    }
                    SectionData sectionData = _positions[trackSections.ElementAt(i / 2)];
                    if (i % 2 == 0)
                    { sectionData.Left = Participants.ElementAt(j); }
                    else { sectionData.Right = Participants.ElementAt(j); }

                    _positions[trackSections.ElementAt(i / 2)] = sectionData;

                }

            }
            else
            {
                throw new AuthenticationException($" {Participants.Count} participants don't fit in {spacesReserved} spaces ");

            }





        }


        void DriversChanged()
        {
            Console.SetCursorPosition(0, 50);
            int trackLenght = 200;



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
                    if (curData.DistanceLeft > trackLenght)
                    {


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
                    else
                    {
                        Console.Write(curData.Left + "; ");
                        Console.WriteLine(curData.DistanceLeft);
                       


                        curData.DistanceLeft += curData.Left.Equipment.Speed;
                    }
                }

                if (curData.Right != null)
                {
                    if (curData.DistanceRight > trackLenght)
                    {
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
                    else
                    {
                        Console.Write(curData.Right + "; ");
                        Console.WriteLine(curData.DistanceRight);
                       curData.DistanceRight += curData.Right.Equipment.Speed;
                    }
                    
                }




            }


        }
        #endregion Participants

        void CheckFinish()
        {
            foreach (KeyValuePair<Section, SectionData> entry in _positions)
            {
                if (entry.Value.Left != null)
                {
                    if (entry.Key.SectionType == SectionTypes.StartGrid || entry.Key.SectionType == SectionTypes.Finish)
                    {
                        if (addLaps(entry.Value.Left) > Rounds)
                        {
                            entry.Value.Left = null;
                        }
                    }
                }
                if (entry.Value.Right != null)
                {

                    if (entry.Key.SectionType == SectionTypes.StartGrid || entry.Key.SectionType == SectionTypes.Finish)
                    {
                        if (addLaps(entry.Value.Right) > Rounds)
                        {
                            entry.Value.Right = null;
                        }
                    }
                }
            }
        }


        public void RandomizeEquipment()
        {
            int minPerf = 0;
            int maxPerf = 5;

            int minQual = 0;
            int maxQual = 5;

            foreach (var participant in Participants)
            {
                participant.Equipment.Performance = _random.Next(minPerf, maxPerf);
                participant.Equipment.Quality = _random.Next(minQual, maxQual);
            }
        }

    }
}
