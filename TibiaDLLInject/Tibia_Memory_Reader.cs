using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection.Metadata;
using System.Diagnostics;

namespace TibiaDLLInject
{
    public class Tibia_Memory_Reader
    {
        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess,
        IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize,
        ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        static  extern uint GetLastError();

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags access,
            bool inheritHandle, int procId);



        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        private byte[] ReadBytes(IntPtr pHandle, IntPtr address, int bytesToRead)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[bytesToRead];
            ReadProcessMemory(pHandle, address, buffer, bytesToRead, ref bytesRead);

            // error handling
            if (buffer[0] == 0)
            {
                var procesID = Process.GetCurrentProcess().Id;
                var errorCode =  GetLastError();
                //var OpeningProcess = OpenProcess(ProcessAccessFlags.All, false, procesID);
                Tibia_Debug.Log("Error code");
                Tibia_Debug.Log("\n" + errorCode);
                //Tibia_Debug.Log("\n" + OpeningProcess);
                return buffer;
            }

            return buffer;
        }

        public int GetInt32(IntPtr pHandle, Int32 address)
        {
            return BitConverter.ToInt32(ReadBytes(pHandle, new IntPtr(address), 4), 0);
        }

        // theres issue with this class, get weird after some time
        public int GetHealth(IntPtr pHandle, Int32 address)
        {
            //Tibia_Debug.Log("Get health from memory reader \n" + "\n" + "\n");
            return GetInt32(pHandle, address + (int)ADDRESSES.HealthAddress) ^
                  GetInt32(pHandle, address + (int)ADDRESSES.XORAddress);
        }

        public int GetMaxHealth(IntPtr pHandle, Int32 address)
        {
            return GetInt32(pHandle, address + (int)ADDRESSES.MaxHealthAddress) ^
                  GetInt32(pHandle, address + (int)ADDRESSES.XORAddress);
        }
        public int GetMana(IntPtr pHandle, Int32 address)
        {
            return GetInt32(pHandle, address + (int)ADDRESSES.ManaAddress) ^
                  GetInt32(pHandle, address + (int)ADDRESSES.XORAddress);
        }
        public int GetMaxMana(IntPtr pHandle, Int32 address)
        {
            return GetInt32(pHandle, address + (int)ADDRESSES.MaxManaAddress) ^
                  GetInt32(pHandle, address + (int)ADDRESSES.XORAddress);
        }

    }
}
