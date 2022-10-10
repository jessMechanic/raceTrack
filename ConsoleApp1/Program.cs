

using controller;
using model;
using System.Drawing;

namespace consoleProj
{
    class write
    {
            
       public static void Main(String[] args) {
            Data.Initialize();

            /* Data.CurrentRace.PlaceParticipants();
             visualisation.Initialize(Data.CurrentRace);
             visualisation.DrawTrack();*/
            Data.InalizeVisualization += visualisation.Initialize;

            Data.NextRace();
           


            debugLines.enables = true;
            Thread.Sleep(-1);

        }
    }
}