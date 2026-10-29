import { Component } from '@angular/core';
import { Footer } from '../footer/footer';
import { Header } from '../header/header';

@Component({
  imports: [Header, Footer],
  selector: 'app-customer-registration',
  styleUrl: './customer-registration.css',
  templateUrl: './customer-registration.html',
})
export class CustomerRegistration {}
