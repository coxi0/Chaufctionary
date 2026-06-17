import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Home } from './pages/home/home';
import { Clients } from './pages/clients/clients';
import { ClientDetail } from './pages/client-detail/client-detail';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: '', component: Home, canActivate: [authGuard] },
  { path: 'clients', component: Clients, canActivate: [authGuard] },
  { path: 'clients/:id', component: ClientDetail, canActivate: [authGuard] },
];
