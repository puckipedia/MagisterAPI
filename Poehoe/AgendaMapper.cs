using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AgendaMapper(BinairFormaat Formaat)
        {
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
                    Item.Lesuur = i;
                    AgendaItems.Add(Item);
                }
            }
        }
    }
}
