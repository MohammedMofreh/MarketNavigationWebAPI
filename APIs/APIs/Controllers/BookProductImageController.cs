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
    public class BookProductImageController : ControllerBase
    {
        #region PrivateFields
        private readonly GraduationDataBaseContext _context;
        private List<string> _imagesExtensions = new List<string> { ".jpg", ".png" };
        private long _maxSize = 1048576;
        #endregion

        #region DependecyInjection
        public BookProductImageController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion
        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<IActionResult> PostBookProductImage([FromForm] ImageDTO imageDTO)
        {
            if (!_imagesExtensions.Contains(Path.GetExtension(imageDTO.ProductImage.FileName.ToLower())))
                return BadRequest("The Extension is not allowed");

            if (imageDTO.ProductImage.Length > _maxSize)
                return BadRequest("The size is bigger than 1 MB");

            var IsValidProduct = await _context.BooksProducts.AnyAsync(x => x.ProductId == imageDTO.ProductId);
            if (!IsValidProduct)
                return BadRequest("The product is not found");

            using var dataStream = new MemoryStream();
            await imageDTO.ProductImage.CopyToAsync(dataStream);

            var BookProductImage = new BooksProductsImage
            {
                ProductId = imageDTO.ProductId,
                ProductImage = dataStream.ToArray(),
                
            };

            await _context.AddAsync(BookProductImage);
            await _context.SaveChangesAsync();

            return Ok(BookProductImage);
        }
        [Authorize (Roles ="Seller")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var BookProductsImage = await _context.BooksProductsImages
                .Include(x => x.Product)
                .Select(x => new ImageDetailsDTO
                {
                    AvgRating = 0,
                    Brand = x.Product.Brand,
                    ProductName = x.Product.ProductName,
                    ProductDescription = x.Product.ProductDescription,
                    Comment = x.Product.Comment,
                    Category = x.Product.Category,
                    Email = x.Product.Email,
                    Price = x.Product.Price,
                    ProductId = x.Product.ProductId,
                    Image = x.ProductImage,
                })
                .ToListAsync();
            return Ok(BookProductsImage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ProductImage = await _context.BooksProductsImages.FindAsync(id);
            if (ProductImage == null)
                return NotFound();
            return Ok(ProductImage);
        }

        [Authorize(Roles = "Seller,Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var Image = await _context.BooksProductsImages.FindAsync(id);
            if (Image == null)
                return NotFound();

            _context.Remove(Image);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}
