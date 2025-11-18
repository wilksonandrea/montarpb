using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x02000823 RID: 2083
	internal class LeaseSink : IMessageSink
	{
		// Token: 0x0600594C RID: 22860 RVA: 0x0013A58B File Offset: 0x0013878B
		public LeaseSink(Lease lease, IMessageSink nextSink)
		{
			this.lease = lease;
			this.nextSink = nextSink;
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x0013A5A1 File Offset: 0x001387A1
		[SecurityCritical]
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.lease.RenewOnCall();
			return this.nextSink.SyncProcessMessage(msg);
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x0013A5BA File Offset: 0x001387BA
		[SecurityCritical]
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			this.lease.RenewOnCall();
			return this.nextSink.AsyncProcessMessage(msg, replySink);
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x0600594F RID: 22863 RVA: 0x0013A5D4 File Offset: 0x001387D4
		public IMessageSink NextSink
		{
			[SecurityCritical]
			get
			{
				return this.nextSink;
			}
		}

		// Token: 0x040028AF RID: 10415
		private Lease lease;

		// Token: 0x040028B0 RID: 10416
		private IMessageSink nextSink;
	}
}
