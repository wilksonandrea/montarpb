using System;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200082B RID: 2091
	internal class RegisteredChannel
	{
		// Token: 0x06005993 RID: 22931 RVA: 0x0013BCE8 File Offset: 0x00139EE8
		internal RegisteredChannel(IChannel chnl)
		{
			this.channel = chnl;
			this.flags = 0;
			if (chnl is IChannelSender)
			{
				this.flags |= 1;
			}
			if (chnl is IChannelReceiver)
			{
				this.flags |= 2;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06005994 RID: 22932 RVA: 0x0013BD37 File Offset: 0x00139F37
		internal virtual IChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x06005995 RID: 22933 RVA: 0x0013BD3F File Offset: 0x00139F3F
		internal virtual bool IsSender()
		{
			return (this.flags & 1) > 0;
		}

		// Token: 0x06005996 RID: 22934 RVA: 0x0013BD4C File Offset: 0x00139F4C
		internal virtual bool IsReceiver()
		{
			return (this.flags & 2) > 0;
		}

		// Token: 0x040028D1 RID: 10449
		private IChannel channel;

		// Token: 0x040028D2 RID: 10450
		private byte flags;

		// Token: 0x040028D3 RID: 10451
		private const byte SENDER = 1;

		// Token: 0x040028D4 RID: 10452
		private const byte RECEIVER = 2;
	}
}
