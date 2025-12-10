import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface ApiResponse<T> {
  status_code: number;
  message: string;
  data: T;
}


@Injectable({ providedIn: 'root' })
export class ClienteService {
  private apiUrl = 'https://localhost:7220';
  constructor(private http: HttpClient) {}

  consultarSaldo(payload: any) {
    return this.http.post<any>(`${this.apiUrl}/clientes/saldo`, payload);
  }

  listarClientes(): Observable<any> {
    return this.http.get(`${this.apiUrl}/clientes`);
  }

atualizarCliente(id: number, payload: any): Observable<ApiResponse<any>> {
  return this.http.put<ApiResponse<any>>(`${this.apiUrl}/clientes/${id}`, payload);
}

criarCliente(payload: any): Observable<any> {
  return this.http.post(`${this.apiUrl}/clientes`, payload);
}

excluirCliente(id: number): Observable<any> {
  return this.http.delete(`${this.apiUrl}/clientes/${id}`);
}

simularDeposito(payload: any) {
  return this.http.post(`${this.apiUrl}/clientes/depositar`, payload);
}

efetivarDeposito(payload: any) {
  return this.http.post(`${this.apiUrl}/clientes/depositar/efetivar`, payload);
}

simularSaque(payload: any): Observable<ApiResponse<any>> {
  return this.http.post<ApiResponse<any>>(`${this.apiUrl}/clientes/saque`, payload);
}

efetivarSaque(payload: any): Observable<ApiResponse<any>> {
  return this.http.post<ApiResponse<any>>(`${this.apiUrl}/clientes/saque/efetivar`, payload);
}


}
