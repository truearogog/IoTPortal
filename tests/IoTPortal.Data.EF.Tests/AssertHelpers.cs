using IoTPortal.Core.Models;

namespace IoTPortal.Data.EF.Tests
{
    internal class AssertHelpers
    {
        public static void AssertDevices(Device device1, Device device2)
        {
            Assert.Equal(device1.Id, device2.Id);
            Assert.Equal(device1.State, device2.State);
            Assert.Equal(device1.Name, device2.Name);
            Assert.Equal(device1.Description, device2.Description);
            Assert.Equal(device1.Created, device2.Created);
            Assert.Equal(device1.Updated, device2.Updated);
        }

        public static void AsserMeasurementGroups(MeasurementGroup measurementGroup1, MeasurementGroup measurementGroup2)
        {
            Assert.Equal(measurementGroup1.Id, measurementGroup2.Id);
            Assert.Equal(measurementGroup1.DeviceId, measurementGroup2.DeviceId);
            Assert.Equal(measurementGroup1.Created, measurementGroup2.Created);
            Assert.Equal(measurementGroup1.Measurements, measurementGroup2.Measurements);
        }

        public static void AssertMeasurementTypes(MeasurementType measurementType1, MeasurementType measurementType2)
        {
            Assert.Equal(measurementType1.Id, measurementType2.Id);
            Assert.Equal(measurementType1.DeviceId, measurementType2.DeviceId);
            Assert.Equal(measurementType1.Variable, measurementType2.Variable);
            Assert.Equal(measurementType1.Name, measurementType2.Name);
            Assert.Equal(measurementType1.Unit, measurementType2.Unit);
            Assert.Equal(measurementType1.Color, measurementType2.Color);
            Assert.Equal(measurementType1.Created, measurementType2.Created);
            Assert.Equal(measurementType1.Updated, measurementType2.Updated);
        }

        public static void AssertUserDeviceRoles(UserDeviceRole userDeviceRole1, UserDeviceRole userDeviceRole2)
        {
            Assert.Equal(userDeviceRole1.UserId, userDeviceRole2.UserId);
            Assert.Equal(userDeviceRole1.DeviceId, userDeviceRole2.DeviceId);
            Assert.Equal(userDeviceRole1.DeviceRole, userDeviceRole2.DeviceRole);
            Assert.Equal(userDeviceRole1.Created, userDeviceRole2.Created);
        }
    }
}
