using System;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007BC RID: 1980
	[Serializable]
	internal sealed class ChannelInfo : IChannelInfo
	{
		// Token: 0x060055A2 RID: 21922 RVA: 0x0012FEB6 File Offset: 0x0012E0B6
		[SecurityCritical]
		internal ChannelInfo()
		{
			this.ChannelData = ChannelServices.CurrentChannelData;
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x060055A3 RID: 21923 RVA: 0x0012FEC9 File Offset: 0x0012E0C9
		// (set) Token: 0x060055A4 RID: 21924 RVA: 0x0012FED1 File Offset: 0x0012E0D1
		public object[] ChannelData
		{
			[SecurityCritical]
			get
			{
				return this.channelData;
			}
			[SecurityCritical]
			set
			{
				this.channelData = value;
			}
		}

		// Token: 0x04002770 RID: 10096
		private object[] channelData;
	}
}
