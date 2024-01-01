#nullable disable

namespace IoTPortal.Data.EF.Entities
{
    public sealed class MeasurementGroupEntity
    {
        public long Id { get; set; }
        public required Guid DeviceId { get; set; }
        public DeviceEntity Device { get; set; }

        public double[] Measurements { get; set; }

        public DateTime Created { get; set; }
    }
}
