using System.Collections.Generic;
using System.Threading.Tasks;
using LumeeraCare.Models;

namespace LumeeraCare.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(long id);
        Task<Customer> CreateAsync(Customer customer);
        Task<bool> UpdateAsync(long id, Customer customer);
        Task<bool> DeleteAsync(long id);
        Task<bool> EmailExistsAsync(string email);
    }
}