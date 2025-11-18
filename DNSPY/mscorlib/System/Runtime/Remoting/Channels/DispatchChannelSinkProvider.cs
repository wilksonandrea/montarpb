using System;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200083D RID: 2109
	internal class DispatchChannelSinkProvider : IServerChannelSinkProvider
	{
		// Token: 0x06005A02 RID: 23042 RVA: 0x0013D30E File Offset: 0x0013B50E
		internal DispatchChannelSinkProvider()
		{
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x0013D316 File Offset: 0x0013B516
		[SecurityCritical]
		public void GetChannelData(IChannelDataStore channelData)
		{
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x0013D318 File Offset: 0x0013B518
		[SecurityCritical]
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			return new DispatchChannelSink();
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06005A05 RID: 23045 RVA: 0x0013D31F File Offset: 0x0013B51F
		// (set) Token: 0x06005A06 RID: 23046 RVA: 0x0013D322 File Offset: 0x0013B522
		public IServerChannelSinkProvider Next
		{
			[SecurityCritical]
			get
			{
				return null;
			}
			[SecurityCritical]
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}
