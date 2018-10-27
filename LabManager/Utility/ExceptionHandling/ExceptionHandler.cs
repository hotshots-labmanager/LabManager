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
    internal interface IMessageHelper<T>
    {
        string GetMessage(T ex);
    }

    public class ExceptionHandler
    {
        private static Dictionary<Type, dynamic> helpers;

        static ExceptionHandler()
        {
            helpers = new Dictionary<Type, dynamic>();
            helpers.Add(typeof(DataException), new DataMessageHelper());
            helpers.Add(typeof(IOException), new IOMessageHelper());
            helpers.Add(typeof(SqlException), new SqlMessageHelper());
        }

        public static string GetErrorMessage(Exception ex)
        {
            Console.WriteLine(ex.GetType());
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
                return "System encountered a null-value that could not be handled, please check input data.";
            }
            return ex.Message;
        }

    }
}