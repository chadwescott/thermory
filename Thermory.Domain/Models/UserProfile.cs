using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace Thermory.Domain.Models
{
    [Table("UserProfile")]
    public class UserProfile : IViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("UserName")]
        public string UserName { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        private IList<UserRoleXref> _userRoles;

        [JsonIgnore]
        [ForeignKey("UserId")]
        public IList<UserRoleXref> UserRoles
        {
            get { return _userRoles; }
            set
            {
                _userRoles = value;
                RoleNames = _userRoles == null ? new List<string>() : UserRoles.Select(r => r.WebPageRole.RoleName).ToList();
            }
        }

        [NotMapped]
        public List<string> RoleNames { get; set; }

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
