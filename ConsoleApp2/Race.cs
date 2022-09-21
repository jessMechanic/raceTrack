using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace model

  
{
    public class Race
    {
      public Track Track ;
      public List<IParticipant> participants ;
      public DateTime StartTime;

      private Random _random;
        private Dictionary<Section, SectionData> _positions; 



        
        public override string ToString()
        {
            return Track.Name;
        }

        public Dictionary<Section, SectionData> GetPositions()
        {
            return _positions;
        }
        public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            this.participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
            _positions = new Dictionary<Section, SectionData>();
        }

        public SectionData GetSectionData(Section section)
        {
            var temp = new SectionData();
            return !_positions.TryAdd(section, temp) ? _positions[section] : temp;
        }

        public void PlaceParticipants()
        {
              LinkedList<Section> trackSections = Track.Sections;
            
            int startgridPosition= 0;
            foreach (var item in trackSections.Select((value, index) => new { index, value }))
            {
                
                if(item.value.SectionType == SectionTypes.StartGrid)
                {
                    startgridPosition = item.index;
                    break;
                }
            }
            int spacesReserved = (startgridPosition + 1) * 2;
            if (spacesReserved > participants.Count)
            {
                int j = 0;
                for (int i = spacesReserved - participants.Count; i < spacesReserved; i++)
                {
                    if (!_positions.ContainsKey(trackSections.ElementAt(i / 2)))
                    {
                        _positions.Add(trackSections.ElementAt(i / 2), new SectionData());
                    }
                     SectionData sectionData = _positions[trackSections.ElementAt(i / 2)];
                    if(i % 2 == 0)
                    { sectionData.Left = participants.ElementAt(j);}
                    else{ sectionData.Right = participants.ElementAt(j);}

                    _positions[trackSections.ElementAt(i / 2)] = sectionData;
                    j++; 
                }
                
            }
            
            
            


        }
       public void  RandomizeEquipment()
        {
            int minPerf = 0;
            int maxPerf = 5;

            int minQual = 0;
            int maxQual = 5;

            foreach(var participant in participants)
            {
                participant.Equipment.Performance = _random.Next(minPerf, maxPerf);
                participant.Equipment.Quality = _random.Next(minQual, maxQual);
            }
        }

    }
}
