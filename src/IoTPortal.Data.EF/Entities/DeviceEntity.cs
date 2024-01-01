#nullable disable

using IoTPortal.Core.Enums;

namespace IoTPortal.Data.EF.Entities
{
    public sealed class DeviceEntity
    {
        public Guid Id { get; set; }
        public DeviceState State { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public string ApiKey { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }

        public ICollection<UserDeviceRoleEntity> UserDeviceRoles { get; set; }
        public ICollection<MeasurementTypeEntity> MeasurementTypes { get; set; }
        public ICollection<MeasurementGroupEntity> MeasurementGroups { get; set; }
    }
}
