using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Math.PI);
            Console.WriteLine(PIn2.Calculate(100));
            Console.WriteLine(BigMath.GetPi(100, 1000));
            Console.ReadKey();
        }
    }
}
