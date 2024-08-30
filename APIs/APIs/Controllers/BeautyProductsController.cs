using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIs.Models;
using Microsoft.AspNetCore.Authorization;
using APIs.DTOs;
using System.Drawing.Drawing2D;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeautyProductsController : ControllerBase
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
        public BeautyProductsController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// PostBeautyProduct
        /// </summary>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region PostBeautyProduct
        //POST: api/BeautyProducts
        [Authorize (Roles ="Seller")]
        [HttpPost]
        public async Task<ActionResult> PostBeautyProduct(ProductDTO ProductDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var BeautyProduct = new BeautyProduct
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
                await _context.AddAsync(BeautyProduct);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework)
                return StatusCode(500, "An error occurred while saving the product.");
            }

            return Ok(BeautyProduct);

        }
        #endregion

        /// <summary>
        /// GetAllBeautyProducts
        /// </summary>
        /// <returns></returns>
        #region GetAllBeautyProducts
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBeautyProducts()
        {
            var BeautyProduct = await _context.BeautyProducts.ToListAsync();
            return Ok(BeautyProduct);
        }
        #endregion

        /// <summary>
        /// UpdateBeautyProduct
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ProductDTO"></param>
        /// <returns></returns>
        #region UpdateBeautyProduct
        [Authorize(Roles = "Seller")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeautyProduct(int id, ProductDTO ProductDTO)
        {

            var ValidEmail = await _context.Sellers.AnyAsync(x => x.Email == ProductDTO.Email);
            if (!ValidEmail)
                return BadRequest();

            var BeautyProduct = await _context.BeautyProducts.FindAsync(id);

            if (BeautyProduct == null)
                return NotFound($"There is not found {id}");

            BeautyProduct.Category = ProductDTO.Category;
            BeautyProduct.ProductName = ProductDTO.ProductName;
            BeautyProduct.Brand = ProductDTO.Brand;
            BeautyProduct.Price = ProductDTO.Price;
            BeautyProduct.ProductDescription = ProductDTO.ProductDescription;
            BeautyProduct.AvgRating = ProductDTO.AvgRating;
            BeautyProduct.Comment = ProductDTO.Comment;
            BeautyProduct.Email = ProductDTO.Email;

            await _context.SaveChangesAsync();
            return Ok(BeautyProduct);
        }
        #endregion


        /// <summary>
        /// DeleteBeautyProduct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region DeleteBeautyProductById
        [Authorize(Roles = "Seller")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeautyProduct(int id)
        {
            var beautyProduct = await _context.BeautyProducts.FindAsync(id);

            if (beautyProduct == null)
                return NotFound($"There is not found {id}");

            var relatedImages = _context.BeautyProductsImages.Where(img => img.ProductId == id);

            _context.BeautyProductsImages.RemoveRange(relatedImages);
            _context.BeautyProducts.Remove(beautyProduct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Handle the exception as needed
                return StatusCode(500, "Internal server error. Could not delete the product.");
            }

            return Ok(beautyProduct);
        } 
        #endregion


        /// <summary>
        /// BeautyProductExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        #region BeautyProductExists
        private bool BeautyProductExists(int id)
        {
            return (_context.BeautyProducts?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        #endregion
    }
}
