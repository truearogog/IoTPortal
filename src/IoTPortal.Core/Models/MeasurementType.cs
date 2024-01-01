#nullable disable

namespace IoTPortal.Core.Models
{
    public class MeasurementType
    {
        public Guid Id { get; set; }
        public required Guid DeviceId { get; set; }

        public required string Variable { get; set; }
        public required string Name { get; set; }
        public required string Unit { get; set; }
        public required string Color { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
