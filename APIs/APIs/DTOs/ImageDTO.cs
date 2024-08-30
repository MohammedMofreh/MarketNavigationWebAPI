using System.ComponentModel.DataAnnotations;

namespace APIs.DTOs
{
    public class ImageDTO
    {
        public int? ProductId { get; set; }
        public IFormFile? ProductImage { get; set; }
 
    }
}
