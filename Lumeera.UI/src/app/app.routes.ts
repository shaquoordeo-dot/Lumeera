import { Routes } from '@angular/router';
import { CustomerRegistration } from './component/customer-registration/customer-registration';
import { Customers } from './component/customers/customers';


export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'customers' },
  { path: 'customers', component: Customers },
  { path: 'customer-registration', component: CustomerRegistration },
  { path: '**', redirectTo: 'customers' },
];
