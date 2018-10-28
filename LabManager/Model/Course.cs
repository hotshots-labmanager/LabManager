using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Model
{
    public class Course
    {
        [Key]
        public String Code { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public Decimal Credits { get; set; }

        public int? NumberOfStudents { get; set; }

        public Course()
        {

        }

        public Course(String code, String name, Decimal credits, int? numberOfStudents) : this()
        {
            Code = code;
            Name = name;
            Credits = credits;
            NumberOfStudents = numberOfStudents;
        }

        public virtual ICollection<TutoringSession> TutoringSessions { get; set; }

        public override bool Equals(object obj)
        {
            Course c = obj as Course;
            if (c == null)
            {
                return false;
            }
            return Code == c.Code;
        }

        public bool FullEquals(object obj)
        {
            Course c = obj as Course;
            return Equals(c) && Code == c.Code && c.Name == Name && Credits == c.Credits && NumberOfStudents == c.NumberOfStudents;
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int hash = 7;
            hash = prime * hash + Code.GetHashCode();
            return hash;
        }
    }
}
