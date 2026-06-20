import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Demande {
  id: number;
  clientId: number;
  utilisateurId: number;
  message: string;
  dateCreation: string;
  clientNom: string;
  chauffeurNom: string;
  conseilActuel: string | null;
}

@Injectable({ providedIn: 'root' })
export class DemandeApi {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/api/demandes`;

  creer(clientId: number, message: string): Observable<Demande> {
    return this.http.post<Demande>(this.url, { clientId, message });
  }

  mesDemandes(): Observable<Demande[]> {
    return this.http.get<Demande[]>(`${this.url}/mes`);
  }

  toutes(): Observable<Demande[]> {
    return this.http.get<Demande[]>(this.url);
  }

  supprimer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
