namespace EventTicket.Domain.Entities;

public class Event
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public int VenueId { get; set; }

    public Venue? Venue { get; set; }
}
