using System;
using System.Collections.Generic;

namespace APIs.Models;

public partial class Seller
{

    public string Email { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string Governate { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string FName { get; set; } = null!;

    public string LName { get; set; } = null!;

    public string ShopName { get; set; } = null!;

    public double Long { get; set; }

    public double Lat { get; set; }

    public virtual ICollection<BeautyProduct> BeautyProducts { get; set; } = new List<BeautyProduct>();

    public virtual ICollection<BooksProduct> BooksProducts { get; set; } = new List<BooksProduct>();

    public virtual ICollection<ElectronicsProduct> ElectronicsProducts { get; set; } = new List<ElectronicsProduct>();

    public virtual ICollection<FashionProduct> FashionProducts { get; set; } = new List<FashionProduct>();

    public virtual ICollection<GamingProduct> GamingProducts { get; set; } = new List<GamingProduct>();

    public virtual ICollection<SellerPhone> SellerPhones { get; set; } = new List<SellerPhone>();

    public virtual ICollection<SportsProduct> SportsProducts { get; set; } = new List<SportsProduct>();
}
