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

        public int Lesuur
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

            Dictionary<string, int> Mappings = new Dictionary<string, int>(Formaat.Fields.Count);
            for (var i = 0; i < Formaat.Fields.Count; i++)
            {
                Mappings[Formaat.Fields[i].Name] = i;
            }

            AgendaItems = new List<AgendaItem>();
            foreach (var AgIt in Formaat.Rows)
            {
                int lesuurvan = (int) AgIt.Objects[Mappings["lesuurvan"]];
                int lesuurtm = (int) AgIt.Objects[Mappings["lesuurtm"]];
                for (int i = lesuurvan; i <= lesuurtm; i++)
                {
                    AgendaItem Item = new AgendaItem();
                    Item.Start = (DateTime)AgIt.Objects[Mappings["dStart"]];
                    Item.End = (DateTime)AgIt.Objects[Mappings["dFinish"]];
                    Item.Lokatie = AgIt.Objects[Mappings["Lokatie"]] as string;
                    Item.Beschrijving = AgIt.Objects[Mappings["Omschrijving"]] as string;
                    Item.Actie = TypeMappings[(int)AgIt.Objects[Mappings["idAgendalessoort"]]];
                    Item.Bericht = AgIt.Objects[Mappings["Bericht"]] as string;
                    Item.Lesuur = i;
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
