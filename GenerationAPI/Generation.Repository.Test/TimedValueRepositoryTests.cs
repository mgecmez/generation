using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Generation.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Generation.Repository.Test
{
    public class TimedValueRepositoryTests
    {
        [Fact]
        public async Task Save_Should_Save_The_Timed_Value_And_Should_Return_All_Count_As_Two()
        {
            var timedValue1 = new TimedValue(Guid.NewGuid(), DateTime.Now, true, (3.5M).ToString());
            var timedValue2 = new TimedValue(Guid.NewGuid(), DateTime.Now, false, (1.0M).ToString());

            var options = new DbContextOptionsBuilder<GenerationDbContext>()
                .UseInMemoryDatabase("generation_db")
                .Options;

            using (var context = new GenerationDbContext(options))
            {
                var repository = new TimedValueRepository(context);
                await repository.Save(timedValue1);
                await repository.Save(timedValue2);
                await context.SaveChangesAsync();
            }

            using (var context = new GenerationDbContext(options))
            {
                var repository = new TimedValueRepository(context);
                repository.All().Count().Should().Be(2);
            }
        }

        [Fact]
        public async Task Delete_Should_Delete_The_Timed_Value_And_Should_Return_All_Count_As_One()
        {
            var timedValue1 = new TimedValue(Guid.NewGuid(), DateTime.Now, true, (3.5M).ToString());
            var timedValue2 = new TimedValue(Guid.NewGuid(), DateTime.Now, false, (1.0M).ToString());

            var options = new DbContextOptionsBuilder<GenerationDbContext>()
                .UseInMemoryDatabase("generation_db")
                .Options;

            using (var context = new GenerationDbContext(options))
            {
                var repository = new TimedValueRepository(context);
                await repository.Save(timedValue1);
                await repository.Save(timedValue2);
                await context.SaveChangesAsync();
            }

            using (var context = new GenerationDbContext(options))
            {
                var repository = new TimedValueRepository(context);
                await repository.Delete(timedValue1.Id);
                await context.SaveChangesAsync();
            }

            using (var context = new GenerationDbContext(options))
            {
                var repository = new TimedValueRepository(context);
                repository.All().Count().Should().Be(1);
            }
        }

        [Fact]
        public async Task Update_Should_Update_The_Timed_Value()
        {
            var timedValue = new TimedValue(Guid.NewGuid(), DateTime.Now, true, (3.50M).ToString());

            var options = new DbContextOptionsBuilder<GenerationDbContext>()
                .UseInMemoryDatabase("generation_db")
                .Options;

            using (var context = new GenerationDbContext(options))
            {
                var repository = new TimedValueRepository(context);
                await repository.Save(timedValue);
                await context.SaveChangesAsync();
            }

            timedValue.SetFields(timedValue.PowerPlantId, DateTime.Now.AddDays(-2), true, (2.50M).ToString());

            using (var context = new GenerationDbContext(options))
            {
                var repository = new TimedValueRepository(context);
                await repository.Update(timedValue);
                await context.SaveChangesAsync();
            }

            using (var context = new GenerationDbContext(options))
            {
                var repository = new TimedValueRepository(context);
                var result = await repository.Get(timedValue.Id);

                result.Should().NotBe(null);
                result.Timestamp.Should().Be(timedValue.Timestamp);
                result.Good.Should().Be(timedValue.Good);
                result.Value.Should().Be(timedValue.Value);
            }
        }

        [Fact]
        public async Task Find_Should_Fid_The_Timed_Value_And_Should_Return_All_Count_As_One()
        {
            var timedValue1 = new TimedValue(Guid.NewGuid(), DateTime.Now, true, (3.50M).ToString());
            var timedValue2 = new TimedValue(Guid.NewGuid(), DateTime.Now, false, (1.0M).ToString());

            var options = new DbContextOptionsBuilder<GenerationDbContext>()
                .UseInMemoryDatabase("generation_db")
                .Options;

            using (var context = new GenerationDbContext(options))
            {
                var repository = new TimedValueRepository(context);
                await repository.Save(timedValue1);
                await repository.Save(timedValue2);
                await context.SaveChangesAsync();
            }

            using (var context = new GenerationDbContext(options))
            {
                var repository = new TimedValueRepository(context);
                var result = repository.Find(c => c.Value == timedValue1.Value);
                result.Should().NotBeNull();
                result.Count().Should().Be(1);
            }
        }
    }
}
