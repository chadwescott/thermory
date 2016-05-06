using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetUserRolesByUserId : DatabaseGetCommand<IList<UserRoleXref>>
    {
        private readonly int _userId;

        public GetUserRolesByUserId(int userId)
        {
            _userId = userId;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.UserRoles.Include(ur => ur.WebPageRole).Where(ur => ur.UserId == _userId).ToList();
        }
    }
}
