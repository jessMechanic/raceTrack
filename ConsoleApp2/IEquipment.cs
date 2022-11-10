using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public enum Cosume
    {
        duck,
        goose,
        swan    ,
        dapper
    }
    public interface IEquipment
    {
        public int Quality { get; set; }
        public int Performance { get; set; }
        public int Speed { get; set; }
     
        public int FixChange { get; set; }
        public bool isBroken { get; set; }

        public Cosume PartCostume { get; set; }
    }
}
