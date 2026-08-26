using System;
using System.Collections.Generic;
using System.Text;

using EventTicket.Domain.Enums;

namespace EventTicket.Domain.Entities;

public class Ticket
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public int? SeatId { get; set; }

    public int SectionId { get; set; }

    public decimal Price { get; set; }

    public TicketStatus Status { get; set; }

    public Event? Event { get; set; }

    public User? User { get; set; }

    public Seat? Seat { get; set; }

    public Section? Section { get; set; }
}