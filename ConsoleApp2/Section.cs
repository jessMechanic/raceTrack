using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
   public enum SectionTypes
    {
        Straight,
        LeftCornor,
        RightCornor,
        StartGrid,
        Finish
    }
    public class Section
    {
        public Section(SectionTypes sectionType)
        {
            SectionType = sectionType;
        }

        public SectionTypes SectionType { get; set; }
    }
}
