using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsWishListController : ControllerBase
    {
        private readonly GraduationDataBaseContext _context;

        public SportsWishListController(GraduationDataBaseContext context)
        {
            _context = context;
        }

        // POST: api/sportswishlist
        [HttpPost]
        public async Task<ActionResult<SportsWishlist>> PostSportsWishlist(WishListDTO sportsWishlistDTO)
        {
            var existingWishlistItem = await _context.SportsWishlists.FirstOrDefaultAsync(sw =>
                sw.Email == sportsWishlistDTO.Email && sw.ProductId == sportsWishlistDTO.ProductId);

            if (existingWishlistItem != null)
            {
                return Conflict("Item already exists in the wishlist");
            }

            var sportsWishlist = new SportsWishlist
            {
                Email = sportsWishlistDTO.Email,
                ProductId = sportsWishlistDTO.ProductId,
                Comment = sportsWishlistDTO.Comment
            };

            _context.SportsWishlists.Add(sportsWishlist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Handle unique constraint violation or other database update errors
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetSportsWishlist), new { email = sportsWishlistDTO.Email, productId = sportsWishlistDTO.ProductId }, sportsWishlist);
        }

        // GET: api/sportswishlist/example@g.com/1
        [HttpGet("{email}/{productId}")]
        public async Task<ActionResult<SportsWishlist>> GetSportsWishlist(string email, int productId)
        {
            var sportsWishlist = await _context.SportsWishlists.FirstOrDefaultAsync(sw => sw.Email == email && sw.ProductId == productId);

            if (sportsWishlist == null)
            {
                return NotFound();
            }

            return sportsWishlist;
        }

        // PUT: api/sportswishlist/example@g.com/1
        [HttpPut("{email}/{productId}")]
        public async Task<IActionResult> UpdateSportsWishlist(string email, int productId, WishListDTO sportsWishlistDTO)
        {
            if (email != sportsWishlistDTO.Email || productId != sportsWishlistDTO.ProductId)
            {
                return BadRequest();
            }

            var sportsWishlist = await _context.SportsWishlists.FirstOrDefaultAsync(sw => sw.Email == email && sw.ProductId == productId);

            if (sportsWishlist == null)
            {
                return NotFound();
            }

            // Update sports wishlist details
            sportsWishlist.Comment = sportsWishlistDTO.Comment;

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

        // DELETE: api/sportswishlist/example@g.com/1
        [HttpDelete("{email}/{productId}")]
        public async Task<IActionResult> DeleteSportsWishlist(string email, int productId)
        {
            var sportsWishlist = await _context.SportsWishlists.FirstOrDefaultAsync(sw => sw.Email == email && sw.ProductId == productId);

            if (sportsWishlist == null)
            {
                return NotFound();
            }

            _context.SportsWishlists.Remove(sportsWishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SportsWishlistExists(string email, int productId)
        {
            return _context.SportsWishlists.Any(sw => sw.Email == email && sw.ProductId == productId);
        }
    }
}

