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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Generation.Service.Test
{
    public class PowerPlantServiceTests
    {
        [Theory, AutoMoqData]
        public void CreatePowerPlant_Should_Success([Frozen] Mock<IPowerPlantMapper> mapper, [Frozen] Mock<IPowerPlantRepository> repository, PowerPlantDto powerPlantDto, PowerPlant powerPlant, PowerPlantService service)
        {
            mapper.Setup(x => x.ToPowerPlant(powerPlantDto)).Returns(powerPlant);
            repository.Setup(x => x.Save(powerPlant)).Returns(Task.FromResult(It.IsAny<Guid>()));

            Action action = async () =>
            {
                await service.Create(powerPlantDto);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void UpdatePowerPlant_Should_Success([Frozen] Mock<IPowerPlantMapper> mapper, [Frozen] Mock<IPowerPlantRepository> repository, PowerPlantDto powerPlantDto, PowerPlant powerPlant, PowerPlantService service)
        {
            mapper.Setup(x => x.ToPowerPlant(powerPlantDto)).Returns(powerPlant);
            repository.Setup(x => x.Update(powerPlant));

            Action action = async () =>
            {
                await service.Update(powerPlantDto);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void GetAll_Should_Success([Frozen] Mock<IPowerPlantMapper> mapper, [Frozen] Mock<IPowerPlantRepository> repository, List<PowerPlant> powerPlants, List<PowerPlantDto> powerPlantsDto, PowerPlantService service)
        {
            repository.Setup(x => x.All()).Returns(powerPlants.AsQueryable);
            mapper.Setup(x => x.ToPowerPlantDtoList(powerPlants)).Returns(powerPlantsDto);

            Action action = async () =>
            {
                var result = await service.GetAll();
                result.Count.Should().Be(powerPlantsDto.Count);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void GetById_Should_Return_As_Expected([Frozen] Mock<IPowerPlantMapper> mapper, [Frozen] Mock<IPowerPlantRepository> repository, Guid id, PowerPlantDto powerPlantDto, PowerPlant powerPlant, PowerPlantService service)
        {
            mapper.Setup(x => x.ToPowerPlantDto(powerPlant)).Returns(powerPlantDto);
            repository.Setup(x => x.Get(id)).Returns(Task.FromResult(powerPlant));

            Action action = async () =>
            {
                var result = await service.GetById(id);
                result.Should().BeEquivalentTo(powerPlantDto);
            };
            action.Should().NotThrow<Exception>();
        }
    }
}
