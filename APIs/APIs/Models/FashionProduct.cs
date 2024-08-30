using System;
using System.Collections.Generic;

namespace APIs.Models;

public partial class FashionProduct
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

    public virtual Seller? EmailNavigation { get; set; }

    public virtual ICollection<FashionProductImage> FashionProductImages { get; set; } = new List<FashionProductImage>();

    public virtual ICollection<FashionWishlist> FashionWishlists { get; set; } = new List<FashionWishlist>();
}
