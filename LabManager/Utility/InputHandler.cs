using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Utility
{
    public static class InputHandler
    {
        public static bool IsFieldsFilledOut(out String message, Dictionary<String, String> values)
        {
            foreach (KeyValuePair<string, string> entry in values)
            {
                String key = entry.Key;
                String value = entry.Value;
                if (value == null || value.Equals(String.Empty))
                {
                    message = "Field " + key + " must be filled out.";
                    return false;
                }
            }
            message = "All fields filled out.";
            return true;
        }
    }
}
