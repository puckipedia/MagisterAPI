using System;
using System.Linq;

namespace Poehoe
{
    public class GetDDScreenLL
    {
        public static Request Create(User User)
        {
            Request Req = new Request();
            Req.Payload = String.Format(@"<?xml version=""1.0"" encoding=""utf-16""?><GetDDScreenLL xmlns=""http://tempuri.org/""><clientID>{0}</clientID></GetDDScreenLL>", User.InitData.Descendants(User.d2p1 + "ClientID").First().Value);
            Req.SoapAction = "http://tempuri.org/IMediusService/GetDDScreenLL";
            Req.Service = "Medius";
            return Req;
        }
    }
}
