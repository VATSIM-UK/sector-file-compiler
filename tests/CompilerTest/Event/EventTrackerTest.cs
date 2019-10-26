using Xunit;
using Moq;
using Compiler.Event;

namespace CompilerTest.Event
{
    public class EventTrackerTest
    {
        private readonly EventTracker eventTracker;

        private readonly Mock<ICompilerEvent> mockEvent;

        private readonly Mock<IEventObserver> mockObserver;

        public EventTrackerTest()
        {
            this.eventTracker = new EventTracker();
            this.mockEvent = new Mock<ICompilerEvent>();
            this.mockObserver = new Mock<IEventObserver>();
        }
        
        [Fact]
        public void TestItAddsEvents()
        {
            this.eventTracker.AddEvent(this.mockEvent.Object);
            Assert.Equal(1, this.eventTracker.CountEvents());
            Assert.Equal(this.mockEvent.Object, this.eventTracker.GetLastEvent());
        }

        [Fact]
        public void TestItHasFatalErrorIfOneEventIsFatal()
        {
            this.eventTracker.AddEvent(this.mockEvent.Object);
            this.mockEvent.Setup(foo => foo.IsFatal()).Returns(true);
            Assert.True(this.eventTracker.HasFatalError());
        }

        [Fact]
        public void TestItHasFatalErrorIfNoEventsFatal()
        {
            this.eventTracker.AddEvent(this.mockEvent.Object);
            this.mockEvent.Setup(foo => foo.IsFatal()).Returns(false);
            Assert.False(this.eventTracker.HasFatalError());
        }

        [Fact]
        public void TestNewEventsArePassedToObservers()
        {
            this.eventTracker.AddObserver(this.mockObserver.Object);
            this.eventTracker.AddEvent(this.mockEvent.Object);
            this.mockObserver.Verify(foo => foo.NewEvent(this.mockEvent.Object), Times.Once);
        }
    }
}
