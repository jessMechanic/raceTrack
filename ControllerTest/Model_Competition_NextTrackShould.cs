using controller;
using model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Section = model.Section;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {

        private Race _race;
        private Track _t;

        Competition _competition;
        Track track0;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
            _competition.Tracks = new Queue<Track>();
            SectionTypes[] Sections0 = { SectionTypes.Straight, SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Finish };
            track0 = (new Track("the pond", Sections0));
            _competition.Participants = new();
            _competition.Participants.Add(new Driver("duck", 1, new Duck(), TeamColors.Blue));
            SectionTypes[] Sections4 = { SectionTypes.RightCornor, SectionTypes.RightCornor, SectionTypes.StartGrid, SectionTypes.RightCornor, SectionTypes.RightCornor, SectionTypes.Straight };
            Track babyPark = (new Track("BabyPart", Sections4));



            _race = new Race(babyPark, _competition.Participants);
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            SectionTypes[] Sections0 = { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Finish };
            Track track = (new Track("the pond", Sections0));
            _competition.Tracks.Enqueue(track);
            var result = _competition.NextTrack();
            Assert.That(track, Is.EqualTo(result));
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            SectionTypes[] Sections0 = { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Finish };
            Track track = (new Track("the pond", Sections0));
            _competition.Tracks.Enqueue(track);

            Track results = _competition.NextTrack();
            Assert.That(track, Is.EqualTo(results));
            results = _competition.NextTrack();
            Assert.IsNull(results);
        }
        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            SectionTypes[] Sections0 = { SectionTypes.StartGrid, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Straight, SectionTypes.LeftCornor, SectionTypes.Finish };
            Track track0 = (new Track("the pond", Sections0));
            Track track1 = (new Track("the pond again", Sections0));
            _competition.Tracks.Enqueue(track0);
            _competition.Tracks.Enqueue(track1);

            Track results = _competition.NextTrack();
            Assert.That(track0, Is.EqualTo(results));
            results = _competition.NextTrack();
            Assert.That(track1, Is.EqualTo(results));
        }
        [Test]
        public void PlaceParticipants()
        {
            _competition.Participants = new List<IParticipant>();
            for (int i = 0; i < 10; i++)
            {
                _competition.Participants.Add(new Driver("duck", 1, new Duck(), TeamColors.Blue));
            }
            Race testRace = new Race(track0, _competition.Participants);

            try
            {
                testRace.PlaceParticipants();

            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }



        }

        [Test]
        public void AddPointsOfUnknownPlayer()
        {
            Driver unknown = new Driver("unknown", 1, new Duck(), TeamColors.Blue);
            _competition.AddPoints(unknown, 10);
            _competition.UpdatePoints();
            Assert.IsTrue(_competition.Points.ContainsKey(unknown));
        }

        [Test]
        public void RandomizeEquepment()
        {
            _competition.Participants = new List<IParticipant>();
            for (int i = 0; i < 10; i++)
            {
                _competition.Participants.Add(new Driver("duck", 1, new Duck(), TeamColors.Blue));
            }
            Race testRace = new Race(track0, _competition.Participants);
            int perf = testRace.Participants.First().Equipment.Performance;

            testRace.RandomizeEquipment();
            Assert.That(testRace.Participants.First().Equipment.Performance, Is.Not.EqualTo(perf));
        }

        [Test]
        public void FinishedRace()
        {

            _race.NextRaceEvent += (_, _) =>
            {
                foreach (IParticipant participant in Data.competition.Participants)
                {
                    Assert.That(participant.Points, Is.Not.Zero);
                }
            };

            _race.RaceTimer.Start();

            // wait 50 sec
            Thread.Sleep(50000);



            //check if theres an active racer
            foreach (Section section in _race.Track.Sections)
                {
                    SectionData sectionData = _race.GetSectionData(section);
                    Assert.That(sectionData.Left, Is.Null);
                    Assert.That(sectionData.Right, Is.Null);

                }
            }


        }


    }
