using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poehoe
{
    class InitDataRequest
    {
        public static Request Create()
        {
            Request Req = new Request();
            Req.Payload = String.Format(
                      @"<?xml version=""1.0"" ?><GetInitData xmlns=""http://tempuri.org/""><browserStats>{0}</browserStats></GetInitData>", GetStats());
            Req.Service = "Data";
            Req.SoapAction = "http://tempuri.org/IDataService/GetInitData";
            return Req;
        }

        public static string GetStats()
        {

            Random random = new Random();
            StringBuilder s = new StringBuilder("stats:Microsoft Windows NT ");
            s.Append(random.Next(4, 7));
            s.Append(".");
            s.Append(random.Next(0, 2));
            s.Append(".");
            s.Append(random.Next(0, 8000));
            s.Append(" Service Pack ");
            s.Append(random.Next(0, 4));
            s.Append("|Chrome ");
            s.Append(random.Next(1, 30));
            s.Append(".");
            s.Append(random.Next(0, 3));
            s.Append(".");
            s.Append(random.Next(0, 2000));
            s.Append(".");
            s.Append(random.Next(0, 200));
            s.Append("|");
            s.Append(random.Next(0, 1921));
            s.Append("x");
            s.Append(random.Next(0, 1081));
            s.Append("|0");
            return s.ToString();
        }
    }
}
