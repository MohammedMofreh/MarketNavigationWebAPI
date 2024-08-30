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
    public class FashionProductController : ControllerBase
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
        public FashionProductController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// PostFashionProduct
        /// </summary>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region PostFashionProduct
        //POST: api/BeautyProducts
        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<ActionResult> PostFashionProduct(ProductDTO ProductDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var FashionProduct = new FashionProduct
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
                await _context.AddAsync(FashionProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                return StatusCode(500, "An error occurred while saving the product.");
            }

            return Ok(FashionProduct);

        }
        #endregion

        /// <summary>
        /// GetAllBeautyProducts
        /// </summary>
        /// <returns></returns>
        #region GetAllFashionProduct
        [HttpGet]
        public async Task<IActionResult> GetAllFashionProducts()
        {
            var FashionProduct = await _context.BeautyProducts.ToListAsync();
            return Ok(FashionProduct);
        }
        #endregion

        /// <summary>
        /// UpdateFashionProduct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region UpdateFashionProduct
        [Authorize(Roles = "Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFashionProduct(int id, ProductDTO ProductDTO)
        {

            var ValidEmail = await _context.Sellers.AnyAsync(x => x.Email == ProductDTO.Email);
            if (!ValidEmail)
                return BadRequest();

            var FashionProduct = await _context.FashionProducts.FindAsync(id);

            if (FashionProduct == null)
                return NotFound($"There is not found {id}");

            FashionProduct.Category = ProductDTO.Category;
            FashionProduct.ProductName = ProductDTO.ProductName;
            FashionProduct.Brand = ProductDTO.Brand;
            FashionProduct.Price = ProductDTO.Price;
            FashionProduct.ProductDescription = ProductDTO.ProductDescription;
            FashionProduct.AvgRating = ProductDTO.AvgRating;
            FashionProduct.Comment = ProductDTO.Comment;
            FashionProduct.Email = ProductDTO.Email;

            await _context.SaveChangesAsync();
            return Ok(FashionProduct);
        }
        #endregion


        /// <summary>
        /// DeleteBeautyProduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region DeleteFashionProduct
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFashionProduct(int id)
        {
            var FashionProduct = await _context.FashionProducts.FindAsync(id);

            if (FashionProduct == null)
                return NotFound($"There is no Fashion product with ID {id}");

            var relatedImages = _context.FashionProductImages.Where(img => img.ProductId == id);

            _context.FashionProductImages.RemoveRange(relatedImages);
            _context.FashionProducts.Remove(FashionProduct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, "Internal server error. Could not delete the product.");
            }

            return Ok(FashionProduct);
        }
        #endregion

        /// <summary>
        /// FashionProductExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region BeautyProductExists
        private bool FashionProductExists(int id)
        {
            return (_context.FashionProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        #endregion
    }
}
