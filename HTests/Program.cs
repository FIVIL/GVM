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

            get(2);
            get(new { a = 3 });
            get(new List<int>());
            get("hamed");
            get('a');
            get(new Program());
            Console.ReadKey();
        }
        static void get(object o)
        {
            Console.WriteLine(o.GetType());
        }
    }
}
