using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GVM.Src.Types
{
    /// <summary>
    /// This Structure represents a 256 singned integral value data type which consist of
    /// 32 byte as data holder.
    /// it also implements most of the interfaces which .Net datatype normally implements 
    /// and have the same functions
    /// </summary>
    public struct Int256 : IComparable, IComparable<Int256>, IEquatable<Int256>
    {
        internal byte[] data;

        #region Constenants
        private static readonly byte[] maxByte =
        {
                 255, 255,255,255
                ,255, 255,255,255
                ,255, 255,255,255
                ,255, 255,255,255
                ,255, 255,255,255
                ,255, 255,255,255
                ,255, 255,255,255
                ,255, 255,255,127
        };

        private static readonly byte[] minByte =
        {
                 0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,128
        };

        private static readonly byte[] zeroByte =
        {
                 0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
        };

        private static readonly byte[] oneByte =
        {
                 1, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
                ,0, 0,0,0
        };
        public static Int256 MaxValue { get => new Int256 { data = maxByte }; }
        public static Int256 MinValue { get => new Int256 { data = minByte }; }
        public static Int256 Zero { get => new Int256 { data = zeroByte }; }
        public static Int256 One { get => new Int256 { data = oneByte }; }
        #endregion

        #region Construct

        #region bool
        private Int256(bool b)
        {
            data = new byte[32];
            data[0] = b ? (byte)1 : (byte)0;
        }
        public static implicit operator Int256(bool b) => new Int256(b);
        #endregion

        #region byte
        private Int256(byte b)
        {
            data = new byte[32];
            data[0] = b;
        }
        public static implicit operator Int256(byte b) => new Int256(b);
        #endregion

        #region Int16
        private Int256(Int16 b)
        {
            data = new byte[32];
            var d = BitConverter.GetBytes(b);
            for (int i = 0; i < d.Length; i++)
            {
                data[i] = d[i];
            }
            if (b < 0)
            {
                for (int i = d.Length; i < data.Length; i++)
                {
                    data[i] = 255;
                }
            }
        }
        public static implicit operator Int256(Int16 b) => new Int256(b);
        #endregion

        #region Int32
        private Int256(Int32 b)
        {
            data = new byte[32];
            var d = BitConverter.GetBytes(b);
            for (int i = 0; i < d.Length; i++)
            {
                data[i] = d[i];
            }
            if (b < 0)
            {
                for (int i = d.Length; i < data.Length; i++)
                {
                    data[i] = 255;
                }
            }
        }
        public static implicit operator Int256(Int32 b) => new Int256(b);
        #endregion

        #region Int64
        private Int256(Int64 b)
        {
            data = new byte[32];
            var d = BitConverter.GetBytes(b);
            for (int i = 0; i < d.Length; i++)
            {
                data[i] = d[i];
            }
            if (b < 0)
            {
                for (int i = d.Length; i < data.Length; i++)
                {
                    data[i] = 255;
                }
            }
        }
        public static implicit operator Int256(Int64 b) => new Int256(b);
        #endregion

        #region byte[]
        private Int256(byte[] b)
        {
            data = new byte[32];
            if (b == null) throw new ArgumentNullException();
            if (b.Length <= 32)
            {
                for (int i = 0; i < b.Length; i++)
                {
                    data[i] = b[i];
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

        }
        public static implicit operator Int256(byte[] b) => new Int256(b);
        #endregion

        #region UInt16
        private Int256(UInt16 b)
        {
            data = new byte[32];
            var d = BitConverter.GetBytes(b);
            for (int i = 0; i < d.Length; i++)
            {
                data[i] = d[i];
            }
        }
        public static implicit operator Int256(UInt16 b) => new Int256(b);
        #endregion

        #region UInt32
        private Int256(UInt32 b)
        {
            data = new byte[32];
            var d = BitConverter.GetBytes(b);
            for (int i = 0; i < d.Length; i++)
            {
                data[i] = d[i];
            }
        }
        public static implicit operator Int256(UInt32 b) => new Int256(b);
        #endregion

        #region UInt64
        private Int256(UInt64 b)
        {
            data = new byte[32];
            var d = BitConverter.GetBytes(b);
            for (int i = 0; i < d.Length; i++)
            {
                data[i] = d[i];
            }
        }
        public static implicit operator Int256(UInt64 b) => new Int256(b);
        #endregion

        #region BitInteger
        public static implicit operator Int256(BigInteger b) => new Int256(b.ToByteArray());
        #endregion

        #region double
        public static implicit operator Int256(double b) =>
            new Int256(new BigInteger(b).ToByteArray());

        public static bool operator ==(Int256 int1, Int256 int2)
        {
            return int1.Equals(int2);
        }

        public static bool operator !=(Int256 int1, Int256 int2)
        {
            return !(int1 == int2);
        }
        #endregion

        #endregion

        #region Statics
        public static Int256 Pars(string s)
        {
            var c = BigInteger.Parse(s);
            if (c.ToByteArray().Length > 32) throw new ArgumentOutOfRangeException();
            return new BigInteger(c.ToByteArray());
        }
        #endregion

        #region NonStatics

        public byte[] ToByteArray() => data;

        #endregion

        #region Interfaces
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            if (obj is Int256)
            {
                // Need to use compare because subtraction will wrap
                // to positive for very large neg numbers, etc.
                Int256 cp = (Int256)obj;
                if (data[31] > 127 && cp.data[31] <= 127) return -1;
                if (data[31] <= 127 && cp.data[31] > 127) return 1;
                for (int i = 31; i > -1; i--)
                {
                    if (data[i] < cp.data[i]) return -1;
                    if (data[i] > cp.data[i]) return 1;
                }
                return 0;
            }
            throw new ArgumentException();
        }

        public int CompareTo(Int256 cp)
        {
            if (data[31] > 127 && cp.data[31] <= 127) return -1;
            if (data[31] <= 127 && cp.data[31] > 127) return 1;
            for (int i = 31; i > -1; i--)
            {
                if (data[i] < cp.data[i]) return -1;
                if (data[i] > cp.data[i]) return 1;
            }
            return 0;
        }

        public bool Equals(Int256 other)
        {
            for (int i = 0; i < 32; i++)
            {
                if (!data[i].Equals(other.data[i])) return false;
            }
            return true;
        }
        #endregion

        #region overrides
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is Int256) return this.Equals((Int256)obj);
            throw new ArgumentException();
        }

        public override string ToString()
        {
            return new BigInteger(data).ToString();
        }

        public override int GetHashCode()
        {
            return 1768953197 + EqualityComparer<byte[]>.Default.GetHashCode(data);
        }

        #endregion

    }
}
