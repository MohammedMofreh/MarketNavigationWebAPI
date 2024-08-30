using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomSearchBasedOnCategoryController : ControllerBase
    {
        private readonly GraduationDataBaseContext _context;

        public CustomSearchBasedOnCategoryController(GraduationDataBaseContext context)
        {
            _context = context;
        }

        [HttpGet("Beauty")]
        public async Task<IActionResult> Get([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Query cannot be empty");
            }

            var beautyResults = await _context.BeautyProducts
                .Where(p => p.ProductName.Contains(query) || p.ProductDescription.Contains(query))
                .Select(p => new SearchResultDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImages = p.BeautyProductsImages.Cast<object>().ToList(), // Cast images to object type
                    AvgRating = p.AvgRating,
                    Comment = p.Comment,
                    Email = p.Email,
                    Price = p.Price,
                    Brand = p.Brand,
                    Category = "Beauty"
                })
                .ToListAsync();
            return Ok(beautyResults);

        }

        [HttpGet("Books")]
        public async Task<IActionResult> GetBookProducts([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Query cannot be empty");
            }

            var bookResults = await _context.BooksProducts
                .Where(p => p.ProductName.Contains(query) || p.ProductDescription.Contains(query))
                .Select(p => new SearchResultDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImages = p.BooksProductsImages.Cast<object>().ToList(),
                    AvgRating = p.AvgRating,
                    Comment = p.Comment,
                    Email = p.Email,
                    Price = p.Price,
                    Brand = p.Brand,
                    Category = "Books"
                })
                .ToListAsync();
            return Ok(bookResults);
        }

        [HttpGet("Fashion")]
        public async Task<IActionResult> GetFashionProducts([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Query cannot be empty");
            }

            var FashionResults = await _context.FashionProducts
                .Where(p => p.ProductName.Contains(query) || p.ProductDescription.Contains(query))
                .Select(p => new SearchResultDto
                {

                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImages = p.FashionProductImages.Cast<object>().ToList(),
                    AvgRating = p.AvgRating,
                    Comment = p.Comment,
                    Email = p.Email,
                    Price = p.Price,
                    Brand = p.Brand,
                    Category = "Fashion"
                })
                .ToListAsync();

            return Ok(FashionResults);

        }

        [HttpGet("Electronics")]
        public async Task<IActionResult> GetElectronicsProducts([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Query cannot be empty");
            }
            var electronicResults = await _context.ElectronicsProducts
                .Where(p => p.ProductName.Contains(query) || p.ProductDescription.Contains(query))
                .Select(p => new SearchResultDto
                {

                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImages = p.ElectronicsProductsImages.Cast<object>().ToList(),
                    AvgRating = p.AvgRating,
                    Comment = p.Comment,
                    Email = p.Email,
                    Price = p.Price,
                    Brand = p.Brand,
                    Category = "Electronics"
                })
                .ToListAsync();

            return Ok(electronicResults);

        }

        [HttpGet("Gaming")]
        public async Task<IActionResult> GetGamingProducts([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Query cannot be empty");
            }
            var gamingResults = await _context.GamingProducts
                .Where(p => p.ProductName.Contains(query) || p.ProductDescription.Contains(query))
                .Select(p => new SearchResultDto
                {

                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImages = p.GamingProductsImages.Cast<object>().ToList(),
                    AvgRating = p.AvgRating,
                    Comment = p.Comment,
                    Email = p.Email,
                    Price = p.Price,
                    Brand = p.Brand,
                    Category = "Gaming"
                })
                .ToListAsync();

            return Ok(gamingResults);

        }

        [HttpGet("Sports")]
        public async Task<IActionResult> GetSportsProducts([FromQuery] string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("Query cannot be empty");
            }

            var sportsResults = await _context.SportsProducts
                .Where(p => p.ProductName.Contains(query) || p.ProductDescription.Contains(query))
                .Select(p => new SearchResultDto
                {

                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImages = p.SportsProductsImages.Cast<object>().ToList(),
                    AvgRating = p.AvgRating,
                    Comment = p.Comment,
                    Email = p.Email,
                    Price = p.Price,
                    Brand = p.Brand,
                    Category = "Sports"
                })
                .ToListAsync();

            return Ok(sportsResults);

        }


    }
}
