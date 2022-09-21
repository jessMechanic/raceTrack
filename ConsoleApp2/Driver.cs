using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class Driver : IParticipant   
    {
        public override string ToString()
        {
            return ($"Duck [ name: {Name} , Points : {Points}  ] ");
        }
        public Driver(string name, int points, IEquipment equipment, TeamColors teamColor)
        {
            Name = name;
            Points = points;
            Equipment = equipment;
            TeamColor = teamColor;
        }
         public ConsoleColor getConsoleColor()
        {
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Gray, ConsoleColor.Blue };
            return colors[(int)(TeamColor)];
        }
        public string Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
    }
}
