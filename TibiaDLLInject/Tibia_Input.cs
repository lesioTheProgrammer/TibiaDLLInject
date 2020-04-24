using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace TibiaDLLInject
{
    class Tibia_Input
    {
        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, uint Msg,
            IntPtr wParam, IntPtr lParam);

        public IntPtr SendKeystroke(IntPtr hWnd, Keys k)
        {
            return PostMessage(hWnd, 0x100, (IntPtr)k, (IntPtr)0);
        }
    }
}
