import { Component, inject, signal, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ClientApi, Client } from '../../services/api/client';

@Component({
  selector: 'app-clients',
  imports: [FormsModule, RouterLink],
  templateUrl: './clients.html',
  styleUrl: './clients.css'
})
export class Clients implements OnInit {
  private api = inject(ClientApi);

  clients = signal<Client[]>([]);
  recherche = '';

  ngOnInit(): void {
    this.charger();
  }

  charger(): void {
    this.api.rechercher(this.recherche).subscribe(data => this.clients.set(data));
  }
}
