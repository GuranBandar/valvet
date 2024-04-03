using System;

namespace Valvetwebb.Kontroller
{
    /// <summary>
    /// Kodtabell
    /// </summary>
    public enum Kodtabell
    {
        /// <summary>
        /// Alla_Koder
        /// </summary>
        Alla_koder = 00,
        /// <summary>
        /// Distrikt
        /// </summary>
        Distrikt = 01,
        /// <summary>
        /// Kön
        /// </summary>
        Kön = 02,
        /// <summary>
        /// Golfklubbar
        /// </summary>
        Golfklubbar = 03,
        /// <summary>
        /// Land
        /// </summary>
        Land = 04,
        /// <summary>
        /// Tee
        /// </summary>
        Tee = 05,
        /// <summary>
        /// Max_och_min
        /// </summary>
        Max_och_min = 06,
        /// <summary>
        /// Medlemskategori
        /// </summary>
        Medlemskategori = 07,
        /// <summary>
        /// Medlemstyp
        /// </summary>
        Medlemstyp = 08,
        /// <summary>
        /// Handicapklasser
        /// </summary>
        Handicapklasser = 09,
        /// <summary>
        /// Redovisningstyper
        /// </summary>
        Redovisningstyper = 10,
        /// <summary>
        /// Tävlingsformer
        /// </summary>
        Spelformer = 11,
        /// <summary>
        /// Tävlingsstatus
        /// </summary>
        Tävlingsstatus = 12,
        /// <summary>
        /// Spelsätt
        /// </summary>
        Spelsätt = 13,
        /// <summary>
        /// Speltyper
        /// </summary>
        Speltyper = 14,
        /// <summary>
        /// ÖppenFör
        /// </summary>
        ÖppenFör = 15,
        /// <summary>
        /// PrincipForOveranmalan
        /// </summary>
        PrincipForOveranmalan = 16,
        /// <summary>
        /// Tävlingsklasser
        /// </summary>
        Tävlingsklasser = 17,
        /// <summary>
        /// Klasstyp
        /// </summary>
        Klasstyp = 18,
        /// <summary>
        /// Rondstatus
        /// </summary>
        Rondstatus = 19,
        /// <summary>
        /// Buffertzoner
        /// </summary>
        Buffertzon = 20,
        /// <summary>
        /// Hcpjustering
        /// </summary>
        Hcpjustering = 21,
        /// <summary>
        /// Användargrupper
        /// </summary>
        Anvandargrupper = 22,
        /// <summary>
        /// WebBrowser
        /// </summary>
        WebBrowsers = 23,
        /// <summary>
        /// Språkkod
        /// </summary>
        Sprakkod = 24
    }

    /// <summary>
    /// 
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
