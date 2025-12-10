import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteService } from '../../services/cliente.service';

@Component({
  selector: 'app-efetivar-saque',
  templateUrl: './efetivar-saque.component.html'
})
export class EfetivarSaqueComponent {

  error = '';
  success = '';

    user: any = JSON.parse(localStorage.getItem('user') || '{}');

  saque: any;

  constructor(private clienteService: ClienteService, private router: Router) {

    this.saque = JSON.parse(sessionStorage.getItem('saque_simulado')!);
  }

  efetivar() {
    const payload = {
      agencia: this.user.agencia,
      conta: this.user.conta,
      dac: this.user.dac,
      senha: this.user.senha,
      valor: this.saque.valor,
      token: this.saque.token
    };

    this.clienteService.efetivarSaque(payload).subscribe({
      next: res => {
        if (res.status_code === 200) {
          this.success = res.message;
          sessionStorage.removeItem('saque_simulado');
          alert('Saque realizado com sucesso');
          setTimeout(() => this.router.navigate(['/home']), 1000);
        } else {
          this.error = res.message;
        }
      },
      error: err => {
        this.error = err.error.message;
      }
    });
  }

  inicio() {
    this.router.navigate(['/home']);
  }
}
