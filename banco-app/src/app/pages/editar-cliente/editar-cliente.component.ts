import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService } from '../../services/cliente.service';


@Component({
  selector: 'app-editar-cliente',
  templateUrl: './editar-cliente.component.html'
})

export class EditarClienteComponent implements OnInit {

  id!: number;

  nome = '';
  email = '';

  error = '';
  success = '';

  constructor(
    private route: ActivatedRoute,
    private clienteService: ClienteService,
    private router: Router
  ) {}

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.carregarCliente();
  }

  carregarCliente() {
    this.clienteService.listarClientes().subscribe({
      next: res => {
        const cliente = res.data.find(c => c.id === this.id);

        if (!cliente) {
          this.error = 'Cliente não encontrado';
          return;
        }

        this.nome = cliente.nome;
        this.email = cliente.email;
      },
      error: () => {
        this.error = 'Erro ao carregar dados do cliente.';
      }
    });
  }

  salvar() {
    const payload = {
      nome: this.nome,
      email: this.email
    };

    this.clienteService.atualizarCliente(this.id, payload).subscribe({
      next: res => {
        if (res.status_code === 202) {
          this.success = 'Cliente atualizado com sucesso!';
          //colocar um alert javascript
          alert('Cliente atualizado com sucesso!');
          this.router.navigate(['/home']);
        } else {
          this.error = res.message;
        }
      },
      error: () => {
        this.error = 'Erro ao atualizar cliente.';
      }
    });
  }

  cancelar()
  {
    this.router.navigate(['/contas'])
  }
}
