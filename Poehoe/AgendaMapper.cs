using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Poehoe
{
    public class AgendaItem
    {
        public DateTime Start
        {
            get;
            internal set;
        }

        public DateTime End
        {
            get;
            internal set;
        }

        public string Beschrijving
        {
            get;
            internal set;
        }

        public string Lokatie
        {
            get;
            internal set;
        }

        public string Actie
        {
            get;
            internal set;
        }

        public string Bericht
        { get; internal set; }

        public string Lesuur
        {
            get;
            internal set;
        }

        public Dictionary<string, object> RawData
        {
            get;
            internal set;
        }
    }

    public class AgendaMapper
    {
        public List<AgendaItem> AgendaItems
        {
            get;
            private set;
        }

        public Dictionary<DateTime, List<AgendaItem>> DataItems
        {
            get;
            private set;
        }

        public static AgendaMapper GetData(string Data, User User)
        {
            XDocument d = XDocument.Parse(Data);
            var LolData = Convert.FromBase64String(d.Descendants(User.tempuri + "VulAgendaCDSResult").First().Value);
            BinairFormaat For = new BinairFormaat(LolData);
            return new AgendaMapper(For, User);
            
        }

        public AgendaMapper(BinairFormaat Formaat, User User)
        {
            Dictionary<int, string> TypeMappings = new Dictionary<int, string>();
            TypeMappings[0] = "Niks";
            foreach (var Item in User.InitData.Descendants(User.d2p1 + "AgendaLessoort"))
            {
                var Lessoort = int.Parse(Item.Descendants(User.d2p1 + "IdAgendaLessoort").First().Value);
                var Omschrijving = Item.Descendants(User.d2p1 + "Omschrijving").First().Value;

                TypeMappings[Lessoort] = Omschrijving;
            }

            Dictionary<string, Tuple<int, string>> Mappings = new Dictionary<string, Tuple<int, string>>(Formaat.Fields.Count);
            for (var i = 0; i < Formaat.Fields.Count; i++)
            {
                Mappings[Formaat.Fields[i].Name] = new Tuple<int, string>(i, Formaat.Fields[i].Description);
            }

            AgendaItems = new List<AgendaItem>();
            foreach (var AgIt in Formaat.Rows)
            {
                int lesuurvan = (int) AgIt.Objects[Mappings["lesuurvan"].Item1];
                int lesuurtm = (int)AgIt.Objects[Mappings["lesuurtm"].Item1];
                for (int i = lesuurvan; i <= lesuurtm; i++)
                {
                    AgendaItem Item = new AgendaItem();
                    Item.Start = (DateTime)AgIt.Objects[Mappings["dStart"].Item1];
                    Item.End = (DateTime)AgIt.Objects[Mappings["dFinish"].Item1];
                    Item.Lokatie = AgIt.Objects[Mappings["Lokatie"].Item1] as string;
                    Item.Beschrijving = AgIt.Objects[Mappings["Omschrijving"].Item1] as string;
                    Item.Actie = TypeMappings[(int)AgIt.Objects[Mappings["idAgendalessoort"].Item1]];
                    Item.Bericht = AgIt.Objects[Mappings["Bericht"].Item1] as string;
                    Item.Lesuur = i+"";
                    if(i == 0)
                        Item.Lesuur = "!";
                    Item.RawData = new Dictionary<string,object>(Mappings.Count);
                    foreach (var D in Mappings)
                    {
                        Item.RawData[D.Value.Item2] = AgIt.Objects[D.Value.Item1];
                    }
                    AgendaItems.Add(Item);
                }
            }
            DataItems = new Dictionary<DateTime, List<AgendaItem>>();
            foreach (var item in AgendaItems)
            {
                if (!DataItems.ContainsKey(item.Start.Date))
                    DataItems[item.Start.Date] = new List<AgendaItem>();
                DataItems[item.Start.Date].Add(item);
            }
        }
    }
}
