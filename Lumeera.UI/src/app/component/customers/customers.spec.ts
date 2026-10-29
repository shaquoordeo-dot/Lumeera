import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { Customers } from './customers';

describe('Customers', () => {
  let component: Customers;
  let fixture: ComponentFixture<Customers>;
  let httpTestingController: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Customers],
      providers: [provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();

    fixture = TestBed.createComponent(Customers);
    component = fixture.componentInstance;
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display customers returned by the API', () => {
    fixture.detectChanges();

    httpTestingController.expectOne('http://localhost:7003/api/Customers').flush([
      {
        id: 1,
        firstName: 'Ada',
        lastName: 'Lovelace',
        email: 'ada@example.com',
        phone: '555-0100',
        passwordHash: null,
        isActive: true,
        createdUtc: '2026-10-29T00:00:00Z',
      },
    ]);

    fixture.detectChanges();

    expect(fixture.nativeElement.textContent).toContain('Ada');
    expect(fixture.nativeElement.textContent).toContain('Lovelace');
    expect(fixture.nativeElement.textContent).toContain('ada@example.com');
    expect(fixture.nativeElement.textContent).toContain('555-0100');
  });

  it('should show an error when the API request fails', () => {
    fixture.detectChanges();

    httpTestingController
      .expectOne('http://localhost:7003/api/Customers')
      .flush('API unavailable', { status: 500, statusText: 'Server Error' });

    fixture.detectChanges();

    expect(fixture.nativeElement.textContent).toContain('Unable to load customers');
  });
});
