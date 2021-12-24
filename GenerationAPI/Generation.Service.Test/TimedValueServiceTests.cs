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
using System.Threading.Tasks;
using Xunit;

namespace Generation.Service.Test
{
    public class TimedValueServiceTests
    {
        [Theory, AutoMoqData]
        public void CreateTimedValue_Should_Success([Frozen] Mock<ITimedValueMapper> mapper, [Frozen] Mock<ITimedValueRepository> repository, TimedValueDto timedValueDto, TimedValue timedValue, TimedValueService service)
        {
            mapper.Setup(x => x.ToTimedValue(timedValueDto)).Returns(timedValue);
            repository.Setup(x => x.Save(timedValue)).Returns(Task.FromResult(It.IsAny<Guid>()));

            Action action = async () =>
            {
                await service.Create(timedValueDto);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void UpdateTimedValue_Should_Success([Frozen] Mock<ITimedValueMapper> mapper, [Frozen] Mock<ITimedValueRepository> repository, TimedValueDto timedValueDto, TimedValue timedValue, TimedValueService service)
        {
            mapper.Setup(x => x.ToTimedValue(timedValueDto)).Returns(timedValue);
            repository.Setup(x => x.Update(timedValue));

            Action action = async () =>
            {
                await service.Update(timedValueDto);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void GetAll_Should_Success([Frozen] Mock<ITimedValueMapper> mapper, [Frozen] Mock<ITimedValueRepository> repository, List<TimedValue> timedValues, List<TimedValueDto> timedValuesDto, TimedValueService service)
        {
            repository.Setup(x => x.All()).Returns(timedValues.AsQueryable);
            mapper.Setup(x => x.ToTimedValueDtoList(timedValues)).Returns(timedValuesDto);

            Action action = async () =>
            {
                var result = await service.GetAll();
                result.Count.Should().Be(timedValuesDto.Count);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void GetById_Should_Return_As_Expected([Frozen] Mock<ITimedValueMapper> mapper, [Frozen] Mock<ITimedValueRepository> repository, Guid id, TimedValueDto timedValueDto, TimedValue timedValue, TimedValueService service)
        {
            mapper.Setup(x => x.ToTimedValueDto(timedValue)).Returns(timedValueDto);
            repository.Setup(x => x.Get(id)).Returns(Task.FromResult(timedValue));

            Action action = async () =>
            {
                var result = await service.GetById(id);
                result.Should().BeEquivalentTo(timedValueDto);
            };
            action.Should().NotThrow<Exception>();
        }
    }
}
