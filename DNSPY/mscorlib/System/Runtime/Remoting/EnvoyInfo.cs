using System;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007BD RID: 1981
	[Serializable]
	internal sealed class EnvoyInfo : IEnvoyInfo
	{
		// Token: 0x060055A5 RID: 21925 RVA: 0x0012FEDC File Offset: 0x0012E0DC
		[SecurityCritical]
		internal static IEnvoyInfo CreateEnvoyInfo(ServerIdentity serverID)
		{
			IEnvoyInfo envoyInfo = null;
			if (serverID != null)
			{
				if (serverID.EnvoyChain == null)
				{
					serverID.RaceSetEnvoyChain(serverID.ServerContext.CreateEnvoyChain(serverID.TPOrObject));
				}
				if (!(serverID.EnvoyChain is EnvoyTerminatorSink))
				{
					envoyInfo = new EnvoyInfo(serverID.EnvoyChain);
				}
			}
			return envoyInfo;
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x0012FF2A File Offset: 0x0012E12A
		[SecurityCritical]
		private EnvoyInfo(IMessageSink sinks)
		{
			this.EnvoySinks = sinks;
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x060055A7 RID: 21927 RVA: 0x0012FF39 File Offset: 0x0012E139
		// (set) Token: 0x060055A8 RID: 21928 RVA: 0x0012FF41 File Offset: 0x0012E141
		public IMessageSink EnvoySinks
		{
			[SecurityCritical]
			get
			{
				return this.envoySinks;
			}
			[SecurityCritical]
			set
			{
				this.envoySinks = value;
			}
		}

		// Token: 0x04002771 RID: 10097
		private IMessageSink envoySinks;
	}
}
