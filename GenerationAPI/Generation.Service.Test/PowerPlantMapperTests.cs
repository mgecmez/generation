using FluentAssertions;
using Generation.Domain;
using Generation.Dto;
using Generation.Service.Mappers;
using Generation.Service.Test.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Generation.Service.Test
{
    public class PowerPlantMapperTests
    {
        [Theory, AutoMoqData]
        public void ToPowerPlant_Should_Success(PowerPlantDto powerPlantDto, PowerPlantMapper mapper)
        {
            Action action = () =>
            {
                var result = mapper.ToPowerPlant(powerPlantDto);
                result.WebId.Should().Be(powerPlantDto.WebId);
            };
            action.Should().NotThrow<Exception>();
        }

        [Theory, AutoMoqData]
        public void ToPowerPlantDto_Should_Success(PowerPlant powerPlant, PowerPlantMapper mapper)
        {
            Action action = () =>
            {
                var result = mapper.ToPowerPlantDto(powerPlant);
                result.Id.Should().Be(powerPlant.Id);
                result.WebId.Should().Be(powerPlant.WebId);
            };
            action.Should().NotThrow<Exception>();
        }
    }
}
