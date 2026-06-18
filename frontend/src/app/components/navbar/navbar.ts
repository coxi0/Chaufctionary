import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar {
  private authState = inject(AuthState);
  private router = inject(Router);

  estConnecte(): boolean {
    return this.authState.isLoggedIn();
  }

  role(): string | null {
    return this.authState.role();
  }

  peutGererUtilisateurs(): boolean {
    const role = this.authState.role();
    return role === 'Planneur' || role === 'Admin';
  }

  deconnexion(): void {
    this.authState.logout();
    this.router.navigate(['/login']);
  }
}
