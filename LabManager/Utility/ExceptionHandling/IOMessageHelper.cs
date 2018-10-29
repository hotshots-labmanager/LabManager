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
    internal class IOMessageHelper : IMessageHelper<IOException>
    {
        public string GetMessage(IOException ex)
        {
            return "An unhandled input/output exception (IOException) occured.";
        }
    }
}