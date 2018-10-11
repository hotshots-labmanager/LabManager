using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.Model
{
    public class Course
    {
        public String Code { get; set; }
        public String Name { get; set; }
        public double Credits { get; set; }
        public int NumberOfStudents { get; set; }
    }


}
