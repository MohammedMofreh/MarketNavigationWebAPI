namespace APIs.DTOs
{
    public class SearchResultDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public ICollection<object> ProductImages { get; set; } // Using object type to handle different image types
        public decimal AvgRating { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
    }
}
