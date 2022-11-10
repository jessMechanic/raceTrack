using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Duck : IEquipment

    {
        static Random _R = new Random();
        static T RandomEnumValue<T>()
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(_R.Next(v.Length));
        }   


        public String toString()
        {
            return ($"Duck [ Quality: {Quality} , performance : {Performance}   ]");
        }

        public Duck(int speed)
        {
            Quality = 100;
            Performance = 1;
            Speed = speed;
            isBroken = false;
         
            FixChange = 80;
            PartCostume = RandomEnumValue<Cosume>();
        }
        public Duck(int speed,int quality,int fixChange)
        {
            Quality = quality;
            Performance = 1;
            Speed = speed;
            isBroken = false;

            FixChange = fixChange;
            PartCostume = RandomEnumValue<Cosume>();
        }
        public Duck()
        {
            Quality = 80;
            Performance = 1;
            Speed = 50;
            isBroken = false;
    
            FixChange = 80;
            PartCostume = RandomEnumValue<Cosume>();
        }

        public int Quality { get; set; }
        public int Performance { get ; set ; }
        public int Speed { get ; set ; }
        public bool isBroken { get  ; set ; }

        public int FixChange { get; set; }
        public Cosume PartCostume { get; set; }
    }
}
