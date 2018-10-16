using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabManager.Model
{
    public class Tutor
    {
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

        public virtual ICollection<HaveTutored> HaveTutored { get; set; }
        
        public virtual ICollection<PlanToTutor> PlanToTutor { get; set; }

        public override bool Equals(object obj)
        {
            Tutor t = obj as Tutor;
            if (t == null)
            {
                return false;
            }
            return Ssn == t.Ssn;
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int hash = 7;
            hash = prime * hash + Ssn.GetHashCode();
            return hash;
        }
    }
}
