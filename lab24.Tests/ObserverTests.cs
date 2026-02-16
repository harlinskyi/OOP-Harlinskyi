using Xunit;
using lab24.Observers;
using System.Collections.Generic;

namespace lab24.Tests
{
    public class ObserverTests
    {
        [Fact]
        public void HistoryLogger_ShouldStoreResults()
        {
            // Arrange
            var publisher = new ResultPublisher();
            var historyObserver = new HistoryLoggerObserver();
            publisher.ResultCalculated += historyObserver.OnResultCalculated;

            // Act
            publisher.PublishResult(100, "TestOp");
            publisher.PublishResult(200, "AnotherOp");

            // Assert
            Assert.Equal(2, historyObserver.History.Count);
            Assert.Contains("TestOp", historyObserver.History[0]);
        }

        [Fact]
        public void Publisher_ShouldInvokeEvent()
        {
            // Arrange
            var publisher = new ResultPublisher();
            double receivedResult = 0;
            string receivedName = "";
            bool wasCalled = false;

            publisher.ResultCalculated += (res, name) =>
            {
                receivedResult = res;
                receivedName = name;
                wasCalled = true;
            };

            // Act
            publisher.PublishResult(42, "Magic");

            // Assert
            Assert.True(wasCalled);
            Assert.Equal(42, receivedResult);
            Assert.Equal("Magic", receivedName);
        }
    }
}