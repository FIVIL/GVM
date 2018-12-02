using GDIC;
using GVM.Src.Components.BaseComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GVM.Src.Components
{
    public class ALU
    {
        private readonly Dictionary<OpCodeName, Action<int, Operand, Operand>> actions;
        private readonly Dictionary<OpType, Func<int, Register>> types;
        private bool doWork;
        private readonly ServiceCollection services;
        private readonly ManualResetEvent[] mres;
        private readonly ManualResetEvent thisMre;
        public ALU(ServiceCollection _service, ManualResetEvent[] _mres, ManualResetEvent resetEvent)
        {
            doWork = true;
            services = _service;
            mres = _mres;
            thisMre = resetEvent;
            actions = new Dictionary<OpCodeName, Action<int, Operand, Operand>>();
            types = new Dictionary<OpType, Func<int, Register>>();

            types.Add(OpType.Immediate, (a) => a);
            types.Add(OpType.MemAddress, (a) =>
            {
                var mem = services.GetService<Memory>();
                return mem[a];
            });
            types.Add(OpType.Register, (a) =>
            {
                var reg = services.GetService<Registers>();
                return reg[(RegistersName)(a)];
            });

            /////////////////////////////////////////////////////////////////
            #region Basic
            actions.Add(OpCodeName.STOP, (c, o1, o2) =>
            {
                if (c != 0) throw new ArgumentException();
                doWork = false;
                Revert();
            });

            actions.Add(OpCodeName.HALT, (c, o1, o2) =>
            {
                if (c != 1) throw new ArgumentException();
                mres[GetSource(o1)].Reset();
            });

            actions.Add(OpCodeName.SIGNAL, (c, o1, o2) =>
            {
                if (c != 1) throw new ArgumentException();
                mres[GetSource(o1)].Set();
            });

            actions.Add(OpCodeName.REVERT, (c, o1, o2) =>
            {
                if (c != 0) throw new ArgumentException();
                thisMre.Reset();
                Revert();
                Init();
                thisMre.Set();
            });

            actions.Add(OpCodeName.EXITE, (c, o1, o2) =>
            {
                if (c != 0) throw new ArgumentException();
                Finalize();
                doWork = false;
            });

            actions.Add(OpCodeName.RESTART, (c, o1, o2) =>
            {
                if (c != 0) throw new ArgumentException();
                thisMre.Reset();
                Revert();
                Init();
                thisMre.Set();
            });

            actions.Add(OpCodeName.START, (c, o1, o2) =>
            {
                if (c != 0) throw new ArgumentException();
                Init();
            });

            actions.Add(OpCodeName.SLEEP, (c, o1, o2) =>
            {
                if (c != 0 || c != 1) throw new ArgumentException();
                if (c == 0)
                {
                    Thread.Sleep(1);
                }
                else if (c == 1)
                {
                    Thread.Sleep(GetSource(o1));
                }
            });

            actions.Add(OpCodeName.MV, (c, o1, o2) =>
            {
                if (c != 2) throw new ArgumentException();
                if (o1.Type == OpType.Immediate) throw new ArgumentException();
                if (o1.Type == OpType.MemAddress) services.GetService<Memory>()[o1.Data] = GetSource(o2);
                if (o1.Type == OpType.Register) services.GetService<Registers>()[(RegistersName)((int)o1.Data)] = GetSource(o2);
            });
            actions.Add(OpCodeName.NOP, (c, o1, o2) =>
            {
                if (c != 0) throw new ArgumentException();
                for (int i = 0; i < 10; i++)
                {

                }
            });
            #endregion

            #region Arithmetic

            //TO DO: setting the flags
            actions.Add(OpCodeName.INC, (c, o1, o2) =>
            {
                Calculator1o0(c, o1, (op1) =>
                      (new BigInteger(op1) + 1).ToByteArray());
            });
            actions.Add(OpCodeName.DEC, (c, o1, o2) =>
            {
                Calculator1o0(c, o1, (op1) =>
                      (new BigInteger(op1) - 1).ToByteArray());
            });
            actions.Add(OpCodeName.ADD, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (BigInteger.Abs(new BigInteger(op1)) + BigInteger.Abs(new BigInteger(op2))).ToByteArray());
            });

            actions.Add(OpCodeName.SADD, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) + new BigInteger(op2)).ToByteArray());
            });

            actions.Add(OpCodeName.MUL, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (BigInteger.Abs(new BigInteger(op1)) * BigInteger.Abs(new BigInteger(op2))).ToByteArray());
            });

            actions.Add(OpCodeName.SMUL, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) * new BigInteger(op2)).ToByteArray());
            });

            actions.Add(OpCodeName.SUB, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (BigInteger.Abs(new BigInteger(op1)) - BigInteger.Abs(new BigInteger(op2))).ToByteArray());
            });

            actions.Add(OpCodeName.SSUB, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) - new BigInteger(op2)).ToByteArray());
            });

            actions.Add(OpCodeName.DIV, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (BigInteger.Abs(new BigInteger(op1)) / BigInteger.Abs(new BigInteger(op2))).ToByteArray());
            });

            actions.Add(OpCodeName.SDIV, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) / new BigInteger(op2)).ToByteArray());
            });

            actions.Add(OpCodeName.MOD, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (BigInteger.Abs(new BigInteger(op1)) % BigInteger.Abs(new BigInteger(op2))).ToByteArray());
            });

            actions.Add(OpCodeName.SMOD, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) % new BigInteger(op2)).ToByteArray());
            });

            actions.Add(OpCodeName.EXP, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (BigInteger.Pow(BigInteger.Abs(new BigInteger(op1)), op2)).ToByteArray());
            });

            actions.Add(OpCodeName.SEXP, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                       BigInteger.Pow(new BigInteger(op1), op2).ToByteArray());
            });

            actions.Add(OpCodeName.LOG, (c, o1, o2) =>
            {
                Calculator1o0(c, o1, (op1) =>
                      BitConverter.GetBytes(BigInteger.Log10(new BigInteger(op1))));
            });

            actions.Add(OpCodeName.ABS, (c, o1, o2) =>
            {
                Calculator1o0(c, o1, (op1) =>
                      BigInteger.Abs(new BigInteger(op1)).ToByteArray());
            });
            #endregion

            #region Logical
            actions.Add(OpCodeName.AND, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) & new BigInteger(op2)).ToByteArray());
            });
            actions.Add(OpCodeName.OR, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) | new BigInteger(op2)).ToByteArray());
            });
            actions.Add(OpCodeName.XOR, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) ^ new BigInteger(op2)).ToByteArray());
            });
            actions.Add(OpCodeName.NOT, (c, o1, o2) =>
            {
                Calculator1o0(c, o1, (op1) =>
                        (~new BigInteger(op1)).ToByteArray());
            });
            actions.Add(OpCodeName.NEG, (c, o1, o2) =>
            {
                Calculator1o0(c, o1, (op1) =>
                        (-new BigInteger(op1)).ToByteArray());
            });
            actions.Add(OpCodeName.SHR, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) >> op2).ToByteArray());
            });

            actions.Add(OpCodeName.SHL, (c, o1, o2) =>
            {
                Calculator2o0(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) << op2).ToByteArray());
            });
            #endregion

            #region Compare
            actions.Add(OpCodeName.GT, (c, o1, o2) =>
            {
                Comprator(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) > new BigInteger(op2)));
            });

            actions.Add(OpCodeName.SGT, (c, o1, o2) =>
            {
                Comprator(c, o1, o2, (op1, op2) =>
                        (BigInteger.Abs(new BigInteger(op1)) > BigInteger.Abs(new BigInteger(op2))));
            });

            actions.Add(OpCodeName.LT, (c, o1, o2) =>
            {
                Comprator(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) < new BigInteger(op2)));
            });

            actions.Add(OpCodeName.SLT, (c, o1, o2) =>
            {
                Comprator(c, o1, o2, (op1, op2) =>
                        (BigInteger.Abs(new BigInteger(op1)) < BigInteger.Abs(new BigInteger(op2))));
            });

            actions.Add(OpCodeName.CP, (c, o1, o2) =>
            {
                Comprator(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) == new BigInteger(op2)));
            });

            actions.Add(OpCodeName.GTE, (c, o1, o2) =>
            {
                Comprator(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) >= new BigInteger(op2)));
            });

            actions.Add(OpCodeName.SGTE, (c, o1, o2) =>
            {
                Comprator(c, o1, o2, (op1, op2) =>
                        (BigInteger.Abs(new BigInteger(op1)) >= BigInteger.Abs(new BigInteger(op2))));
            });

            actions.Add(OpCodeName.LTE, (c, o1, o2) =>
            {
                Comprator(c, o1, o2, (op1, op2) =>
                        (new BigInteger(op1) <= new BigInteger(op2)));
            });

            actions.Add(OpCodeName.SLTE, (c, o1, o2) =>
            {
                Comprator(c, o1, o2, (op1, op2) =>
                        (BigInteger.Abs(new BigInteger(op1)) <= BigInteger.Abs(new BigInteger(op2))));
            });
            #endregion

            #region Jumps
            //To Do: flag bases

            #region OperandComparisonBASE
            actions.Add(OpCodeName.JIFGT, (c, o1, o2) =>
            {
                Jump(o1, (op1, op2) => new BigInteger(op1) > new BigInteger(op2));
            });
            actions.Add(OpCodeName.JIFSGT, (c, o1, o2) =>
            {
                Jump(o1, (op1, op2) => BigInteger.Abs(new BigInteger(op1)) > BigInteger.Abs(new BigInteger(op2)));
            });

            actions.Add(OpCodeName.JIFLT, (c, o1, o2) =>
            {
                Jump(o1, (op1, op2) => new BigInteger(op1) < new BigInteger(op2));
            });

            actions.Add(OpCodeName.JIFSLT, (c, o1, o2) =>
            {
                Jump(o1, (op1, op2) => BigInteger.Abs(new BigInteger(op1)) < BigInteger.Abs(new BigInteger(op2)));
            });

            actions.Add(OpCodeName.JIFGTE, (c, o1, o2) =>
            {
                Jump(o1, (op1, op2) => new BigInteger(op1) >= new BigInteger(op2));
            });
            actions.Add(OpCodeName.JIFSGTE, (c, o1, o2) =>
            {
                Jump(o1, (op1, op2) => BigInteger.Abs(new BigInteger(op1)) >= BigInteger.Abs(new BigInteger(op2)));
            });

            actions.Add(OpCodeName.JIFLTE, (c, o1, o2) =>
            {
                Jump(o1, (op1, op2) => new BigInteger(op1) <= new BigInteger(op2));
            });
            actions.Add(OpCodeName.JIFSLTE, (c, o1, o2) =>
            {
                Jump(o1, (op1, op2) => BigInteger.Abs(new BigInteger(op1)) <= BigInteger.Abs(new BigInteger(op2)));
            });
            #endregion
            #endregion

            #region Stack
            actions.Add(OpCodeName.PUSH, (c, o1, o2) =>
            {
                if (c != 0 || c != 1 || c != 2) throw new ArgumentException();
                if (c == 0) services.GetService<Stack>().Push(new Register());
                if (c == 2) for (int i = 0; i < o2.Data; i++)
                    {
                        services.GetService<Stack>().Push(GetSource(o1));
                    }
            });

            actions.Add(OpCodeName.POP, (c, o1, o2) =>
            {
                if (c != 0 || c != 1) throw new ArgumentException();
                if (c == 0)
                {
                    services.GetService<Stack>().Pop(out var r);
                    services.GetService<Registers>()[RegistersName.AX] = r;
                }
                if (c == 1) for (int i = 0; i < o1.Data; i++)
                    {
                        services.GetService<Stack>().Pop(out var r);
                        services.GetService<Registers>()[RegistersName.AX] = r;
                    }
            });

            actions.Add(OpCodeName.DUP, (c, o1, o2) =>
            {
                if (c != 0 || c != 1) throw new ArgumentException();
                if (c == 0)
                {
                    services.GetService<Stack>().Pop(out var s);
                    services.GetService<Stack>().Push(s);
                    services.GetService<Stack>().Push(s);
                }
                if (c == 1)
                {
                    services.GetService<Stack>().Pop(out var s);
                    services.GetService<Stack>().Push(s);
                    for (int i = 0; i < o1.Data; i++)
                    {
                        services.GetService<Stack>().Push(s);
                    }
                }
            });

            actions.Add(OpCodeName.EXCH, (c, o1, o2) =>
            {
                if (c != 0) throw new ArgumentException();

                services.GetService<Stack>().Pop(out var s1);
                services.GetService<Stack>().Pop(out var s2);
                services.GetService<Stack>().Push(s2);
                services.GetService<Stack>().Push(s1);

            });
            #endregion

            #region IO
            //TO DO
            #endregion

            #region Storage
            //TODO
            #endregion

            #region BlockChain
            //TODO
            #endregion

            #region Transaction
            //TODO
            #endregion

            #region SENDER OF THIS TRANSACTION
            //TODO
            #endregion

            #region Func
            actions.Add(OpCodeName.CALL, (c, o1, o2) =>
            {
                var ReturnAddress = services.GetService<Registers>()[RegistersName.IP];
                ReturnAddress++;
                if (!services.GetService<Stack>().Push(ReturnAddress)) throw new ArgumentNullException();
                services.GetService<Registers>()[RegistersName.IP] = GetSource(o1);
            });

            actions.Add(OpCodeName.RET, (c, o1, o2) =>
            {
                if (!services.GetService<Stack>().Pop(out var ReturnAddress)) throw new ArgumentNullException();
                services.GetService<Registers>()[RegistersName.IP] = GetSource(new Operand((byte[])ReturnAddress));
            });

            //TODO Globals
            #endregion

            #region Account

            //TOOD

            #endregion

            #region Serialization
            actions.Add(OpCodeName.SHA2, (c, o1, o2) =>
            {
                if (c != 0 || c != 1) throw new ArgumentException();
                if (c == 0)
                {
                    if (!services.GetService<Stack>().Pop(out var op1)) throw new ArgumentException();
                    using (var sha = SHA256.Create())
                    {
                        var p = sha.ComputeHash(((byte[])GetSource(new Operand((byte[])op1))));
                        if (!services.GetService<Stack>().Push(p)) throw new ArgumentException();
                    }

                }
                else
                {
                    using (var sha = SHA256.Create())
                    {
                        var p = sha.ComputeHash(((byte[])GetSource(o1)));
                        services.GetService<Registers>()[RegistersName.AX] = p;
                    }
                }
            });
            actions.Add(OpCodeName.SHA3, (c, o1, o2) =>
            {
                throw new ArgumentException();
            });
            actions.Add(OpCodeName.MD5, (c, o1, o2) =>
            {
                if (c != 0 || c != 1) throw new ArgumentException();
                if (c == 0)
                {
                    if (!services.GetService<Stack>().Pop(out var op1)) throw new ArgumentException();
                    using (var sha = MD5.Create())
                    {
                        var p = sha.ComputeHash(((byte[])GetSource(new Operand((byte[])op1))));
                        if (!services.GetService<Stack>().Push(p)) throw new ArgumentException();
                    }

                }
                else
                {
                    using (var sha = MD5.Create())
                    {
                        var p = sha.ComputeHash(((byte[])GetSource(o1)));
                        services.GetService<Registers>()[RegistersName.AX] = p;
                    }
                }
            });
            actions.Add(OpCodeName.BE2B, (c, o1, o2) =>
            {
                throw new ArgumentException();
            });

            actions.Add(OpCodeName.SHA2T, (c, o1, o2) =>
            {
                if (c != 0 || c != 1) throw new ArgumentException();
                if (c == 0)
                {
                    if (!services.GetService<Stack>().Pop(out var op1)) throw new ArgumentException();
                    using (var sha = SHA256.Create())
                    {
                        var p = sha.ComputeHash(((byte[])GetSource(new Operand((byte[])op1))));
                        p = sha.ComputeHash(p);
                        if (!services.GetService<Stack>().Push(p)) throw new ArgumentException();
                    }

                }
                else
                {
                    using (var sha = SHA256.Create())
                    {
                        var p = sha.ComputeHash(((byte[])GetSource(o1)));
                        p = sha.ComputeHash(p);
                        services.GetService<Registers>()[RegistersName.AX] = p;
                    }
                }
            });
            actions.Add(OpCodeName.SHA3T, (c, o1, o2) =>
            {
                throw new ArgumentException();
            });
            actions.Add(OpCodeName.MD5T, (c, o1, o2) =>
            {
                if (c != 0 || c != 1) throw new ArgumentException();
                if (c == 0)
                {
                    if (!services.GetService<Stack>().Pop(out var op1)) throw new ArgumentException();
                    using (var sha = MD5.Create())
                    {
                        var p = sha.ComputeHash(((byte[])GetSource(new Operand((byte[])op1))));
                        p = sha.ComputeHash(p);
                        if (!services.GetService<Stack>().Push(p)) throw new ArgumentException();
                    }

                }
                else
                {
                    using (var sha = MD5.Create())
                    {
                        var p = sha.ComputeHash(((byte[])GetSource(o1)));
                        p = sha.ComputeHash(p);
                        services.GetService<Registers>()[RegistersName.AX] = p;
                    }
                }
            });
            actions.Add(OpCodeName.BE2BT, (c, o1, o2) =>
            {
                throw new ArgumentException();
            });

            
            #endregion
        }
        private void Revert()
        {

        }
        private void Init()
        {

        }

        private void Finalize()
        {

        }
        private Register GetSource(Operand o) => types[o.Type](o.Data);

        private void Calculator2o0(int c, Operand o1, Operand o2, Func<Register, Register, byte[]> f)
        {
            if (c != 0 || c != 2) throw new ArgumentException();
            if (c == 0)
            {
                if (services.GetService<Registers>()[RegistersName.SP] < 3) throw new NullReferenceException();
                services.GetService<Stack>().Pop(out var op1);
                services.GetService<Stack>().Pop(out var op2);
                var res = f(op1, op2);
                var tempres = res;
                if (tempres.Length > Utilities.Statics.bufferSize) tempres = tempres.Take(Utilities.Statics.bufferSize).ToArray();
                if (tempres.Length < Utilities.Statics.bufferSize)
                {
                    var t = new byte[Utilities.Statics.bufferSize];
                    for (int i = 0; i < tempres.Length; i++)
                    {
                        t[i] = tempres[i];
                    }
                }
                services.GetService<Stack>().Push(tempres);
            }
            else
            {
                var op1 = GetSource(o1);
                var op2 = GetSource(o2);
                var res = f(op1, op2);
                var tempres = res;
                if (tempres.Length > Utilities.Statics.bufferSize) tempres = tempres.Take(Utilities.Statics.bufferSize).ToArray();
                if (tempres.Length < Utilities.Statics.bufferSize)
                {
                    var t = new byte[Utilities.Statics.bufferSize];
                    for (int i = 0; i < tempres.Length; i++)
                    {
                        t[i] = tempres[i];
                    }
                }
                services.GetService<Registers>()[RegistersName.AX] = tempres;
            }
        }
        private void Calculator1o0(int c, Operand o1, Func<Register, byte[]> f)
        {
            if (c != 0 || c != 1) throw new ArgumentException();
            if (c == 0)
            {
                if (services.GetService<Registers>()[RegistersName.SP] < 2) throw new NullReferenceException();
                services.GetService<Stack>().Pop(out var op1);
                var res = f(op1);
                var tempres = res;
                if (tempres.Length > Utilities.Statics.bufferSize) tempres = tempres.Take(Utilities.Statics.bufferSize).ToArray();
                if (tempres.Length < Utilities.Statics.bufferSize)
                {
                    var t = new byte[Utilities.Statics.bufferSize];
                    for (int i = 0; i < tempres.Length; i++)
                    {
                        t[i] = tempres[i];
                    }
                }
                services.GetService<Stack>().Push(tempres);
            }
            else
            {
                var op1 = GetSource(o1);
                var res = f(op1);
                var tempres = res;
                if (tempres.Length > Utilities.Statics.bufferSize) tempres = tempres.Take(Utilities.Statics.bufferSize).ToArray();
                if (tempres.Length < Utilities.Statics.bufferSize)
                {
                    var t = new byte[Utilities.Statics.bufferSize];
                    for (int i = 0; i < tempres.Length; i++)
                    {
                        t[i] = tempres[i];
                    }
                }
                services.GetService<Registers>()[RegistersName.AX] = tempres;
            }
        }
        private void Comprator(int c, Operand o1, Operand o2, Func<Register, Register, bool> f)
        {
            if (c != 0 || c != 2) throw new ArgumentException();
            if (c == 0)
            {
                if (services.GetService<Registers>()[RegistersName.SP] < 3) throw new NullReferenceException();
                services.GetService<Stack>().Pop(out var op1);
                services.GetService<Stack>().Pop(out var op2);
                if (f(op1, op2))
                    services.GetService<Flags>()[FlagsNames.Accumulator].Set();
                else
                    services.GetService<Flags>()[FlagsNames.Accumulator].Clear();
            }
            else
            {
                var op1 = GetSource(o1);
                var op2 = GetSource(o2);
                if (f(op1, op2))
                    services.GetService<Flags>()[FlagsNames.Accumulator].Set();
                else
                    services.GetService<Flags>()[FlagsNames.Accumulator].Clear();
            }
        }
        private void Jump(Operand op, Func<Register, Register, bool> f)
        {
            if (services.GetService<Registers>()[RegistersName.SP] < 3) throw new NullReferenceException();
            services.GetService<Stack>().Pop(out var op1);
            services.GetService<Stack>().Pop(out var op2);
            if (f(op1, op2))
                services.GetService<Registers>()[RegistersName.IP] = GetSource(op);
        }
    }
}
