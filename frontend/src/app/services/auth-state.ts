import { Injectable, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthState {
  private readonly tokenKey = 'token';

  token = signal<string | null>(localStorage.getItem(this.tokenKey));

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
    this.token.set(token);
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.token.set(null);
  }

  isLoggedIn(): boolean {
    return this.token() !== null;
  }
}
