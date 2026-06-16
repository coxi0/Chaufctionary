import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth } from '../../services/api/auth';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  private fb = inject(FormBuilder);
  private auth = inject(Auth);
  private authState = inject(AuthState);
  private router = inject(Router);

  erreur = signal<string | null>(null);

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    motDePasse: ['', Validators.required]
  });

  onSubmit(): void {
    if (this.form.invalid) return;
    const { email, motDePasse } = this.form.value;

    this.auth.login(email!, motDePasse!).subscribe({
      next: (res) => {
        this.authState.setToken(res.token);
        this.router.navigate(['/']);
      },
      error: () => this.erreur.set('Email ou mot de passe incorrect')
    });
  }
}
