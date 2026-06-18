import { Component, inject, signal, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { UtilisateurApi, Utilisateur } from '../../services/api/utilisateur';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-users',
  imports: [RouterLink],
  templateUrl: './users.html',
  styleUrl: './users.css'
})
export class Users implements OnInit {
  private api = inject(UtilisateurApi);
  private authState = inject(AuthState);

  utilisateurs = signal<Utilisateur[]>([]);

  ngOnInit(): void {
    this.charger();
  }

  charger(): void {
    this.api.lister().subscribe(data => this.utilisateurs.set(data));
  }

  peutGerer(u: Utilisateur): boolean {
    const role = this.authState.role();
    if (role === 'Admin') return u.roleId === 1 || u.roleId === 2;
    if (role === 'Planneur') return u.roleId === 1;
    return false;
  }

  supprimer(u: Utilisateur): void {
    if (!confirm(`Supprimer ${u.email} ?`)) return;
    this.api.supprimer(u.id).subscribe(() => this.charger());
  }
}
