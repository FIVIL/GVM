using GDIC;
using GVM.Src.Components.BaseComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVM.Src.Components
{
    public class InstructionSet
    {
        private readonly Register[] data;
        private readonly int size;
        private readonly ServiceCollection services;
        private int ip { get => services.GetService<Registers>()[RegistersName.IP]; }
        private void Plus() => services.GetService<Registers>()[RegistersName.IP]++;
        public InstructionSet(int iSize, ServiceCollection service)
        {
            data = new Register[iSize];
            services = service;
        }
        public bool Fetch(out Instruction ins)
        {
            if (ip >= Utilities.Statics.instructionSize)
            {
                ins = new Instruction();
                return false;
            }
            if (ip < 0) throw new ArgumentException();
            ins = data[ip];
            Plus();
            return true;
        }
    }
}
