using System;
using System.Net;
using System.Net.Sockets;

namespace Plugin.Core.Utility
{
	public class StateObject
	{
		public Socket WorkSocket;

		public EndPoint RemoteEP;

		public const int TcpBufferSize = 8912;

		public const int UdpBufferSize = 8096;

		public byte[] TcpBuffer = new byte[8912];

		public byte[] UdpBuffer = new byte[8096];

		public StateObject()
		{
		}
	}
}