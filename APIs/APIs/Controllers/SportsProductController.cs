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
    public class SportsProductController : ControllerBase
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
        public SportsProductController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// PostSportsProduct
        /// </summary>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region PostSportsProduct
        //POST: api/BeautyProducts
        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<ActionResult> PostSportsProduct(ProductDTO ProductDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var SportsProduct = new SportsProduct
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
                await _context.AddAsync(SportsProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                return StatusCode(500, "An error occurred while saving the product.");
            }

            return Ok(SportsProduct);

        }
        #endregion

        /// <summary>
        /// GetAllSportsProduct
        /// </summary>
        /// <returns></returns>
        #region GetAllSportsProduct
        [HttpGet]
        public async Task<IActionResult> GetAllSportsProduct()
        {
            var SportsProduct = await _context.SportsProducts.ToListAsync();
            return Ok(SportsProduct);
        }
        #endregion

        /// <summary>
        /// UpdateSportsProduct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region UpdateSportsProduct
        [Authorize(Roles = "Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSportsProduct(int id, ProductDTO ProductDTO)
        {

            var ValidEmail = await _context.Sellers.AnyAsync(x => x.Email == ProductDTO.Email);
            if (!ValidEmail)
                return BadRequest();

            var SportsProduct = await _context.SportsProducts.FindAsync(id);

            if (SportsProduct == null)
                return NotFound($"There is not found {id}");

            SportsProduct.Category = ProductDTO.Category;
            SportsProduct.ProductName = ProductDTO.ProductName;
            SportsProduct.Brand = ProductDTO.Brand;
            SportsProduct.Price = ProductDTO.Price;
            SportsProduct.ProductDescription = ProductDTO.ProductDescription;
            SportsProduct.AvgRating = ProductDTO.AvgRating;
            SportsProduct.Comment = ProductDTO.Comment;
            SportsProduct.Email = ProductDTO.Email;

            await _context.SaveChangesAsync();
            return Ok(SportsProduct);
        }
        #endregion


        /// <summary>
        /// DeleteSportsProduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region DeleteSportsProduct
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSportsProduct(int id)
        {
            var SportsProduct = await _context.SportsProducts.FindAsync(id);

            if (SportsProduct == null)
                return NotFound($"There is no Sports product with ID {id}");

            var relatedImages = _context.SportsProductsImages.Where(img => img.ProductId == id);

            _context.SportsProductsImages.RemoveRange(relatedImages);
            _context.SportsProducts.Remove(SportsProduct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, "Internal server error. Could not delete the product.");
            }

            return Ok(SportsProduct);
        }
        #endregion

        /// <summary>
        /// SportsProductExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region SportsProductExists
        private bool SportsProductExists(int id)
        {
            return (_context.SportsProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        #endregion
    }
}
