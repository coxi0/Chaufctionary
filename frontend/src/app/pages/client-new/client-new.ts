import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ClientApi } from '../../services/api/client';

@Component({
  selector: 'app-client-new',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './client-new.html',
  styleUrl: './client-new.css'
})
export class ClientNew {
  private fb = inject(FormBuilder);
  private api = inject(ClientApi);
  private router = inject(Router);

  erreur = signal<string | null>(null);

  form = this.fb.nonNullable.group({
    numero: ['', Validators.required],
    nom: ['', Validators.required],
    adresse: [''],
    ville: [''],
    codePostal: [''],
    telephone: [''],
    latitude: [0],
    longitude: [0],
    notes: [''],
    conseilAcces: ['']
  });

  onSubmit(): void {
    if (this.form.invalid) return;
    this.api.creer(this.form.getRawValue()).subscribe({
      next: (c) => this.router.navigate(['/clients', c.id]),
      error: () => this.erreur.set('La création a échoué.')
    });
  }
}
