using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.Model
{
    public class TutoringSession
    {
        [Key]
        [Column(Order = 0)]
        public String Code { get; set; }
        [Key]
        [Column(Order = 1)]
        public DateTime StartTime { get; set; }
        [Key]
        [Column(Order = 2)]
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

        public virtual ICollection<Tutor> PlanToTutor { get; set; }

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
