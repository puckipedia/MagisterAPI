using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poehoe
{
    public class GetLeerlingenDataRequest
    {
        public static Request Create(User User, IEnumerable<int> Ids)
        {
            string SThing = "";

            foreach (var i in Ids)
            {
                SThing += "<d2p1:int>" + i + "</d2p1:int>";
            }
            Request Req = new Request();
            Req.Payload = String.Format("<?xml version=\"1.0\" encoding=\"utf-16\"?><GetLeerlingenData xmlns=\"http://tempuri.org/\">"+
                "<clientID>{0}</clientID>"+
                "<ids xmlns:d2p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\" "+
            "xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">{1}</ids>"+
            "<getData>true</getData><getDescription>true</getDescription><getPicture>true</getPicture><getOndSoort>true</getOndSoort></GetLeerlingenData>",
            User.InitData.Descendants(User.d2p1 + "ClientID").First().Value, SThing);

            Req.Service = "Data";
            Req.SoapAction = "http://tempuri.org/IDataService/GetLeerlingenData";

            return Req;
        }
    }
}
