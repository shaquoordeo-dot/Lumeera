export interface Customer {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  passwordHash: string | null;
  phone: string | null;
  isActive: boolean;
  createdUtc: string;
}
