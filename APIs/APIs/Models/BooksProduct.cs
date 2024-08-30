using System;
using System.Collections.Generic;
using APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace APIs.Models;

public partial class BooksProduct
{
    public int ProductId { get; set; }

    public string Category { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public decimal Price { get; set; }

    public string ProductDescription { get; set; } = null!;

    public decimal AvgRating { get; set; }

    public string Comment { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<BooksProductsImage> BooksProductsImages { get; set; } = new List<BooksProductsImage>();

    public virtual ICollection<BooksWishlist> BooksWishlists { get; set; } = new List<BooksWishlist>();

    public virtual Seller? EmailNavigation { get; set; }
}


public static class BooksProductEndpoints
{
	public static void MapBooksProductEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/BooksProduct");

        group.MapGet("/", async (GraduationDataBaseContext db) =>
        {
            return await db.BooksProducts.ToListAsync();
        })
        .WithName("GetAllBooksProducts")
        .Produces<List<BooksProduct>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async  (int productid, GraduationDataBaseContext db) =>
        {
            return await db.BooksProducts.AsNoTracking()
                .FirstOrDefaultAsync(model => model.ProductId == productid)
                is BooksProduct model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetBooksProductById")
        .Produces<BooksProduct>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async  (int productid, BooksProduct booksProduct, GraduationDataBaseContext db) =>
        {
            var affected = await db.BooksProducts
                .Where(model => model.ProductId == productid)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.ProductId, booksProduct.ProductId)
                  .SetProperty(m => m.Category, booksProduct.Category)
                  .SetProperty(m => m.ProductName, booksProduct.ProductName)
                  .SetProperty(m => m.Brand, booksProduct.Brand)
                  .SetProperty(m => m.Price, booksProduct.Price)
                  .SetProperty(m => m.ProductDescription, booksProduct.ProductDescription)
                  .SetProperty(m => m.AvgRating, booksProduct.AvgRating)
                  .SetProperty(m => m.Comment, booksProduct.Comment)
                  .SetProperty(m => m.Email, booksProduct.Email)
                  );
            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateBooksProduct")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (BooksProduct booksProduct, GraduationDataBaseContext db) =>
        {
            db.BooksProducts.Add(booksProduct);
            await db.SaveChangesAsync();
            return Results.Created($"/api/BooksProduct/{booksProduct.ProductId}",booksProduct);
        })
        .WithName("CreateBooksProduct")
        .Produces<BooksProduct>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async  (int productid, GraduationDataBaseContext db) =>
        {
            var affected = await db.BooksProducts
                .Where(model => model.ProductId == productid)
                .ExecuteDeleteAsync();
            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteBooksProduct")
        .Produces<BooksProduct>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}