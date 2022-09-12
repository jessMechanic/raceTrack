using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    internal class Track
    {
        public String Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
    }
}
