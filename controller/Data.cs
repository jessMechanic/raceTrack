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
            
            addParticipants(new Driver("duck", 1, new Duck(), TeamColors.Blue));
            addParticipants(new Driver("goose", 1, new Duck(), TeamColors.Blue));
            addParticipants(new Driver("swan", 1, new Duck(), TeamColors.Yellow));
        }
        public static void addtracks()
        {
            
            SectionTypes[] Sections1 = {  SectionTypes.StartGrid, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.Straight, SectionTypes.RightCornor,SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.Straight,  SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCornor, SectionTypes.RightCornor,SectionTypes.Finish };
            Track track1 = (new Track("the pond", Sections1));
            track1.startPosition = new System.Numerics.Vector2(3, 3);

            SectionTypes[] Sections2 = { SectionTypes.Straight, SectionTypes.StartGrid, SectionTypes.Straight,SectionTypes.RightCornor,SectionTypes.LeftCornor,SectionTypes.Straight,SectionTypes.Straight, SectionTypes.LeftCornor,SectionTypes.RightCornor, SectionTypes.RightCornor, SectionTypes.Finish };
            Track track2 = (new Track("the pond", Sections2));
            track2.startPosition = new System.Numerics.Vector2(0, 1);


            competition.Tracks.Enqueue(track2);
            competition.Tracks.Enqueue(track1);


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