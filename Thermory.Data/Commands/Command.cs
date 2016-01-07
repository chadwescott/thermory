using System;

namespace Thermory.Data.Commands
{
    internal abstract class Command : ICommand
    {
        protected readonly log4net.ILog Logger =
               log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected string ClassName { get { return GetType().Name; } }

        protected abstract void OnExecute();

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
        {
            Logger.Info(string.Format("{0} Execute Start", ClassName));
        }

        protected virtual void OnAfterExecute()
        {
            Logger.Info(string.Format("{0} Execute Complete", ClassName));
        }

        protected virtual void HandleException(Exception ex)
        {
            Logger.Error(string.Format("{0} Exception: {1}", ClassName, ex.Message), ex);
            Logger.Error(ex.Message, ex);
            throw ex;
        }

        protected ThermoryContext CreateContext()
        {
            return new ThermoryContext("DefaultConnection");
        }
    }
}
