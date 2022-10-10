using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
     public class NextRaceEventArg:EventArgs
    {
        public Track Track { get; set; }
        public NextRaceEventArg(Track track)
        {
            Track = track;
        }
    }
}
