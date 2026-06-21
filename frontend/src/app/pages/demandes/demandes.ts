import { Component, inject, signal, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DemandeApi, Demande } from '../../services/api/demande';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-demandes',
  imports: [RouterLink],
  templateUrl: './demandes.html',
  styleUrl: './demandes.css'
})
export class Demandes implements OnInit {
  private demandeApi = inject(DemandeApi);
  private authState = inject(AuthState);

  demandes = signal<Demande[]>([]);
  erreur = signal<string | null>(null);

  estGestionnaire(): boolean {
    const role = this.authState.role();
    return role === 'Planneur' || role === 'Admin';
  }

  ngOnInit(): void {
    this.charger();
  }

  private charger(): void {
    const source = this.estGestionnaire()
      ? this.demandeApi.toutes()
      : this.demandeApi.mesDemandes();
    source.subscribe({
      next: d => { this.demandes.set(d); this.erreur.set(null); },
      error: () => this.erreur.set('Impossible de charger les demandes.')
    });
  }

  supprimer(id: number): void {
    if (!confirm('Supprimer cette demande ?')) return;
    this.demandeApi.supprimer(id).subscribe(() => this.charger());
  }
}
