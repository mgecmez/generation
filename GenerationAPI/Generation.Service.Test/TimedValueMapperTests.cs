using AutoFixture.Xunit2;
using FluentAssertions;
using Generation.Domain;
using Generation.Dto;
using Generation.Service.Mappers;
using Generation.Service.Test.Attribute;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Generation.Service.Test
{
    public class TimedValueMapperTests
    {
        [Theory, AutoMoqData]
        public void ToTimedValue_Should_Success(TimedValueDto timedValueDto, TimedValueMapper mapper)
        {
            Action action = () =>
            {
                var result = mapper.ToTimedValue(timedValueDto);
                result.Timestamp.Should().Be(timedValueDto.Timestamp);
                result.Good.Should().Be(timedValueDto.Good);
                result.Value.Should().Be(timedValueDto.Value.ToString());
                result.PowerPlantId.Should().Be(timedValueDto.PowerPlantId);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void ToTimedValueDto_Should_Success(TimedValue timedValue, TimedValueMapper mapper)
        {
            Action action = () =>
            {
                var result = mapper.ToTimedValueDto(timedValue);
                result.Id.Should().Be(timedValue.Id);
                result.Timestamp.Should().Be(timedValue.Timestamp);
                result.Good.Should().Be(timedValue.Good);
                result.Value.Should().Be(timedValue.Value);
                result.PowerPlantId.Should().Be(timedValue.PowerPlantId);
            };
            action.Should().NotThrow<Exception>();
        }
    }
}
