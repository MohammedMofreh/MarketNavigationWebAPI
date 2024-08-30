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
    public class BeautyProductImageController : ControllerBase
    {
        /// <summary>
        /// Private Fields Used in DependecyInjection
        /// </summary>
        #region PrivateFields
        private readonly GraduationDataBaseContext _context;
        private new List<string> _imagesExtensions = new List<string> { ".jpg", ".png" };
        private long _maxSize = 1048576;
        #endregion

        /// <summary>
        /// DependecyInjection
        /// </summary>
        /// <param name="context"></param>
        #region DependecyInjection
        public BeautyProductImageController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// PostBeautyProductImage
        /// </summary>
        /// <param name="imageDTO"></param>
        /// <returns></returns>
        #region PostBeautyProductImage
        [Authorize(Roles = "Seller")]
        [HttpPost]
        public async Task<IActionResult> PostBeautyProductImage([FromForm] ImageDTO imageDTO)
        {
            if (!_imagesExtensions.Contains(Path.GetExtension(imageDTO.ProductImage.FileName.ToLower())))
                return BadRequest("The Extension is not allowed");

            if (imageDTO.ProductImage.Length > _maxSize)
                return BadRequest("The size is bigger than 1 MB");

            var IsValidProduct = await _context.BeautyProducts.AnyAsync(x => x.ProductId == imageDTO.ProductId);
            if (!IsValidProduct)
                return BadRequest("The product is not found");

            using var dataStream = new MemoryStream();
            await imageDTO.ProductImage.CopyToAsync(dataStream);

            var BeautyProductImage = new BeautyProductsImage
            {
                ProductId = imageDTO.ProductId,
                ProductImage = dataStream.ToArray(),
            };

            await _context.AddAsync(BeautyProductImage);
            await _context.SaveChangesAsync();

            return Ok(BeautyProductImage);
        }
        #endregion

        /// <summary>
        /// GetAllBeautyProductsImage
        /// </summary>
        /// <returns></returns>
        #region GetAllBeautyProductsImage
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var BeautyProductsImage = await _context.BeautyProductsImages.
                Include(x => x.Product)
                .Select(x => new ImageDetailsDTO
                {
                    ProductImageId = x.ProductImageId,
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
            return Ok(BeautyProductsImage);
        }
        #endregion

        /// <summary>
        /// GetImageById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region GetImageById
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ProductImage = await _context.BeautyProductsImages.FindAsync(id);
            if (ProductImage == null)
                return NotFound();
            return Ok(ProductImage);
        }
        #endregion

        /// <summary>
        /// DeleteImageById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region DeleteImageById
        [Authorize(Roles = "Seller")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var Image = await _context.BeautyProductsImages.FindAsync(id);
            if (Image == null)
                return NotFound();

            _context.Remove(Image);
            _context.SaveChanges();
            return Ok();
        } 
        #endregion
    }
}
