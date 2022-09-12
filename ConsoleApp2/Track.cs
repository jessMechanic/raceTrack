using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Track
    {
        public Track(String name, LinkedList<Section> Sections)
        {
            Name = name;
            Sections = Sections;
        }
        public String Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
    }
}
