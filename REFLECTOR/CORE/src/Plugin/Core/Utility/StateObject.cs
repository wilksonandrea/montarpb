namespace Plugin.Core.Utility
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    public class StateObject
    {
        public Socket WorkSocket;
        public EndPoint RemoteEP;
        public const int TcpBufferSize = 0x22d0;
        public const int UdpBufferSize = 0x1fa0;
        public byte[] TcpBuffer = new byte[0x22d0];
        public byte[] UdpBuffer = new byte[0x1fa0];
    }
}

