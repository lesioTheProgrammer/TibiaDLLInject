using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;

namespace TibiaDLLInject.Injecting.Piping
{
    public class PipeClient
    {

        #region Singleton

        //static readonly PipeClient _instance = new PipeClient();

        //public static PipeClient Instance
        //{
        //    get { return _instance; }
        //}

        #endregion

        #region Fields

        private NamedPipeClientStream _pipe;
        private string _pipeName = "";

        #endregion

        #region Properties

        public NamedPipeClientStream Pipe
        {
            get { return _pipe; }
            set { _pipe = value; }
        }
        public string PipeName
        {
            get { return _pipeName; }
            set { _pipeName = value; }
        }

        #endregion

        #region Constructor

        public PipeClient(int gameClientProcessId)
        {
            PipeName = string.Format("exTibiaC{0}", gameClientProcessId);
            Pipe = new NamedPipeClientStream(".", PipeName, PipeDirection.InOut);
        }

        #endregion

        #region Methods

        public bool Connect()
        {
            try
            {
                Pipe.Connect(10000);
                if (Pipe.IsConnected)
                    return true;
                return false;
            }
            catch (TimeoutException te)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Write(byte[] data)
        {
            if (Pipe.IsConnected && Pipe.CanWrite)
            {
                try
                {
                    Pipe.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {
                }
            }
        }

        //public void Send(NetworkMessage message)
        //{
        //    Write(message.Data);
        //}

        #endregion
    }
}
