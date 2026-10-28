using System.Collections.Generic;
using System.Threading.Tasks;
using LumeeraCare.Models;
using LumeeraCare.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LumeeraCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customers;

        public CustomersController(ICustomerService customers)
        {
            _customers = customers;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var result = await _customers.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Customer>> GetById(long id)
        {
            var customer = await _customers.GetByIdAsync(id).ConfigureAwait(false);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Customer request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName))
            {
                return BadRequest("FirstName, LastName and Email are required.");
            }

            if (await _customers.EmailExistsAsync(request.Email).ConfigureAwait(false))
            {
                return BadRequest("Email is already in use.");
            }

            var created = await _customers.CreateAsync(request).ConfigureAwait(false);
            return CreatedAtRoute("GetCustomer", new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] Customer request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            var ok = await _customers.UpdateAsync(id, request).ConfigureAwait(false);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var ok = await _customers.DeleteAsync(id).ConfigureAwait(false);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}