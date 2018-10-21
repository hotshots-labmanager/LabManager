using LabManager.Database.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Model
{
    public class HaveTutored
    {
        public String Ssn { get; set; }

        public String Code { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Decimal Hours { get; set; }

        public HaveTutored()
        {

        }

        public HaveTutored(String ssn, String code, DateTime startTime, DateTime endTime, Decimal hours) : this()
        {
            Ssn = ssn;
            Code = code;
            StartTime = startTime;
            EndTime = endTime;
            Hours = hours;
        }

        public virtual Tutor Tutor { get; set; }

        public virtual TutoringSession TutoringSession { get; set; }

        public override bool Equals(object obj)
        {
            HaveTutored ht = obj as HaveTutored;
            if (ht == null)
            {
                return false;
            }
            return Ssn == ht.Ssn && Code == ht.Code && StartTime == ht.StartTime && EndTime == ht.EndTime;
        }

        public bool FullEquals(object obj)
        {
            HaveTutored ht = obj as HaveTutored;
            return Equals(obj) && Hours == ht.Hours;
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
