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
                Tibia_Debug.Log("Error code");
                Tibia_Debug.Log("\n" + errorCode);
                return buffer;
            }
            return buffer;
        }

        public int GetInt32(IntPtr pHandle, Int32 address)
        {
            return BitConverter.ToInt32(ReadBytes(pHandle, new IntPtr(address), 4), 0);
        }


        public string GetString(IntPtr pHandle, Int32 address)
        {
            return BitConverter.ToString(ReadBytes(pHandle, new IntPtr(address), 30), 0);
        }

        // for long items
        #region
        private byte[] ReadBytesLong(IntPtr pHandle, IntPtr address, int bytesToRead)
        {
            int bytesRead = 0;
            byte[] buffer = new byte[bytesToRead];
            ReadProcessMemory(pHandle, address, buffer, bytesToRead, ref bytesRead);

            // error handling
            if (buffer[0] == 0)
            {
                var procesID = Process.GetCurrentProcess().Id;
                var errorCode = GetLastError();
                Tibia_Debug.Log("Error code");
                Tibia_Debug.Log("\n" + errorCode);
                return buffer;
            }
            return buffer;
        }


      

        #endregion



        // theres issue with this class, get weird after some time
        public int GetHealth(IntPtr pHandle, Int32 address)
        {
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


        private string ConvertHexToString(string hexString)
        {
            string valueToReturn = "";
            string[] hexValuesSplit = hexString.Split('-');

            foreach (String hex in hexValuesSplit)
            {
                // Convert the number expressed in base-16 to an integer. 
                int value = Convert.ToInt32(hex, 16);
                // Get the character corresponding to the integral value. 
                char charValue = (char)value;
                if (charValue.ToString() == "\0")
                {
                    break;
                }
                valueToReturn += charValue;
            }
            return valueToReturn;
        }


        //public string GetArmorFromList(IntPtr pHandle, Int32 address)
        //{
        //    return ConvertHexToString(GetString(pHandle, address + (int)ADDRESSES.ArmorAddress));
        //}

        public IList<string> GetFirstDialogBoxList(IntPtr pHandle, Int32 address)
        {

            var listOfItemsMArket = new List<string>();
            var listAddresses = GetMarketTopEnums.ListOfTopLeftMarket();
            int i = 0;
            foreach (var item in listAddresses)
            {
                try
                {
                    listOfItemsMArket.Add(ConvertHexToString(GetString(pHandle,
                        address + (int)listAddresses[i])) );
                    i++;
                }
                catch (Exception ex)
                {
                    var exMessage = ex.Message;
                    throw;
                }
            }
            return listOfItemsMArket;
        }

    }
}
