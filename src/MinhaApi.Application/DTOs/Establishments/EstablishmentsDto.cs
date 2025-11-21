namespace MinhaApi.DTOs;

public class EstablishmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime Created_At { get; set; }
}
