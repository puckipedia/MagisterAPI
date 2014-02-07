using Poehoe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Welp
{
    class Program
    {
        static void Main(string[] args)
        {
            var Sch = new School("chrlyceumdelft");
            Console.WriteLine(Sch.SchoolVersion);
            Console.ReadLine();
        }
    }
}
