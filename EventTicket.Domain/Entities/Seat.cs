namespace EventTicket.Domain.Entities;

public class Seat
{
    public int Id { get; set; }

    public string Row { get; set; } = string.Empty;

    public int Number { get; set; }

    public int SectionId { get; set; }

    public Section? Section { get; set; }
}
