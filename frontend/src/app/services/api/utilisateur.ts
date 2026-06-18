import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface CreerUtilisateur {
  nom: string;
  prenom: string;
  email: string;
  motDePasse: string;
  roleId: number;
}

@Injectable({ providedIn: 'root' })
export class UtilisateurApi {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/api/utilisateurs`;

  creer(utilisateur: CreerUtilisateur): Observable<void> {
    return this.http.post<void>(this.url, utilisateur);
  }
}
