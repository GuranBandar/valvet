using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Valvetwebb.Kontroller;

namespace Valvetwebb
{
    /// <summary>
    /// Hjälpklass för att veta vilka processer som är startade
    /// </summary>
    public class ProcessFileNameFinderClass
    {
        /// <summary>
        /// Lista alla öppna processer
        /// </summary>
        /// <returns>Alla öppna processer</returns>
        public static HashSet<string> GetAllRunningProcessFilePaths()
        {
            var allProcesses = System.Diagnostics.Process.GetProcesses();
            HashSet<string> ProcessExeNames = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            foreach (Process p in allProcesses)
            {
                string processExePath = GetProcessExecutablePath(p);
                ProcessExeNames.Add(System.IO.Path.GetFileName(processExePath));
            }
            return ProcessExeNames;
        }


        /// <summary>
        /// Get executable path of running process
        /// </summary>
        /// <param name="Process"></param>
        /// <returns></returns>
        public static string GetProcessExecutablePath(Process Process)
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    return GetExecutablePathAboveXP(Process.Id);// this gets the process file name without running as administrator 
                }
                return Process.MainModule.FileName;// Vista and later this requires running as administrator.
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        public static string GetExecutablePathAboveXP(int ProcessId)
        {
            int MAX_PATH = 260;
            StringBuilder sb = new StringBuilder(MAX_PATH + 1);
            IntPtr hprocess = OpenProcess(ProcessAccessFlags.PROCESS_QUERY_LIMITED_INFORMATION, false, ProcessId);
            if (hprocess != IntPtr.Zero)
            {
                try
                {
                    int size = sb.Capacity;
                    if (QueryFullProcessImageName(hprocess, 0, sb, ref size))
                    {
                        return sb.ToString();
                    }
                }
                finally
                {
                    CloseHandle(hprocess);
                }
            }
            return "";
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, [Out(), MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpExeName, ref int lpdwSize);

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hHandle);
    }
}
