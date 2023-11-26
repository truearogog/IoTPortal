namespace IoTPortal.Data.EF.Entities
{
    public sealed class DeviceEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string UserId { get; set; }

        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }
    }
}
