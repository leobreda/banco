import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteService } from '../../services/cliente.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  saldo: any = null;
  user: any = JSON.parse(localStorage.getItem('user') || '{}');

  constructor(private clienteService: ClienteService, private  router: Router, private auth: AuthService) {
    // consultar saldo ao carregar
    this.consultarSaldo();
  }

  

  consultarSaldo() {
    const payload = {
      agencia: this.user.agencia,
      conta: this.user.conta,
      dac: this.user.dac,
      senha: (this.user.agencia.toString().padStart(4, '0')
      +''
      +this.user.conta.toString().padStart(5, '0')
      +''
      +this.user.dac.toString().padStart(1, '0'))
    };
    this.clienteService.consultarSaldo(payload).subscribe({
      next: res => {
        if (res.status_code === 200) {
          this.saldo = res.data;
        }
      },
      error: () => {}
    });
  }

  irParaContas() {
    this.router.navigate(['/contas']);
  }
  irParaNovaConta() {
  this.router.navigate(['/nova-conta']);
}

irParaDepositar() {
  this.router.navigate(['/depositar']);
}

irParaSaque() {
  this.router.navigate(['/saque']);
}



  logout() {
    this.auth.logout();
  }
}
