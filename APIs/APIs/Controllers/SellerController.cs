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
    
    public class SellerController : ControllerBase
    {
        private readonly GraduationDataBaseContext _context;

        public SellerController(GraduationDataBaseContext context)
        {
            _context = context;
        }



        // GET: api/seller/example@g.com
        [HttpGet("{email}")]
        public async Task<ActionResult<Seller>> GetSeller(string email)
        {
            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Email == email);

            if (seller == null)
            {
                return NotFound();
            }

            return seller;
        }

        [Authorize(Roles = "Admin,Seller")]
        // PUT: api/seller/example@g.com
        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateSeller(string email, SellerDTO sellerDTO)
        {
            if (email != sellerDTO.Email)
            {
                return BadRequest();
            }

            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Email == email);

            if (seller == null)
            {
                return NotFound();
            }

            // Update seller's details
            seller.Email = sellerDTO.Email;
            seller.Governate = sellerDTO.Governate;
            seller.City = sellerDTO.City;
            seller.Street = sellerDTO.Street;
            seller.FName = sellerDTO.FName;
            seller.LName = sellerDTO.LName;
            seller.ShopName = sellerDTO.ShopName;

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
        [Authorize(Roles = "Admin")]
        // DELETE: api/seller/example@g.com
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteSeller(string email)
        {
            var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Email == email);

            if (seller == null)
            {
                return NotFound();
            }

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SellerExists(string email)
        {
            return _context.Sellers.Any(e => e.Email == email);
        }

        
        [HttpGet("MyProducts")]
        public async Task<IActionResult> GetProductsBySeller(string email)
        {
            var beautyResults = await _context.BeautyProducts
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.Brand,
                    p.Price,
                    p.ProductDescription,
                    p.AvgRating,
                    p.Comment,
                    Category = "Beauty"
                })
                .ToListAsync();

            var bookResults = await _context.BooksProducts
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    Brand = p.ProductDescription,
                    p.Price,
                    p.ProductDescription,
                    p.AvgRating,
                    p.Comment,
                    Category = "Books"
                })
                .ToListAsync();

            var electronicResults = await _context.ElectronicsProducts
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.Brand,
                    p.Price,
                    p.ProductDescription,
                    p.AvgRating,
                    p.Comment,
                    Category = "Electronics"
                })
                .ToListAsync();

            var fashionResults = await _context.FashionProducts
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    Brand = p.ProductDescription,
                    p.Price,
                    p.ProductDescription,
                    p.AvgRating,
                    p.Comment,
                    Category = "Fashion"
                })
                .ToListAsync();

            var gamingResults = await _context.GamingProducts
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    Brand = p.ProductDescription,
                    p.Price,
                    p.ProductDescription,
                    p.AvgRating,
                    p.Comment,
                    Category = "Gaming"
                })
                .ToListAsync();

            var sportsResults = await _context.SportsProducts
                .Where(p => p.Email == email)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    Brand = p.ProductDescription,
                    p.Price,
                    p.ProductDescription,
                    p.AvgRating,
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
                return NotFound(new { Message = "No products found for this seller." });
            }

            return Ok(combinedResults);
        }
    }



}
