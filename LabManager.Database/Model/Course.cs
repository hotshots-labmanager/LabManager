using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.Model
{
    public class Course
    {
        [Key]
        public String Code { get; set; }
        public String Name { get; set; }
        public double Credits { get; set; }
        public int NumberOfStudents { get; set; }

        public Course()
        {
        }

        public Course(String code, String name, double credits, int numberOfStudents)
        {
            this.Code = code;
            this.Name = name;
            this.Credits = credits;
            this.NumberOfStudents = numberOfStudents;

        }

        public virtual List<TutoringSession> TutoringSessions { get; set; }

    }





}
