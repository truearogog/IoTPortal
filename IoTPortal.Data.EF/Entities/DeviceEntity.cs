namespace IoTPortal.Data.EF.Entities
{
    public sealed class DeviceEntity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
