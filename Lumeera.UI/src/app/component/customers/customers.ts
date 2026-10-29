import { Component, OnInit } from '@angular/core';
import { Customer } from '../../models/customer';
import { CustomerService } from '../../services/customer-service';
import { NgForOf } from '../../../../node_modules/@angular/common/types/_common_module-chunk';

@Component({
  imports: [NgForOf],
  selector: 'app-customers',
  styleUrl: './customers.css',
  templateUrl: './customers.html',
})
export class Customers implements OnInit{
  customers:Customer[]=[];
  constructor(private customerService:CustomerService){}
  ngOnInit(): void {
    this.customerService.getAllCustomers().subscribe({
      next:(data)=>{
        this.customers=data;
      },    
    error:(err)=>{
      console.error('error fetching customer',err)
    }
    })
  }
}
