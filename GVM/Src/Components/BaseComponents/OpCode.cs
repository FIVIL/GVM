using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GVM.Src.Components.BaseComponents
{
    public enum OpCodeName
    {
        #region Basic
        //STOP EXECUTION AND REVERT EVERYTHING
        STOP,
        //HALT CURRENT EXECUTION AND WAITS FOR A SIGNAL COMMAND
        HALT,
        SIGNAL,
        //REVERT EVERUTHING
        REVERT,
        //AT THE END OF THE EXECUTION FOR FINALIZING STATE
        EXITE,
        //HALR CURRENT EXECUTION, REVERT STATE, AND THEN START EXCUTION FROM BEGINING OVER AGAIN
        RESTART,
        //AT THE START OF THE CODE
        START,
        SLEEP,
        MV,
        NOP,
        EXCH,
        #endregion

        #region Arithmetic
        ADD,
        SADD,
        MUL,
        SMUL,
        SUB,
        SSUB,
        DIV,
        SDIV,
        //MODULU
        MOD,
        SMOD,
        //EXPONENTIAL
        EXP,
        SEXP,
        LOG,
        ABS,
        #endregion

        #region Logical
        INC,
        DEC,
        AND,
        OR,
        XOR,
        NOT,
        //TWO'S COMPLEMENT MAKE IT NEGETIVE
        NEG,
        //SHIFT LEFT
        SHL,
        //SHIFT RIGHT
        SHR,
        #endregion

        #region Compare
        //GREATER THAN
        GT,
        SGT,
        //LESS THAN
        LT,
        SLT,
        //IF EQUAL TRUE ELSE FALSE
        CP,
        #endregion

        #region Flag
        CLS,
        SS,
        CLZ,
        SZ,
        CLC,
        SC,
        CLO,
        SO,
        CLA,
        SA,
        #endregion

        #region Jumps

        #region FlagBase
        JCS,
        JSS,
        JCZ,
        JSZ,
        JCC,
        JSC,
        JCO,
        JSO,
        JCA,
        JSA,
        #endregion

        #region OperandComparisonBASE
        //JUMP IF GREATER THAN
        JIFGT,
        //SIGNED
        JIFSGT,
        //LESS
        JIFLT,
        //SIGNED
        JIFSL,
        //OR EQUAL
        JIFGTE,
        JIFSGTE,
        JIFLTE,
        JIFSLTE,
        #endregion

        #endregion

        #region Stack
        PUSH,
        POP,
        DUP,
        #endregion

        #region IO
        //READ THE CURRENT VALUE AT IO FILE AND PUT IT IN IO REGISTER(CX)
        REIO,
        //WRITE THE VALUE OF CX INTO IO
        WRIO,
        //AND ALSO PUSH INTO STACK
        REIOP,
        //WRITE THE STACK TOP VALUE INTO IO BUFFER
        WRIOT,
        //AND ALSO POP
        WRIOP,
        #endregion

        //STORAGE UNMANGED MEMORY OF .NET TYPES
        #region STORAGE

        #endregion
    }
    public class OpCode
    {
        public OpCode(int code)
        {

        }
        public OpCodeName CodeName { get; }
        public int AvailableInputsCount { get; }
        public int OutPutCounts { get; }
        public double Price { get; }
    }
}
