import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Home } from './pages/home/home';
import { Clients } from './pages/clients/clients';
import { ClientDetail } from './pages/client-detail/client-detail';
import { ClientEdit } from './pages/client-edit/client-edit';
import { ClientNew } from './pages/client-new/client-new';
import { UserNew } from './pages/user-new/user-new';
import { Users } from './pages/users/users';
import { UserEdit } from './pages/user-edit/user-edit';
import { Demandes } from './pages/demandes/demandes';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: '', component: Home, canActivate: [authGuard] },
  { path: 'clients', component: Clients, canActivate: [authGuard] },
  { path: 'clients/nouveau', component: ClientNew, canActivate: [authGuard] },
  { path: 'clients/:id/modifier', component: ClientEdit, canActivate: [authGuard] },
  { path: 'clients/:id', component: ClientDetail, canActivate: [authGuard] },
  { path: 'utilisateurs', component: Users, canActivate: [authGuard] },
  { path: 'utilisateurs/nouveau', component: UserNew, canActivate: [authGuard] },
  { path: 'utilisateurs/:id/modifier', component: UserEdit, canActivate: [authGuard] },
  { path: 'demandes', component: Demandes, canActivate: [authGuard] },
];
