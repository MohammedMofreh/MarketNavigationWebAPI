using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamingWishListController : ControllerBase
    {
        private readonly GraduationDataBaseContext _context;

        public GamingWishListController(GraduationDataBaseContext context)
        {
            _context = context;
        }

        // POST: api/gamingwishlist
        [HttpPost]
        public async Task<ActionResult<GamingWishlist>> PostGamingWishlist(WishListDTO gamingWishlistDTO)
        {
            var existingWishlistItem = await _context.GamingWishlists.FirstOrDefaultAsync(gw =>
                gw.Email == gamingWishlistDTO.Email && gw.ProductId == gamingWishlistDTO.ProductId);

            if (existingWishlistItem != null)
            {
                return Conflict("Item already exists in the wishlist");
            }

            var gamingWishlist = new GamingWishlist
            {
                Email = gamingWishlistDTO.Email,
                ProductId = gamingWishlistDTO.ProductId,
                Comment = gamingWishlistDTO.Comment
            };

            _context.GamingWishlists.Add(gamingWishlist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Handle unique constraint violation or other database update errors
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetGamingWishlist), new { email = gamingWishlistDTO.Email, productId = gamingWishlistDTO.ProductId }, gamingWishlist);
        }

        // GET: api/gamingwishlist/example@g.com/1
        [HttpGet("{email}/{productId}")]
        public async Task<ActionResult<GamingWishlist>> GetGamingWishlist(string email, int productId)
        {
            var gamingWishlist = await _context.GamingWishlists.FirstOrDefaultAsync(gw => gw.Email == email && gw.ProductId == productId);

            if (gamingWishlist == null)
            {
                return NotFound();
            }

            return gamingWishlist;
        }

        // PUT: api/gamingwishlist/example@g.com/1
        [HttpPut("{email}/{productId}")]
        public async Task<IActionResult> UpdateGamingWishlist(string email, int productId, WishListDTO gamingWishlistDTO)
        {
            if (email != gamingWishlistDTO.Email || productId != gamingWishlistDTO.ProductId)
            {
                return BadRequest();
            }

            var gamingWishlist = await _context.GamingWishlists.FirstOrDefaultAsync(gw => gw.Email == email && gw.ProductId == productId);

            if (gamingWishlist == null)
            {
                return NotFound();
            }

            // Update gaming wishlist details
            gamingWishlist.Comment = gamingWishlistDTO.Comment;

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

        // DELETE: api/gamingwishlist/example@g.com/1
        [HttpDelete("{email}/{productId}")]
        public async Task<IActionResult> DeleteGamingWishlist(string email, int productId)
        {
            var gamingWishlist = await _context.GamingWishlists.FirstOrDefaultAsync(gw => gw.Email == email && gw.ProductId == productId);

            if (gamingWishlist == null)
            {
                return NotFound();
            }

            _context.GamingWishlists.Remove(gamingWishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GamingWishlistExists(string email, int productId)
        {
            return _context.GamingWishlists.Any(gw => gw.Email == email && gw.ProductId == productId);
        }
    }
}

