import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Home } from './pages/home/home';
import { Clients } from './pages/clients/clients';
import { ClientDetail } from './pages/client-detail/client-detail';
import { ClientEdit } from './pages/client-edit/client-edit';
import { ClientNew } from './pages/client-new/client-new';
import { UserNew } from './pages/user-new/user-new';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: '', component: Home, canActivate: [authGuard] },
  { path: 'clients', component: Clients, canActivate: [authGuard] },
  { path: 'clients/nouveau', component: ClientNew, canActivate: [authGuard] },
  { path: 'clients/:id/modifier', component: ClientEdit, canActivate: [authGuard] },
  { path: 'clients/:id', component: ClientDetail, canActivate: [authGuard] },
  { path: 'utilisateurs/nouveau', component: UserNew, canActivate: [authGuard] },
];
