using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabManager.Model
{
    public class TutorTutoringSession
    {
        [Key, Column(Order = 0)]
        public String Ssn { get; set; }

        [Key, Column(Order = 1)]
        public String Code { get; set; }

        [Key, Column(Order = 2)]
        public DateTime StartTime { get; set; }

        [Key, Column(Order = 3)]
        public DateTime EndTime { get; set; }

        public TutorTutoringSession()
        {

        }

        public TutorTutoringSession(String ssn, String code, DateTime startTime, DateTime endTime) : this()
        {
            Ssn = ssn;
            Code = code;
            StartTime = startTime;
            EndTime = endTime;
        }

        public virtual Tutor Tutor { get; set; }

        public virtual TutoringSession TutoringSession { get; set; }

        public override bool Equals(object obj)
        {
            TutorTutoringSession ht = obj as TutorTutoringSession;
            if (ht == null)
            {
                return false;
            }
            return Ssn == ht.Ssn && Code == ht.Code && StartTime == ht.StartTime && EndTime == ht.EndTime;
        }

        public bool FullEquals(object obj)
        {
            return Equals(obj);
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int hash = 7;
            hash = prime * hash + Ssn.GetHashCode() + Code.GetHashCode() + StartTime.GetHashCode() + EndTime.GetHashCode();
            return hash;
        }
    }
}
