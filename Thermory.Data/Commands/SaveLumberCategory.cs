using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveLumberCategory : DatabaseCommand
    {
        private readonly LumberCategory _model;

        public SaveLumberCategory(LumberCategory model)
        {
            _model = model;
        }

        protected override void OnBeforeExecute(ThermoryContext context)
        {
            base.OnBeforeExecute(context);
            if (_model.Id != Guid.Empty) return;
            _model.SortOrder = context.LumberCategories.Select(c => c.SortOrder).Max() + 1;
        }

        protected override void OnExecute(ThermoryContext context)
        {
            var connection = new SqlConnection(context.Database.Connection.ConnectionString);
            try
            {
                connection.Open();
                var command = new SqlCommand("UpdateLumberCategories", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@id", _model.Id));
                command.Parameters.Add(new SqlParameter("@name", _model.Name));
                command.Parameters.Add(new SqlParameter("@sortOrder", _model.SortOrder));

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
