using GDIC;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVM.Src.Components
{
    public class Stack
    {
        private readonly Register[] data;
        private readonly int size;
        private readonly ServiceCollection services;
        private int sp { get => services.GetService<Registers>()[RegistersName.SP]; }
        private void Plus() => services.GetService<Registers>()[RegistersName.SP]++;
        private void Minus() => services.GetService<Registers>()[RegistersName.SP]--;
        public Stack(int size, ServiceCollection service)
        {
            this.size = size;
            data = new Register[size];
            services = service;
        }
        public Register Peek() => data[sp];
        public bool Push(Register r)
        {
            Plus();
            if (sp >= Utilities.Statics.stackSize) return false;
            data[sp] = r;
            return true;
        }
        public bool Pop(out Register r)
        {
            if (sp == 0)
            {
                r = null;
                return false;
            }
            r = data[sp];
            Minus();
            return true;
        }
    }
}
