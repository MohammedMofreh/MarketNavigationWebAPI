using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerPhoneController : ControllerBase
    {
        private readonly GraduationDataBaseContext _context;

        public SellerPhoneController(GraduationDataBaseContext context)
        {
            _context = context;
        }

        // POST: api/sellerphone
        [HttpPost]
        public async Task<ActionResult<SellerPhone>> PostSellerPhone(SellerPhoneDTO sellerPhoneDTO)
        {
            var existingSellerPhone = await _context.SellerPhones.FirstOrDefaultAsync(sp => sp.Email == sellerPhoneDTO.Email);

            if (existingSellerPhone != null)
            {
                return Conflict("Seller phone with this email already exists");
            }

            var sellerPhone = new SellerPhone
            {
                Email = sellerPhoneDTO.Email,
                PhoneNumber = sellerPhoneDTO.PhoneNumber
            };

            _context.SellerPhones.Add(sellerPhone);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Handle unique constraint violation or other database update errors
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetSellerPhone), new { email = sellerPhoneDTO.Email }, sellerPhone);
        }

        // GET: api/sellerphone/example@g.com
        [HttpGet("{email}")]
        public async Task<ActionResult<SellerPhone>> GetSellerPhone(string email)
        {
            var sellerPhone = await _context.SellerPhones.FirstOrDefaultAsync(sp => sp.Email == email);

            if (sellerPhone == null)
            {
                return NotFound();
            }

            return sellerPhone;
        }

        // PUT: api/sellerphone/example@g.com
        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateSellerPhone(string email, SellerPhoneDTO sellerPhoneDTO)
        {
            if (email != sellerPhoneDTO.Email)
            {
                return BadRequest();
            }

            var sellerPhone = await _context.SellerPhones.FirstOrDefaultAsync(sp => sp.Email == email);

            if (sellerPhone == null)
            {
                return NotFound();
            }

            // Update seller phone details
            sellerPhone.Email = sellerPhoneDTO.Email;
            sellerPhone.PhoneNumber = sellerPhoneDTO.PhoneNumber;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/sellerphone/example@g.com
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteSellerPhone(string email)
        {
            var sellerPhone = await _context.SellerPhones.FirstOrDefaultAsync(sp => sp.Email == email);

            if (sellerPhone == null)
            {
                return NotFound();
            }

            _context.SellerPhones.Remove(sellerPhone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SellerPhoneExists(string email)
        {
            return _context.SellerPhones.Any(sp => sp.Email == email);
        }
    }
}

