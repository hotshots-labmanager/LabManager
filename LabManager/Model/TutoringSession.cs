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
        private String code;
        private DateTime startTime;
        private DateTime endTime;

        public String Code
        {
            get { return code; }
            set
            {
                code = value;
                // Update the relations
                foreach (HaveTutored ht in HaveTutored)
                {
                    ht.Code = value;
                }
                foreach (PlanToTutor ptt in PlanToTutor)
                {
                    ptt.Code = value;
                }
            }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                // Update the relations
                foreach (HaveTutored ht in HaveTutored)
                {
                    ht.StartTime = value;
                }
                foreach (PlanToTutor ptt in PlanToTutor)
                {
                    ptt.StartTime = value;
                }
            }
        }

        public DateTime EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                // Update the relations
                foreach (HaveTutored ht in HaveTutored)
                {
                    ht.EndTime = value;
                }
                foreach (PlanToTutor ptt in PlanToTutor)
                {
                    ptt.EndTime = value;
                }
            }
        }

        public int? NumberOfParticipants { get; set; }

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
            else
            {
                // HaveTutored already exists in this object; update it instead
                HaveTutored sHt = HaveTutored.First(x => x.Equals(ht));
                sHt.Hours = ht.Hours;
            }
        }

        public void AddPlanToTutor(PlanToTutor ptt)
        {
            if (!PlanToTutor.Contains(ptt))
            {
                ptt.TutoringSession = this;
                PlanToTutor.Add(ptt);
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

        public bool FullEquals(object obj)
        {
            TutoringSession ts = obj as TutoringSession;
            return Equals(ts) && NumberOfParticipants == ts.NumberOfParticipants;
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
