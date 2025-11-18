using System;
using System.Net;
using System.Net.Sockets;

namespace Plugin.Core.Utility
{
	// Token: 0x02000038 RID: 56
	public class StateObject
	{
		// Token: 0x060001EB RID: 491 RVA: 0x00002C6D File Offset: 0x00000E6D
		public StateObject()
		{
		}

		// Token: 0x0400009C RID: 156
		public Socket WorkSocket;

		// Token: 0x0400009D RID: 157
		public EndPoint RemoteEP;

		// Token: 0x0400009E RID: 158
		public const int TcpBufferSize = 8912;

		// Token: 0x0400009F RID: 159
		public const int UdpBufferSize = 8096;

		// Token: 0x040000A0 RID: 160
		public byte[] TcpBuffer = new byte[8912];

		// Token: 0x040000A1 RID: 161
		public byte[] UdpBuffer = new byte[8096];
	}
}
