import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteService } from '../../services/cliente.service';

@Component({
  selector: 'app-saque',
  templateUrl: './saque.component.html'
})
export class SaqueComponent {

  valor: string = '';
  
  error = '';
  token = '';

  user: any = JSON.parse(localStorage.getItem('user') || '{}');

  constructor(private clienteService: ClienteService, private router: Router) {
  //  this.conta = JSON.parse(sessionStorage.getItem('user')!);
    console.log(this.user);
  }

  simular() {
    const payload = {
      agencia: this.user.agencia,
      conta: this.user.conta,
      dac: this.user.dac,
      senha: this.user.senha,
      valor: this.valor
    };

    this.clienteService.simularSaque(payload).subscribe({
      next: res => {
        if (res.status_code === 200) {
          this.token = res.data.token;

          // enviar token e valor para a próxima página
          sessionStorage.setItem('saque_simulado', JSON.stringify({
            valor: this.valor,
            token: this.token
          }));

          this.router.navigate(['/saque/efetivar']);
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
