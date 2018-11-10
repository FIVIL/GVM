using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GVM.Src.Components.BaseComponents
{
    public enum OpType
    {
        Immediate,
        MemAddress,
        Register
    }
    ///// <summary>
    ///// Only For Demonstration Perpous, no actual use
    ///// </summary>
    //public enum OpParts
    //{
    //    //Immediate,MemoryAddress,register
    //    Type,
    //    Data
    //}

    /// <summary>
    /// Operands
    /// </summary>
    public class Operand
    {
        public Register Data { get; private set; }
        public OpType Type { get; private set; }
        public Operand(byte[] bytes)
        {
            Type = (OpType)bytes[0];
            Data = bytes.Skip(1).ToArray();
        }
    }
}
