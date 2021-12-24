using System;
using System.Collections.Generic;
using Generation.Api.Controllers;
using Generation.Api.Test.Attribute;
using Generation.Dto;
using Generation.Service;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Threading.Tasks;

namespace Generation.Api.Test
{
    public class TimedValueControllerTests
    {
        [Theory, AutoMoqData]
        public void GetAll_Should_Return_As_Expected(Mock<ITimedValueService> timedValueServiceMock, List<TimedValueDto> expected)
        {
            var timedValueController = new TimedValueController(timedValueServiceMock.Object);
            timedValueServiceMock.Setup(c => c.GetAll()).Returns(Task.FromResult(expected));

            var result = timedValueController.Get();

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<List<TimedValueDto>>().Subject;

            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void GetById_Should_Return_As_Expected(Mock<ITimedValueService> timedValueServiceMock, Guid id, TimedValueDto expected)
        {
            var timedValueController = new TimedValueController(timedValueServiceMock.Object);
            timedValueServiceMock.Setup(c => c.GetById(id)).Returns(Task.FromResult(expected));

            var result = timedValueController.Get(id);

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<TimedValueDto>().Subject;

            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void Post_Should_Return_As_Expected(Mock<ITimedValueService> timedValueServiceMock, TimedValueDto timedValue)
        {
            var timedValueController = new TimedValueController(timedValueServiceMock.Object);
            timedValueServiceMock.Setup(c => c.Create(timedValue));

            var actual = timedValueController.Post(timedValue);

            actual.GetType().Should().Be(typeof(OkResult));
        }

        [Theory, AutoMoqData]
        public void Put_Should_Return_As_Expected(Mock<ITimedValueService> timedValueServiceMock, TimedValueDto expected)
        {
            var timedValueController = new TimedValueController(timedValueServiceMock.Object);
            timedValueServiceMock.Setup(c => c.Update(expected)).Returns(Task.FromResult(expected));

            var result = timedValueController.Put(expected);

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<TimedValueDto>().Subject;

            Assert.Equal(expected, actual);
        }
    }
}
