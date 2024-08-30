using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly GraduationDataBaseContext _context;

        public SearchController(GraduationDataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
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

            var fashionResults = await _context.FashionProducts
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

            var combinedResults = beautyResults.Concat(bookResults)
                                                .Concat(electronicResults)
                                                .Concat(fashionResults)
                                                .Concat(gamingResults)
                                                .Concat(sportsResults);

            return Ok(combinedResults);
        }
    }

}
