using System;
using System.Collections.Generic;
using System.Text;

namespace GVM.Src.Components
{
    public enum FlagsNames
    {
        Sign,
        Zero,
        Carry,
        Overflow,
        Accumulator
    }
    public class Flags
    {
        public struct Flag
        {
            private bool value;
            public void Set()
            {
                value = true;
            }
            public void Clear()
            {
                value = false;
            }
            public static implicit operator Flag(bool b) => new Flag() { value = b };
            public static implicit operator bool(Flag f) => f.value;
        }

        private Flag[] flags;

        public Flags()
        {
            flags = new Flag[Enum.GetNames(typeof(FlagsNames)).Length];
        }

        public Flag this[FlagsNames index]
        {
            get => flags[(int)index];
            set => flags[(int)index] = value;
        }
    }
}
