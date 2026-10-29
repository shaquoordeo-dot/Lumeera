import { CommonModule } from '@angular/common';
import { Component, OnInit, signal } from '@angular/core';
import { Customer } from '../../models/customer';
import { CustomerService } from '../../services/customer-service';

@Component({
  imports: [CommonModule],
  selector: 'app-customers',
  styleUrl: './customers.css',
  templateUrl: './customers.html',
})
export class Customers implements OnInit {
  customers = signal<Customer[]>([]);
  loading = signal(true);
  loadError = signal<string | null>(null);

  constructor(private customerService: CustomerService) {}

  ngOnInit(): void {
    this.customerService.getAllCustomers().subscribe({
      next: (data) => {
         console.log('Customers response:', data);
        this.customers.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        console.error('Error fetching customers', err);
        this.loadError.set('Unable to load customers. Check that the API is running and try again.');
        this.loading.set(false);
      },
    });
  }
}
