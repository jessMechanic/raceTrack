using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public Vector2 startPosition;
        public override string ToString()
        {
            return Name;
        }
        public static LinkedList<Section> convertToList(SectionTypes[] sections)
        {
            LinkedList<Section> list = new LinkedList<Section>();

            foreach( SectionTypes section in sections)
            {
                list.AddLast (new Section (section));
            }

            return list;
        }
        public Track(String name, SectionTypes[] SectionsIn)
        {
            Name = name;
            Sections = convertToList(SectionsIn);
        }
        
    }
}
