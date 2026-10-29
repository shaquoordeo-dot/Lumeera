export interface Customer {
    id: number,
    firstName: string,
    lastName: string,
    email: string,
    passwordHash: any,
    phone: number,
    isActive: boolean,
    createdUtc: Date
}
