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
        #endregion

        #region Arithmetic
        INC,
        DEC,
        ADD,
        SADD,
        MUL,
        SMUL,
        SUB,
        SSUB,
        //2 / 1 becuase 2 is bigger
        DIV,
        SDIV,
        //MODULU 2 % 1
        MOD,
        SMOD,
        //EXPONENTIAL pow(2,1)
        EXP,
        SEXP,
        //log 10
        LOG,
        ABS,
        #endregion

        #region Logical
        AND,
        OR,
        XOR,
        NOT,
        //TWO'S COMPLEMENT MAKE IT NEGETIVE
        NEG,
        //SHIFT LEFT op2 shifts op1 (op2<<op1)
        SHL,
        //SHIFT RIGHT op2 shifts op1(op2>>op1)
        SHR,
        #endregion

        #region Compare
        //GREATER THAN
        GT,
        SGT,
        //LESS THAN
        LT,
        SLT,
        //or equel
        GTE,
        SGTE,
        LTE,
        SLTE,
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
        JIFSLT,
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
        EXCH,
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
        //LOAD FILE AND PUT IT IN STORAGE AND PUT IT ADDRESS TO IO REG
        LDF,
        //SAVE INDICATED FILE FROM STORAGE TO HARDDISC WITH GIVEN NAME
        STF,
        #endregion

        //STORAGE UNMANGED MEMORY OF .NET TYPES AND VARIABLES GO IN HERE
        #region Storage
        ST,
        LD,
        #endregion

        #region BlockChain
        //LOAD VALUE FROM BLOCKCHAIN
        LDB,
        //BLOCK TIME STAMP
        BTIME,
        //BLOCKHASH
        BHASH,
        //GET BLOCK INDEX
        BINDEX,
        //DIFFICULTY
        DIFF,
        #endregion

        #region Transaction
        //TRANSACTION HASH
        TXH,
        //TX SENDER
        TXSENDER,
        //RECIPIENT
        TXRECIP,
        //SEND TIME
        TXTIME,
        //INPUT VALUE OF TRANSACTION
        TXIN,
        //OUTPUT VALUE OF TRANSACTION
        TXOUT,
        //TRANSACTION TYPE(FUNCTION CALL OR TRANSFER)
        TXTYPE,
        #endregion

        #region SENDER OF THIS TRANSACTION
        //THE VERIY FIRST SENDER OF TRANSACTION
        ORIGIN,
        //MAYBE THE SAME WITH ABOVE, OR MAYBE THE OTHER APP WICH CALL TO THIS
        CALLER,
        //THE SUPPLAY SENT WITH THE TRANSACTION
        SUPPLY,
        #endregion

        #region Func
        //FUNCTION CALL
        CALL,
        //RET FUNCTION
        RET,
        //CALL TO OTHER APP
        GCALL,
        //CREATE
        CREATE,
        #endregion

        #region Account
        //GET ACC COUDE
        ACCODE,
        //ACC BALANCE
        ACBALANCE,
        //ACC NONCE
        ACNONCE,
        //TIME STAMP
        ACTIME,
        #endregion

        #region Serialization
        SHA2,
        SHA3,
        MD5,
        //BLACKE2B
        BE2B,
        //TWICE
        SHA2T,
        SHA3T,
        MD5T,
        BE2B2,
        JSON,
        BASE64,
        BASE58CH,
        //AES
        ENCAES,
        DECAES,
        //ECC
        ENCECC,
        DECECC,
        //CHECK SIGNTURE CORRECT
        CHECKSIG,
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
