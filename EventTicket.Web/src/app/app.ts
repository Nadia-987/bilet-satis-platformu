

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EventService, EventItem, Seat } from './event.service';

@Component({
  selector: 'app-root',
  imports: [CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {

  events: EventItem[] = [];
  selectedEvent: EventItem | null = null;
  selectedSeat: Seat | null = null;
  soldSeatIds: number[] = [];
  createdTicketId: number | null = null;

  constructor(private eventService: EventService) {}

  ngOnInit(): void {
    this.eventService.getEvents().subscribe({
      next: (data) => {
        
        
        this.events = data;
       
      },
      error: (error) => {
        console.error('Etkinlikler alınamadı:', error);
      }
    });
  }

  selectEvent(event: EventItem): void {
    this.selectedEvent = event;
    this.selectedSeat = null;
    this.soldSeatIds = [];

    this.eventService.getTicketsByEvent(event.id).subscribe({
      next: (tickets) => {
        this.soldSeatIds = tickets
          .filter(ticket => ticket.status === 1)
          .map(ticket => ticket.seatId);
      },
      error: (error) => {
        console.error('Biletler alınamadı:', error);
      }
    });
  }

  selectSeat(seat: Seat): void {
    if (this.soldSeatIds.includes(seat.id)) {
      return;
    }

    this.selectedSeat = seat;
  }
  reserveSeat(): void {
    if (!this.selectedEvent || !this.selectedSeat) {
      return;
    }


    const reservation = {
      eventId: this.selectedEvent.id,
      userId: 1,
      seatId: this.selectedSeat.id,
      sectionId: this.selectedEvent.venue?.sections[0]?.id,
      reservedAt: new Date().toISOString(),
      expiresAt: new Date(Date.now() + 10 * 60 * 1000).toISOString(),
      status: 0
    };

    this.eventService.createReservation(reservation).subscribe({
      next: () => {
        alert('Rezervasyon başarıyla oluşturuldu!');
      },
      error: (error) => {
        alert(error.error || 'Rezervasyon oluşturulamadı.');
        console.error(error);
      }
    });
  }
  buyTicket(): void {
    if (!this.selectedEvent || !this.selectedSeat) {
      return;
    }

    const ticket = {
      eventId: this.selectedEvent.id,
      userId: 1,
      seatId: this.selectedSeat.id,
      sectionId: this.selectedEvent.venue?.sections[0]?.id,
      price: this.selectedEvent.venue?.sections[0]?.price,
      status: 0
    };

    this.eventService.createTicket(ticket).subscribe({
      next: (createdTicket) => {
        this.createdTicketId = createdTicket.id;
        alert('Bilet oluşturuldu!');
      },
      error: (error) => {
        alert(error.error || 'Bilet oluşturulamadı.');
        console.error(error);
      }
    });
  }
  sellTicket(): void {
    if (!this.createdTicketId) {
      return;
    }

    this.eventService.sellTicket(this.createdTicketId).subscribe({
      next: () => {
        alert('Bilet başarıyla satıldı!');
      },
      error: (error) => {
        alert(error.error || 'Bilet satılamadı.');
        console.error(error);
      }
    });
  }
}

