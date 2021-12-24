using Generation.Domain.Test.Attribute;
using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace Generation.Domain.Test
{
    public class TimedValueTests
    {
        [Theory, AutoMoqData]
        public void Create_Timed_Value_Should_Throw_Exception_When_Value_Is_Zero(Guid powerPlantId, DateTime timestamp, bool good)
        {
            Assert.Throws<Exception>(() => new TimedValue(powerPlantId, timestamp, good, ""));
        }

        [Theory, AutoMoqData]
        public void Create_Timed_Value_Should_Success(Guid powerPlantId, DateTime timestamp, bool good, string value)
        {
            var timedValue = new TimedValue(powerPlantId,timestamp, good, value);

            timedValue.Timestamp.Should().Be(timestamp);
            timedValue.Good.Should().Be(good);
            timedValue.Value.Should().Be(value);
        }

        [Theory, AutoMoqData]
        public void SetFields_Should_Update_Fields(Guid powerPlantId, DateTime timestamp, bool good, string value, TimedValue timedValue)
        {
            timedValue.SetFields(powerPlantId, timestamp, good, value);

            timedValue.Timestamp.Should().Be(timestamp);
            timedValue.Good.Should().Be(good);
            timedValue.Value.Should().Be(value);
        }
    }
}
