using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class AddUserRole : DatabaseContextCommand
    {
        private readonly UserRoleXref _userRole;

        public AddUserRole(UserRoleXref userRole)
        {
            _userRole = userRole;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.UserRoles.Add(_userRole);
            context.SaveChanges();
        }
    }
}
