using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }
        public Vector2 startDirection;
        public ConsoleColor ThemeColor;
        public ConsoleColor TrackBoundry;
        public ConsoleColor TrackColor;
        public int Rounds;
           public Track(String name, SectionTypes[] SectionsIn)
        {
            Name = name;
            Sections = convertToList(SectionsIn);
            ThemeColor =  ConsoleColor.Green;
            TrackBoundry = ConsoleColor.DarkGreen;
            TrackColor = ConsoleColor.Blue;
            startDirection = new Vector2(1, 0);
            Rounds = 1;
        }public override string ToString()
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
             public Section NextTrackSection(Section sectionIn)
        {
            for (int i = 0; i < Sections.Count; i++)
            {
               if(sectionIn == Sections.ElementAt(i))
                {
                    return Sections.ElementAt((i + 1) % (Sections.Count  ));
                } 
            }
            throw new AuthenticationException($"no sections found");

            
        }
        
    }
}
