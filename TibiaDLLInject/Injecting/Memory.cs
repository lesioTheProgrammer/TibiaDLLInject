using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace TibiaDLLInject.Injecting
{
    public static class Memory
    {
        #region Methods

        public static byte[] ReadBytes(long address, uint bytesToRead, IntPtr pHandle)
        {
            IntPtr ptrBytesRead;
            byte[] buffer = new byte[bytesToRead];
            if (NativeMethods.NativeMethods.ReadProcessMemory(pHandle,
                new IntPtr(address), buffer, (IntPtr)bytesToRead, out ptrBytesRead) != 0)
            {
                return buffer;
            }
            else return null;
        }

        public static byte[] ReadBytes(IntPtr handle, long address, uint bytesToRead)
        {
            IntPtr ptrBytesRead;
            byte[] buffer = new byte[bytesToRead];
            if (NativeMethods.NativeMethods.ReadProcessMemory(handle, new IntPtr(address), buffer, 
                (IntPtr)bytesToRead, out ptrBytesRead) != 0)
            {
                return buffer;
            }
            return null;
        }

        public static byte ReadByte(long address, IntPtr pHandle)
        {
            return ReadBytes(pHandle, address, 1)[0];
        }

        public static byte ReadByte(IntPtr handle, long address)
        {
            return ReadBytes(handle, address, 1)[0];
        }

        public static short ReadInt16(long address, IntPtr pHandle)
        {
            return BitConverter.ToInt16(ReadBytes(pHandle, address, 2), 0);
        }

        public static short ReadInt16(IntPtr handle, long address)
        {
            return BitConverter.ToInt16(ReadBytes(handle, address, 2), 0);
        }

        public static ushort ReadUInt16(long address, IntPtr pHandle)
        {
            return BitConverter.ToUInt16(ReadBytes(pHandle, address, 2), 0);
        }

        public static ushort ReadUInt16(IntPtr handle, long address)
        {
            return BitConverter.ToUInt16(ReadBytes(handle, address, 2), 0);
        }

        public static short ReadShort(long address, IntPtr pHandle)
        {
            return BitConverter.ToInt16(ReadBytes(pHandle, address, 2), 0);
        }

        public static short ReadShort(IntPtr handle, long address)
        {
            return BitConverter.ToInt16(ReadBytes(handle, address, 2), 0);
        }

        public static int ReadInt32(long address, IntPtr pHandle)
        {
            return BitConverter.ToInt32(ReadBytes(pHandle, address, 4), 0);
        }

        public static int ReadInt32(IntPtr handle, long address)
        {
            return BitConverter.ToInt32(ReadBytes(handle, address, 4), 0);
        }

        public static uint ReadUInt32(long address, IntPtr pHandle)
        {
            return BitConverter.ToUInt32(ReadBytes(pHandle, address, 4), 0);
        }

        public static uint ReadUInt32(IntPtr handle, long address)
        {
            return BitConverter.ToUInt32(ReadBytes(handle, address, 4), 0);
        }

        public static ulong ReadUInt64(long address, IntPtr pHandle)
        {
            return BitConverter.ToUInt64(ReadBytes(pHandle, address, 8), 0);
        }

        public static ulong ReadUInt64(IntPtr handle, long address)
        {
            return BitConverter.ToUInt64(ReadBytes(handle, address, 8), 0);
        }

        public static int ReadInt(long address, IntPtr pHandle)
        {
            return BitConverter.ToInt32(ReadBytes(pHandle, address, 4), 0);
        }

        public static int ReadInt(IntPtr handle, long address)
        {
            return BitConverter.ToInt32(ReadBytes(handle, address, 4), 0);
        }

        public static double ReadDouble(long address, IntPtr pHandle)
        {
            return BitConverter.ToDouble(ReadBytes(pHandle, address, 8), 0);
        }

        public static double ReadDouble(IntPtr handle, long address)
        {
            return BitConverter.ToDouble(ReadBytes(handle, address, 8), 0);
        }

        public static string ReadString(long address, IntPtr pHandle)
        {
            return ReadString(pHandle, address, 0);
        }

        public static string ReadString(IntPtr handle, long address)
        {
            return ReadString(handle, address, 0);
        }

        public static string ReadString(long address, uint length, IntPtr pHandle)
        {
            return ReadString(pHandle, address, length);
        }

        public static string ReadString(IntPtr handle, long address, uint length)
        {
            if (length > 0)
            {
                byte[] buffer;
                buffer = ReadBytes(handle, address, length);
                return System.Text.ASCIIEncoding.Default.GetString(buffer).Split(new Char())[0];
            }
            else
            {
                string s = "";
                byte temp = ReadByte(handle, address++);
                while (temp != 0)
                {
                    s += (char)temp;
                    temp = ReadByte(handle, address++);
                }
                return s;
            }
        }

        public static bool WriteBytes(long address, byte[] bytes, uint length, IntPtr pHandle)
        {
            IntPtr bytesWritten;
            int result = NativeMethods.NativeMethods.WriteProcessMemory(pHandle, new IntPtr(address), bytes, (IntPtr)length, out bytesWritten);
            return result != 0;
        }

        public static bool WriteBytes(IntPtr handle, long address, byte[] bytes, uint length)
        {
            IntPtr bytesWritten;
            int result = NativeMethods.NativeMethods.WriteProcessMemory(handle, new IntPtr(address), bytes, 
                (IntPtr)length, out bytesWritten);
            return result != 0;
        }

        public static bool WriteInt32(long address, int value, IntPtr pHandle)
        {
            return WriteBytes(pHandle, address, BitConverter.GetBytes(value), 4);
        }

        public static bool WriteInt32(IntPtr handle, long address, int value)
        {
            return WriteBytes(handle, address, BitConverter.GetBytes(value), 4);
        }

        public static bool WriteUInt32(long address, uint value, IntPtr pHandle)
        {
            return WriteBytes(pHandle, address, BitConverter.GetBytes(value), 4);
        }

        public static bool WriteUInt32(IntPtr handle, long address, uint value)
        {
            return WriteBytes(handle, address, BitConverter.GetBytes(value), 4);
        }

        public static bool WriteUInt64(long address, ulong value, IntPtr pHandle)
        {
            return WriteBytes(pHandle, address, BitConverter.GetBytes(value), 8);
        }

        public static bool WriteUInt64(IntPtr handle, long address, ulong value)
        {
            return WriteBytes(handle, address, BitConverter.GetBytes(value), 8);
        }

        public static bool WriteInt16(long address, short value, IntPtr pHandle)
        {
            return WriteBytes(pHandle, address, BitConverter.GetBytes(value), 2);
        }

        public static bool WriteInt16(IntPtr handle, long address, short value)
        {
            return WriteBytes(handle, address, BitConverter.GetBytes(value), 2);
        }

        public static bool WriteUInt16(long address, ushort value, IntPtr pHandle)
        {
            return WriteBytes(pHandle, address, BitConverter.GetBytes(value), 2);
        }

        public static bool WriteUInt16(IntPtr handle, long address, ushort value)
        {
            return WriteBytes(handle, address, BitConverter.GetBytes(value), 2);
        }

        public static bool WriteInt(long address, int value, IntPtr pHandle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return WriteBytes(pHandle, address, bytes, 4);
        }

        public static bool WriteInt(IntPtr handle, long address, int value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return WriteBytes(handle, address, bytes, 4);
        }

        public static bool WriteDouble(long address, double value, IntPtr pHandle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return WriteBytes(pHandle, address, bytes, 8);
        }

        public static bool WriteDouble(IntPtr handle, long address, double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            return WriteBytes(handle, address, bytes, 8);
        }

        public static bool WriteByte(long address, byte value, IntPtr pHandle)
        {
            return WriteBytes(pHandle, address, new byte[] { value }, 1);
        }

        public static bool WriteByte(IntPtr handle, long address, byte value)
        {
            return WriteBytes(handle, address, new byte[] { value }, 1);
        }

        public static bool WriteString(long address, string str, IntPtr pHandle)
        {
            return WriteString(pHandle, address, str);
        }

        public static bool WriteString(IntPtr handle, long address, string str)
        {
            str += '\0';
            byte[] bytes = System.Text.ASCIIEncoding.Default.GetBytes(str);
            return WriteBytes(handle, address, bytes, (uint)bytes.Length);
        }

        public static bool WriteStringNoEncoding(IntPtr handle, long address, string str)
        {
            str += '\0';
            byte[] bytes = str.ToByteArray();
            return WriteBytes(handle, address, bytes, (uint)bytes.Length);
        }

        public static byte[] ToByteArray(this string s)
        {
            List<byte> value = new List<byte>();
            foreach (char c in s.ToCharArray())
                value.Add(c.ToByte());
            return value.ToArray();
        }

        public static byte ToByte(this char value)
        {
            return (byte)value;
        }

        #endregion
    }
}

