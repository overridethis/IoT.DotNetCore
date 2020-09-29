using System;
using IoT.DotNetCore.BuildMonitor.Contracts;
using Xunit;

namespace IoT.DotNetCore.BuildMonitor.Tests.Contracts
{
    public class RunTests
    {
        [Theory]
        [InlineData("completed", true)]
        [InlineData("queued", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void Can_identify_is_completed_from_status(string status, bool result)
        {
            // Arrange.
            var model = new CheckRun { Status = status };

            // Act.
            // Assert.
            Assert.Equal(model.IsCompleted(), result);
        }

        [Theory]
        [InlineData("completed", false)]
        [InlineData("queued", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void Can_identify_is_queued_from_status(string status, bool result)
        {
            // Arrange.
            var model = new CheckRun { Status = status };

            // Act.
            // Assert.
            Assert.Equal(model.IsQueued(), result);
        }
        
        [Theory]
        [InlineData("completed", false)]
        [InlineData("queued", false)]
        [InlineData("failure", true)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void Can_identify_is_failure_from_status(string status, bool result)
        {
            // Arrange.
            var model = new CheckRun { Status = status };

            // Act.
            // Assert.
            Assert.Equal(model.IsQueued(), result);
        }
    }
}
