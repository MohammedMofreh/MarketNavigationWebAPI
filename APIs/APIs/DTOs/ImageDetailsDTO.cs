using APIs.Models;

namespace APIs.DTOs
{
    public class ImageDetailsDTO
    {
        public int ProductImageId { get; set; }

        public int ProductId { get; set; }

        public string Category { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public decimal Price { get; set; }

        public string ProductDescription { get; set; } = null!;

        public decimal AvgRating { get; set; }

        public string Comment { get; set; } = null!;

        public string? Email { get; set; }

        public byte[] Image { get; set; }
       
    }
}
