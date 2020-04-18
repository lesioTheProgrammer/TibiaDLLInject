using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TibiaDLLInject
{
    public class Tibia_Memory_Reader
    {
        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,
            byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        private byte[] ReadBytes(IntPtr pHandle, IntPtr address, int bytesToRead)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[bytesToRead];
            ReadProcessMemory(pHandle, address, buffer, bytesToRead, ref bytesRead);
            return buffer;
        }

        public int GetInt32(IntPtr pHandle, Int32 address)
        { return BitConverter.ToInt32(ReadBytes(pHandle, new IntPtr(address), 4), 0); }

        // theres issue with this class, get weird after some time
        public int GetHealth(IntPtr pHandle, Int32 address)
        {
            return GetInt32(pHandle, address + (int)ADDRESSES.HealthAddress) ^
                  GetInt32(pHandle, address + (int)ADDRESSES.XORAddress);
        }

        public int GetMaxHealth(IntPtr pHandle, Int32 address)
        { return GetInt32(pHandle, address + (int)ADDRESSES.MaxHealthAddress) ^
                GetInt32(pHandle, address + (int)ADDRESSES.XORAddress); }
        public int GetMana(IntPtr pHandle, Int32 address)
        { return GetInt32(pHandle, address + (int)ADDRESSES.ManaAddress) ^
                GetInt32(pHandle, address + (int)ADDRESSES.XORAddress); }
        public int GetMaxMana(IntPtr pHandle, Int32 address)
        { return GetInt32(pHandle, address + (int)ADDRESSES.MaxManaAddress) ^
                GetInt32(pHandle, address + (int)ADDRESSES.XORAddress); }
    
}
}
