using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.Model
{
    public class Tutor
    {
        [Key]
        public String Ssn { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }

        public Tutor()
        {
        }

        public Tutor(String ssn, String firstName, String lastName, String email, String password) : this()
        {
            Ssn = ssn;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;

        }

        [ForeignKey("ssn")]
        public virtual List<TutoringSession> TutoringSessions { get; set; }

    }
}
