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
        public int GetHealth(IntPtr pHandle, Int32 address)
        { return GetInt32(pHandle, address + (int)ADDRESSES.HEALTH_POINTS) ^
                GetInt32(pHandle, address + (int)ADDRESSES.XOR); }
        public int GetMaxHealth(IntPtr pHandle, Int32 address)
        { return GetInt32(pHandle, address + (int)ADDRESSES.MAX_HEALTH_POINTS) ^
                GetInt32(pHandle, address + (int)ADDRESSES.XOR); }
        public int GetMana(IntPtr pHandle, Int32 address)
        { return GetInt32(pHandle, address + (int)ADDRESSES.MANA_POINTS) ^
                GetInt32(pHandle, address + (int)ADDRESSES.XOR); }
        public int GetMaxMana(IntPtr pHandle, Int32 address)
        { return GetInt32(pHandle, address + (int)ADDRESSES.MAX_MANA_POINTS) ^
                GetInt32(pHandle, address + (int)ADDRESSES.XOR); }
    
}
}
