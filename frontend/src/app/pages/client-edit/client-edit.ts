import { Component, inject, signal, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ClientApi } from '../../services/api/client';

@Component({
  selector: 'app-client-edit',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './client-edit.html',
  styleUrl: './client-edit.css'
})
export class ClientEdit implements OnInit {
  private fb = inject(FormBuilder);
  private api = inject(ClientApi);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  id = 0;
  erreur = signal<string | null>(null);
  proposition = signal<string | null>(null);

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

  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.proposition.set(this.route.snapshot.queryParamMap.get('proposition'));
    this.api.getById(this.id).subscribe(c =>
      this.form.patchValue({ ...c, notes: c.notes ?? '', conseilAcces: c.conseilAcces ?? '' }));
  }

  appliquerProposition(): void {
    const p = this.proposition();
    if (p !== null) this.form.patchValue({ conseilAcces: p });
  }

  onSubmit(): void {
    if (this.form.invalid) return;
    this.api.modifier(this.id, this.form.getRawValue()).subscribe({
      next: () => this.router.navigate(['/clients', this.id]),
      error: () => this.erreur.set('La modification a échoué.')
    });
  }
}
