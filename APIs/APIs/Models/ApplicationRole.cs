using Microsoft.AspNetCore.Identity;

namespace APIs.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base()
        {
            Name = roleName;
        }
    }
}
