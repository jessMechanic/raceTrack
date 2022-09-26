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
            return ($"Duck [ Quality: {Quality} , performance : {Performance}   ]");
        }

        public Duck()
        {
            Quality = 1;
            Performance = 1;
            Speed = 80;
            isBroken = false;
        }
       

       public int Quality { get; set; }
        public int Performance { get ; set ; }
        public int Speed { get ; set ; }
        public bool isBroken { get  ; set ; }
        public int BreakChange { get; set; }
    }
}
