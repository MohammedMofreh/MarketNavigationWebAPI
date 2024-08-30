using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FashionWishListController : ControllerBase
    {
        private readonly GraduationDataBaseContext _context;

        public FashionWishListController(GraduationDataBaseContext context)
        {
            _context = context;
        }

        // POST: api/fashionwishlist
        [HttpPost]
        public async Task<ActionResult<FashionWishlist>> PostFashionWishlist(WishListDTO fashionWishlistDTO)
        {
            var existingWishlistItem = await _context.FashionWishlists.FirstOrDefaultAsync(fw =>
                fw.Email == fashionWishlistDTO.Email && fw.ProductId == fashionWishlistDTO.ProductId);

            if (existingWishlistItem != null)
            {
                return Conflict("Item already exists in the wishlist");
            }

            var fashionWishlist = new FashionWishlist
            {
                Email = fashionWishlistDTO.Email,
                ProductId = fashionWishlistDTO.ProductId,
                Comment = fashionWishlistDTO.Comment
            };

            _context.FashionWishlists.Add(fashionWishlist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Handle unique constraint violation or other database update errors
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetFashionWishlist), new { email = fashionWishlistDTO.Email, productId = fashionWishlistDTO.ProductId }, fashionWishlist);
        }

        // GET: api/fashionwishlist/example@g.com/1
        [HttpGet("{email}/{productId}")]
        public async Task<ActionResult<FashionWishlist>> GetFashionWishlist(string email, int productId)
        {
            var fashionWishlist = await _context.FashionWishlists.FirstOrDefaultAsync(fw => fw.Email == email && fw.ProductId == productId);

            if (fashionWishlist == null)
            {
                return NotFound();
            }

            return fashionWishlist;
        }

        // PUT: api/fashionwishlist/example@g.com/1
        [HttpPut("{email}/{productId}")]
        public async Task<IActionResult> UpdateFashionWishlist(string email, int productId, WishListDTO fashionWishlistDTO)
        {
            if (email != fashionWishlistDTO.Email || productId != fashionWishlistDTO.ProductId)
            {
                return BadRequest();
            }

            var fashionWishlist = await _context.FashionWishlists.FirstOrDefaultAsync(fw => fw.Email == email && fw.ProductId == productId);

            if (fashionWishlist == null)
            {
                return NotFound();
            }

            // Update fashion wishlist details
            fashionWishlist.Comment = fashionWishlistDTO.Comment;

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

        // DELETE: api/fashionwishlist/example@g.com/1
        [HttpDelete("{email}/{productId}")]
        public async Task<IActionResult> DeleteFashionWishlist(string email, int productId)
        {
            var fashionWishlist = await _context.FashionWishlists.FirstOrDefaultAsync(fw => fw.Email == email && fw.ProductId == productId);

            if (fashionWishlist == null)
            {
                return NotFound();
            }

            _context.FashionWishlists.Remove(fashionWishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FashionWishlistExists(string email, int productId)
        {
            return _context.FashionWishlists.Any(fw => fw.Email == email && fw.ProductId == productId);
        }
    }
}
