using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeautyWishListController : ControllerBase
    {
        /// <summary>
        /// Private Fields
        /// </summary>
        #region Private Fields
        private readonly GraduationDataBaseContext _context;

        #endregion

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="context"></param>
        #region Dependency Injection
        public BeautyWishListController(GraduationDataBaseContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// GetMyWishListProductsByBuyer
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        #region GetMyWishListProductsByBuyer
        [Authorize(Roles = "Buyer")]
        [HttpGet("MyWishListProducts")]
        public async Task<ActionResult<BeautyWishlist>> GetMyWishListProductsByBuyer(string email)
        {
            var beautyWishlist = await _context.BeautyWishlists.FirstOrDefaultAsync(bw => bw.Email == email);

            if (beautyWishlist == null)
            {
                return NotFound();
            }

            return beautyWishlist;
        }
        #endregion

        /// <summary>
        /// PostBeautyWishlist
        /// </summary>
        /// <param name="beautyWishlistDTO"></param>
        /// <returns></returns>
        #region PostBeautyWishlist
        // POST: api/beautywishlist
        [Authorize(Roles = "Buyer")]
        [HttpPost]
        public async Task<ActionResult<BeautyWishlist>> PostBeautyWishlist(WishListDTO beautyWishlistDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the item already exists in the wishlist
            var existingWishlistItem = await _context.BeautyWishlists.FirstOrDefaultAsync(bw =>
                bw.Email == beautyWishlistDTO.Email && bw.ProductId == beautyWishlistDTO.ProductId);

            if (existingWishlistItem != null)
            {
                return Conflict("Item already exists in the wishlist");
            }

            // Create a new BeautyWishlist entity
            var beautyWishlist = new BeautyWishlist
            {
                Email = beautyWishlistDTO.Email,
                ProductId = beautyWishlistDTO.ProductId,
                Comment = beautyWishlistDTO.Comment
            };

            // Add the entity to the context
            _context.BeautyWishlists.Add(beautyWishlist);

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Handle unique constraint violation or other database update errors
                return BadRequest("Unable to save wishlist item.");
            }

            // Return a CreatedAtAction response with the created entity
            return CreatedAtAction(nameof(GetBeautyWishlist), new { email = beautyWishlistDTO.Email, productId = beautyWishlistDTO.ProductId }, beautyWishlist);
        }
        #endregion

        /// <summary>
        /// GetBeautyWishlistByEmailAndProductId
        /// </summary>
        /// <param name="email"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        #region GetBeautyWishlistByEmailAndProductId
        // GET: api/beautywishlist/example@g.com/1
        [Authorize(Roles = "Buyer")]
        [HttpGet("{email}/{productId}")]
        public async Task<ActionResult<BeautyWishlist>> GetBeautyWishlist(string email, int productId)
        {
            var beautyWishlist = await _context.BeautyWishlists.FirstOrDefaultAsync(bw => bw.Email == email && bw.ProductId == productId);

            if (beautyWishlist == null)
            {
                return NotFound();
            }

            return beautyWishlist;
        }
        #endregion

        /// <summary>
        /// GetBeautyWishlistByProductID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        #region GetBeautyWishlistByProductID
        // GET: api/beautywishlist/example@g.com/1
        [Authorize(Roles = "Buyer")]
        [HttpGet("{productId}")]
        public async Task<ActionResult<BeautyWishlist>> GetBeautyWishlistByProductID(int productId)
        {
            var beautyWishlist = await _context.BeautyWishlists
                .Include(bw => bw.Product)
                .FirstOrDefaultAsync(bw => bw.ProductId == productId);

            if (beautyWishlist == null)
            {
                return NotFound();
            }

            return beautyWishlist;
        }
        #endregion

        /// <summary>
        /// UpdateBeautyWishlist
        /// </summary>
        /// <param name="email"></param>
        /// <param name="productId"></param>
        /// <param name="beautyWishlistDTO"></param>
        /// <returns></returns>
        #region UpdateBeautyWishlist
        // PUT: api/beautywishlist/example@g.com/1
        [Authorize(Roles = "Buyer")]
        [HttpPut("{email}/{productId}")]
        public async Task<IActionResult> UpdateBeautyWishlist(string email, int productId, WishListDTO beautyWishlistDTO)
        {
            if (email != beautyWishlistDTO.Email || productId != beautyWishlistDTO.ProductId)
            {
                return BadRequest();
            }

            var beautyWishlist = await _context.BeautyWishlists.FirstOrDefaultAsync(bw => bw.Email == email && bw.ProductId == productId);

            if (beautyWishlist == null)
            {
                return NotFound();
            }

            // Update beauty wishlist details
            beautyWishlist.Comment = beautyWishlistDTO.Comment;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // Handle unique constraint violation or other database update errors
                return BadRequest();
            }

            return Ok();
        }
        #endregion

        /// <summary>
        /// DeleteBeautyWishlist
        /// </summary>
        /// <param name="email"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        #region DeleteBeautyWishlist
        // DELETE: api/beautywishlist/example@g.com/1
        [Authorize(Roles = "Buyer")]
        [HttpDelete("{email}/{productId}")]
        public async Task<IActionResult> DeleteBeautyWishlist(string email, int productId)
        {
            var beautyWishlist = await _context.BeautyWishlists.FirstOrDefaultAsync(bw => bw.Email == email && bw.ProductId == productId);

            if (beautyWishlist == null)
            {
                return NotFound();
            }

            _context.BeautyWishlists.Remove(beautyWishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        /// <summary>
        /// DeleteBeautyWishlistByProductID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        #region DeleteBeautyWishlistByProductID
        // DELETE: api/beautywishlist/example@g.com/1
        [Authorize(Roles = "Buyer")]
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteBeautyWishlistByProductID(int productId)
        {
            var beautyWishlist = await _context.BeautyWishlists.FirstOrDefaultAsync(bw => bw.ProductId == productId);

            if (beautyWishlist == null)
            {
                return NotFound();
            }

            _context.BeautyWishlists.Remove(beautyWishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        /// <summary>
        /// BeautyWishlistExists
        /// </summary>
        /// <param name="email"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        #region BeautyWishlistExists
        private bool BeautyWishlistExists(string email, int productId)
        {
            return _context.BeautyWishlists.Any(bw => bw.Email == email && bw.ProductId == productId);
        } 
        #endregion
    }
}

