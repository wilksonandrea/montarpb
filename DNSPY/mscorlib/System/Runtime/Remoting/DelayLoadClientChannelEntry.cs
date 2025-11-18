using System;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007B2 RID: 1970
	internal class DelayLoadClientChannelEntry
	{
		// Token: 0x06005549 RID: 21833 RVA: 0x0012EE2A File Offset: 0x0012D02A
		internal DelayLoadClientChannelEntry(RemotingXmlConfigFileData.ChannelEntry entry, bool ensureSecurity)
		{
			this._entry = entry;
			this._channel = null;
			this._bRegistered = false;
			this._ensureSecurity = ensureSecurity;
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x0600554A RID: 21834 RVA: 0x0012EE4E File Offset: 0x0012D04E
		internal IChannelSender Channel
		{
			[SecurityCritical]
			get
			{
				if (this._channel == null && !this._bRegistered)
				{
					this._channel = (IChannelSender)RemotingConfigHandler.CreateChannelFromConfigEntry(this._entry);
					this._entry = null;
				}
				return this._channel;
			}
		}

		// Token: 0x0600554B RID: 21835 RVA: 0x0012EE83 File Offset: 0x0012D083
		internal void RegisterChannel()
		{
			ChannelServices.RegisterChannel(this._channel, this._ensureSecurity);
			this._bRegistered = true;
			this._channel = null;
		}

		// Token: 0x04002746 RID: 10054
		private RemotingXmlConfigFileData.ChannelEntry _entry;

		// Token: 0x04002747 RID: 10055
		private IChannelSender _channel;

		// Token: 0x04002748 RID: 10056
		private bool _bRegistered;

		// Token: 0x04002749 RID: 10057
		private bool _ensureSecurity;
	}
}
