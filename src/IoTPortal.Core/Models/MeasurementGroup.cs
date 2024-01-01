#nullable disable

namespace IoTPortal.Core.Models
{
    public class MeasurementGroup
    {
        public long Id { get; set; }
        public required Guid DeviceId { get; set; }

        public double[] Measurements { get; set; }

        public DateTime Created { get; set; }   
    }
}
