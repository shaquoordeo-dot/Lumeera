import { HttpClient } from '@angular/common/http';
import { inject, Injectable, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { Customers } from '../component/customers/customers';
import { Customer } from '../models/customer';
@Injectable({
providedIn:'root'
})

export class CustomerService {
    private apiURL='https://localhost:7003/api/Customers';
    constructor(private http:HttpClient){}
    getAllCustomers():Observable<Customer[]>{
        return this.http.get<Customer[]>(this.apiURL);

    }
}
