using System;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Thermory.Domain.Models;

namespace Thermory.Data.Commands
{
    internal class SaveLumberSubCategory : DatabaseConnectionCommand
    {
        private readonly LumberSubCategory _model;

        public SaveLumberSubCategory(LumberSubCategory model)
        {
            _model = model;
        }

        protected override void OnExecute(SqlConnection connection)
        {
            var command = new SqlCommand("SaveLumberSubCategories", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("@id", _model.Id));
            command.Parameters.Add(new SqlParameter("@name", _model.Name));
            command.Parameters.Add(new SqlParameter("@width", _model.WidthInMillimeters));
            command.Parameters.Add(new SqlParameter("@thickness", _model.ThicknessInMillimeters));
            command.Parameters.Add(new SqlParameter("@sortOrder", _model.SortOrder));
            command.Parameters.Add(new SqlParameter("@bundleSize", _model.BundleSize));
            command.Parameters.Add(new SqlParameter("@weight", _model.Weight));

            command.ExecuteNonQuery();
        }
    }
}
