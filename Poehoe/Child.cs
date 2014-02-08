using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poehoe
{
    /// <summary>
    /// A child (leerling)
    /// </summary>
    public class Child
    {
        /// <summary>
        /// The user the child belongs to
        /// </summary>
        public User User
        {
            get;
            private set;
        }

        /// <summary>
        /// TableID. Don't ask me what it does, I don't know!
        /// </summary>
        public string TableID
        {
            get;
            private set;
        }

        /// <summary>
        /// Stamnummer, used for getting the calendar
        /// </summary>
        public string StamNummer
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor. Don't use!
        /// </summary>
        internal Child()
        {

        }
    }
}
