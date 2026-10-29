import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer';

@Injectable({ providedIn: 'root' })

export class CustomerService {
  private readonly apiURL = 'https://localhost:7003/api/Customers';

  constructor(private http: HttpClient) {}

  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiURL);
  }
}
