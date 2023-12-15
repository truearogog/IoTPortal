using IoTPortal.Core.Enums;
using IoTPortal.Core.Models;

namespace IoTPortal.Data.EF.Entities
{
    public sealed class UserDeviceRoleEntity
    {
        public required string UserId { get; set; }
        public Guid DeviceId { get; set; }
        public DeviceEntity? Device { get; set; }
        public DeviceRole DeviceRole { get; set; }

        public DateTime Created { get; set; }
    }
}
