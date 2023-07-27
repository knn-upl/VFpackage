using Microsoft.AspNetCore.Identity;

namespace VF.Models
{
    public class Roles : IdentityRole<long>
    {        
        public ICollection<UserRoles>? UserRole { get; set; }
    }
}
