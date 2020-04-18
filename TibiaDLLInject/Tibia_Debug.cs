using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TibiaDLLInject
{
    public static class Tibia_Debug
    {

        private static RichTextBox m_logSpace;
        public static RichTextBox LogSpace
        {
            get { return m_logSpace; }
            private set { m_logSpace = value; }
        }

        public static void Log(string msg)
        {
            m_logSpace.Text += DateTime.Now + "::" + msg + "\n";
        }

        public static void SetLogSpace(RichTextBox lSpace)
        {
            m_logSpace = lSpace;
        }


    }



}
