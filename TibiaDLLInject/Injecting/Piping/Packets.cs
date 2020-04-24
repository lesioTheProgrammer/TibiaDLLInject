using System;
using System.Collections.Generic;
using System.Text;

namespace TibiaDLLInject.Injecting.Piping
{
    public class Packets
    {
        #region Singleton

        private static Packets _instance = new Packets();

        public static Packets Instance
        {
            get { return _instance; }
        }

        #endregion

        #region Delegates & Events

        public event EventHandler<PacketReceivedEventArgs> PacketReceived;

        protected virtual void OnPacketReceived(PacketReceivedEventArgs e)
        {
            if (PacketReceived != null)
            {
                lastRecievedPacket = e.Packet;
            }
        }

        #endregion

        #region Fields

        private Collection<Packet> _lastPackets = new Collection<Packet>();
        private Packet lastRecievedPacket = new Packet();

        #endregion

        #region Constructor

        public Packets()
        {
            PacketReceived += new EventHandler<PacketReceivedEventArgs>(Packets_PacketReceived);
        }

        void Packets_PacketReceived(object sender, PacketReceivedEventArgs e)
        {
            lastRecievedPacket = e.Packet;
        }

        #endregion

        #region Methods

        public void LogPacket(Packet p)
        {
            OnPacketReceived(new PacketReceivedEventArgs(p));
            if (p.Type == PacketType.ContainerOpen)
                Helpers.Debug.WriteLine("Container has been opened", ConsoleColor.Red);
            Helpers.Debug.WriteLine(string.Format("Lenght: {0} Packet: {1}", p.Length, p.Body.ToStringFast()), ConsoleColor.DarkBlue);
        }

        public bool WaitForPacket(PacketType packettype, int maxTime)
        {
            Stopwatch watcher = new Stopwatch();
            watcher.Start();

            while (watcher.ElapsedMilliseconds < maxTime)
            {
                if (lastRecievedPacket.Type == packettype) return true; //quitting, and will return true
                Thread.Sleep(2);
            }
            return false;
        }

        #endregion
    }
}
