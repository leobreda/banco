import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { ContasComponent } from './pages/contas/contas.component';
import { NovaContaComponent } from './pages/nova-conta/nova-conta.component';
import { DepositarComponent } from './pages/depositar/depositar.component';
import { EditarClienteComponent } from './pages/editar-cliente/editar-cliente.component';
import { SaqueComponent } from './pages/saque/saque.component';
import { EfetivarSaqueComponent } from './pages/efetivar-saque/efetivar-saque.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'contas', component: ContasComponent },
  {path: 'clientes/editar/:id', component: EditarClienteComponent},
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  {  path: 'nova-conta',  component: NovaContaComponent},
  {  path: 'depositar',  component: DepositarComponent},
  {  path: 'saque',component: SaqueComponent},
  {  path: 'saque/efetivar',  component: EfetivarSaqueComponent},
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: '**', redirectTo: 'login' }
];
  
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
