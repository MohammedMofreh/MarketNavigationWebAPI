using System;
using System.Collections.Generic;

namespace APIs.Models;

public partial class ElectronicsProduct
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

    public virtual ICollection<ElectronicsProductsImage> ElectronicsProductsImages { get; set; } = new List<ElectronicsProductsImage>();

    public virtual ICollection<ElectronicsWishlist> ElectronicsWishlists { get; set; } = new List<ElectronicsWishlist>();

    public virtual Seller? EmailNavigation { get; set; }
}
