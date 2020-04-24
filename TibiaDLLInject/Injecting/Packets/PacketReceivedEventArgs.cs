using System;
using System.Collections.Generic;
using System.Text;

namespace TibiaDLLInject.Injecting.Packets
{
    public class PacketReceivedEventArgs : EventArgs
    {
        private Packet _packet;

        public Packet Packet
        {
            get { return _packet; }
            set { _packet = value; }
        }

        public PacketReceivedEventArgs() { }

        public PacketReceivedEventArgs(Packet marketItem)
        {
            this._packet = marketItem;
        }

    }
}
