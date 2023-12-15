using IoTPortal.Core.Enums;

namespace IoTPortal.Core.Models
{
    public class UserDeviceRole
    {
        public required string UserId { get; set; }
        public Guid DeviceId { get; set; }
        public Device? Device { get; set; }
        public DeviceRole DeviceRole { get; set; }

        public DateTime Created { get; set; }
    }
}
