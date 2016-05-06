using System.Collections.Generic;
using Thermory.Business;
using Thermory.Domain.Models;

namespace Thermory.Web.Controllers.Api
{
    public class UserGridController : ThermoryGridController<UserProfile>
    {
        protected override void FinalizeResults(List<UserProfile> results)
        {
            foreach (var user in results)
            {
                user.UserRoles = CommandDirectory.Instance.GetUserRolesByUserId(user.UserId);
            }
        }
    }
}