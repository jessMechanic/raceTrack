using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public static class enumHelper
    {
       
        public static ConsoleColor toConsoleColor(this TeamColors color)
        {
            ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Gray, ConsoleColor.Blue,ConsoleColor.Cyan };
            return colors[(int)(color)];
        }
        public static Color toColor(this TeamColors color)
        {
            Color[] colors = {Color.Red,Color.Green, Color.Yellow, Color.Gray, Color.Blue, Color.Cyan };
            return colors[(int)(color)];
        }
    }
    public enum TeamColors
    {
        Red,
        Green,
        Yellow,
        Grey,
        Blue,
        Cyan

    }

    
    public interface IParticipant
    {
        public String Name { get; set; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColors TeamColor { get; set; }
    

        public int speed();
    }
}
