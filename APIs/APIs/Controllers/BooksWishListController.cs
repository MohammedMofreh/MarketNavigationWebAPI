using APIs.DTOs;
using APIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[Authorize(Roles = "Buyer")]
[ApiController]
public class BooksWishlistController : ControllerBase
{
    private readonly GraduationDataBaseContext _context;

    public BooksWishlistController(GraduationDataBaseContext context)
    {
        _context = context;
    }


    [HttpGet("MyWishListProducts")]
    public async Task<ActionResult<BooksWishlist>> GetMyWishListProductsByBuyer(string email)
    {
        var BooksWishlist = await _context.BooksWishlists.FirstOrDefaultAsync(bw => bw.Email == email);

        if (BooksWishlist == null)
        {
            return NotFound();
        }

        return BooksWishlist;
    }

    // GET: api/BooksWishlist/example@g.com/1
    [HttpGet("{productId}")]
    public async Task<ActionResult<BooksWishlist>> GetBooksWishlistByProductID(int productId)
    {
        var BooksWishlist = await _context.BooksWishlists
            .Include(bw => bw.Product)
            .FirstOrDefaultAsync(bw => bw.ProductId == productId);

        if (BooksWishlist == null)
        {
            return NotFound();
        }

        return BooksWishlist;
    }

    // DELETE: api/BooksWishlistss/example@g.com/1
    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteBookWishlistByProductID(int productId)
    {
        var BooksWishlist = await _context.BooksWishlists.FirstOrDefaultAsync(bw => bw.ProductId == productId);

        if (BooksWishlist == null)
        {
            return NotFound();
        }

        _context.BooksWishlists.Remove(BooksWishlist);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // POST: api/bookswishlist
    [HttpPost]
    public async Task<ActionResult<BooksWishlist>> PostBooksWishlist(WishListDTO booksWishlistDTO)
    {
        var existingWishlistItem = await _context.BooksWishlists.FirstOrDefaultAsync(bw =>
            bw.Email == booksWishlistDTO.Email && bw.ProductId == booksWishlistDTO.ProductId);

        if (existingWishlistItem != null)
        {
            return Conflict("Item already exists in the wishlist");
        }

        var booksWishlist = new BooksWishlist
        {
            Email = booksWishlistDTO.Email,
            ProductId = booksWishlistDTO.ProductId,
            Comment = booksWishlistDTO.Comment
        };

        _context.BooksWishlists.Add(booksWishlist);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            // Handle unique constraint violation or other database update errors
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetBooksWishlist), new { email = booksWishlistDTO.Email, productId = booksWishlistDTO.ProductId }, booksWishlist);
    }

    // GET: api/bookswishlist/example@g.com/1
    [HttpGet("{email}/{productId}")]
    public async Task<ActionResult<BooksWishlist>> GetBooksWishlist(string email, int productId)
    {
        var booksWishlist = await _context.BooksWishlists.FirstOrDefaultAsync(bw => bw.Email == email && bw.ProductId == productId);

        if (booksWishlist == null)
        {
            return NotFound();
        }

        return booksWishlist;
    }

    // PUT: api/bookswishlist/example@g.com/1
    [HttpPut("{email}/{productId}")]
    public async Task<IActionResult> UpdateBooksWishlist(string email, int productId, WishListDTO booksWishlistDTO)
    {
        if (email != booksWishlistDTO.Email || productId != booksWishlistDTO.ProductId)
        {
            return BadRequest();
        }

        var booksWishlist = await _context.BooksWishlists.FirstOrDefaultAsync(bw => bw.Email == email && bw.ProductId == productId);

        if (booksWishlist == null)
        {
            return NotFound();
        }

        // Update books wishlist details
        booksWishlist.Comment = booksWishlistDTO.Comment;

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

    // DELETE: api/bookswishlist/example@g.com/1
    [HttpDelete("{email}/{productId}")]
    public async Task<IActionResult> DeleteBooksWishlist(string email, int productId)
    {
        var booksWishlist = await _context.BooksWishlists.FirstOrDefaultAsync(bw => bw.Email == email && bw.ProductId == productId);

        if (booksWishlist == null)
        {
            return NotFound();
        }

        _context.BooksWishlists.Remove(booksWishlist);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BooksWishlistExists(string email, int productId)
    {
        return _context.BooksWishlists.Any(bw => bw.Email == email && bw.ProductId == productId);
    }
}