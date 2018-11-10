using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace GVM.Src.Components.BaseComponents
{
    public enum InstructionParts
    {
        //first two byte(0-1)
        OpCode,
        //third byte(2)
        OpCount,
        //5 byte as it can be address or register (3-7),or 3 to 31 for one operands
        FirstOp,
        //the rest as it can be immidate (8-31)
        SeccondOp
    }
    public struct Instruction
    {
        private Register data;
        public static implicit operator Instruction(Register r) => new Instruction() { data = r };
        public static implicit operator Register(Instruction i) => i.data;
        //public Register this[InstructionParts index]
        //{
        //    get
        //    {
        //        switch (index)
        //        {
        //            case InstructionParts.OpCode:
        //                Register r = ((byte[])data).Take(2).ToArray();
        //                return r;
        //            case InstructionParts.OpCount:
        //                r = ((byte[])data).Skip(2).Take(1).ToArray();
        //                return r;
        //            case InstructionParts.FirstOp:
        //                r = ((byte[])data).Skip(3).Take(5).ToArray();
        //                return r;
        //            case InstructionParts.SeccondOp:
        //                r = ((byte[])data).Skip(8).ToArray();
        //                return r;
        //            default:
        //                return null;
        //        }
        //    }
        //}
        public OpCode OpCode
        {
            get => new OpCode(BitConverter.ToInt32(new byte[] { ((byte[])data)[0], ((byte[])data)[1] }, 0));
        }
        public int OpCount
        {
            get => BitConverter.ToInt32(new byte[] { ((byte[])data)[2] }, 0);
        }
        public Operand FirstOperand
        {
            get => OpCount > 1
                ? new Operand(((byte[])data).Skip(3).Take(8).ToArray())
                : new Operand(((byte[])data).Skip(3).ToArray());
        }
        public Operand SeccondOperand
        {
            get => OpCount > 1
                ? new Operand(((byte[])data).Skip(8).ToArray())
                : throw new ArgumentNullException();
        }
    }
}
