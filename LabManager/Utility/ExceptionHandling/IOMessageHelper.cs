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
            //if (ex is FileNotFoundException)
            //{
            //    FileNotFoundException fileEx = ex as FileNotFoundException;
            //    return string.Format("Kunde inte hitta filen: {0}", fileEx.FileName);
            //}
            //else if (ex is DirectoryNotFoundException)
            //{
            //    DirectoryNotFoundException dirEx = ex as DirectoryNotFoundException;
            //    Regex pathMatcher = new Regex(@"[^']+");
            //    string path = pathMatcher.Matches(dirEx.Message)[1].Value;
            //    return string.Format("Kunde inte hitta sökvägen: {0}", path);
            //}
            return "An unhandled input/output exception (IOException) occured.";
        }
    }
}