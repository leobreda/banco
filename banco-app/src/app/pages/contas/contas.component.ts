import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteService } from '../../services/cliente.service';

@Component({
  selector: 'app-contas',
  templateUrl: './contas.component.html'
})
export class ContasComponent  {

  clientes: any[] = [];
  error = '';
  user: any = JSON.parse(localStorage.getItem('user') || '{}');
  constructor(private clienteService: ClienteService, private  router: Router) {}

  ngOnInit(): void {
    this.carregarContas();
  }

  carregarContas() {
    this.clienteService.listarClientes().subscribe({
      next: res => {
        if (res.status_code === 200) {
          this.clientes = res.data;
        } else {
          this.error = res.message;
        }
      },
      error: err => {
        this.error = err.error?.message || 'Erro ao buscar contas.';
      }
    });
  }

excluirCliente(id: number) {
console.log(this.user);
  if(id==this.user.id)
    return alert("Você não pode excluir a sua própria conta!");

    if (!confirm('Deseja realmente excluir este cliente?')) {
      return;
    }

    this.clienteService.excluirCliente(id).subscribe({
      next: res => {
        if (res.status_code === 202) {
          this.clientes = this.clientes.filter(c => c.id !== id);
        } else {
          this.error = res.message;
        }
      },
      error: () => {
        this.error = 'Erro ao tentar excluir o cliente.';
      }
    });
  }


    inicio() {
    this.router.navigate(['/home']);
  }
}
 