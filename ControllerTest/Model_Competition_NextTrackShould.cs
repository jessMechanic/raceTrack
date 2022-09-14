using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerTest
{
    [TestFixture]
    public class Model_Competition_NextTrackShould
    {
         Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
            _competition.Tracks = new Queue<Track>();
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
            Track track = (new Track("the pond",Sections0));
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
    }
}