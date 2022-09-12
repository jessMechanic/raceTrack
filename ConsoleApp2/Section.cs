using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    enum SectionTypes
    {
        Straight,
        LeftCornor,
        RightCornor,
        StartGrid,
        Finish
    }
    internal class Section
    {
        public SectionTypes SectionType { get; set; }
    }
}
