using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    [Table("webpages_Roles")]
    public class WebPageRole
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        [ForeignKey("RoleId")]
        public List<UserRoleXref> UserRoles { get; set; } 
    }
}
