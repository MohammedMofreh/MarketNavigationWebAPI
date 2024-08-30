using Microsoft.AspNetCore.Identity;

namespace APIs.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string NameOfPerson { get; set; }
    }
}
