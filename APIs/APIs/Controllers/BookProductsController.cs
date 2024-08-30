using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookProductsController : ControllerBase
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
        public BookProductsController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// PostBookProduct
        /// </summary>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region PostBookProduct
        //POST: api/BookProduct
        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<ActionResult> PostBookProduct(ProductDTO ProductDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var BookProduct = new BooksProduct
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
                await _context.AddAsync(BookProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                return StatusCode(500, "An error occurred while saving the product.");
            }

            return Ok(BookProduct);

        }
        #endregion

        /// <summary>
        /// GetAllBooksProducts
        /// </summary>
        /// <returns></returns>
        #region GetAllBooksProducts
        [HttpGet]
        public async Task<IActionResult> GetAllBooksProducts()
        {
            var BookProduct = await _context.BooksProducts.ToListAsync();
            return Ok(BookProduct);
        }
        #endregion

        /// <summary>
        /// UpdateBooksProducts
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region UpdateBooksProducts
        [Authorize(Roles = "Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooksProducts(int id, ProductDTO ProductDTO)
        {

            var ValidEmail = await _context.Sellers.AnyAsync(x => x.Email == ProductDTO.Email);
            if (!ValidEmail)
                return BadRequest();

            var BookProduct = await _context.BooksProducts.FindAsync(id);

            if (BookProduct == null)
                return NotFound($"There is not found {id}");

            BookProduct.Category = ProductDTO.Category;
            BookProduct.ProductName = ProductDTO.ProductName;
            BookProduct.Brand = ProductDTO.Brand;
            BookProduct.Price = ProductDTO.Price;
            BookProduct.ProductDescription = ProductDTO.ProductDescription;
            BookProduct.AvgRating = ProductDTO.AvgRating;
            BookProduct.Comment = ProductDTO.Comment;
            BookProduct.Email = ProductDTO.Email;

            await _context.SaveChangesAsync();
            return Ok(BookProduct);
        }
        #endregion


        /// <summary>
        /// DeleteBookProduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region DeleteBookProduct
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookProduct(int id)
        {
            var bookProduct = await _context.BooksProducts.FindAsync(id);

            if (bookProduct == null)
                return NotFound($"There is no book product with ID {id}");

            var relatedImages = _context.BooksProductsImages.Where(img => img.ProductId == id);

            _context.BooksProductsImages.RemoveRange(relatedImages);
            _context.BooksProducts.Remove(bookProduct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, "Internal server error. Could not delete the product.");
            }

            return Ok(bookProduct);
        }

        #endregion

        /// <summary>
        /// BookProductExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region BookProductExists
        private bool BookProductExists(int id)
        {
            return (_context.BooksProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        #endregion

    }
}
