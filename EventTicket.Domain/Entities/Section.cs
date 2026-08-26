using EventTicket.Domain.Enums;
namespace EventTicket.Domain.Entities;

public class Section
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public SectionType Type { get; set; }

    public int Capacity { get; set; }

    public decimal Price { get; set; }

    public int VenueId { get; set; }

    public Venue? Venue { get; set; }

    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}