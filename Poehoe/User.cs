using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Poehoe
{
    /// <summary>
    /// A logged in Magister User
    /// </summary>
    public class User
    {
        /// <summary>
        /// The school of the user
        /// </summary>
        public School School
        {
            get;
            private set;
        }

        /// <summary>
        /// The children (leerlingen) the user can manage
        /// </summary>
        public List<Child> Children
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructor. Don't use!
        /// </summary>
        internal User(School S)
        {
            School = S;
            _client.BaseAddress = new Uri(String.Format("https://{0}.swp.nl/{1}/WCFServices/", School.SchoolNaam, School.SchoolVersion));
        }

        internal HttpClient _client = new HttpClient();

        public XDocument InitData
        {
            get;
            private set;
        }

        public static XNamespace d2p1
        {
            get
            {
                return "http://schemas.datacontract.org/2004/07/MagisterWeb.Services.Data";
            }
        }

        public static XNamespace tempuri
        {
            get
            {
                return "http://tempuri.org/";
            }
        }

        internal async Task GetInfo(string Username, string Password)
        {
            await LoginRequest.Create(Username, Password).Send(this);
            InitData = XDocument.Parse(await InitDataRequest.Create().Send(this));
        }
    }
}
