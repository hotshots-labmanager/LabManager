using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LabManager.Utility.ExceptionHandling
{
    internal class DataMessageHelper : IMessageHelper<DataException>
    {
        public string GetMessage(DataException ex)
        {
            if (ex is DbUpdateException)
            {
                SqlException innerEx = UnwrapInnerException(ex) as SqlException;
                if (innerEx.Number >= 60000 && innerEx.Number < 70000)
                {
                    // This exception is a custom exception written in database;
                    // call on LabManager message helper class
                    return new LabManagerMessageHelper().GetMessage(innerEx);
                }
                else if (innerEx.Number <= 50000)
                {
                    return new SqlMessageHelper().GetMessage(innerEx);
                }
                return "An unhandled database update exception (DbUpdateException) occured.";
            }
            else if (ex is DbEntityValidationException)
            {
                DbEntityValidationException valEx = ex as DbEntityValidationException;
                return GetDbEntityValidationExceptionMessage(valEx);
            }
            return "An unhandled data exception (DataException) occured.";
        }

        private static string GetDbEntityValidationExceptionMessage(DbEntityValidationException ex)
        {
            StringBuilder builder = new StringBuilder();
            ICollection<DbEntityValidationResult> validationResults = ex.EntityValidationErrors.ToList();
            for (int i = 0; i < validationResults.Count; i++)
            {
                DbEntityValidationResult result = validationResults.ElementAt(i);
                ICollection<DbValidationError> validationErrors = result.ValidationErrors;
                for (int j = 0; j < validationErrors.Count; j++)
                {
                    DbValidationError error = validationErrors.ElementAt(j);
                    builder.Append(error.ErrorMessage);
                    if (j != validationErrors.Count - 1)
                    {
                        builder.Append("; ");
                    }
                }
                if (i != validationResults.Count - 1)
                {
                    builder.AppendLine();
                }
            }
            return builder.ToString();
        }

        private static Exception UnwrapInnerException(DataException e)
        {
            return e.InnerException.InnerException;
        }
    }
}