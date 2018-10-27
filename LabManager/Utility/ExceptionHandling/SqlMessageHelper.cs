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
    internal class SqlMessageHelper : IMessageHelper<SqlException>
    {
        // SQL Server error codes
        private const int CANNOT_INSERT_NULL = 515;
        private const int DATA_TYPE_CONVERSION_ERROR = 8114;
        private const int LOGIN_FAILED = 4060;
        private const int NON_MATCHING_TABLE_DEFINITION = 213;
        private const int PRIMARY_KEY_VIOLATION = 2627;
        private const int RAISE_ERROR = 50000;
        private const int TRUNCATED_DATA = 8152;
        private const int WRONG_CREDENTIALS = 18456;

        public string GetMessage(SqlException ex)
        {
            string message = ex.Message;
            Console.WriteLine(message);
            switch (ex.Number)
            {
                case CANNOT_INSERT_NULL:
                    return GetCannotInsertNullMessage();
                case DATA_TYPE_CONVERSION_ERROR:
                    return GetDataTypeConversionErrorMessage();
                case LOGIN_FAILED:
                    return "Login failed, please check username and password.";
                case NON_MATCHING_TABLE_DEFINITION:
                    return "Database does not accept the input data in one of the fields.";
                case PRIMARY_KEY_VIOLATION:
                    return GetPrimaryKeyViolationMessage();
                case RAISE_ERROR:
                    return message;
                case TRUNCATED_DATA:
                    return "One field exceeded maximum length.";
                case WRONG_CREDENTIALS:
                    return GetWrongCredentialsMessage();
            }
            return "An unhandled SQL exception (SqlException) occured.";
        }

        public static string GetCannotInsertNullMessage()
        {
            return "Database does not accept null-values, please fill out all the fields.";
        }

        public static string GetDataTypeConversionErrorMessage()
        {
            return "Cannot convert data type.";
        }

        public static string GetPrimaryKeyViolationMessage()
        {
            return "Primary key already exists in the database, please try again.";
        }

        public static string GetWrongCredentialsMessage()
        {
            return "Wrong credentials to the database, please check and try again.";
        }
    }
}