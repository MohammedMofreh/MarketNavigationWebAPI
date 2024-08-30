using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "Admin,Buyer")]
    public class BuyerController : ControllerBase
    {
        private readonly GraduationDataBaseContext _context;

        public BuyerController(GraduationDataBaseContext context)
        {
            _context = context;
        }

        // GET: api/buyer/example@g.com
        [HttpGet("{email}")]
        public async Task<ActionResult<Buyer>> GetBuyer(string email)
        {
            var buyer = await _context.Buyers.FindAsync(email);

            if (buyer == null)
            {
                return NotFound();
            }

            return buyer;
        }

        // PUT: api/buyer/example@g.com
        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateBuyer(string email, BuyerDTO buyerDTO)
        {
            if (email != buyerDTO.Email)
            {
                return BadRequest();
            }

            var buyer = await _context.Buyers.FindAsync(email);
            if (buyer == null)
            {
                return NotFound();
            }

            // Update buyer's details
            buyer.Email = buyerDTO.Email;
            buyer.FName = buyerDTO.FName;
            buyer.LName = buyerDTO.LName;
            buyer.Pass=buyerDTO.Pass;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) {
                return BadRequest();
            }
            
            return Ok();
            
        }

        // DELETE: api/buyer/5
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteBuyer(string email)
        {
            var buyer = await _context.Buyers.FindAsync(email);
            if (buyer == null)
            {
                return NotFound();
            }

            _context.Buyers.Remove(buyer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuyerExists(string email)
        {
            return _context.Buyers.Any(e => e.Email == email);
        }

        [HttpGet("MyWishListProducts")]
        public async Task<IActionResult> GetWishListProductsByBuyer(string email)
        {
            var beautyResults = await _context.BeautyWishlists
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.Comment,
                    Category = "Beauty"
                })
                .ToListAsync();

            var bookResults = await _context.BooksWishlists
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.Comment,
                    Category = "Books"
                })
                .ToListAsync();

            var electronicResults = await _context.ElectronicsWishlists
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.Comment,
                    Category = "Electronics"
                })
                .ToListAsync();

            var fashionResults = await _context.FashionWishlists
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.Comment,
                    Category = "Fashion"
                })
                .ToListAsync();

            var gamingResults = await _context.GamingWishlists
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.Comment,
                    Category = "Gaming"
                })
                .ToListAsync();

            var sportsResults = await _context.SportsWishlists
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.Comment,
                    Category = "Sports"
                })
                .ToListAsync();

            var combinedResults = beautyResults
                .Concat(bookResults)
                .Concat(electronicResults)
                .Concat(fashionResults)
                .Concat(gamingResults)
                .Concat(sportsResults);

            if (!combinedResults.Any())
            {
                return NotFound(new { Message = $"No WishList products found for this Buyer{email}." });
            }

            return Ok(combinedResults);
        }
    }
}

