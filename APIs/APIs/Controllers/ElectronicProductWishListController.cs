using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Authorize(Roles = "Buyer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ElectronicProductWishListController : ControllerBase
    {
        private readonly GraduationDataBaseContext _context;

        public ElectronicProductWishListController(GraduationDataBaseContext context)
        {
            _context = context;
        }

        // POST: api/electronicswishlist
        [HttpPost]
        public async Task<ActionResult<ElectronicsWishlist>> PostElectronicsWishlist(WishListDTO electronicsWishlistDTO)
        {
            var existingWishlistItem = await _context.ElectronicsWishlists.FirstOrDefaultAsync(ew =>
                ew.Email == electronicsWishlistDTO.Email && ew.ProductId == electronicsWishlistDTO.ProductId);

            if (existingWishlistItem != null)
            {
                return Conflict("Item already exists in the wishlist");
            }

            var electronicsWishlist = new ElectronicsWishlist
            {
                Email = electronicsWishlistDTO.Email,
                ProductId = electronicsWishlistDTO.ProductId,
                Comment = electronicsWishlistDTO.Comment
            };

            _context.ElectronicsWishlists.Add(electronicsWishlist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Handle unique constraint violation or other database update errors
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetElectronicsWishlist), new { email = electronicsWishlistDTO.Email, productId = electronicsWishlistDTO.ProductId }, electronicsWishlist);
        }

        // GET: api/electronicswishlist/example@g.com/1
        [HttpGet("{email}/{productId}")]
        public async Task<ActionResult<ElectronicsWishlist>> GetElectronicsWishlist(string email, int productId)
        {
            var electronicsWishlist = await _context.ElectronicsWishlists.FirstOrDefaultAsync(ew => ew.Email == email && ew.ProductId == productId);

            if (electronicsWishlist == null)
            {
                return NotFound();
            }

            return electronicsWishlist;
        }

        // PUT: api/electronicswishlist/example@g.com/1
        [HttpPut("{email}/{productId}")]
        public async Task<IActionResult> UpdateElectronicsWishlist(string email, int productId, WishListDTO electronicsWishlistDTO)
        {
            if (email != electronicsWishlistDTO.Email || productId != electronicsWishlistDTO.ProductId)
            {
                return BadRequest();
            }

            var electronicsWishlist = await _context.ElectronicsWishlists.FirstOrDefaultAsync(ew => ew.Email == email && ew.ProductId == productId);

            if (electronicsWishlist == null)
            {
                return NotFound();
            }

            // Update electronics wishlist details
            electronicsWishlist.Comment = electronicsWishlistDTO.Comment;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Handle unique constraint violation or other database update errors
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/electronicswishlist/example@g.com/1
        [HttpDelete("{email}/{productId}")]
        public async Task<IActionResult> DeleteElectronicsWishlist(string email, int productId)
        {
            var electronicsWishlist = await _context.ElectronicsWishlists.FirstOrDefaultAsync(ew => ew.Email == email && ew.ProductId == productId);

            if (electronicsWishlist == null)
            {
                return NotFound();
            }

            _context.ElectronicsWishlists.Remove(electronicsWishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElectronicsWishlistExists(string email, int productId)
        {
            return _context.ElectronicsWishlists.Any(ew => ew.Email == email && ew.ProductId == productId);
        }
    }

}
