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

        private static Dictionary<string, string[]> pkMappings;

        static SqlMessageHelper()
        {
            pkMappings = new Dictionary<string, string[]>();
            pkMappings.Add("course", new string[] { "code" });
            pkMappings.Add("tutor", new string[] { "social security number" });
            pkMappings.Add("tutoringsession", new string[] { "code", "start time", "end time" });
            pkMappings.Add("tutortutoringsession", new string[] { "social security number", "code", "start time", "end time" });
        }

        public string GetMessage(SqlException ex)
        {
            string message = ex.Message;
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
                    return GetPrimaryKeyViolationMessage(message);
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

        public static string GetPrimaryKeyViolationMessage(String message)
        {
            message = message.Substring(message.IndexOf("duplicate key"));
            String truncated = message.Substring(message.IndexOf('\'') + 1);

            String tableWithDbo = truncated.Substring(0, truncated.IndexOf('\''));
            String tableWithoutDbo = tableWithDbo.Substring(4);

            String tableName = tableWithoutDbo;

            truncated = truncated.Substring(truncated.IndexOf('(') + 1);
            String keys = truncated.Substring(0, truncated.IndexOf(')')).Replace(",", "");
            String[] keysAsArr = keys.Split(' ');

            String keysOutput = "";
            String tableNameLowered = tableName.ToLower();
            String[] mappings = pkMappings[tableNameLowered];
            for (int i = 0; i < mappings.Length; i++)
            {
                String primaryKey = pkMappings[tableNameLowered][i];
                keysOutput += primaryKey + " " + keysAsArr[i];

                if (i != pkMappings[tableNameLowered].Length - 1)
                {
                    if (i + 2 == pkMappings[tableNameLowered].Length)
                    {
                        keysOutput += " and ";
                    }
                    else
                    {
                        keysOutput += ", ";
                    }
                }
            }

            String output = String.Format("There already exists an {0} with {1}.", tableNameLowered, keysOutput);
            return output;
        }

        public static string GetWrongCredentialsMessage()
        {
            return "Wrong credentials to the database, please check and try again.";
        }
    }
}