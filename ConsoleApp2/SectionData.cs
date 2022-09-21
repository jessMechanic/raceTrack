using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class SectionData
    {
        public IParticipant Left { get; set; }
        public int DistanceLeft { get; set; }

        public IParticipant Right { get; set; }
        public int DistanceRight { get; set; }

        public override string ToString()
        {
            return $"left : {(Left != null ? Left.Name : " none ")} ; right : {(Right !=  null ? Right.Name:" none ")}";
        }
    }
}
