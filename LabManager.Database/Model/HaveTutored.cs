using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.Model
{
    public class HaveTutored
    {
        [Key]
        [Column(Order = 0)]
        public String Ssn { get; set; }

        [Key]
        [Column(Order = 1)]
        public String Code { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime StartTime { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Endtime { get; set; }

        public Decimal Hours { get; set; }

        public HaveTutored()
        {

        }

        public HaveTutored(String ssn, String code, DateTime startTime, DateTime endTime, Decimal hours) : this()
        {
            Ssn = ssn;
            Code = code;
            StartTime = startTime;
            Endtime = endTime;
            Hours = hours;
        }

        public virtual Tutor Tutor { get; set; }
        public virtual TutoringSession TutorSession { get; set; }

    }
}
