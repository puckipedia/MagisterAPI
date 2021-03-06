﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        private Task<string> _getSchoolVersionTask;

        public async Task<string> SchoolVersion()
        {
            return await _getSchoolVersionTask;
        }

        /// <summary>
        /// Create a school object
        /// </summary>
        /// <param name="SchoolNaam"></param>
        public School(string SchoolNaam)
        {
            this.SchoolNaam = SchoolNaam;
            _getSchoolVersionTask = GetSchoolVersion();
        }

        public async Task<bool> LoadSchoolVersion()
        {
            var version = await GetSchoolVersion();
            return version != null;
        }

        /// <summary>
        /// Get a school version
        /// </summary>
        /// <returns>The version the school runs</returns>
        private async Task<string> GetSchoolVersion()
        {
            HttpWebRequest Request = WebRequest.Create(String.Format("http://{0}.swp.nl/", SchoolNaam)) as HttpWebRequest;
            Request.Method = "GET";
            var Response = await Request.GetResponseAsync();
            return Response.ResponseUri.AbsolutePath.Split('/')[1];
        }

        /// <summary>
        /// The Client GUID used for authenticating
        /// </summary>
        public Guid ClientGuid
        {
            get;
            private set;
        }

        /// <summary>
        /// Login to Magister
        /// </summary>
        /// <param name="Username">The username to login with</param>
        /// <param name="Password">The password to login with</param>
        /// <returns>The user that logged in</returns>
        public async Task<User> Login(string Username, string Password)
        {
            User U = new User(this);
            await U.GetInfo(Username, Password);
            return U;
        }
    }
}
