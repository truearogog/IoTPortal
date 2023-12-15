#nullable disable

namespace IoTPortal.Core.Models
{
    public class Device
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public ICollection<UserDeviceRole> UserDeviceRoles { get; set; }
    }
}
