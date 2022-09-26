

using controller;
using model;
using System.Drawing;

namespace consoleProj
{
    class write
    {
            
       public static void Main(String[] args) {
            Data.Initialize();
            Data.NextRace();

            
          
            
            
           

             Data.CurrentRace.PlaceParticipants();

           
            visualisation.Initialize(Data.CurrentRace);

             visualisation.DrawTrack();
              Data.CurrentRace.startTimer();
            Thread.Sleep(-1);




        }
    }
}