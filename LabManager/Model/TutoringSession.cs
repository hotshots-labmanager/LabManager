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

        [Key, Column(Order = 0)]
        public String Code
        {
            get { return code; }
            set
            {
                code = value;
                foreach (TutorTutoringSession ptt in Tutors)
                {
                    ptt.Code = value;
                }
            }
        }

        [Key, Column(Order = 1)]
        public DateTime StartTime
        {
            get { return startTime; }
            set
            {
                startTime = value;
                foreach (TutorTutoringSession ptt in Tutors)
                {
                    ptt.StartTime = value;
                }
            }
        }

        [Key, Column(Order = 2)]
        public DateTime EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                foreach (TutorTutoringSession ptt in Tutors)
                {
                    ptt.EndTime = value;
                }
            }
        }

        public int? NumberOfParticipants { get; set; }

        public TutoringSession()
        {
            Tutors = new HashSet<TutorTutoringSession>();
        }

        public TutoringSession(String code, DateTime startTime, DateTime endTime, int? numberOfParticipants) : this()
        {
            Code = code;
            StartTime = startTime;
            EndTime = endTime;
            NumberOfParticipants = numberOfParticipants;
        }

        [ForeignKey("Code")]
        public virtual Course Course { get; set; }
        
        public virtual ICollection<TutorTutoringSession> Tutors { get; set; }

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
