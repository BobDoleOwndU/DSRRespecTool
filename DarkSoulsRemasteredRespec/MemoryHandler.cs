using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace DarkSoulsRemasteredRespec
{
    public static class MemoryHandler
    {
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        } //ProcessAccessFlags

        private static int[] offsets = { 0x38, 0x1e0, 0x98, 0x38, 0x180, 0x18 };
        private static int[] paramOffsets = { 0x270, 0x274, 0x278, 0x27C, 0x280, 0x284, 0x288, 0x28C };

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern int CloseHandle(IntPtr hProcess);

        public static int[] ReadMemory()
        {
            int[] stats = new int[8];
            Process process = Process.GetProcessesByName("DarkSoulsRemastered").FirstOrDefault();
            IntPtr processHandle = OpenProcess(ProcessAccessFlags.All, false, process.Id);

            IntPtr moduleAddress = (IntPtr)0;
            int moduleCount = process.Modules.Count;

            for (int i = 0; i < moduleCount; i++)
            {
                ProcessModule m = process.Modules[i];

                if (m.ModuleName == "DarkSoulsRemastered.exe")
                {
                    moduleAddress = m.BaseAddress;
                } //if
            } //for

            IntPtr address = GetValueAtAddress(processHandle, moduleAddress + 0x01D05718, 8);

            foreach (int i in offsets)
            {
                address = GetValueAtAddress(processHandle, address + i, 8);
            } //foreach

            int statsCount = stats.Length;

            for (int i = 0; i < statsCount; i++)
            {
                stats[i] = (int)GetValueAtAddress(processHandle, address + paramOffsets[i], 4);
            } //for

            CloseHandle(processHandle);

            return stats;
        } //ReadMemory

        public static void WriteMemory(int[] stats)
        {
            Process process = Process.GetProcessesByName("DarkSoulsRemastered").FirstOrDefault();
            IntPtr processHandle = OpenProcess(ProcessAccessFlags.All, false, process.Id);

            IntPtr moduleAddress = (IntPtr)0;
            int moduleCount = process.Modules.Count;

            for (int i = 0; i < moduleCount; i++)
            {
                ProcessModule m = process.Modules[i];

                if (m.ModuleName == "DarkSoulsRemastered.exe")
                {
                    moduleAddress = m.BaseAddress;
                } //if
            } //for

            IntPtr address = GetValueAtAddress(processHandle, moduleAddress + 0x01D05718, 8);

            foreach (int i in offsets)
            {
                address = GetValueAtAddress(processHandle, address + i, 8);
            } //foreach

            int statsCount = stats.Count();

            for (int i = 0; i < statsCount; i++)
            {
                SetValueAtAddress(processHandle, address + paramOffsets[i], stats[i]);
            } //for

            CloseHandle(processHandle);
        } //WriteMemory()

        private static IntPtr GetValueAtAddress(IntPtr processHandle, IntPtr address, int dataSize)
        {
            IntPtr bytesRead;
            byte[] buffer = new byte[dataSize];

            ReadProcessMemory(processHandle, address, buffer, buffer.Length, out bytesRead);

            if(dataSize == 4)
                return (IntPtr)BitConverter.ToInt32(buffer, 0);
            else
                return (IntPtr)BitConverter.ToInt64(buffer, 0);
        } //GetAddress

        private static void SetValueAtAddress(IntPtr processHandle, IntPtr address, int value)
        {
            IntPtr bytesWritten;
            byte[] buffer = BitConverter.GetBytes(value);

            WriteProcessMemory(processHandle, address, buffer, buffer.Length, out bytesWritten);
        } //SetValueAtAddress
    } //class
} //namespace
