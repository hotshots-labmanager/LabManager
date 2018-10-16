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
        [Column("code", Order = 0)]
        public String Code { get; set; }
        [Key]
        [Column("startTime", Order = 1)]
        public DateTime StartTime { get; set; }
        [Key]
        [Column("endTime", Order = 2)]
        public DateTime EndTime { get; set; }
        [Column("numberOfParticipants")]
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
    }
}
