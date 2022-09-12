

using controller;
using model;

namespace consoleProj
{
    class write
    {
            
       public static void Main(String[] args) {
            Data.Initialize();
            foreach(IParticipant part in   Data.competition.Participants)
            {
                Console.WriteLine(part.ToString());
            }
            Console.WriteLine(Data.CurrentRace);
            Data.NextRace();
            Console.WriteLine(Data.CurrentRace);

        }
    }
}