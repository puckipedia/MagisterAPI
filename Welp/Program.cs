using Poehoe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var Sch = new School("praedinius");
            var T = Sch.Login("3283", "boerboerboer");
            T.Wait();
            var DDFR = GetLeerlingenDataRequest.Create(T.Result, new int[] {int.Parse(T.Result.InitData.Descendants(User.d2p1+"MijnTabelID").First().Value)}).Send(T.Result);
            DDFR.Wait();
            XDocument D = XDocument.Parse(DDFR.Result);
            var Data = new BinairFormaat(Convert.FromBase64String(D.Descendants(User.tempuri + "GetLeerlingenDataResult").First().Value));
            foreach(var F in Data.Fields) {
                Debug.WriteLine("public object {0} {1}\n\tget;\n\tprivate set;\n{2}", F.Name, "{", "}");

            }
             Console.ReadLine();
        }
    }
}
