using GDIC;
using GVM.Src.Components.BaseComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace GVM.Src.Components
{
    public class Memory
    {
        private readonly Register[] mem;
        private readonly ServiceCollection services;
        public Memory(int memSize, ServiceCollection service)
        {
            services = service;
            mem = new Register[memSize];
        }
        public Register this[int index]
        {
            get => mem[index];
            set => mem[index] = value;
        }
    }
}
