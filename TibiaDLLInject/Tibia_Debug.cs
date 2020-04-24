using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TibiaDLLInject
{
    public static class Tibia_Debug
    {

        private static TextBox m_logSpace;
        public static TextBox LogSpace
        {
            get { return m_logSpace; }
            private set { m_logSpace = value; }
        }

        public static void Log(string msg)
        {
            m_logSpace.Text += DateTime.Now + "::" + msg + "\n";
        }

        public static void SetLogSpace(TextBox lSpace)
        {
            m_logSpace = lSpace;
        }

        public static void ClearLogConsole()
        {
            m_logSpace.Clear();
        }

        public static void Log<T>(string msg, T key)
        {
            m_logSpace.Text += DateTime.Now + "::" + msg + "\n";
        }

        public static void LogExcepction(Exception ex)
        {
            m_logSpace.Text += "Exception: " + ex.ToString() + "\n";
        }


    }



}
