using System.ComponentModel.DataAnnotations.Schema;

namespace Thermory.Web.Models
{
    [Table("UserRole_V")]
    public class UserRoleView : IViewModel
    {
        [Column("UserId")]
        public string recid { get; set; }

        [Column("UserName")]
        public string EmailAddress { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("RoleNames")]
        public string RoleNames { get; set; }
    }
}