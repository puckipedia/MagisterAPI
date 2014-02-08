using Poehoe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Welp
{
    class Program
    {
        static void Main(string[] args)
        {
            Poehoe.Crypto.ZipCrypto = new PoehoeZipWin32.ZipCrypto();
            Poehoe.Crypto.AesKey = "087EC4624E964AE27DBDFE03279A2EE4";
            Poehoe.Crypto.ZipKey = "yawUBRu+reduka5UPha2#=cRUc@ThekawEvuju&?g$dru9ped=a@REQ!7h_?anut";
            Poehoe.Crypto.AesCrypto = new AesCrypto();
            var Sch = new School("chrlyceumdelft");
            Console.WriteLine("Poehoe connecting to {0} version {1}", Sch.SchoolNaam, Sch.SchoolVersion);
            var T = Sch.Login("118556", "$PASS");
            T.Wait();
            var Req = AgendaRequest.Create(new DateTime(2014, 02, 2), new DateTime(2014, 02, 7), T.Result, "118556").Send(T.Result);
            Req.Wait();
            var Am = AgendaMapper.GetData(Req.Result, T.Result);

            Console.ReadLine();
        }
    }
}
