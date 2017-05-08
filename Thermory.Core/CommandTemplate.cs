using System;

namespace Thermory.Core
{
    public abstract class CommandTemplate : ICommand
    {
        protected string ClassName { get { return GetType().Name; } }

        protected readonly Guid Id = Guid.NewGuid();

        public void Execute()
        {
            try
            {
                OnBeforeExecute();
                OnExecute();
                OnAfterExecute();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        protected virtual void OnBeforeExecute()
        { }

        protected abstract void OnExecute();

        protected virtual void OnAfterExecute()
        { }

        protected virtual void HandleException(Exception ex)
        {
            throw ex;
        }
    }
}
