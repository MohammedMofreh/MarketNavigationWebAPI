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
    public class GamingProductController : ControllerBase
    {
        /// <summary>
        /// Private Fields Used in DependecyInjection
        /// </summary>
        #region PrivateFields
        private readonly GraduationDataBaseContext _context;
        #endregion

        /// <summary>
        /// DependecyInjection
        /// </summary>
        /// <param name="context"></param>
        #region DependecyInjection
        public GamingProductController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// PostBeautyProduct
        /// </summary>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region PostGamingProduct
        //POST: api/GamingProduct
        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<ActionResult> PostGamingProduct(ProductDTO ProductDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var GamingProduct = new GamingProduct
            {
                Category = ProductDTO.Category,
                ProductName = ProductDTO.ProductName,
                Brand = ProductDTO.Brand,
                Price = ProductDTO.Price,
                ProductDescription = ProductDTO.ProductDescription,
                AvgRating = ProductDTO.AvgRating,
                Comment = ProductDTO.Comment,
                Email = ProductDTO.Email,
            };
            try
            {
                await _context.AddAsync(GamingProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                return StatusCode(500, "An error occurred while saving the product.");
            }

            return Ok(GamingProduct);

        }
        #endregion

        /// <summary>
        /// GetAllGamingProduct
        /// </summary>
        /// <returns></returns>
        #region GetAllGamingProduct
        [HttpGet]
        public async Task<IActionResult> GetAllGamingProduct()
        {
            var GamingProduct = await _context.GamingProducts.ToListAsync();
            return Ok(GamingProduct);
        }
        #endregion

        /// <summary>
        /// UpdateGamingProduct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region UpdateGamingProduct
        [Authorize(Roles = "Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGamingProduct(int id, ProductDTO ProductDTO)
        {

            var ValidEmail = await _context.Sellers.AnyAsync(x => x.Email == ProductDTO.Email);
            if (!ValidEmail)
                return BadRequest();

            var GamingProduct = await _context.GamingProducts.FindAsync(id);

            if (GamingProduct == null)
                return NotFound($"There is not found {id}");

            GamingProduct.Category = ProductDTO.Category;
            GamingProduct.ProductName = ProductDTO.ProductName;
            GamingProduct.Brand = ProductDTO.Brand;
            GamingProduct.Price = ProductDTO.Price;
            GamingProduct.ProductDescription = ProductDTO.ProductDescription;
            GamingProduct.AvgRating = ProductDTO.AvgRating;
            GamingProduct.Comment = ProductDTO.Comment;
            GamingProduct.Email = ProductDTO.Email;

            await _context.SaveChangesAsync();
            return Ok(GamingProduct);
        }
        #endregion


        /// <summary>
        /// DeleteGamingProduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region DeleteGamingProduct
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGamingProduct(int id)
        {
            var GamingProduct = await _context.GamingProducts.FindAsync(id);

            if (GamingProduct == null)
                return NotFound($"There is no Gaming product with ID {id}");

            var relatedImages = _context.GamingProductsImages.Where(img => img.ProductId == id);

            _context.GamingProductsImages.RemoveRange(relatedImages);
            _context.GamingProducts.Remove(GamingProduct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, "Internal server error. Could not delete the product.");
            }

            return Ok(GamingProduct);
        }
        #endregion

        /// <summary>
        /// GamingProductExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region GamingProductExists
        private bool GamingProductExists(int id)
        {
            return (_context.GamingProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        #endregion
    }
}
