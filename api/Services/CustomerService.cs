using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LumeeraCare.Data;
using LumeeraCare.Models;
using Microsoft.EntityFrameworkCore;

namespace LumeeraCare.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .AsNoTracking()
                .OrderBy(c => c.LastName)
                .ThenBy(c => c.FirstName)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Customer?> GetByIdAsync(long id)
        {
            return await _context.Customers.FindAsync(id).ConfigureAwait(false);
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            customer.Email = customer.Email.Trim().ToLowerInvariant();
            customer.CreatedUtc = DateTime.UtcNow;
            customer.IsActive = customer.IsActive;

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return customer;
        }

        public async Task<bool> UpdateAsync(long id, Customer customer)
        {
            var existing = await _context.Customers.FindAsync(id).ConfigureAwait(false);
            if (existing == null)
            {
                return false;
            }

            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;
            existing.Email = customer.Email.Trim().ToLowerInvariant();
            existing.Phone = customer.Phone;
            existing.IsActive = customer.IsActive;

            if (!string.IsNullOrWhiteSpace(customer.PasswordHash))
            {
                existing.PasswordHash = customer.PasswordHash;
            }

            _context.Customers.Update(existing);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existing = await _context.Customers.FindAsync(id).ConfigureAwait(false);
            if (existing == null)
            {
                return false;
            }

            _context.Customers.Remove(existing);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var normalized = email.Trim().ToLowerInvariant();
            return await _context.Customers.AnyAsync(c => c.Email == normalized).ConfigureAwait(false);
        }
    }
}