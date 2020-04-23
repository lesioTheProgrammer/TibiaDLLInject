using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TibiaDLLInject.Injecting
{
    public  class TibiaProcess
    {

         Tibia_Inject m_client;

        internal Tibia_Inject GetChangingIndexOfProcess(int index)
        {
            // mclient Update method
            if (index < 0 ||
               index >= Process.GetProcessesByName("tibia").Length)
                return m_client;

            m_client = new Tibia_Inject(GetfProcess("tibia", index));
            Tibia_Debug.Log("Injected into :: " + m_client.cHandle.ToString());
            return m_client;
        }

        internal Process GetfProcess(string name, int index)
        {
            return Process.GetProcessesByName(name)[index];
        }



    }
}
