using model; 

namespace controller
{
    public static class Data
    {
        public static Competition competition;

        public  static void Initialize()
        {
            competition = new Competition(); 
            addRacers(); 
            addtracks();


        }
           
        public static void addRacers()
        {
            
            addParticipants(new Driver("duck", 1, new Duck(), TeamColors.Blue));
            addParticipants(new Driver("goose", 1, new Duck(), TeamColors.Blue));
            addParticipants(new Driver("swan", 1, new Duck(), TeamColors.Yellow));
        }
        public static void addtracks()
        {
            Section[] Sections0 = { new Section(SectionTypes.StartGrid), new Section(SectionTypes.Straight), new Section(SectionTypes.LeftCornor), new Section(SectionTypes.Straight), new Section(SectionTypes.LeftCornor), new Section(SectionTypes.Straight), new Section(SectionTypes.LeftCornor), new Section(SectionTypes.Finish) };
            addTrack(new Track("the pond", new LinkedList<Section>(Sections0)));

            Section[] Sections1 = { new Section(SectionTypes.StartGrid), new Section(SectionTypes.Straight), new Section(SectionTypes.Straight), new Section(SectionTypes.Finish) };
            addTrack(new Track("river", new LinkedList<Section>(Sections1)));


            Section[] Sections2 = { new Section(SectionTypes.StartGrid), new Section(SectionTypes.Straight), new Section(SectionTypes.LeftCornor), new Section(SectionTypes.Straight), new Section(SectionTypes.RightCornor), new Section(SectionTypes.Finish) };
            addTrack(new Track("marshes", new LinkedList<Section>(Sections1)));
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