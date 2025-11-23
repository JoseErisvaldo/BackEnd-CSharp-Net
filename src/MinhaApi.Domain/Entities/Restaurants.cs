namespace MinhaApi.Entities;

public class Restaurants
{
    public Guid Id { get; set; }

    public Guid EstablishmentId { get; set; }
    public Establishments Establishment { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public bool Active { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
