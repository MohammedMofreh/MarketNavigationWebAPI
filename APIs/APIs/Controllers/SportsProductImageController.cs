using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsProductImageController : ControllerBase
    {
        #region PrivateFields
        private readonly GraduationDataBaseContext _context;
        private List<string> _imagesExtensions = new List<string> { ".jpg", ".png" };
        private long _maxSize = 1048576;
        #endregion

        #region DependecyInjection
        public SportsProductImageController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> PostSportsProductImage([FromForm] ImageDTO imageDTO)
        {
            if (!_imagesExtensions.Contains(Path.GetExtension(imageDTO.ProductImage.FileName.ToLower())))
                return BadRequest("The Extension is not allowed");

            if (imageDTO.ProductImage.Length > _maxSize)
                return BadRequest("The size is bigger than 1 MB");

            var IsValidProduct = await _context.SportsProducts.AnyAsync(x => x.ProductId == imageDTO.ProductId);
            if (!IsValidProduct)
                return BadRequest("The product is not found");

            using var dataStream = new MemoryStream();
            await imageDTO.ProductImage.CopyToAsync(dataStream);

            var SportsProductImage = new SportsProductsImage
            {
                ProductId = imageDTO.ProductId,
                ProductImage = dataStream.ToArray(),
            };

            await _context.AddAsync(SportsProductImage);
            await _context.SaveChangesAsync();

            return Ok(SportsProductImage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var SportsProductsImage = await _context.SportsProductsImages
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
            return Ok(SportsProductsImage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ProductImage = await _context.SportsProductsImages.FindAsync(id);
            if (ProductImage == null)
                return NotFound();
            return Ok(ProductImage);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var Image = await _context.SportsProductsImages.FindAsync(id);
            if (Image == null)
                return NotFound();

            _context.Remove(Image);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}
