import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './pages/login/login.component';
import { HomeComponent } from './pages/home/home.component';
import { AuthService } from './services/auth.service';
import { ClienteService } from './services/cliente.service';
import { AuthGuard } from './guards/auth.guard';
import { ContasComponent } from './pages/contas/contas.component';
import { NovaContaComponent } from './pages/nova-conta/nova-conta.component';
import { DepositarComponent } from './pages/depositar/depositar.component';
import { EditarClienteComponent } from './pages/editar-cliente/editar-cliente.component';
import { SaqueComponent } from './pages/saque/saque.component';
import { EfetivarSaqueComponent } from './pages/efetivar-saque/efetivar-saque.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    ContasComponent,
    NovaContaComponent,
    DepositarComponent,
    EditarClienteComponent,
    SaqueComponent,
    EfetivarSaqueComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [AuthService, ClienteService, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
