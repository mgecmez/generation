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
    public class PowerPlantControllerTests
    {
        [Theory, AutoMoqData]
        public void GetAll_Should_Return_As_Expected(Mock<IPowerPlantService> powerPlantServiceMock, List<PowerPlantDto> expected)
        {
            var powerPlantController = new PowerPlantController(powerPlantServiceMock.Object);
            powerPlantServiceMock.Setup(c => c.GetAll()).Returns(Task.FromResult(expected));

            var result = powerPlantController.Get();

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<List<PowerPlantDto>>().Subject;

            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void GetById_Should_Return_As_Expected(Mock<IPowerPlantService> powerPlantServiceMock, Guid id, PowerPlantDto expected)
        {
            var powerPlantController = new PowerPlantController(powerPlantServiceMock.Object);
            powerPlantServiceMock.Setup(c => c.GetById(id)).Returns(Task.FromResult(expected));

            var result = powerPlantController.Get(id);

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<PowerPlantDto>().Subject;

            Assert.Equal(expected, actual);
        }

        [Theory, AutoMoqData]
        public void Post_Should_Return_As_Expected(Mock<IPowerPlantService> powerPlantServiceMock, PowerPlantDto powerPlant)
        {
            var powerPlantController = new PowerPlantController(powerPlantServiceMock.Object);
            powerPlantServiceMock.Setup(c => c.Create(powerPlant));

            var actual = powerPlantController.Post(powerPlant);

            actual.GetType().Should().Be(typeof(OkResult));
        }

        [Theory, AutoMoqData]
        public void Put_Should_Return_As_Expected(Mock<IPowerPlantService> powerPlantServiceMock, PowerPlantDto expected)
        {
            var powerPlantController = new PowerPlantController(powerPlantServiceMock.Object);
            powerPlantServiceMock.Setup(c => c.Update(expected)).Returns(Task.FromResult(expected));

            var result = powerPlantController.Put(expected);

            var apiOkResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var actual = apiOkResult.Value.Should().BeAssignableTo<PowerPlantDto>().Subject;

            Assert.Equal(expected, actual);
        }
    }
}
