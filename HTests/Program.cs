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
            var flags = new Dictionary<int, bool>(4);
            foreach (var item in flags)
            {
                Console.WriteLine(item.Key);
            }
            Console.WriteLine(flags.Count);
            Console.ReadKey();
        }
    }
}
