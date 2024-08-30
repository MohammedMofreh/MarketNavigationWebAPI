using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfflineSuggesionController : ControllerBase
    {
        /// <summary>
        /// Private Fields Used in DependecyInjection
        /// </summary>
        #region Private Fields
        private readonly GraduationDataBaseContext _context;
        #endregion

        /// <summary>
        /// DependecyInjection
        /// </summary>
        /// <param name="context"></param>
        #region MyRegion
        public OfflineSuggesionController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// Method to get All Shops in Our System
        /// </summary>
        /// <returns></returns>
        #region GetAllShops
        [HttpGet]
        public async Task<IActionResult> GetAllShops()
        {
            var sellers = await _context.Sellers.ToListAsync();
            return Ok(sellers);
        }
        #endregion

        /// <summary>
        /// Get Seller Locations That Sell Specific Products , Buyer wants to buy it
        /// </summary>
        /// <param name="productNameOrDescription"></param>
        /// <returns></returns>
        #region GetSellerLocations
        [HttpGet("GetSellerLocations")]
        public async Task<IActionResult> GetSellerLocations([FromQuery] string productNameOrDescription)
        {
            var beautyEmails = _context.BeautyProducts
                .Where(p => p.ProductName.Contains(productNameOrDescription) || p.ProductDescription.Contains(productNameOrDescription))
                .Select(p => p.Email);

            var bookEmails = _context.BooksProducts
                .Where(p => p.ProductName.Contains(productNameOrDescription) || p.ProductDescription.Contains(productNameOrDescription))
                .Select(p => p.Email);

            var electronicsEmails = _context.ElectronicsProducts
                .Where(p => p.ProductName.Contains(productNameOrDescription) || p.ProductDescription.Contains(productNameOrDescription))
                .Select(p => p.Email);

            var fashionEmails = _context.FashionProducts
                .Where(p => p.ProductName.Contains(productNameOrDescription) || p.ProductDescription.Contains(productNameOrDescription))
                .Select(p => p.Email);

            var gamingEmails = _context.GamingProducts
                .Where(p => p.ProductName.Contains(productNameOrDescription) || p.ProductDescription.Contains(productNameOrDescription))
                .Select(p => p.Email);

            var sportsEmails = _context.SportsProducts
                .Where(p => p.ProductName.Contains(productNameOrDescription) || p.ProductDescription.Contains(productNameOrDescription))
                .Select(p => p.Email);

            var allEmails = await beautyEmails
                .Union(bookEmails)
                .Union(electronicsEmails)
                .Union(fashionEmails)
                .Union(gamingEmails)
                .Union(sportsEmails)
                .Distinct()
                .ToListAsync();

            var sellers = await _context.Sellers
                .Where(s => allEmails.Contains(s.Email))
                .Select(s => new
                {

                    s.FName,
                    s.LName,
                    s.Email,
                    s.Long,
                    s.Lat
                })
                .ToListAsync();

            if (!sellers.Any())
            {
                return NotFound("No sellers found for the specified product.");
            }

            var locations = sellers.Select(s => new
            {
                s.FName,
                s.LName,
                s.Email,
                Longitude = s.Long,
                Latitude = s.Lat
            });

            return Ok(locations);
        }
        #endregion

    }
}
