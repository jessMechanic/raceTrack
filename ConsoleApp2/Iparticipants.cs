using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    internal enum TeamColors
    {
        Red,
        Green,
        Yellow,
        Grey,
        Blue

    }
    internal interface IParticipant
    {
        public String Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }

    }
}
