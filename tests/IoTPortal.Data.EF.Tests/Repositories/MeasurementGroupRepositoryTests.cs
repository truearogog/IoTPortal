using IoTPortal.Core.Models;
using IoTPortal.Data.EF.Entities;
using IoTPortal.Data.EF.Repositories;

namespace IoTPortal.Data.EF.Tests.Repositories
{
    public class MeasurementGroupRepositoryTests
    {
        [Fact]
        public async Task GetForDevice()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var measurementGroups = GetMeasurementGroupEntities(deviceId);

            var expected = mapper.Map<IEnumerable<MeasurementGroup>>(measurementGroups.Where(x => x.DeviceId == deviceId));

            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.Add(new DeviceEntity
                {
                    Id = deviceId,
                    State = Core.Enums.DeviceState.Setup,
                    Name = "test name",
                    Description = "test description",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                });
                context.MeasurementGroups.AddRange(measurementGroups);
                context.SaveChanges();
            }))
            {
                var measurementGroupRepository = new MeasurementGroupRepository(fixture.Context, mapper);

                var actual = await measurementGroupRepository.GetForDevice(deviceId);

                Assert.Equal(expected.Count(), actual.Count());
                foreach (var (expected_, actual_) in expected.Zip(actual))
                {
                    AssertHelpers.AsserMeasurementGroups(expected_, actual_);
                }
            }
        }

        [Fact]
        public async Task GetForDevicePredicate()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var measurementGroups = GetMeasurementGroupEntities(deviceId);

            var expected = mapper.Map<IEnumerable<MeasurementGroup>>(measurementGroups
                .Where(x => x.DeviceId == deviceId)
                .Where(x => x.Id > 5));

            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.Add(new DeviceEntity
                {
                    Id = deviceId,
                    State = Core.Enums.DeviceState.Setup,
                    Name = "test name",
                    Description = "test description",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                });
                context.MeasurementGroups.AddRange(measurementGroups);
                context.SaveChanges();
            }))
            {
                var measurementGroupRepository = new MeasurementGroupRepository(fixture.Context, mapper);
                var actual = await measurementGroupRepository.GetForDevice(deviceId, x => x.Id > 5);

                Assert.Equal(expected.Count(), actual.Count());
                foreach (var (expected_, actual_) in expected.Zip(actual))
                {
                    AssertHelpers.AsserMeasurementGroups(expected_, actual_);
                }
            }
        }

        [Fact]
        public async Task Create()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.Add(new DeviceEntity
                {
                    Id = deviceId,
                    State = Core.Enums.DeviceState.Setup,
                    Name = "test name",
                    Description = "test description",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                });
                context.SaveChanges();
            }))
            {
                var measurementGroup = new MeasurementGroup
                {
                    Id = 1,
                    Created = DateTime.UtcNow,
                    DeviceId = deviceId,
                    Measurements = Enumerable.Range(0, 5).Select(x => Random.Shared.NextDouble() * 100d).ToArray()
                };

                var measurementGroupRepository = new MeasurementGroupRepository(fixture.Context, mapper);
                await measurementGroupRepository.Create(measurementGroup);

                Assert.NotNull(fixture.Context.MeasurementGroups.FirstOrDefault(x => x.Id == measurementGroup.Id));
            }
        }

        [Fact]
        public async Task CreateRange()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.Add(new DeviceEntity
                {
                    Id = deviceId,
                    State = Core.Enums.DeviceState.Setup,
                    Name = "test name",
                    Description = "test description",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                });
                context.SaveChanges();
            }))
            {
                var measurementGroups = Enumerable.Range(1, 10).Select(id => new MeasurementGroup
                {
                    Id = id,
                    Created = DateTime.UtcNow,
                    DeviceId = deviceId,
                    Measurements = Enumerable.Range(0, 5).Select(x => Random.Shared.NextDouble() * 100d).ToArray()
                });

                var measurementGroupRepository = new MeasurementGroupRepository(fixture.Context, mapper);
                await measurementGroupRepository.CreateRange(measurementGroups);

                foreach (var measurementGroup in measurementGroups)
                {
                    Assert.NotNull(fixture.Context.MeasurementGroups.FirstOrDefault(x => x.Id == measurementGroup.Id));
                }
            }
        }

        private List<MeasurementGroupEntity> GetMeasurementGroupEntities(Guid deviceId)
        {
            return Enumerable.Range(1, 10)
                .Select(i => new MeasurementGroupEntity
                {
                    Id = i,
                    DeviceId = deviceId,
                    Created = DateTime.UtcNow,
                    Measurements = Enumerable.Range(0, 5).Select(x => Random.Shared.NextDouble() * 100d).ToArray()
                }).ToList();
        }
    }
}