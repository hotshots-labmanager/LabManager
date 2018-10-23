using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace LabManager.Utility
{
    internal interface IMessageHelper<T>
    {
        string GetMessage(T ex);
    }

    public class ExceptionHandler
    {
        private static Dictionary<Type, dynamic> helpers;

        private const int CANNOT_INSERT_NULL = 515;
        private const int DATA_TYPE_CONVERSION_ERROR = 8114;
        private const int LOGIN_FAILED = 4060;
        private const int NON_MATCHING_TABLE_DEFINITION = 213;
        private const int PRIMARY_KEY_VIOLATION = 2627;
        private const int RAISE_ERROR = 50000;
        private const int TRUNCATED_DATA = 8152;
        private const int WRONG_CREDENTIALS = 18456;

        static ExceptionHandler()
        {
            helpers = new Dictionary<Type, dynamic>();
            helpers.Add(typeof(IOException), new IOMessageHelper());
            helpers.Add(typeof(SqlException), new SqlMessageHelper());
           // helpers.Add(typeof(DataException), new DataMessageHelper());

        }

        public static string GetErrorMessage(Exception ex)
        {
            if (ex is IOException)
            {
                IOException ioEx = ex as IOException;
                return helpers[typeof(IOException)].GetMessage(ioEx);
            }
            else if (ex is DataException)
            {
                DataException dataEx = ex as DataException;
                return helpers[typeof(DataException)].GetMessage(dataEx);
            }
            else if (ex is SqlException)
            {
                SqlException sqlEx = ex as SqlException;
                return helpers[typeof(SqlException)].GetMessage(sqlEx);
            }
            return GetGenericErrorMessage(ex);
        }

        private static string GetGenericErrorMessage(Exception ex)
        {
            if (ex is NullReferenceException)
            {
                return "Programmet stötte på null-värde som det inte kan hantera, var god kontrollera alla indata-fält.";
            }
            return ex.Message;
        }

        private class SqlMessageHelper : IMessageHelper<SqlException>
        {
            public string GetMessage(SqlException ex)
            {
                string message = ex.Message;
                switch (ex.ErrorCode)
                {
                    case CANNOT_INSERT_NULL:
                        return GetCannotInsertNullMessage();
                    case DATA_TYPE_CONVERSION_ERROR:
                        return GetDataTypeConversionErrorMessage();
                    case LOGIN_FAILED:
                        return "Inloggningen till databasen misslyckades; kontrollera användarnamn och lösenord";
                    case NON_MATCHING_TABLE_DEFINITION:
                        return "Databasen accepterar inte indatan för ett fält";
                    case PRIMARY_KEY_VIOLATION:
                        return GetPrimaryKeyViolationMessage();
                    case RAISE_ERROR:
                        return message;
                    case TRUNCATED_DATA:
                        return "Ett indata-fält överskrider maximala tillåtna längden";
                    case WRONG_CREDENTIALS:
                        return GetWrongCredentialsMessage();
                }
                return "Ett SQL-fel (SqlException) uppstod.";
            }

            public static string GetCannotInsertNullMessage()
            {
                return "Databasen accepterar inte null-värde, var god fyll i alla fält.";
            }

            public static string GetDataTypeConversionErrorMessage()
            {
                return "Kan inte konvertera datatypen.";
            }

            public static string GetPrimaryKeyViolationMessage()
            {
                return "Primärnyckeln används redan av en viss tupel, var god välj en annan.";
            }

            public static string GetWrongCredentialsMessage()
            {
                return "Fel inloggningsuppgifter till databasen, var god kontrollera dessa och försök igen.";
            }
        }

        private class IOMessageHelper : IMessageHelper<IOException>
        {
            public string GetMessage(IOException ex)
            {
                if (ex is FileNotFoundException)
                {
                    FileNotFoundException fileEx = ex as FileNotFoundException;
                    return string.Format("Kunde inte hitta filen: {0}", fileEx.FileName);
                }
                else if (ex is DirectoryNotFoundException)
                {
                    DirectoryNotFoundException dirEx = ex as DirectoryNotFoundException;
                    Regex pathMatcher = new Regex(@"[^']+");
                    string path = pathMatcher.Matches(dirEx.Message)[1].Value;
                    return string.Format("Kunde inte hitta sökvägen: {0}", path);
                }
                return "Ett indata/utdata-fel (IOException) uppstod.";
            }
        }

        //private class DataMessageHelper : IMessageHelper<DataException>
        //{
        //    public string GetMessage(DataException ex)
        //    {
        //        if (ex is ValidationException)
        //        {
        //            ValidationException valEx = ex as ValidationException;
        //            return GetValidationExceptionMessage(valEx);
        //        }
        //        return "Ett datafel (DataException) uppstod.";
        //    }

        //    private static string GetValidationExceptionMessage(ValidationException ex)
        //    {
        //        StringBuilder builder = new StringBuilder();
        //        ICollection<ValidationResult> validationResults = ex.EntityValidationErrors.ToList();
        //        for (int i = 0; i < validationResults.Count; i++)
        //        {
        //            DbEntityValidationResult result = validationResults.ElementAt(i);
        //            ICollection<DbValidationError> validationErrors = result.ValidationErrors;
        //            for (int j = 0; j < validationErrors.Count; j++)
        //            {
        //                DbValidationError error = validationErrors.ElementAt(j);
        //                builder.Append(error.ErrorMessage);
        //                if (j != validationErrors.Count - 1)
        //                {
        //                    builder.Append("; ");
        //                }
        //            }
        //            if (i != validationResults.Count - 1)
        //            {
        //                builder.AppendLine();
        //            }
        //        }
        //        return builder.ToString();
        //    }
        //}

    }
}