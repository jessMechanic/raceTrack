using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; }
        public Queue<Track> Tracks { get; set; }

        public Track NextTrack() => (Tracks == null || !Tracks.Any()) ? null : Tracks.Dequeue();

        public Dictionary<IParticipant, int> Points;
        private Dictionary<IParticipant, int> _points;

        public Competition()
        {
            Points = new Dictionary<IParticipant, int>();
            _points = new Dictionary<IParticipant, int>();

        }


        public void AddPoints(IParticipant participant, int points)
        {

         if(!_points.ContainsKey(participant))
            {
                _points.Add(participant, 0);
            }
            _points[participant] += points;
        }
        public void UpdatePoints()
        {
            Points = _points;
        }

    }
}
