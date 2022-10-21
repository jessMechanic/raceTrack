using model;

namespace controller
{
    public static class Data
    {
        public static Competition competition;
        public static Race? CurrentRace;
        public delegate void NextRaceDelate(Race race);
        public static NextRaceDelate InalizeVisualization;

        public static void Initialize()
        {
            competition = new Competition();
            competition.Tracks = new Queue<Track>();
            addRacers();
            addtracks();
            




        }
        public static void NextRace()
        {
          
            Track track = competition.NextTrack();
            if (track != null)
            {
                CurrentRace = new Race(track, new List<IParticipant>( competition.Participants));
                
                InalizeVisualization?.Invoke(CurrentRace);
                CurrentRace.PlaceParticipants();
                CurrentRace.NextRaceEvent += CurrentRace_NextRaceEvent;
               CurrentRace.RaceTimer.Start();

            }



        }

        private static void CurrentRace_NextRaceEvent(object? sender, NextRaceEventArg e)
        {
            NextRace();
        }

        public static void addRacers()
        {

            addParticipants(new Driver("duck", 1, new Duck(100), TeamColors.Blue));
            addParticipants(new Driver("goose", 1, new Duck(100), TeamColors.Red));
            addParticipants(new Driver("swan", 1, new Duck(100), TeamColors.Yellow));
            addParticipants(new Driver("swan", 1, new Duck(100), TeamColors.Cyan));
          



        }
        public static void addtracks()
        {

            SectionTypes[] Sections1 = {SectionTypes.Straight,SectionTypes.Straight, SectionTypes.StartGrid, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.RightCornor };




            Track ThePond = (new Track("the pond", Sections1));


            SectionTypes[] Sections2 = { SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.StartGrid, SectionTypes.LeftCornor, SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.Straight };
            Track ball = (new Track("8-ball", Sections2));
            ball.startDirection = new System.Numerics.Vector2(0, 1);
            ball.Rounds = 1;

            SectionTypes[] Sections3 = { SectionTypes.Straight, SectionTypes.Straight, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.RightCornor, SectionTypes.Finish };
            Track river = (new Track("the river", Sections3));
            river.Rounds = 1;


            SectionTypes[] Sections4 = { SectionTypes.RightCornor, SectionTypes.RightCornor, SectionTypes.StartGrid, SectionTypes.RightCornor, SectionTypes.RightCornor,SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.RightCornor };
            Track babyPark = (new Track("BabyPart", Sections4));
            babyPark.Rounds = 1;
            babyPark.startDirection = new System.Numerics.Vector2(0, 1);



            competition.Tracks.Enqueue(ball);
            competition.Tracks.Enqueue(river);
            competition.Tracks.Enqueue(babyPark);
            competition.Tracks.Enqueue(ThePond);




        }



        public static void addParticipants(IParticipant participant)
        {
            if (competition.Participants == null)
            {
                competition.Participants = new List<IParticipant>();
            }
            competition.Participants.Add(participant);
        }

        public static void addTrack(Track track)
        {
            if (competition.Tracks == null)
            {
                competition.Tracks = new Queue<Track>();
            }
            competition.Tracks.Enqueue(track);
        }
    }
}

/*
⠀⠀⠀⠀⠀⠀⢀⣠⠤⠔⠒⠒⠒⠒⠒⠢⠤⢤⣀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⢀⠴⠊⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠉⠲⣄⠀⠀⠀
⠀⠀⡰⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠈⢧⠀⠀
⠀⡸⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⢇⠀
⠀⡇⠀⠀⠀⢀⡶⠛⣿⣷⡄⠀⠀⠀⣰⣿⠛⢿⣷⡄⠀⠀⠀⢸⠀
⠀⡇⠀⠀⠀⢸⣷⣶⣿⣿⡇⠀⠀⠀⢻⣿⣶⣿⣿⣿⠀⠀⠀⢸⠀
⠀⡇⠀⠀⠀⠈⠛⠻⠿⠟⠁⠀⠀⠀⠈⠛⠻⠿⠛⠁⠀⠀⠀⢸⠀
⠀⠹⣄⠀⠀⠀⠀⠀⠀⠀⠀⢀⣀⣀⠀⠀⠀⠀⠀⠀⠀⠀⣠⠏⠀
⠀⠀⠈⠢⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⣤⣚⡁⠀⠀
⠀⠀⠀⠀⠈⠙⠒⢢⡤⠤⠤⠤⠤⠤⠖⠒⠒⠋⠉⠉⠀⠉⠉⢦
⠀⠀⠀⠀⠀⠀⠀⢸⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  ⠀⢸
⠀⠀⠀⠀⠀⠀⠀⢸⡀⠀⠀⠀⠀⣤⠀⠀⠀⢀⣀⣀⣀⠀ ⠀⠀⢸
⠀⠀⠀⠀⠀⠀⠀⠈⡇⠀⠀⠀⢠⣿⠀⠀⠀⢸⠀⠀⣿⠀⠀ ⠀⣸
⠀⠀⠀⠀⠀⠀⠀⠀⢱⠀⠀⠀⢸⠘⡆⠀⠀⢸⣀⡰⠋⣆⠀⣠⠇
⠀⠀⠀⠀⠀⠀⠀⠀⠀⠳⠤⠤⠼⠀⠘⠤⠴⠃⠀⠀⠀⠈⠉⠁⠀
        _                       
       (_)                      
  _   _ _ _ __  _ __   ___  ___ 
 | | | | | '_ \| '_ \ / _ \/ _ \
 | |_| | | |_) | |_) |  __/  __/
  \__, |_| .__/| .__/ \___|\___|
   __/ | | |   | |              
  |___/  |_|   |_|             
 
 
 
 
 
 */