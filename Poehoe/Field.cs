using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poehoe
{
    public class Field
    {
        public string Name
        {
            get;
            private set;
        }
        private byte p2;
        private int p3;
        private string p4;
        private int p5;
        private bool p6;
        private bool p7;
        private string p8;

        public Field(string p1, byte p2, int p3, string p4, int p5, bool p6, bool p7, string p8)
        {
            // TODO: Complete member initialization
            Name = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.p4 = p4;
            this.p5 = p5;
            this.p6 = p6;
            this.p7 = p7;
            this.p8 = p8;
        }
    }
}
