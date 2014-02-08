using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Poehoe
{
    public class AgendaRequest
    {
        public static Request Create(DateTime Start, DateTime End, User User, string Stamnummer)
        {
            Request Req = new Request();
            Req.Payload = String.Format(
                    @"<?xml version=""1.0"" encoding=""utf-16""?><VulAgendaCDS xmlns=""http://tempuri.org/""><clientID>{0}</clientID><isPersAgenda>false</isPersAgenda><pers></pers><stamnr>{1}</stamnr><gebr></gebr><dStart>{2:o}</dStart><dFinish>{3:o}</dFinish><bPersoonlijk>true</bPersoonlijk><bSchool>true</bSchool><bRooster>true</bRooster><bVerborgen>false</bVerborgen><bTaken>true</bTaken><useAgenda20>false</useAgenda20></VulAgendaCDS>",
                    User.InitData.Descendants(User.d2p1 + "ClientID").First().Value, Stamnummer, Start, End);
            Req.Service = "Agenda";
            XDocument D;
            Req.SoapAction = "http://tempuri.org/IAgendaService/VulAgendaCDS";
            return Req;
        }
    }
}
