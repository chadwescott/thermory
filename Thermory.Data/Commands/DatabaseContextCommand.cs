using System;
using System.Data.Entity.Validation;
using System.Linq;
using Thermory.Domain.Commands;

namespace Thermory.Data.Commands
{
    internal abstract class DatabaseContextCommand : ICommand
    {
        protected readonly log4net.ILog Logger =
               log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected string ClassName { get { return GetType().Name; } }

        protected abstract void OnExecute(ThermoryContext context);

        public void Execute()
        {
            using (var context = CreateContext())
            {
                Execute(context);
            }
        }

        public void Execute(ThermoryContext context)
        {
            try
            {
                OnBeforeExecute(context);
                OnExecute(context);
                OnAfterExecute(context);
            }
            catch (DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("\n", errorMessages);
                HandleException(context, new Exception(fullErrorMessage));
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
        }

        protected virtual void OnBeforeExecute(ThermoryContext context)
        {
            Logger.Info(string.Format("{0} Execute Start", ClassName));
        }

        protected virtual void OnAfterExecute(ThermoryContext context)
        {
            Logger.Info(string.Format("{0} Execute Complete", ClassName));
        }

        protected virtual void HandleException(ThermoryContext cont, Exception ex)
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
