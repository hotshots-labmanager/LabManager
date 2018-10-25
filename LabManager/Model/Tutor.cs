using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LabManager.Model
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
            HaveTutored = new List<HaveTutored>();
            PlanToTutor = new List<PlanToTutor>();
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

        public void AddHaveTutored(HaveTutored ht)
        {
            if (!HaveTutored.Contains(ht))
            {
                ht.Tutor = this;
                HaveTutored.Add(ht);
            }
        }

        public void AddPlanToTutor(PlanToTutor ptt)
        {
            if (!PlanToTutor.Contains(ptt))
            {
                ptt.Tutor = this;
                PlanToTutor.Add(ptt);
            }
        }

        public override bool Equals(object obj)
        {
            Tutor t = obj as Tutor;
            if (t == null)
            {
                return false;
            }
            return Ssn == t.Ssn;
        }

        public bool FullEquals(object obj)
        {
            Tutor t = obj as Tutor;
            return Equals(obj) && FirstName == t.FirstName && LastName == t.LastName && Email == t.Email && Password == t.Password;
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
