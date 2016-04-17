using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Domain.Models
{
    [Table("webpages_UsersInRoles")]
    public class UserRoleXref
    {
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public UserProfile UserProfile { get; set; }

        [ForeignKey("RoleId")]
        public WebPageRole WebPageRole { get; set; }
    }
}
