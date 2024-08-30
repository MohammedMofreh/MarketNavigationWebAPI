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
    public class ElectronicsProductController : ControllerBase
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
        public ElectronicsProductController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// PostElectronicsProduct
        /// </summary>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region PostElectronicsProduct
        //POST: api/BookProduct
        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<ActionResult> PostElectronicsProduct(ProductDTO ProductDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var electronicsProduct = new ElectronicsProduct
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
                await _context.AddAsync(electronicsProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                return StatusCode(500, "An error occurred while saving the product.");
            }

            return Ok(electronicsProduct);

        }
        #endregion

        /// <summary>
        /// GetAllElectronicsProduct
        /// </summary>
        /// <returns></returns>
        #region GetAllElectronicsProduct
        [HttpGet]
        public async Task<IActionResult> GetAllElectronicsProduct()
        {
            var ElectronicProduct = await _context.ElectronicsProducts.ToListAsync();
            return Ok(ElectronicProduct);
        }
        #endregion

        /// <summary>
        /// UpdateElectronicProduct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region UpdateElectronicProduct
        [Authorize(Roles = "Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateElectronicProduct(int id, ProductDTO ProductDTO)
        {
            var ValidEmail = await _context.Sellers.AnyAsync(x => x.Email == ProductDTO.Email);
            if (!ValidEmail)
                return BadRequest();

            var ElectronicProduct = await _context.ElectronicsProducts.FindAsync(id);

            if (ElectronicProduct == null)
                return NotFound($"There is not found {id}");

            ElectronicProduct.Category = ProductDTO.Category;
            ElectronicProduct.ProductName = ProductDTO.ProductName;
            ElectronicProduct.Brand = ProductDTO.Brand;
            ElectronicProduct.Price = ProductDTO.Price;
            ElectronicProduct.ProductDescription = ProductDTO.ProductDescription;
            ElectronicProduct.AvgRating = ProductDTO.AvgRating;
            ElectronicProduct.Comment = ProductDTO.Comment;
            ElectronicProduct.Email = ProductDTO.Email;

            await _context.SaveChangesAsync();
            return Ok(ElectronicProduct);
        }
        #endregion


        /// <summary>
        /// DeleteElectronicProduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region DeleteElectronicProduct
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectronicProduct(int id)
        {
            var ElectronicProduct = await _context.ElectronicsProducts.FindAsync(id);

            if (ElectronicProduct == null)
                return NotFound($"There is no Electronic product with ID {id}");

            var relatedImages = _context.ElectronicsProductsImages.Where(img => img.ProductId == id);

            _context.ElectronicsProductsImages.RemoveRange(relatedImages);
            _context.ElectronicsProducts.Remove(ElectronicProduct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, "Internal server error. Could not delete the product.");
            }

            return Ok(ElectronicProduct);
        }

        #endregion

        /// <summary>
        /// ElectronicProductExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region ElectronicProductExists
        private bool ElectronicProductExists(int id)
        {
            return (_context.ElectronicsProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        #endregion
    }
}
