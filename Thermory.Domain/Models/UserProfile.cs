using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Thermory.Domain.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [ForeignKey("UserId")]
        public List<UserRoleXref> UserRoles { get; set; }

        [NotMapped]
        public string RoleNames
        {
            get
            {
                return UserRoles.Any() ? string.Join(", ", UserRoles.Select(r => r.WebPageRole.RoleName)) : string.Empty;
            }
        }

        [NotMapped]
        public string recid
        {
            get { return UserId.ToString(); }
            set
            {
                int userId;
                if (int.TryParse(value, out userId))
                    UserId = userId;
            }
        }
    }
}
