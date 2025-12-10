import { Component } from '@angular/core';
import { ClienteService } from '../../services/cliente.service';
import { Router } from '@angular/router';

// Add this interface before the component
interface DepositoResponse {
  status_code: number;
  message: string;
  data: {
    token: string;
    [key: string]: any;
  };
}

@Component({
  selector: 'app-depositar',
  templateUrl: './depositar.component.html'
})
export class DepositarComponent {

  model = {
    agencia: '',
    conta: '',
    dac: '',
    valor: ''
  };

  simulacao: any = null;
  token = '';
  mensagem = '';
  erro = '';
  user: any = JSON.parse(localStorage.getItem('user') || '{}');


  constructor(private clienteService: ClienteService, private  router: Router) {
    this.model.agencia = this.user.agencia;
    this.model.conta = this.user.conta;
    this.model.dac = this.user.dac;
  }

  simular() {

if(this.model.valor=='')
  return alert('Informe o valor');

    const payload = {
      agencia: Number(this.model.agencia),
      conta: Number(this.model.conta),
      dac: Number(this.model.dac),
      valor: Number(this.model.valor)
    };

    this.clienteService.simularDeposito(payload).subscribe({
      next: (res: DepositoResponse) => {
        if (res.status_code === 200) 
        {
          this.simulacao = res.data;
          this.token = res.data.token;
          this.mensagem = res.message;
          this.erro = '';
        } 
        else
        {
          this.erro = res.message;
        }
      },
      error: err => {
        this.erro = err.error?.message || 'Erro na simulação.';
      }
    });
  }

  efetivar() {
    const payload = {
      agencia: Number(this.model.agencia),
      conta: Number(this.model.conta),
      dac: Number(this.model.dac),
      valor: Number(this.model.valor),
      token: this.token
    };

    this.clienteService.efetivarDeposito(payload).subscribe({
      next: (res: DepositoResponse) => {
        if (res.status_code === 200) {
          alert(res.message);
          this.router.navigate(['/home']);
        } else {
          this.erro = res.message;
        }
      },
      error: err => {
        this.erro = err.error?.message || 'Erro ao efetivar depósito.';
      }
    });
  }
    inicio() {
    this.router.navigate(['/home']);
  }
}
