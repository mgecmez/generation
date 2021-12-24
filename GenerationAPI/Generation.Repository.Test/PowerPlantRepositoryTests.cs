using FluentAssertions;
using Generation.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Generation.Repository.Test
{
    public class PowerPlantRepositoryTests
    {
        [Fact]
        public void Save_Should_Save_The_Powerplant_And_Should_Return_All_Count_As_Two()
        {
            var powerPlant1 = new PowerPlant("webid_1");
            var powerPlant2 = new PowerPlant("webid_2");

            var options = new DbContextOptionsBuilder<GenerationDbContext>()
                .UseInMemoryDatabase("generation_db")
                .Options;

            using (var context = new GenerationDbContext(options))
            {
                var repository = new PowerPlantRepository(context);
                repository.Save(powerPlant1);
                repository.Save(powerPlant2);
                context.SaveChanges();
            }

            using (var context = new GenerationDbContext(options))
            {
                var repository = new PowerPlantRepository(context);
                repository.All().Count().Should().Be(2);
            }
        }

        [Fact]
        public void Delete_Should_Delete_The_Powerplant_And_Should_Return_All_Count_As_One()
        {
            var powerPlant1 = new PowerPlant("webid_1");
            var powerPlant2 = new PowerPlant("webid_2");

            var options = new DbContextOptionsBuilder<GenerationDbContext>()
                .UseInMemoryDatabase("generation_db")
                .Options;

            using (var context = new GenerationDbContext(options))
            {
                var repository = new PowerPlantRepository(context);
                repository.Save(powerPlant1);
                repository.Save(powerPlant2);
                context.SaveChanges();
            }

            using (var context = new GenerationDbContext(options))
            {
                var repository = new PowerPlantRepository(context);
                repository.Delete(powerPlant1.Id);
                context.SaveChanges();
            }

            using (var context = new GenerationDbContext(options))
            {
                var repository = new PowerPlantRepository(context);
                repository.All().Count().Should().Be(1);
            }
        }

        [Fact]
        public async Task Update_Should_Update_The_Powerplant()
        {
            var powerPlant = new PowerPlant("webid_1");

            var options = new DbContextOptionsBuilder<GenerationDbContext>()
                .UseInMemoryDatabase("generation_db")
                .Options;

            using (var context = new GenerationDbContext(options))
            {
                var repository = new PowerPlantRepository(context);
                await repository.Save(powerPlant);
                await context.SaveChangesAsync();
            }

            powerPlant.SetFields(powerPlant.WebId);

            using (var context = new GenerationDbContext(options))
            {
                var repository = new PowerPlantRepository(context);
                await repository.Update(powerPlant);
                await context.SaveChangesAsync();
            }

            using (var context = new GenerationDbContext(options))
            {
                var repository = new PowerPlantRepository(context);
                var result = await repository.Get(powerPlant.Id);

                result.Should().NotBe(null);
                result.WebId.Should().Be(powerPlant.WebId);
            }
        }

        [Fact]
        public async Task Find_Should_Fid_The_Powerplant_And_Should_Return_All_Count_As_One()
        {
            var powerPlant1 = new PowerPlant("webid_1");
            var powerPlant2 = new PowerPlant("webid_2");

            var options = new DbContextOptionsBuilder<GenerationDbContext>()
                .UseInMemoryDatabase("generation_db")
                .Options;

            using (var context = new GenerationDbContext(options))
            {
                var repository = new PowerPlantRepository(context);
                await repository.Save(powerPlant1);
                await repository.Save(powerPlant2);
                await context.SaveChangesAsync();
            }

            using (var context = new GenerationDbContext(options))
            {
                var repository = new PowerPlantRepository(context);
                var result = repository.Find(c => c.WebId == powerPlant1.WebId);
                result.Should().NotBeNull();
                result.Count().Should().Be(1);
            }
        }
    }
}
