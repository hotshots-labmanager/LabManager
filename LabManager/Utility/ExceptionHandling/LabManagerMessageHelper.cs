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
    internal class LabManagerMessageHelper : IMessageHelper<SqlException>
    {
        // LabManager error codes
        private const int TUTORINGSESSION_END_TIME_BEFORE_START_TIME = 61000;
        private const int TUTORINGSESSION_CONCURRENT_SESSION = 61001;

        private const int TUTORTUTORINGSESSION_OVERLAPPING = 63000;

        public string GetMessage(SqlException ex)
        {
            string message = ex.Message;
            switch (ex.Number)
            {
                case TUTORINGSESSION_END_TIME_BEFORE_START_TIME:
                    return message;
                case TUTORINGSESSION_CONCURRENT_SESSION:
                    return message;
                case TUTORTUTORINGSESSION_OVERLAPPING:
                    return message;
            }
            return "An unhandled LabManager exception (SqlException) occured.";
        }
    }
}