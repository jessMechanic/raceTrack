using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Duck : IEquipment
    {
           public String toString()
        {
            return ($"Duck [ Quality: {Quality} , performance : {Performance}   ");
        }

        public int Quality { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Performance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Speed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool isBroken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
