using System;
using System.Collections.Generic;

namespace APIs.Models;

public partial class BeautyProduct
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

    public virtual ICollection<BeautyProductsImage> BeautyProductsImages { get; set; } = new List<BeautyProductsImage>();

    public virtual ICollection<BeautyWishlist> BeautyWishlists { get; set; } = new List<BeautyWishlist>();

    public virtual Seller? EmailNavigation { get; set; }
}
