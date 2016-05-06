using System.Collections.Generic;
using System.Linq;
using Thermory.Data.Commands;
using Thermory.Domain.Models;

namespace Thermory.Data.CommandBuilders
{
    internal class UpdateUserRolesBuilder : CommandBuilder
    {
        private readonly UserProfile _user;

        public UpdateUserRolesBuilder(UserProfile user)
        {
            _user = user;
            var currentRoles = GetCurrentUserRoles();
            var existingRoles = GetExistingUserRoles();

            foreach (var userRole in existingRoles.Where(ur => currentRoles.All(r => r.RoleId != ur.RoleId)))
                Commands.Add(new DeleteUserRole(userRole));

            foreach (var userRole in currentRoles.Where(ur => existingRoles.All(r => r.RoleId != ur.RoleId)))
                Commands.Add(new AddUserRole(userRole));
        }

        private IList<UserRoleXref> GetExistingUserRoles()
        {
            var command = new GetUserRolesByUserId(_user.UserId);
            command.Execute();
            return command.Result;
        }

        private IList<UserRoleXref> GetCurrentUserRoles()
        {
            if (_user.RoleNames == null)
                return new List<UserRoleXref>();

            var command = new GetAllRoles();
            command.Execute();
            return
                command.Result.Where(r => _user.RoleNames.Contains(r.RoleName))
                    .Select(r => new UserRoleXref {RoleId = r.RoleId, UserId = _user.UserId})
                    .ToList();
        }
    }
}
