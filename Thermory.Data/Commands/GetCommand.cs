using Thermory.Domain;

namespace Thermory.Data.Commands
{
    internal abstract class GetCommand<T> : Command, IGetCommand<T>
    {
        public T Results { get; protected set; }

        protected void InvokeRepositoryRead(System.Action<ThermoryContext> action)
        {
            using (var context = CreateContext())
            {
                action(context);
            }
        }
    }
}
