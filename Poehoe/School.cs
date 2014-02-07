using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Poehoe
{
    /// <summary>
    /// A school
    /// </summary>
    public class School
    {
        /// <summary>
        /// The name of the school
        /// </summary>
        public string SchoolNaam
        {
            get;
            private set;
        }

        /// <summary>
        /// The version of the school
        /// </summary>
        public string SchoolVersion
        {
            get
            {
                _getSchoolVersionTask.Wait();
                return _getSchoolVersionTask.Result;
            }
        }

        private Task<string> _getSchoolVersionTask;

        /// <summary>
        /// Create a school object
        /// </summary>
        /// <param name="SchoolNaam"></param>
        public School(string SchoolNaam)
        {
            this.SchoolNaam = SchoolNaam;
            _getSchoolVersionTask = GetSchoolVersion();
        }

        /// <summary>
        /// Get a school version
        /// </summary>
        /// <returns>The version the school runs</returns>
        private async Task<string> GetSchoolVersion()
        {
            HttpWebRequest Request = WebRequest.Create(String.Format("https://{0}.swp.nl", SchoolNaam)) as HttpWebRequest;
            var Response = await Request.GetResponseAsync();
            return Response.ResponseUri.AbsolutePath.Split('/')[1];
        }
    }
}
