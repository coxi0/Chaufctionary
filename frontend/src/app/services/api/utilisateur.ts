import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Utilisateur {
  id: number;
  nom: string;
  prenom: string;
  email: string;
  estActif: boolean;
  roleId: number;
  role: string;
}

export interface CreerUtilisateur {
  nom: string;
  prenom: string;
  email: string;
  motDePasse: string;
  roleId: number;
}

export interface ModifierUtilisateur {
  nom: string;
  prenom: string;
  email: string;
  estActif: boolean;
  roleId: number;
}

@Injectable({ providedIn: 'root' })
export class UtilisateurApi {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/api/utilisateurs`;

  lister(): Observable<Utilisateur[]> {
    return this.http.get<Utilisateur[]>(this.url);
  }

  getById(id: number): Observable<Utilisateur> {
    return this.http.get<Utilisateur>(`${this.url}/${id}`);
  }

  creer(utilisateur: CreerUtilisateur): Observable<void> {
    return this.http.post<void>(this.url, utilisateur);
  }

  modifier(id: number, utilisateur: ModifierUtilisateur): Observable<void> {
    return this.http.put<void>(`${this.url}/${id}`, utilisateur);
  }

  supprimer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
