using GVM.Src.Components.BaseComponents;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVM.Src.Components
{
    public enum RegistersName
    {
        //Instruction Pointer
        IP,
        //Accumulators
        AX,
        BX,
        //IO
        CX,
        //STORAGE
        DX,
        //BC(blockchain)
        EX,
        //NETWORK
        FX,
        //Base Pointer
        BP,
        //Stack Pointer
        SP
    }
    public class Registers
    {
        private readonly Register[] registers;
        public Registers()
        {
            registers = new Register[Enum.GetNames(typeof(RegistersName)).Length];
        }
        public Register this[RegistersName index]
        {
            get => registers[(int)index];
            set => registers[(int)index] = value;
        }
    }
}
