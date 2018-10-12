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
        public  DateTime EndTime { get; set; }
        public int NumberOfParticipants { get; set; }

        public TutoringSession()
        {

        }

        public TutoringSession(String code, DateTime startTime, DateTime endTime, int numberOfParticipants)
        {
            this.Code = code;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.NumberOfParticipants = numberOfParticipants;
        }
        public virtual Course Course { get; set; }

        public virtual List<Tutor> Tutors { get; set; }
    }
}
