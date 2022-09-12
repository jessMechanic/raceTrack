using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model

  
{
    internal class Race
    {
      public Track Track ;
      public List<IParticipant> participants ;
      public DateTime StartTime;

      private Random _random;
      private Dictionary<Section, SectionData> _positions;

    public Race(Track track, List<IParticipant> participants)
        {
            Track = track;
            this.participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
        }

        public SectionData GetSectionData(Section section)
        {
          return _positions[section];
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
