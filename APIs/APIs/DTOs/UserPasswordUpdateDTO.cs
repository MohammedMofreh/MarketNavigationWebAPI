using System.ComponentModel.DataAnnotations;

namespace APIs.DTOs
{
    public class UserPasswordUpdateDTO
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

    }
}
