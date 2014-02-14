using System;
using System.Linq;

namespace Poehoe
{
    public class DDFieldsRequest
    {
        public static Request Create(User User)
        {
            Request Req = new Request();
            Req.Payload = String.Format(@"<?xml version=""1.0"" encoding=""utf-16""?><GetDDFields xmlns=""http://tempuri.org/""><clientID>{0}</clientID></GetDDFields>", User.InitData.Descendants(User.d2p1 + "ClientID").First().Value);
            Req.SoapAction = "http://tempuri.org/IMediusService/GetDDFields";
            Req.Service = "Medius";
            return Req;
        }
    }
}
