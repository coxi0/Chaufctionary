import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { UtilisateurApi } from '../../services/api/utilisateur';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-user-new',
  imports: [ReactiveFormsModule],
  templateUrl: './user-new.html',
  styleUrl: './user-new.css'
})
export class UserNew {
  private fb = inject(FormBuilder);
  private api = inject(UtilisateurApi);
  private authState = inject(AuthState);

  erreur = signal<string | null>(null);
  succes = signal(false);

  form = this.fb.nonNullable.group({
    nom: ['', Validators.required],
    prenom: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    motDePasse: ['', Validators.required],
    roleId: [1, Validators.required]
  });

  rolesDisponibles(): { id: number; libelle: string }[] {
    if (this.authState.role() === 'Admin') {
      return [{ id: 1, libelle: 'Chauffeur' }, { id: 2, libelle: 'Planneur' }];
    }
    return [{ id: 1, libelle: 'Chauffeur' }];
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.erreur.set(null);
    this.api.creer(this.form.getRawValue()).subscribe({
      next: () => { this.succes.set(true); this.form.reset({ roleId: 1 }); },
      error: () => this.erreur.set('La création a échoué.')
    });
  }
}
