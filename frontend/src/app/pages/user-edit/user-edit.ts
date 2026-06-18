import { Component, inject, signal, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { UtilisateurApi } from '../../services/api/utilisateur';
import { AuthState } from '../../services/auth-state';

@Component({
  selector: 'app-user-edit',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './user-edit.html',
  styleUrl: './user-edit.css'
})
export class UserEdit implements OnInit {
  private fb = inject(FormBuilder);
  private api = inject(UtilisateurApi);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private authState = inject(AuthState);

  id = 0;
  erreur = signal<string | null>(null);

  form = this.fb.nonNullable.group({
    nom: ['', Validators.required],
    prenom: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    roleId: [1, Validators.required],
    estActif: [true]
  });

  rolesDisponibles(): { id: number; libelle: string }[] {
    if (this.authState.role() === 'Admin') {
      return [{ id: 1, libelle: 'Chauffeur' }, { id: 2, libelle: 'Planneur' }];
    }
    return [{ id: 1, libelle: 'Chauffeur' }];
  }

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.api.getById(this.id).subscribe(u => this.form.patchValue(u));
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.api.modifier(this.id, this.form.getRawValue()).subscribe({
      next: () => this.router.navigate(['/utilisateurs']),
      error: () => this.erreur.set('La modification a échoué.')
    });
  }
}
