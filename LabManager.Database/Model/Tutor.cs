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
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
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
        public virtual ICollection<TutoringSession> TutoringSessions { get; set; }

    }
}
