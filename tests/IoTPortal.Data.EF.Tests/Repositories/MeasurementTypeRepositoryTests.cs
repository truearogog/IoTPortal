using IoTPortal.Core.Models;
using IoTPortal.Data.EF.Entities;
using IoTPortal.Data.EF.Repositories;

namespace IoTPortal.Data.EF.Tests.Repositories
{
    public class MeasurementTypeRepositoryTests
    {
        [Fact]
        public async Task GetAll()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var measurementTypes = GetMeasurementTypeEntities(deviceId).ToList();

            var expected = mapper.Map<IEnumerable<MeasurementType>>(measurementTypes);

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
                context.MeasurementTypes.AddRange(measurementTypes);
                context.SaveChanges();
            }))
            {
                var measurementTypeRepository = new MeasurementTypeRepository(fixture.Context, mapper);

                var actual = await measurementTypeRepository.GetAll();

                Assert.Equal(expected.Count(), actual.Count());
                foreach (var (expected_, actual_) in expected.Zip(actual))
                {
                    AssertHelpers.AssertMeasurementTypes(expected_, actual_);
                }
            }
        }


        [Fact]
        public async Task GetAllPredicate()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var measurementTypes = GetMeasurementTypeEntities(deviceId).ToList();

            var expected = mapper.Map<IEnumerable<MeasurementType>>(measurementTypes.Where(x => x.Created > DateTime.UtcNow.AddDays(-1)));

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
                context.MeasurementTypes.AddRange(measurementTypes);
                context.SaveChanges();
            }))
            {
                var measurementTypeRepository = new MeasurementTypeRepository(fixture.Context, mapper);

                var actual = await measurementTypeRepository.GetAll(x => x.Created > DateTime.UtcNow.AddDays(-1));

                Assert.Equal(expected.Count(), actual.Count());
                foreach (var (expected_, actual_) in expected.Zip(actual))
                {
                    AssertHelpers.AssertMeasurementTypes(expected_, actual_);
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
                var expected = mapper.Map<MeasurementType>(GetMeasurementTypeEntities(deviceId).First());
                var measurementTypeRepository = new MeasurementTypeRepository(fixture.Context, mapper);
                await measurementTypeRepository.Create(expected);

                var actual = mapper.Map<MeasurementType>(fixture.Context.MeasurementTypes.First(x => x.Id == expected.Id));
                AssertHelpers.AssertMeasurementTypes(expected, actual);
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
                var measurementTypes = Enumerable.Range(1, 10).Select(i => new MeasurementType
                {
                    Id = Guid.NewGuid(),
                    DeviceId = deviceId,
                    Unit = "test unit " + i,
                    Name = "test name " + i,
                    Variable = "test variable " + i,
                    Color = "#000000",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                }).ToList();

                var measurementTypeRepository = new MeasurementTypeRepository(fixture.Context, mapper);
                await measurementTypeRepository.CreateRange(measurementTypes);

                foreach (var measurementType in measurementTypes)
                {
                    Assert.NotNull(fixture.Context.MeasurementTypes.FirstOrDefault(x => x.Id == measurementType.Id));
                }
            }
        }

        [Fact]
        public async Task Update()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var measurementType = GetMeasurementTypeEntities(deviceId).First();
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
                context.MeasurementTypes.Add(measurementType);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }))
            {
                var updatedMeasurementTypeModel = new MeasurementType
                {
                    Id = measurementType.Id,
                    DeviceId = deviceId,
                    Unit = "updated unit ",
                    Name = "updated name ",
                    Variable = "updated variable",
                    Color = "#000000",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                };

                var measurementTypeRepository = new MeasurementTypeRepository(fixture.Context, mapper);
                await measurementTypeRepository.Update(updatedMeasurementTypeModel);

                var updatedMeasurementType = mapper.Map<MeasurementType>(fixture.Context.MeasurementTypes.First(x => x.Id == measurementType.Id));

                AssertHelpers.AssertMeasurementTypes(updatedMeasurementTypeModel, updatedMeasurementType);
            }
        }

        [Fact]
        public async Task UpdateRange()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var measurementTypes = GetMeasurementTypeEntities(deviceId).ToList();

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
                context.MeasurementTypes.AddRange(measurementTypes);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }))
            {
                var updatedMeasurementTypeModels = mapper.Map<IEnumerable<MeasurementType>>(measurementTypes).Select((x, i) =>
                {
                    x.Variable = "updated variable " + (i + 1);
                    x.Name = "updated variable " + (i + 1);
                    x.Unit = "updated unit " + (i + 1);
                    return x;
                }).ToList();

                var measurementTypeRepository = new MeasurementTypeRepository(fixture.Context, mapper);
                await measurementTypeRepository.UpdateRange(updatedMeasurementTypeModels);

                var updatedMeasurementTypes = mapper.Map<IEnumerable<MeasurementType>>(
                    measurementTypes.Select(device => fixture.Context.MeasurementTypes.FirstOrDefault(x => x.Id == device.Id)).OfType<MeasurementTypeEntity>());

                foreach (var (updatedMeasurementTypeModel, updatedMeasurementType) in updatedMeasurementTypeModels.Zip(updatedMeasurementTypes))
                {
                    AssertHelpers.AssertMeasurementTypes(updatedMeasurementTypeModel, updatedMeasurementType);
                }
            }
        }

        [Fact]
        public async Task Delete()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var measurementType = GetMeasurementTypeEntities(deviceId).First();

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
                context.MeasurementTypes.Add(measurementType);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }))
            {
                var measurementTypeRepository = new MeasurementTypeRepository(fixture.Context, mapper);
                await measurementTypeRepository.Delete(measurementType.Id);

                Assert.Null(fixture.Context.MeasurementTypes.FirstOrDefault(x => x.Id == measurementType.Id));
            }
        }

        [Fact]
        public async Task DeleteRange()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var measurementTypes = GetMeasurementTypeEntities(deviceId).ToList();

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
                context.MeasurementTypes.AddRange(measurementTypes);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }))
            {
                var measurementTypeRepository = new MeasurementTypeRepository(fixture.Context, mapper);
                await measurementTypeRepository.DeleteRange(measurementTypes.Select(x => x.Id));

                foreach (var measurementType in measurementTypes)
                {
                    Assert.Null(fixture.Context.MeasurementTypes.FirstOrDefault(x => x.Id == measurementType.Id));
                }
            }
        }

        private IEnumerable<MeasurementTypeEntity> GetMeasurementTypeEntities(Guid deviceId)
        {
            return Enumerable.Range(1, 10)
                .Select(i => new MeasurementTypeEntity
                {
                    Id = Guid.NewGuid(),
                    DeviceId = deviceId,
                    Unit = "test unit " + i,
                    Name = "test name " + i,
                    Variable = "test variable " + i,
                    Color = "#000000",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                });
        }
    }
}
