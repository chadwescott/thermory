using System.Data;
using System.Data.SqlClient;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveLumberCategory : DatabaseConnectionCommand
    {
        private readonly LumberCategory _model;

        public SaveLumberCategory(LumberCategory model)
        {
            _model = model;
        }

        protected override void OnExecute(SqlConnection connection)
        {
            var command = new SqlCommand("SaveLumberCategories", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@id", _model.Id));
            command.Parameters.Add(new SqlParameter("@name", _model.Name));
            command.Parameters.Add(new SqlParameter("@sortOrder", _model.SortOrder));

            command.ExecuteNonQuery();
        }
    }
}
