using model; 

namespace controller
{
    public static class Data
    {
        public static Competition competition;
        public static Race? CurrentRace;

        
        public  static void Initialize()
        {
            competition = new Competition();
            competition.Tracks = new Queue<Track>();
            addRacers(); 
            addtracks();
            NextRace();





        }
        public static void NextRace()
        {
           Track track = competition.NextTrack();
            if(track != null)
            {
                CurrentRace = new Race(track, competition.Participants);
            }
        }
        public static void addRacers()
        {
            Duck superDuck = new Duck();
            superDuck.Speed = 1000;
            addParticipants(new Driver("duck", 1, new Duck(), TeamColors.Blue));
            addParticipants(new Driver("goose", 1, new Duck(), TeamColors.Red));
            addParticipants(new Driver("swan", 1, new Duck(), TeamColors.Yellow));
            addParticipants(new Driver("swan", 1, superDuck, TeamColors.Cyan));


        }
        public static void addtracks()
        {
            
            SectionTypes[] Sections1 = {SectionTypes.Straight, SectionTypes.StartGrid, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.RightCornor,SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.Straight,  SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.RightCornor, SectionTypes.Straight };
            Track dummyTrack = (new Track("dummyTrack", Sections1));



            Track ThePond = (new Track("the pond", Sections1));


            SectionTypes[] Sections2 = { SectionTypes.Straight, SectionTypes.StartGrid, SectionTypes.LeftCornor,SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCornor,SectionTypes.Straight, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.LeftCornor };
            Track ball = (new Track("8-ball", Sections2));
            ball.TrackBoundry = ConsoleColor.White;
            ball.ThemeColor = ConsoleColor.Green;
            ball.TrackColor = ConsoleColor.Black;

            SectionTypes[] Sections3 = {SectionTypes.Straight, SectionTypes.StartGrid, SectionTypes.Straight,SectionTypes.RightCornor,SectionTypes.LeftCornor,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.RightCornor, SectionTypes.Finish };
            Track river = (new Track("the river", Sections3));



            SectionTypes[] Sections4 = {SectionTypes.RightCornor,SectionTypes.StartGrid,SectionTypes.RightCornor,SectionTypes.RightCornor,SectionTypes.Straight, SectionTypes.RightCornor };
            Track babyPark = (new Track("BabyPart", Sections4));
            babyPark.Rounds = 8;
            babyPark.startDirection = new System.Numerics.Vector2(0, 1);


            competition.Tracks.Enqueue(dummyTrack);
            competition.Tracks.Enqueue(babyPark);
            competition.Tracks.Enqueue(ThePond);
            competition.Tracks.Enqueue(river);
            competition.Tracks.Enqueue(ball);


        }



        public static void addParticipants( IParticipant participant)
        {
               if(competition.Participants == null)
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