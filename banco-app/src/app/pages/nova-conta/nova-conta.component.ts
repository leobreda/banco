import { Component } from '@angular/core';
import { ClienteService } from '../../services/cliente.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nova-conta',
  templateUrl: './nova-conta.component.html'
})
export class NovaContaComponent {

  model = {
    nome: '',
    email: '',
    agencia: '',
    conta: '',
    dac: ''
  };

  mensagem = '';
  erro = '';

  constructor(private clienteService: ClienteService, private router: Router) {}

  salvar() {
    const payload = {
      nome: this.model.nome,
      email: this.model.email,
      agencia: Number(this.model.agencia),
      conta: Number(this.model.conta),
      dac: Number(this.model.dac)
    };

    this.clienteService.criarCliente(payload).subscribe({
      next: res => {
        if (res.status_code === 201) {
          alert(res.message);
          this.router.navigate(['/home']);
        } else {
          this.erro = res.message;
        }
      },
      error: err => {
        this.erro = err.error?.message || 'Erro ao criar cliente.';
      }
    });
  }

  inicio() {
    this.router.navigate(['/home']);
  }
}
