namespace crm.api.Entities;

public class Organization(string name)
{
    public int Id { get; init; }
    public string Name { get; init; } = name;
    public string? Description { get; init; }
    public string? Cnpj { get; init; }
    public Category? Category { get; init; }
    public LeadOrigin? LeadOrigin { get; init; }
    public Sector? Sector { get; init; }
}