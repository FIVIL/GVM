using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace GVM.Src.Components.BaseComponents
{
    public struct Register
    {
        byte[] value;
        public static implicit operator byte[] (Register r) => r.value;
        public static implicit operator Register(byte[] r)
            => r.Length != Utilities.Statics.bufferSize
            ? throw new ArgumentOutOfRangeException()
            : new Register() { value = r };

        public static implicit operator int(Register r)
            => BitConverter.ToInt32(new byte[]
                {r.value[0],r.value[1]
                ,r.value[2],r.value[3]}, 0);
        public static implicit operator Register(int r)
        {
            var s = new byte[Utilities.Statics.bufferSize];
            var p = BitConverter.GetBytes(r);
            for (int i = 0; i < p.Length; i++)
            {
                s[i] = p[i];
            }
            if (r < 0)
            {
                for (int i = p.Length; i < s.Length; i++)
                {
                    s[i] = 0xff;
                }
            }
            return new Register()
            {
                value = s
            };

        }

        public static implicit operator long(Register r)
            => BitConverter.ToInt32(new byte[]
                {r.value[0],r.value[1],r.value[2],r.value[3]
                ,r.value[4],r.value[5],r.value[6],r.value[7]}, 0);

        public static implicit operator Register(long r)
        {
            var s = new byte[Utilities.Statics.bufferSize];
            var p = BitConverter.GetBytes(r);
            for (int i = 0; i < p.Length; i++)
            {
                s[i] = p[i];
            }
            if (r < 0)
            {
                for (int i = p.Length; i < s.Length; i++)
                {
                    s[i] = 0xff;
                }
            }
            return new Register()
            {
                value = s
            };

        }
    }
}
