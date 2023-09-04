using System;

namespace Valvet.Datalager
{
    /// <summary>
    /// Alla datatyper
    /// </summary>
    public enum DataTyp
    {
        /// <summary>
        /// 
        /// </summary>
        Binary,
        /// <summary>
        /// 
        /// </summary>
        Bool,
        /// <summary>
        /// 
        /// </summary>
        Byte,
        /// <summary>
        /// 
        /// </summary>
        Char,
        /// <summary>
        /// 
        /// </summary>
        Date,
        /// <summary>
        /// 
        /// </summary>
        DateTime,
        /// <summary>
        /// 
        /// </summary>
        Decimal,
        /// <summary>
        /// 
        /// </summary>
        Int,
        /// <summary>
        /// 
        /// </summary>
        Longblob,
        /// <summary>
        /// 
        /// </summary>
        NChar,
        /// <summary>
        /// 
        /// </summary>
        NVarChar,
        /// <summary>
        /// 
        /// </summary>
        SmallDateTime,
        /// <summary>
        /// 
        /// </summary>
        String,
        /// <summary>
        /// 
        /// </summary>
        Time,
        /// <summary>
        /// 
        /// </summary>
        TimeStamp,
        /// <summary>
        /// 
        /// </summary>
        VarChar
    }

    /// <summary>
    /// 
    /// </summary>
    [Flags()]
    public enum ProcessAccessFlags : uint
    {
        /// <summary>
        /// PROCESS_ALL_ACCESS = 0x1f0fff
        /// </summary>
        PROCESS_ALL_ACCESS = 0x1f0fff,
        /// <summary>
        /// PROCESS_TERMINATE = 0x1
        /// </summary>
        PROCESS_TERMINATE = 0x1,
        /// <summary>
        /// PROCESS_CREATE_THREAD = 0x2
        /// </summary>
        PROCESS_CREATE_THREAD = 0x2,
        /// <summary>
        /// PROCESS_VM_OPERATION = 0x8
        /// </summary>
        PROCESS_VM_OPERATION = 0x8,
        /// <summary>
        /// PROCESS_VM_READ = 0x10
        /// </summary>
        PROCESS_VM_READ = 0x10,
        /// <summary>
        /// PROCESS_VM_WRITE = 0x20
        /// </summary>
        PROCESS_VM_WRITE = 0x20,
        /// <summary>
        /// PROCESS_DUP_HANDLE = 0x40
        /// </summary>
        PROCESS_DUP_HANDLE = 0x40,
        /// <summary>
        /// PROCESS_SET_INFORMATION = 0x200
        /// </summary>
        PROCESS_SET_INFORMATION = 0x200,
        /// <summary>
        /// PROCESS_SET_QUOTA = 0x100
        /// </summary>
        PROCESS_SET_QUOTA = 0x100,
        /// <summary>
        /// PROCESS_QUERY_INFORMATION = 0x400
        /// </summary>
        PROCESS_QUERY_INFORMATION = 0x400,
        /// <summary>
        /// PROCESS_QUERY_LIMITED_INFORMATION = 0x1000
        /// </summary>
        PROCESS_QUERY_LIMITED_INFORMATION = 0x1000,
        /// <summary>
        /// SYNCHRONIZE = 0x100000
        /// </summary>
        SYNCHRONIZE = 0x100000,
        /// <summary>
        /// PROCESS_CREATE_PROCESS = 0x80
        /// </summary>
        PROCESS_CREATE_PROCESS = 0x80,
        /// <summary>
        /// PROCESS_SUSPEND_RESUME = 0x800
        /// </summary>
        PROCESS_SUSPEND_RESUME = 0x800
    }
}
