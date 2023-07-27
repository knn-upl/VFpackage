using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace VF.Models
{
    public class UserRoles
    {
    
        public long? UserId { get; set; }
        public long? RoleId { get; set; }
        public Users? User { get; set; }
        public Roles? Role { get; set; }
    }
  
}
