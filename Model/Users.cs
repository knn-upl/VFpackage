using Microsoft.AspNetCore.Identity;

namespace VF.Models
{
    public class Users : IdentityUser<long>
    {
        public UserRoles? UserRole { get; set; }
    }
}
