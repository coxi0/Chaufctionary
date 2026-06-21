import { Component, inject, signal, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FavoriApi } from '../../services/api/favori';
import { Client } from '../../services/api/client';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit {
  private favoriApi = inject(FavoriApi);
  private authState = inject(AuthState);

  favoris = signal<Client[]>([]);
  erreur = signal<string | null>(null);

  role(): string | null {
    return this.authState.role();
  }

  peutGererUtilisateurs(): boolean {
    const role = this.role();
    return role === 'Planneur' || role === 'Admin';
  }

  ngOnInit(): void {
    this.favoriApi.mesFavoris().subscribe({
      next: f => this.favoris.set(f),
      error: () => this.erreur.set('Impossible de charger vos favoris.')
    });
  }

  retirer(clientId: number): void {
    this.favoriApi.supprimer(clientId).subscribe(() =>
      this.favoris.update(liste => liste.filter(c => c.id !== clientId)));
  }
}
