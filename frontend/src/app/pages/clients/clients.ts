import { Component, inject, signal, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ClientApi, Client } from '../../services/api/client';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-clients',
  imports: [FormsModule, RouterLink],
  templateUrl: './clients.html',
  styleUrl: './clients.css'
})
export class Clients implements OnInit {
  private api = inject(ClientApi);
  private authState = inject(AuthState);

  clients = signal<Client[]>([]);
  recherche = '';

  peutCreer(): boolean {
    const role = this.authState.role();
    return role === 'Planneur' || role === 'Admin';
  }

  ngOnInit(): void {
    this.charger();
  }

  charger(): void {
    this.api.rechercher(this.recherche).subscribe(data => this.clients.set(data));
  }
}
