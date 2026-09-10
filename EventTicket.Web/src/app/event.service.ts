

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Seat {
  id: number;
  row: string;
  number: number;
}

export interface Section {
  id: number;
  name: string;
  type: number;
  capacity: number;
  price: number;
  seats: Seat[];
}

export interface EventItem {
  id: number;
  name: string;
  description: string;
  startDate: string;
  venueId: number;
  venue?: {
    id: number;
    name: string;
    sections: Section[];
  };
}

@Injectable({
  providedIn: 'root'
})
export class EventService {

  private apiUrl = 'https://localhost:7212/api/Events';

  constructor(private http: HttpClient) {}

  getEvents(): Observable<EventItem[]> {
    return this.http.get<EventItem[]>(this.apiUrl);
  }
  createReservation(reservation: any): Observable<any> {
    return this.http.post<any>(
      'https://localhost:7212/api/Reservations',
      reservation
    );
  }
  createTicket(ticket: any): Observable<any> {
    return this.http.post<any>(
      'https://localhost:7212/api/Tickets',
      ticket
    );
  }
  getTicketsByEvent(eventId: number): Observable<any[]> {
    return this.http.get<any[]>(
      `https://localhost:7212/api/Tickets/event/${eventId}`
    );
  }
  sellTicket(ticketId: number): Observable<any> {
    return this.http.put<any>(
      `https://localhost:7212/api/Tickets/${ticketId}/sell`,
      {}
    );
  }
}

