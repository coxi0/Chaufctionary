import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Client } from './client';

@Injectable({ providedIn: 'root' })
export class FavoriApi {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/api/favoris`;

  mesFavoris(): Observable<Client[]> {
    return this.http.get<Client[]>(this.url);
  }

  ajouter(clientId: number): Observable<void> {
    return this.http.post<void>(`${this.url}/${clientId}`, {});
  }

  supprimer(clientId: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${clientId}`);
  }
}
