import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  agencia = '';
  conta = '';
  dac = '';
  senha = '';

  error = '';

  constructor(private auth: AuthService, private router: Router) {}

  logar() {
    const payload = {
      agencia: this.agencia,
      conta: this.conta,
      dac: this.dac,
      senha: this.senha
    };
    this.auth.login(payload).subscribe({
      next: res => {
        console.log(res); 
        if (res.status_code === 200) {
          this.router.navigate(['/home']);
        } else {
          this.error = res.message || 'Erro no login';
        }
      },
      error: err => {
        this.error = err.error.message;
      } 
    });
  }
}
