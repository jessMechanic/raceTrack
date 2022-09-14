

using controller;
using model;
using System.Drawing;

namespace consoleProj
{
    class write
    {
            
       public static void Main(String[] args) {
            Data.Initialize();
            Track nextTrack = Data.competition.NextTrack();
            Console.Write("here should be printed");
            visualisation.DrawTrack(nextTrack);





        }
    }
}