using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class GetUserById : DatabaseGetCommand<UserProfile>
    {
        private readonly int _id;

        public GetUserById(int id)
        {
            _id = id;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            Result = context.UserProfiles.SingleOrDefault(u => u.UserId == _id);
        }
    }
}
