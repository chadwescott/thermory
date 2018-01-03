using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class DeleteUserRole : DatabaseContextCommand
    {
        private readonly UserRoleXref _userRole;

        public DeleteUserRole(UserRoleXref userRole)
        {
            _userRole = userRole;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            context.UserRoles.Attach(_userRole);
            context.UserRoles.Remove(_userRole);
            context.SaveChanges();
        }
    }
}
