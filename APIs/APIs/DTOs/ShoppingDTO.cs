namespace APIs.DTOs
{
    public class ShoppingDTO
    {
        public int BeautyProductId { get; set; }
        public int BooksProductId { get; set; }
        public int ElectronicsProductId { get; set; }
        public int FashionProductId { get; set; }
        public int GamingProductId { get; set; }
        public int SportsProductId { get; set; }
        public int Count { get; set; }

        public string? ApplicationUserId { get; set; }

    }
}
