using LabManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabManager.Database.DAL
{
    public class TutoringSessionUpdateDTO
    {
        private TutoringSession old;
        private TutoringSession updated;

        public TutoringSessionUpdateDTO(TutoringSession old, TutoringSession updated)
        {
            this.old = old;
            this.updated = updated;
        }

        public TutoringSession Old
        {
            get { return old; }
            private set { }
        }

        public TutoringSession Updated
        {
            get { return updated; }
            private set { }
        }
    }
}
