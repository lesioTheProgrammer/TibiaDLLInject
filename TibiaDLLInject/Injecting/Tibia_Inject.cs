using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TibiaDLLInject
{
     public class Tibia_Inject
    {
        private readonly IntPtr m_cHandle;
        private readonly IntPtr m_baseAddress;
        private readonly IntPtr m_cHWND;

        public IntPtr cHandle { get { return m_cHandle; } }
        public IntPtr baseAddress { get { return m_baseAddress; } }
        public IntPtr cHWND { get { return m_cHWND; } }

        
        public Tibia_Inject(Process proc)
        {
            this.m_cHandle = proc.Handle; // cHandle changes during program runtime
            this.m_baseAddress = proc.MainModule.BaseAddress;
            this.m_cHWND = proc.MainWindowHandle;
        }
    }
}
