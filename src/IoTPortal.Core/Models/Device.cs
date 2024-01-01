#nullable disable

using IoTPortal.Core.Enums;

namespace IoTPortal.Core.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public DeviceState State { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public string ApiKey { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<UserDeviceRole> UserDeviceRoles { get; set; }
        public ICollection<MeasurementType> MeasurementTypes { get; set; }
    }
}
