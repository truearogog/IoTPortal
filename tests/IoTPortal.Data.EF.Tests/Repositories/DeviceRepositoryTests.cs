using IoTPortal.Core.Models;
using IoTPortal.Data.EF.Entities;
using IoTPortal.Data.EF.Repositories;

namespace IoTPortal.Data.EF.Tests.Repositories
{
    public class DeviceRepositoryTests
    {
        [Fact]
        public async Task GetAll()
        {
            var mapper = MapperHelpers.GetMapper();
            var devices = GetDeviceEntities().ToList();

            var expected = mapper.Map<IEnumerable<Device>>(devices);

            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.AddRange(devices);
                context.SaveChanges();
            }))
            {
                var deviceRepository = new DeviceRepository(fixture.Context, mapper);

                var actual = await deviceRepository.GetAll();

                Assert.Equal(expected.Count(), actual.Count());
                foreach (var (expected_, actual_) in expected.Zip(actual))
                {
                    AssertHelpers.AssertDevices(expected_, actual_);
                }
            }
        }

        [Fact]
        public async Task GetAllPredicate()
        {
            var mapper = MapperHelpers.GetMapper();
            var devices = GetDeviceEntities().ToList();

            var expected = mapper.Map<IEnumerable<Device>>(devices.Where(x => x.State == Core.Enums.DeviceState.Ready));

            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.AddRange(devices);
                context.SaveChanges();
            }))
            {
                var deviceRepository = new DeviceRepository(fixture.Context, mapper);

                var actual = await deviceRepository.GetAll(x => x.State == Core.Enums.DeviceState.Ready);

                Assert.Equal(expected.Count(), actual.Count());
                foreach (var (expected_, actual_) in expected.Zip(actual))
                {
                    AssertHelpers.AssertDevices(expected_, actual_);
                }
            }
        }

        [Fact]
        public async Task GetById()
        {
            var mapper = MapperHelpers.GetMapper();
            var device = GetDeviceEntities().First();

            var expected = mapper.Map<Device>(device);

            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.Add(device);
                context.SaveChanges();
            }))
            {
                var deviceRepository = new DeviceRepository(fixture.Context, mapper);

                var actual = await deviceRepository.GetById(device.Id);

                AssertHelpers.AssertDevices(expected, actual);
            }
        }

        [Fact]
        public async Task Create()
        {
            var mapper = MapperHelpers.GetMapper();

            using (var fixture = new TestAppDbFixture(context => { }))
            {
                var device = new Device
                {
                    Id = Guid.NewGuid(),
                    State = Core.Enums.DeviceState.Setup,
                    Name = "test name",
                    Description = "test description",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                };

                var deviceRepository = new DeviceRepository(fixture.Context, mapper);
                await deviceRepository.Create(device);

                Assert.NotNull(fixture.Context.Devices.FirstOrDefault(x => x.Id == device.Id));
            }
        }

        [Fact]
        public async Task CreateRange()
        {
            var mapper = MapperHelpers.GetMapper();

            using (var fixture = new TestAppDbFixture(context => { }))
            {
                var devices = Enumerable.Range(1, 10).Select(i => new Device
                {
                    Id = Guid.NewGuid(),
                    State = Core.Enums.DeviceState.Setup,
                    Name = "test name " + i,
                    Description = "test description " + i,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                }).ToList();

                var deviceRepository = new DeviceRepository(fixture.Context, mapper);
                await deviceRepository.CreateRange(devices);

                foreach (var device in devices)
                {
                    Assert.NotNull(fixture.Context.Devices.FirstOrDefault(x => x.Id == device.Id));
                }
            }
        }

        [Fact]
        public async Task Update()
        {
            var mapper = MapperHelpers.GetMapper();
            var device = GetDeviceEntities().First();

            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.Add(device);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }))
            {
                var updatedDeviceModel = new Device
                {
                    Id = device.Id,
                    State = Core.Enums.DeviceState.Ready,
                    Name = "updated name",
                    Description = "updated description",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                };

                var deviceRepository = new DeviceRepository(fixture.Context, mapper);
                await deviceRepository.Update(updatedDeviceModel);

                var updatedDevice = mapper.Map<Device>(fixture.Context.Devices.First(x => x.Id == device.Id));

                AssertHelpers.AssertDevices(updatedDeviceModel, updatedDevice);
            }
        }

        [Fact]
        public async Task UpdateRange()
        {
            var mapper = MapperHelpers.GetMapper();
            var devices = GetDeviceEntities().ToList();

            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.AddRange(devices);
                context.SaveChanges();
                context.ChangeTracker.Clear();
            }))
            {
                var updatedDeviceModels = mapper.Map<IEnumerable<Device>>(devices).Select((x, i) =>
                {
                    x.State = Core.Enums.DeviceState.Ready;
                    x.Name = "updated name " + (i + 1);
                    x.Description = "updated description " + (i + 1);
                    return x;
                }).ToList();

                var deviceRepository = new DeviceRepository(fixture.Context, mapper);
                await deviceRepository.UpdateRange(updatedDeviceModels);

                var updatedDevices = mapper.Map<IEnumerable<Device>>(
                    devices.Select(device => fixture.Context.Devices.FirstOrDefault(x => x.Id == device.Id)).OfType<DeviceEntity>());

                foreach (var (updatedDeviceModel, updatedDevice) in updatedDeviceModels.Zip(updatedDevices))
                {
                    AssertHelpers.AssertDevices(updatedDeviceModel, updatedDevice);
                }
            }
        }

        [Fact]
        public async Task Delete()
        {
            var mapper = MapperHelpers.GetMapper();
            var device = GetDeviceEntities().First();

            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.Add(device);
                context.SaveChanges();
            }))
            {
                var deviceRepository = new DeviceRepository(fixture.Context, mapper);
                await deviceRepository.Delete(device.Id);

                Assert.Null(fixture.Context.Devices.FirstOrDefault(x => x.Id == device.Id));
            }
        }

        [Fact]
        public async Task DeleteRange()
        {
            var mapper = MapperHelpers.GetMapper();
            var devices = GetDeviceEntities().ToList();

            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.AddRange(devices);
                context.SaveChanges();
            }))
            {
                var deviceRepository = new DeviceRepository(fixture.Context, mapper);
                await deviceRepository.DeleteRange(devices.Select(x => x.Id));

                foreach (var device in devices)
                {
                    Assert.Null(fixture.Context.Devices.FirstOrDefault(x => x.Id == device.Id));
                }
            }
        }

        private static IEnumerable<DeviceEntity> GetDeviceEntities()
        {
            return Enumerable.Range(1, 10)
                .Select(i => new DeviceEntity
                {
                    Id = Guid.NewGuid(),
                    State = i <= 5 ? Core.Enums.DeviceState.Ready : Core.Enums.DeviceState.Setup,
                    Name = "test name " + i,
                    Description = "test description " + i,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                });
        }
    }
}
