import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Client {
  id: number;
  numero: string;
  nom: string;
  adresse: string;
  ville: string;
  codePostal: string;
  telephone: string;
  latitude: number;
  longitude: number;
  notes: string | null;
}

@Injectable({ providedIn: 'root' })
export class ClientApi {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/api/clients`;

  rechercher(terme: string): Observable<Client[]> {
    return this.http.get<Client[]>(this.url, { params: { recherche: terme } });
  }

  getById(id: number): Observable<Client> {
    return this.http.get<Client>(`${this.url}/${id}`);
  }

  creer(client: Omit<Client, 'id'>): Observable<Client> {
    return this.http.post<Client>(this.url, client);
  }

  modifier(id: number, client: Omit<Client, 'id'>): Observable<Client> {
    return this.http.put<Client>(`${this.url}/${id}`, client);
  }

  supprimer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }
}
