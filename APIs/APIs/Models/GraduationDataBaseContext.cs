using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIs.Models;

public partial class GraduationDataBaseContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
{
    public GraduationDataBaseContext()
    {
    }

    public GraduationDataBaseContext(DbContextOptions<GraduationDataBaseContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// The DBSet Of All Tables In Our Database  
    /// </summary>
    #region DBsets
    public virtual DbSet<BeautyProduct> BeautyProducts { get; set; }

    public virtual DbSet<BeautyProductsImage> BeautyProductsImages { get; set; }

    public virtual DbSet<BeautyWishlist> BeautyWishlists { get; set; }

    public virtual DbSet<BooksProduct> BooksProducts { get; set; }

    public virtual DbSet<BooksProductsImage> BooksProductsImages { get; set; }

    public virtual DbSet<BooksWishlist> BooksWishlists { get; set; }

    public virtual DbSet<Buyer> Buyers { get; set; }

    public virtual DbSet<ElectronicsProduct> ElectronicsProducts { get; set; }

    public virtual DbSet<ElectronicsProductsImage> ElectronicsProductsImages { get; set; }

    public virtual DbSet<ElectronicsWishlist> ElectronicsWishlists { get; set; }

    public virtual DbSet<FashionProduct> FashionProducts { get; set; }

    public virtual DbSet<FashionProductImage> FashionProductImages { get; set; }

    public virtual DbSet<FashionWishlist> FashionWishlists { get; set; }

    public virtual DbSet<GamingProduct> GamingProducts { get; set; }

    public virtual DbSet<GamingProductsImage> GamingProductsImages { get; set; }

    public virtual DbSet<GamingWishlist> GamingWishlists { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<SellerPhone> SellerPhones { get; set; }

    public virtual DbSet<SportsProduct> SportsProducts { get; set; }

    public virtual DbSet<SportsProductsImage> SportsProductsImages { get; set; }

    public virtual DbSet<SportsWishlist> SportsWishlists { get; set; }


    #endregion

    /// <summary>
    /// Configuration of connectionString
    /// </summary>
    /// <param name="optionsBuilder"></param>
    #region Configuration_of_connectionString
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-7FTITKR;Database=LastVersion_GraduationDataBase;Trusted_Connection=True;TrustServerCertificate=Yes");
    }
    #endregion

    /// <summary>
    /// The Aeatures And Properties Of Our Tables ,Relationships , colmns , ... etc
    /// </summary>
    /// <param name="modelBuilder"></param>
    #region OnModelCreating-FluentOperations-
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BeautyProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Beauty_Products_pk");

            entity.ToTable("Beauty_Products");

            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.AvgRating)
                .HasColumnType("numeric(2, 1)")
                .HasColumnName("Avg_Rating");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("numeric(20, 2)");
            entity.Property(e => e.ProductDescription)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.BeautyProducts)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("Beauty_Products_FK");
        });

        modelBuilder.Entity<BeautyProductsImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("Beauty_Products_Image_pk");

            entity.ToTable("Beauty_Products_Image");

            entity.Property(e => e.ProductImageId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Product_Image_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.BeautyProductsImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Beauty_Products_Image_FK");
        });

        modelBuilder.Entity<BeautyWishlist>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.ProductId }).HasName("Beauty_Wishlist_pk");

            entity.ToTable("Beauty_Wishlist");

            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Product_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(320)
                .IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.BeautyWishlists)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Beauty_Wishlist_FK_Email");

            entity.HasOne(d => d.Product).WithMany(p => p.BeautyWishlists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Beauty_Wishlist_FK_PID");
        });

        modelBuilder.Entity<BooksProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Books_Prodcuts_pk");

            entity.ToTable("Books_Products");

            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.AvgRating)
                .HasColumnType("numeric(2, 1)")
                .HasColumnName("Avg_Rating");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("numeric(20, 2)");
            entity.Property(e => e.ProductDescription)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.BooksProducts)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("Books_Products_FK");
        });

        modelBuilder.Entity<BooksProductsImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("Books_Prodcuts_Image_pk");

            entity.ToTable("Books_Products_Image");

            entity.Property(e => e.ProductImageId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Product_Image_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.BooksProductsImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Books_Products_Image_FK");
        });

        modelBuilder.Entity<BooksWishlist>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.ProductId }).HasName("Books_Wishlist_pk");

            entity.ToTable("Books_Wishlist");

            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Product_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(320)
                .IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.BooksWishlists)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Books_Wishlist_FK_Email");

            entity.HasOne(d => d.Product).WithMany(p => p.BooksWishlists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Books_Wishlist_FK_PID");
        });

        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("Buyer_pk");

            entity.ToTable("Buyer");

            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.FName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("F_Name");
            entity.Property(e => e.LName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("L_Name");
            entity.Property(e => e.Pass)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ElectronicsProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Electronics_Products_pk");

            entity.ToTable("Electronics_Products");

            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.AvgRating)
                .HasColumnType("numeric(2, 1)")
                .HasColumnName("Avg_Rating");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("numeric(20, 2)");
            entity.Property(e => e.ProductDescription)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.ElectronicsProducts)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("Electronics_Products_FK");
        });

        modelBuilder.Entity<ElectronicsProductsImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("Electronics_Prodcuts_Image_pk");

            entity.ToTable("Electronics_Products_Image");

            entity.Property(e => e.ProductImageId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Product_Image_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.ElectronicsProductsImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Electronics_Products_Image_FK");
        });

        modelBuilder.Entity<ElectronicsWishlist>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.ProductId }).HasName("Electronics_Wishlist_pk");

            entity.ToTable("Electronics_Wishlist");

            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Product_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(320)
                .IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.ElectronicsWishlists)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Electronics_Wishlist_FK_Email");

            entity.HasOne(d => d.Product).WithMany(p => p.ElectronicsWishlists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Electronics_Wishlist_FK_PID");
        });

        modelBuilder.Entity<FashionProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Fashion_Product_pk");

            entity.ToTable("Fashion_Product");

            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.AvgRating)
                .HasColumnType("numeric(2, 1)")
                .HasColumnName("Avg_Rating");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("numeric(20, 2)");
            entity.Property(e => e.ProductDescription)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.FashionProducts)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("Fashion_Products_FK");
        });

        modelBuilder.Entity<FashionProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("Fashion_Product_Image_pk");

            entity.ToTable("Fashion_Product_Image");

            entity.Property(e => e.ProductImageId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Product_Image_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.FashionProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Fashion_Product_Image_FK");
        });

        modelBuilder.Entity<FashionWishlist>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.ProductId }).HasName("Fashion_Wishlist_pk");

            entity.ToTable("Fashion_Wishlist");

            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Product_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(320)
                .IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.FashionWishlists)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fashion_Wishlist_FK_Email");

            entity.HasOne(d => d.Product).WithMany(p => p.FashionWishlists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fashion_Wishlist_FK_PID");
        });

        modelBuilder.Entity<GamingProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Gaming_Products_pk");

            entity.ToTable("Gaming_Products");

            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.AvgRating)
                .HasColumnType("numeric(2, 1)")
                .HasColumnName("Avg_Rating");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("numeric(20, 2)");
            entity.Property(e => e.ProductDescription)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.GamingProducts)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("Gaming_Products_FK");
        });

        modelBuilder.Entity<GamingProductsImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("Gaming_Products_Image_pk");

            entity.ToTable("Gaming_Products_Image");

            entity.Property(e => e.ProductImageId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Product_Image_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.GamingProductsImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Gaming_Products_Image_FK");
        });

        modelBuilder.Entity<GamingWishlist>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.ProductId }).HasName("Gaming_Wishlist_pk");

            entity.ToTable("Gaming_Wishlist");

            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Product_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(320)
                .IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.GamingWishlists)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Gaming_Wishlist_FK_Email");

            entity.HasOne(d => d.Product).WithMany(p => p.GamingWishlists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Gaming_Wishlist_FK_PID");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("Seller_pk");

            entity.ToTable("Seller");

            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.FName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("F_Name");
            entity.Property(e => e.Governate)
                .HasMaxLength(40)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.LName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("L_Name");
            entity.Property(e => e.Pass)
                .HasMaxLength(40)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.ShopName)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Shop_Name");
            entity.Property(e => e.Street)
                .HasMaxLength(90)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
        });

        modelBuilder.Entity<SellerPhone>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.PhoneNumber }).HasName("Seller_Phone_pk");

            entity.ToTable("Seller_Phone");

            entity.HasIndex(e => e.PhoneNumber, "Seller_Phone_unique").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.SellerPhones)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Seller_Phone_FK");
        });

        modelBuilder.Entity<SportsProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Sports_Products_pk");

            entity.ToTable("Sports_Products");

            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.AvgRating)
                .HasColumnType("numeric(2, 1)")
                .HasColumnName("Avg_Rating");
            entity.Property(e => e.Brand)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Comment)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("numeric(20, 2)");
            entity.Property(e => e.ProductDescription)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Description");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_CI_AI")
                .HasColumnName("Product_Name");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.SportsProducts)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("Sports_Products_FK");
        });

        modelBuilder.Entity<SportsProductsImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("Sports_Products_Image_pk");

            entity.ToTable("Sports_Products_Image");

            entity.Property(e => e.ProductImageId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Product_Image_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Product).WithMany(p => p.SportsProductsImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Sports_Products_Image_FK");
        });

        modelBuilder.Entity<SportsWishlist>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.ProductId }).HasName("Sports_Wishlist_pk");

            entity.ToTable("Sports_Wishlist");

            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Product_ID");
            entity.Property(e => e.Comment)
                .HasMaxLength(320)
                .IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.SportsWishlists)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Sports_Wishlist_FK_Email");

            entity.HasOne(d => d.Product).WithMany(p => p.SportsWishlists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Sports_Wishlist_FK_PID");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    #endregion

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
