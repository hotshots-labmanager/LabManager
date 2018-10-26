using LabManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.DAL
{
    public class TutorUpdateDTO
    {
        private Tutor old;
        private Tutor updated;

        public TutorUpdateDTO(Tutor old, Tutor updated)
        {
            this.old = new Tutor(old.Ssn, old.FirstName, old.LastName, old.Email, old.Password);
            this.updated = updated;
        }

        public Tutor Old
        {
            get { return old; }
            private set { }
        }

        public Tutor Updated
        {
            get { return updated; }
            private set { }
        }
    }
}
