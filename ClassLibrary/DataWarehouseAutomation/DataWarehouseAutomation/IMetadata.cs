namespace DataWarehouseAutomation
{
    public interface IMetadata
    {
        string Id { get; set; }

        string Name { get; set; }

        string? Notes { get; set; }

        List<Extension>? Extensions { get; set; }
    }
}