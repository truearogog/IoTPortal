using IoTPortal.Core.Models;
using IoTPortal.Data.EF.Entities;
using IoTPortal.Data.EF.Repositories;

namespace IoTPortal.Data.EF.Tests.Repositories
{
    public class UserDeviceRoleRepositoryTests
    {
        [Fact]
        public async Task GetAllPredicate()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var userDeviceRoles = GetUserDeviceRoleEntities(deviceId).ToList();

            var expected = mapper.Map<IEnumerable<UserDeviceRole>>(userDeviceRoles.Where(x => x.DeviceRole == Core.Enums.DeviceRole.Owner));

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
                context.UserDeviceRoles.AddRange(userDeviceRoles);
                context.SaveChanges();
            }))
            {
                var userDeviceRoleRepository = new UserDeviceRoleRepository(fixture.Context, mapper);

                var actual = await userDeviceRoleRepository.GetAll(x => x.DeviceRole == Core.Enums.DeviceRole.Owner);

                Assert.Equal(expected.Count(), actual.Count());
                foreach (var (expected_, actual_) in expected.Zip(actual))
                {
                    AssertHelpers.AssertUserDeviceRoles(expected_, actual_);
                }
            }
        }

        [Fact]
        public async Task GetForDevice()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var userDeviceRoles = GetUserDeviceRoleEntities(deviceId).ToList();

            var expected = mapper.Map<IEnumerable<UserDeviceRole>>(userDeviceRoles.Where(x => x.DeviceId == deviceId));

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
                context.UserDeviceRoles.AddRange(userDeviceRoles);
                context.SaveChanges();
            }))
            {
                var userDeviceRoleRepository = new UserDeviceRoleRepository(fixture.Context, mapper);

                var actual = await userDeviceRoleRepository.GetForDevice(deviceId);

                Assert.Equal(expected.Count(), actual.Count());
                foreach (var (expected_, actual_) in expected.Zip(actual))
                {
                    AssertHelpers.AssertUserDeviceRoles(expected_, actual_);
                }
            }
        }

        [Fact]
        public async Task GetDevicesForUser()
        {
            var mapper = MapperHelpers.GetMapper();
            var devices = Enumerable.Range(1, 10).Select(i => new DeviceEntity
            {
                Id = Guid.NewGuid(),
                State = Core.Enums.DeviceState.Setup,
                Name = "test name " + i,
                Description = "test description" + i,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            }).ToList();
            var userDeviceRoles = devices.Select(device => GetUserDeviceRoleEntities(device.Id).First()).ToList();

            var expected = mapper.Map<IEnumerable<Device>>(devices);

            using (var fixture = new TestAppDbFixture(context =>
            {
                context.Devices.AddRange(devices);
                context.UserDeviceRoles.AddRange(userDeviceRoles);
                context.SaveChanges();
            }))
            {
                var userDeviceRoleRepository = new UserDeviceRoleRepository(fixture.Context, mapper);

                var actual = await userDeviceRoleRepository.GetDevicesForUser("1");

                Assert.Equal(expected.Count(), actual.Count());
                foreach (var (expected_, actual_) in expected.Zip(actual))
                {
                    AssertHelpers.AssertDevices(expected_, actual_);
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
                var userDeviceRole = new UserDeviceRole
                {
                    UserId = "1",
                    DeviceId = deviceId,
                    DeviceRole = Core.Enums.DeviceRole.Owner,
                    Created = DateTime.UtcNow,
                };

                var userDeviceRoleRepository = new UserDeviceRoleRepository(fixture.Context, mapper);
                await userDeviceRoleRepository.Create(userDeviceRole);

                Assert.NotNull(fixture.Context.UserDeviceRoles.FirstOrDefault(x => x.UserId == "1" && x.DeviceId == deviceId));
            }
        }

        [Fact]
        public async Task Delete()
        {
            var mapper = MapperHelpers.GetMapper();
            var deviceId = Guid.NewGuid();
            var userDeviceRole = GetUserDeviceRoleEntities(deviceId).First();

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
                context.UserDeviceRoles.Add(userDeviceRole);
                context.ChangeTracker.Clear();
            }))
            {
                var userDeviceRoleRepository = new UserDeviceRoleRepository(fixture.Context, mapper);
                await userDeviceRoleRepository.Delete(userDeviceRole.DeviceId, userDeviceRole.UserId);

                Assert.Null(fixture.Context.UserDeviceRoles.FirstOrDefault(x => x.UserId == userDeviceRole.UserId && x.DeviceId == userDeviceRole.DeviceId));
            }
        }

        private static IEnumerable<UserDeviceRoleEntity> GetUserDeviceRoleEntities(Guid deviceId)
        {
            var date = DateTime.UtcNow;
            return Enumerable.Range(1, 10)
                .Select(i => new UserDeviceRoleEntity
                {
                    UserId = i.ToString(),
                    DeviceId = deviceId,
                    DeviceRole = (Core.Enums.DeviceRole)(i % 3),
                    Created = date.AddMinutes(i)
                });
        }
    }
}
