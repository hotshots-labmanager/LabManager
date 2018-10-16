using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Model
{
    public class TutoringSession
    {
        public String Code { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int NumberOfParticipants { get; set; }

        public TutoringSession()
        {

        }

        public TutoringSession(String code, DateTime startTime, DateTime endTime, int numberOfParticipants) : this()
        {
            Code = code;
            StartTime = startTime;
            EndTime = endTime;
            NumberOfParticipants = numberOfParticipants;
        }

        public virtual Course Course { get; set; }

        public virtual ICollection<HaveTutored> HaveTutored { get; set; }

        public virtual ICollection<PlanToTutor> PlanToTutor { get; set; }

        public void AddHaveTutored(HaveTutored ht)
        {
            if (!HaveTutored.Contains(ht))
            {
                ht.TutoringSession = this;
                HaveTutored.Add(ht);
            }
        }

        public override bool Equals(object obj)
        {
            TutoringSession ts = obj as TutoringSession;
            if (ts == null)
            {
                return false;
            }
            return Code == ts.Code && StartTime == ts.StartTime && EndTime == ts.EndTime;
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int hash = 7;
            hash = prime * hash + Code.GetHashCode() + StartTime.GetHashCode() + EndTime.GetHashCode();
            return hash;
        }
    }
}
