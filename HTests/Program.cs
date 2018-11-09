using GDIC;
using GVM.Src.Components;
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
            var k = new ServiceCollection();
            var f = new Registers();
            f[RegistersName.SP] = 0;
            k.AddSingelton(f);
            var s = new Stack(GVM.Src.Utilities.Statics.stackSize, k);
            for (int i = 0; i < GVM.Src.Utilities.Statics.stackSize; i++)
            {
                Console.WriteLine(k.GetService<Registers>()[RegistersName.SP]);
                s.Push(1);
            }
            Console.WriteLine("a");
            Console.ReadKey();
        }

    }
}
