using System;
using System.Collections.Generic;

namespace APIs.Models;

public partial class Buyer
{
    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string FName { get; set; } = null!;

    public string LName { get; set; } = null!;

    public virtual ICollection<BeautyWishlist> BeautyWishlists { get; set; } = new List<BeautyWishlist>();

    public virtual ICollection<BooksWishlist> BooksWishlists { get; set; } = new List<BooksWishlist>();

    public virtual ICollection<ElectronicsWishlist> ElectronicsWishlists { get; set; } = new List<ElectronicsWishlist>();

    public virtual ICollection<FashionWishlist> FashionWishlists { get; set; } = new List<FashionWishlist>();

    public virtual ICollection<GamingWishlist> GamingWishlists { get; set; } = new List<GamingWishlist>();

    public virtual ICollection<SportsWishlist> SportsWishlists { get; set; } = new List<SportsWishlist>();
}
