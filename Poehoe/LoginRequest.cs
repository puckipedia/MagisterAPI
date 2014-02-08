using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poehoe
{
    public class LoginRequest
    {
        public static Request Create(string Username, string Password) {
            Request Req = new Request();
            Req.SoapAction = "http://tempuri.org/ILoginService/Login";
            Req.Service = "Login";
            var ConvertedUsername = Crypto.CryptPass(Username);
            var ConvertedPassword = Crypto.CryptPass(Password);

            Req.Payload
                = String.Format(
                    "<?xml version=\"1.0\" encoding=\"utf-16\"?><Login xmlns=\"http://tempuri.org/\"><userName>{0}</userName><password>{1}</password><language>NL</language><debugMode>false</debugMode></Login>",
                               ConvertedUsername,
                               ConvertedPassword
                );

            return Req;
        }
    }
}
