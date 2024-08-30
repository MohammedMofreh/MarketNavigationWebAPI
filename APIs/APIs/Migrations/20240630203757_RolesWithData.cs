using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIs.Migrations
{
    /// <inheritdoc />
    public partial class RolesWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameOfPerson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buyer",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: false),
                    Pass = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    F_Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    L_Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Buyer_pk", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Seller",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: false),
                    Pass = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false, collation: "Arabic_CI_AI"),
                    Governate = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false, collation: "Arabic_CI_AI"),
                    City = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, collation: "Arabic_CI_AI"),
                    Street = table.Column<string>(type: "varchar(90)", unicode: false, maxLength: 90, nullable: false, collation: "Arabic_CI_AI"),
                    F_Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, collation: "Arabic_CI_AI"),
                    L_Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, collation: "Arabic_CI_AI"),
                    Shop_Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Long = table.Column<double>(type: "float", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Seller_pk", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beauty_Products",
                columns: table => new
                {
                    Product_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Product_Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Brand = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Price = table.Column<decimal>(type: "numeric(20,2)", nullable: false),
                    Product_Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Avg_Rating = table.Column<decimal>(type: "numeric(2,1)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Beauty_Products_pk", x => x.Product_ID);
                    table.ForeignKey(
                        name: "Beauty_Products_FK",
                        column: x => x.Email,
                        principalTable: "Seller",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Books_Products",
                columns: table => new
                {
                    Product_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Product_Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Brand = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Price = table.Column<decimal>(type: "numeric(20,2)", nullable: false),
                    Product_Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Avg_Rating = table.Column<decimal>(type: "numeric(2,1)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Books_Prodcuts_pk", x => x.Product_ID);
                    table.ForeignKey(
                        name: "Books_Products_FK",
                        column: x => x.Email,
                        principalTable: "Seller",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Electronics_Products",
                columns: table => new
                {
                    Product_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Product_Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Brand = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Price = table.Column<decimal>(type: "numeric(20,2)", nullable: false),
                    Product_Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Avg_Rating = table.Column<decimal>(type: "numeric(2,1)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Electronics_Products_pk", x => x.Product_ID);
                    table.ForeignKey(
                        name: "Electronics_Products_FK",
                        column: x => x.Email,
                        principalTable: "Seller",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Fashion_Product",
                columns: table => new
                {
                    Product_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Product_Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Brand = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Price = table.Column<decimal>(type: "numeric(20,2)", nullable: false),
                    Product_Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Avg_Rating = table.Column<decimal>(type: "numeric(2,1)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Fashion_Product_pk", x => x.Product_ID);
                    table.ForeignKey(
                        name: "Fashion_Products_FK",
                        column: x => x.Email,
                        principalTable: "Seller",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Gaming_Products",
                columns: table => new
                {
                    Product_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Product_Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Brand = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Price = table.Column<decimal>(type: "numeric(20,2)", nullable: false),
                    Product_Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Avg_Rating = table.Column<decimal>(type: "numeric(2,1)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Gaming_Products_pk", x => x.Product_ID);
                    table.ForeignKey(
                        name: "Gaming_Products_FK",
                        column: x => x.Email,
                        principalTable: "Seller",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Seller_Phone",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: false),
                    Phone_Number = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Seller_Phone_pk", x => new { x.Email, x.Phone_Number });
                    table.ForeignKey(
                        name: "Seller_Phone_FK",
                        column: x => x.Email,
                        principalTable: "Seller",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Sports_Products",
                columns: table => new
                {
                    Product_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Product_Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Brand = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, collation: "Arabic_CI_AI"),
                    Price = table.Column<decimal>(type: "numeric(20,2)", nullable: false),
                    Product_Description = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Avg_Rating = table.Column<decimal>(type: "numeric(2,1)", nullable: false),
                    Comment = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false, collation: "Arabic_CI_AI"),
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Sports_Products_pk", x => x.Product_ID);
                    table.ForeignKey(
                        name: "Sports_Products_FK",
                        column: x => x.Email,
                        principalTable: "Seller",
                        principalColumn: "Email");
                });

            migrationBuilder.CreateTable(
                name: "Beauty_Products_Image",
                columns: table => new
                {
                    Product_Image_ID = table.Column<int>(type: "int", unicode: false, maxLength: 30, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<int>(type: "int", nullable: true),
                    ProductImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Beauty_Products_Image_pk", x => x.Product_Image_ID);
                    table.ForeignKey(
                        name: "Beauty_Products_Image_FK",
                        column: x => x.Product_ID,
                        principalTable: "Beauty_Products",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Beauty_Wishlist",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: false),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Beauty_Wishlist_pk", x => new { x.Email, x.Product_ID });
                    table.ForeignKey(
                        name: "Beauty_Wishlist_FK_Email",
                        column: x => x.Email,
                        principalTable: "Buyer",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "Beauty_Wishlist_FK_PID",
                        column: x => x.Product_ID,
                        principalTable: "Beauty_Products",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Books_Products_Image",
                columns: table => new
                {
                    Product_Image_ID = table.Column<int>(type: "int", unicode: false, maxLength: 30, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<int>(type: "int", nullable: true),
                    ProductImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Books_Prodcuts_Image_pk", x => x.Product_Image_ID);
                    table.ForeignKey(
                        name: "Books_Products_Image_FK",
                        column: x => x.Product_ID,
                        principalTable: "Books_Products",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Books_Wishlist",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: false),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Books_Wishlist_pk", x => new { x.Email, x.Product_ID });
                    table.ForeignKey(
                        name: "Books_Wishlist_FK_Email",
                        column: x => x.Email,
                        principalTable: "Buyer",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "Books_Wishlist_FK_PID",
                        column: x => x.Product_ID,
                        principalTable: "Books_Products",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Electronics_Products_Image",
                columns: table => new
                {
                    Product_Image_ID = table.Column<int>(type: "int", unicode: false, maxLength: 30, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<int>(type: "int", nullable: true),
                    ProductImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Electronics_Prodcuts_Image_pk", x => x.Product_Image_ID);
                    table.ForeignKey(
                        name: "Electronics_Products_Image_FK",
                        column: x => x.Product_ID,
                        principalTable: "Electronics_Products",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Electronics_Wishlist",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: false),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Electronics_Wishlist_pk", x => new { x.Email, x.Product_ID });
                    table.ForeignKey(
                        name: "Electronics_Wishlist_FK_Email",
                        column: x => x.Email,
                        principalTable: "Buyer",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "Electronics_Wishlist_FK_PID",
                        column: x => x.Product_ID,
                        principalTable: "Electronics_Products",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Fashion_Product_Image",
                columns: table => new
                {
                    Product_Image_ID = table.Column<int>(type: "int", unicode: false, maxLength: 30, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<int>(type: "int", nullable: true),
                    ProductImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Fashion_Product_Image_pk", x => x.Product_Image_ID);
                    table.ForeignKey(
                        name: "Fashion_Product_Image_FK",
                        column: x => x.Product_ID,
                        principalTable: "Fashion_Product",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Fashion_Wishlist",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: false),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Fashion_Wishlist_pk", x => new { x.Email, x.Product_ID });
                    table.ForeignKey(
                        name: "Fashion_Wishlist_FK_Email",
                        column: x => x.Email,
                        principalTable: "Buyer",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "Fashion_Wishlist_FK_PID",
                        column: x => x.Product_ID,
                        principalTable: "Fashion_Product",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Gaming_Products_Image",
                columns: table => new
                {
                    Product_Image_ID = table.Column<int>(type: "int", unicode: false, maxLength: 30, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<int>(type: "int", nullable: true),
                    ProductImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Gaming_Products_Image_pk", x => x.Product_Image_ID);
                    table.ForeignKey(
                        name: "Gaming_Products_Image_FK",
                        column: x => x.Product_ID,
                        principalTable: "Gaming_Products",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Gaming_Wishlist",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: false),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Gaming_Wishlist_pk", x => new { x.Email, x.Product_ID });
                    table.ForeignKey(
                        name: "Gaming_Wishlist_FK_Email",
                        column: x => x.Email,
                        principalTable: "Buyer",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "Gaming_Wishlist_FK_PID",
                        column: x => x.Product_ID,
                        principalTable: "Gaming_Products",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Sports_Products_Image",
                columns: table => new
                {
                    Product_Image_ID = table.Column<int>(type: "int", unicode: false, maxLength: 30, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<int>(type: "int", nullable: true),
                    ProductImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Sports_Products_Image_pk", x => x.Product_Image_ID);
                    table.ForeignKey(
                        name: "Sports_Products_Image_FK",
                        column: x => x.Product_ID,
                        principalTable: "Sports_Products",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateTable(
                name: "Sports_Wishlist",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: false),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "varchar(320)", unicode: false, maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Sports_Wishlist_pk", x => new { x.Email, x.Product_ID });
                    table.ForeignKey(
                        name: "Sports_Wishlist_FK_Email",
                        column: x => x.Email,
                        principalTable: "Buyer",
                        principalColumn: "Email");
                    table.ForeignKey(
                        name: "Sports_Wishlist_FK_PID",
                        column: x => x.Product_ID,
                        principalTable: "Sports_Products",
                        principalColumn: "Product_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Beauty_Products_Email",
                table: "Beauty_Products",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Beauty_Products_Image_Product_ID",
                table: "Beauty_Products_Image",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Beauty_Wishlist_Product_ID",
                table: "Beauty_Wishlist",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Products_Email",
                table: "Books_Products",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Products_Image_Product_ID",
                table: "Books_Products_Image",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Wishlist_Product_ID",
                table: "Books_Wishlist",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Electronics_Products_Email",
                table: "Electronics_Products",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Electronics_Products_Image_Product_ID",
                table: "Electronics_Products_Image",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Electronics_Wishlist_Product_ID",
                table: "Electronics_Wishlist",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Fashion_Product_Email",
                table: "Fashion_Product",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Fashion_Product_Image_Product_ID",
                table: "Fashion_Product_Image",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Fashion_Wishlist_Product_ID",
                table: "Fashion_Wishlist",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Gaming_Products_Email",
                table: "Gaming_Products",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Gaming_Products_Image_Product_ID",
                table: "Gaming_Products_Image",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Gaming_Wishlist_Product_ID",
                table: "Gaming_Wishlist",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "Seller_Phone_unique",
                table: "Seller_Phone",
                column: "Phone_Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sports_Products_Email",
                table: "Sports_Products",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Sports_Products_Image_Product_ID",
                table: "Sports_Products_Image",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Sports_Wishlist_Product_ID",
                table: "Sports_Wishlist",
                column: "Product_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Beauty_Products_Image");

            migrationBuilder.DropTable(
                name: "Beauty_Wishlist");

            migrationBuilder.DropTable(
                name: "Books_Products_Image");

            migrationBuilder.DropTable(
                name: "Books_Wishlist");

            migrationBuilder.DropTable(
                name: "Electronics_Products_Image");

            migrationBuilder.DropTable(
                name: "Electronics_Wishlist");

            migrationBuilder.DropTable(
                name: "Fashion_Product_Image");

            migrationBuilder.DropTable(
                name: "Fashion_Wishlist");

            migrationBuilder.DropTable(
                name: "Gaming_Products_Image");

            migrationBuilder.DropTable(
                name: "Gaming_Wishlist");

            migrationBuilder.DropTable(
                name: "Seller_Phone");

            migrationBuilder.DropTable(
                name: "Sports_Products_Image");

            migrationBuilder.DropTable(
                name: "Sports_Wishlist");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Beauty_Products");

            migrationBuilder.DropTable(
                name: "Books_Products");

            migrationBuilder.DropTable(
                name: "Electronics_Products");

            migrationBuilder.DropTable(
                name: "Fashion_Product");

            migrationBuilder.DropTable(
                name: "Gaming_Products");

            migrationBuilder.DropTable(
                name: "Buyer");

            migrationBuilder.DropTable(
                name: "Sports_Products");

            migrationBuilder.DropTable(
                name: "Seller");
        }
    }
}
