import { Component, inject, signal, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ClientApi, Client } from '../../services/api/client';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-client-detail',
  imports: [RouterLink],
  templateUrl: './client-detail.html',
  styleUrl: './client-detail.css'
})
export class ClientDetail implements OnInit {
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private api = inject(ClientApi);
  private authState = inject(AuthState);

  private id = 0;
  client = signal<Client | null>(null);

  peutModifier(): boolean {
    const role = this.authState.role();
    return role === 'Planneur' || role === 'Admin';
  }

  peutSupprimer(): boolean {
    const role = this.authState.role();
    return role === 'Planneur' || role === 'Admin';
  }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.api.getById(this.id).subscribe(c => this.client.set(c));
  }

  supprimer(): void {
    if (!confirm('Supprimer ce client ?')) return;
    this.api.supprimer(this.id).subscribe(() => this.router.navigate(['/clients']));
  }
}
