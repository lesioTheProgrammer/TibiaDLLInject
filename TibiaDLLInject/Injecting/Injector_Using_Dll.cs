using System;
using System.IO;
using TibiaDLLInject.NativeMethods;

namespace TibiaDLLInject.Injecting
{
    class Injector_Using_Dll
    {
        #region Singleton

        private static Injector_Using_Dll _instance = new Injector_Using_Dll();

        public static Injector_Using_Dll Instance
        {
            get { return _instance; }
        }

        #endregion

        IntPtr ProcessHandle;


        public void Inject(string processName, int index)
        {
            TibiaProcess tibiaProcess = new TibiaProcess();
            var TibianProcess  = tibiaProcess.GetfProcess(processName, index);

            string dllPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath.ToString(), "exCore.dll");
            ProcessHandle = NativeMethods.NativeMethods.OpenProcess(NativeMethods.NativeMethods.PROCESS_ALL_ACCESS, 0, (uint)TibianProcess.Id);
            Inject(dllPath);
        }

        public bool Inject(string filename)
        {

            // I have to find dll later on ;c
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Dll to inject does not exist: " + filename);
            }

            IntPtr remoteAddress = NativeMethods.NativeMethods.VirtualAllocEx(
                ProcessHandle,
                IntPtr.Zero,
                (uint)filename.Length,
                NativeMethods.NativeMethods.AllocationType.Commit | NativeMethods.NativeMethods.AllocationType.Reserve,
                NativeMethods.NativeMethods.MemoryProtection.ExecuteReadWrite);

            Memory.WriteStringNoEncoding(ProcessHandle, remoteAddress.ToInt32(), filename);

            IntPtr thread = NativeMethods.NativeMethods.CreateRemoteThread(
                ProcessHandle, IntPtr.Zero, 0,
                NativeMethods.NativeMethods.GetProcAddress(NativeMethods.NativeMethods.GetModuleHandle("Kernel32"), "LoadLibraryA"),
                remoteAddress, 0, IntPtr.Zero);

            NativeMethods.NativeMethods.WaitForSingleObject(thread, 0xFFFFFFFF);

            NativeMethods.NativeMethods.VirtualFreeEx(
                ProcessHandle,
                remoteAddress,
                (uint)filename.Length,
                NativeMethods.NativeMethods.AllocationType.Release);

            return thread.ToInt32() > 0 && remoteAddress.ToInt32() > 0;
        }
    }
}
