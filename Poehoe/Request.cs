using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Poehoe
{
    public class Request
    {
        public string Payload
        {
            get;
            set;
        }

        public string SoapAction
        {
            get;
            set;
        }

        public string Service
        {
            get;
            set;
        }

        public async Task<string> Send(User U)
        {
            await U.SetBaseAddress();
                ByteArrayContent C = new ByteArrayContent(Crypto.EncryptRequest(this));
                C.Headers.Add("soapaction", "\"" + SoapAction + "\"");
                C.Headers.Add("content-type", "text/xml; charset=utf-8");
                var Req = await U._client.PostAsync(String.Format("{0}Service.svc", this.Service), C);
                var ba = await Req.Content.ReadAsByteArrayAsync();
                return Crypto.DecryptResponse(ba);
        }
    }
}
