namespace IoTPortal.Data.EF.Entities
{
    public sealed class MeasurementTypeEntity
    {
        public Guid Id { get; set; }
        public required Guid DeviceId { get; set; }
        public DeviceEntity? Device { get; set; }

        public required string Unit { get; set; }
        public required string Name { get; set; }
        public required string Variable { get; set; }
        public required string Color { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }
    }
}
