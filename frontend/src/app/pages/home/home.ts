import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  private authState = inject(AuthState);
  private router = inject(Router);

  deconnexion(): void {
    this.authState.logout();
    this.router.navigate(['/login']);
  }
}
