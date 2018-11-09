using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using static System.Console;

namespace HTests
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch sp = new Stopwatch();
            sp.Start();
            for (int i = 0; i < 2000_000; i++)
            {
                BigInteger b = new BigInteger(1);
                for (int j = 0; j < 32; j++)
                {
                    b = b << 1;
                }
            }
            sp.Stop();
            Console.WriteLine(sp.ElapsedMilliseconds);
            Console.ReadKey();
        }

    }
}
