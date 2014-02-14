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
    public class LeerlingData
    {
        public int idleer
        {
            get;
            private set;
        }
        public int stamnr
        {
            get;
            private set;
        }
        public string achternaam
        {
            get;
            private set;
        }
        public string roepnaam
        {
            get;
            private set;
        }
        public object naam_vol
        {
            get;
            private set;
        }
        public string tussenvoeg
        {
            get;
            private set;
        }
        public string woonplaats
        {
            get;
            private set;
        }
        public string straat
        {
            get;
            private set;
        }
        public string huisnr
        {
            get;
            private set;
        }
        public object huisnr_tv
        {
            get;
            private set;
        }
        public string postcode
        {
            get;
            private set;
        }
        public object medisch
        {
            get;
            private set;
        }
        public object memo
        {
            get;
            private set;
        }
        public DateTime geb_datum
        {
            get;
            private set;
        }
        public int idGebr
        {
            get;
            private set;
        }

        public int idAanm
        {
            get;
            private set;
        }
        public int idStud
        {
            get;
            private set;
        }
        public object C_PROFIEL
        {
            get;
            private set;
        }
        public object C_PROFIEL2
        {
            get;
            private set;
        }
        public int idBgrp
        {
            get;
            private set;
        }
        public string STUDIE
        {
            get;
            private set;
        }
        public DateTime dBegin
        {
            get;
            private set;
        }
        public DateTime dEinde
        {
            get;
            private set;
        }
        public int studieslu
        {
            get;
            private set;
        }
        public string groep
        {
            get;
            private set;
        }

        public static LeerlingData Get(BinairFormaat Formaat, Row Row)
        {
            LeerlingData D = new LeerlingData();
            for (var i = 0; i < Row.Objects.Count; i++)
            {
                string Param = Formaat.Fields[i].Name;
                switch (Param)
                {
                    case "idleer":
                        D.idleer = (int)Row.Objects[i];
                        break;
                    case "stamnr":
                        D.stamnr = (int)Row.Objects[i];
                        break;
                    case "achternaam":
                        D.achternaam = (string)Row.Objects[i];
                        break;
                    case "roepnaam":
                        D.roepnaam = (string)Row.Objects[i];
                        break;
                    case "naam_vol":
                        D.naam_vol = (object)Row.Objects[i];
                        break;
                    case "tussenvoeg":
                        D.tussenvoeg = (string)Row.Objects[i];
                        break;
                    case "woonplaats":
                        D.woonplaats = (string)Row.Objects[i];
                        break;
                    case "straat":
                        D.straat = (string)Row.Objects[i];
                        break;
                    case "huisnr":
                        D.huisnr = (string)Row.Objects[i];
                        break;
                    case "huisnr_tv":
                        D.huisnr_tv = (object)Row.Objects[i];
                        break;
                    case "postcode":
                        D.postcode = (string)Row.Objects[i];
                        break;
                    case "medisch":
                        D.medisch = (object)Row.Objects[i];
                        break;
                    case "memo":
                        D.memo = (object)Row.Objects[i];
                        break;
                    case "geb_datum":
                        D.geb_datum = (DateTime)Row.Objects[i];
                        break;
                    case "idGebr":
                        D.idGebr = (int)Row.Objects[i];
                        break;
                    case "idAanm":
                        D.idAanm = (int)Row.Objects[i];
                        break;
                    case "idStud":
                        D.idStud = (int)Row.Objects[i];
                        break;
                    case "C_PROFIEL":
                        D.C_PROFIEL = (object)Row.Objects[i];
                        break;
                    case "C_PROFIEL2":
                        D.C_PROFIEL2 = (object)Row.Objects[i];
                        break;
                    case "idBgrp":
                        D.idBgrp = (int)Row.Objects[i];
                        break;
                    case "STUDIE":
                        D.STUDIE = (string)Row.Objects[i];
                        break;
                    case "dBegin":
                        D.dBegin = (DateTime)Row.Objects[i];
                        break;
                    case "dEinde":
                        D.dEinde = (DateTime)Row.Objects[i];
                        break;
                    case "studieslu":
                        D.studieslu = (int)Row.Objects[i];
                        break;
                    case "groep":
                        D.groep = (string)Row.Objects[i];
                        break;
                }
            }
            return D;
        }
    }

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

        private LeerlingData _data = null;

        public async Task<LeerlingData> GetLeerlingData()
        {
            if (_data != null)
                return _data;

            var Data = await GetLeerlingenDataRequest.Create(this, new int[] { int.Parse(InitData.Descendants(User.d2p1 + "MijnTabelID").First().Value) }).Send(this);
            XDocument D = XDocument.Parse(Data);
            var BinFor = new BinairFormaat(Convert.FromBase64String(D.Descendants(User.tempuri + "GetLeerlingenDataResult").First().Value));
            _data = LeerlingData.Get(BinFor, BinFor.Rows[0]);
            return _data;
        }

        public string Username
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
            SetBaseAddress();
        }

        internal async Task SetBaseAddress()
        {
            if (_client.BaseAddress == null)
                _client.BaseAddress = new Uri(String.Format("https://{0}.swp.nl/{1}/WCFServices/", School.SchoolNaam, await School.SchoolVersion()));
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
            this.Username = Username;
            await LoginRequest.Create(Username, Password).Send(this);
            InitData = XDocument.Parse(await InitDataRequest.Create().Send(this));
        }
    }
}
