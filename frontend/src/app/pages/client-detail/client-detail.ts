import { Component, inject, signal, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ClientApi, Client } from '../../services/api/client';
import { FavoriApi } from '../../services/api/favori';
import { DemandeApi } from '../../services/api/demande';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-client-detail',
  imports: [RouterLink, FormsModule],
  templateUrl: './client-detail.html',
  styleUrl: './client-detail.css'
})
export class ClientDetail implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private api = inject(ClientApi);
  private favoriApi = inject(FavoriApi);
  private demandeApi = inject(DemandeApi);
  private authState = inject(AuthState);

  private id = 0;
  client = signal<Client | null>(null);
  estFavori = signal(false);

  messageDemande = '';
  demandeEnvoyee = signal(false);

  peutModifier(): boolean {
    const role = this.authState.role();
    return role === 'Planneur' || role === 'Admin';
  }

  peutSupprimer(): boolean {
    const role = this.authState.role();
    return role === 'Planneur' || role === 'Admin';
  }

  peutDemander(): boolean {
    return this.authState.role() === 'Chauffeur';
  }

  envoyerDemande(): void {
    if (this.messageDemande.trim() === '') return;
    this.demandeApi.creer(this.id, this.messageDemande).subscribe(() => {
      this.messageDemande = '';
      this.demandeEnvoyee.set(true);
    });
  }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.api.getById(this.id).subscribe(c => {
      this.client.set(c);
      this.messageDemande = c.conseilAcces ?? '';   // pré-remplit avec l'accès actuel
    });
    this.favoriApi.mesFavoris().subscribe(favoris =>
      this.estFavori.set(favoris.some(c => c.id === this.id)));
  }

  basculerFavori(): void {
    if (this.estFavori()) {
      this.favoriApi.supprimer(this.id).subscribe(() => this.estFavori.set(false));
    } else {
      this.favoriApi.ajouter(this.id).subscribe(() => this.estFavori.set(true));
    }
  }

  supprimer(): void {
    if (!confirm('Supprimer ce client ?')) return;
    this.api.supprimer(this.id).subscribe(() => this.router.navigate(['/clients']));
  }
}
