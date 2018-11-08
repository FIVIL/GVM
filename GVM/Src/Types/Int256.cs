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
    public struct Int256 /*: IComparable, IComparable<Int256>, IEquatable<Int256>*/
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
        public Int256 MaxValue { get => new Int256 { data = maxByte }; }
        public Int256 MinValue { get => new Int256 { data = minByte }; }
        public Int256 Zero { get => new Int256 { data = zeroByte }; }
        public Int256 One { get => new Int256 { data = oneByte }; }
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
            if (b.Length < 32)
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
        #endregion

        #endregion

        #region Statics
        public static Int256 Pars(string s)
        {
            var c = BigInteger.Parse(s);
            if (c.ToByteArray().Length > 31) throw new ArgumentOutOfRangeException();
            return new BigInteger(c.ToByteArray());
        }
        #endregion

        #region NonStatics

        public byte[] ToByteArray() => data;
        #endregion

    }
}
