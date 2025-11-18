using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200084D RID: 2125
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ChannelDataStore : IChannelDataStore
	{
		// Token: 0x06005A2D RID: 23085 RVA: 0x0013D384 File Offset: 0x0013B584
		private ChannelDataStore(string[] channelUrls, DictionaryEntry[] extraData)
		{
			this._channelURIs = channelUrls;
			this._extraData = extraData;
		}

		// Token: 0x06005A2E RID: 23086 RVA: 0x0013D39A File Offset: 0x0013B59A
		public ChannelDataStore(string[] channelURIs)
		{
			this._channelURIs = channelURIs;
			this._extraData = null;
		}

		// Token: 0x06005A2F RID: 23087 RVA: 0x0013D3B0 File Offset: 0x0013B5B0
		[SecurityCritical]
		internal ChannelDataStore InternalShallowCopy()
		{
			return new ChannelDataStore(this._channelURIs, this._extraData);
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06005A30 RID: 23088 RVA: 0x0013D3C3 File Offset: 0x0013B5C3
		// (set) Token: 0x06005A31 RID: 23089 RVA: 0x0013D3CB File Offset: 0x0013B5CB
		public string[] ChannelUris
		{
			[SecurityCritical]
			get
			{
				return this._channelURIs;
			}
			set
			{
				this._channelURIs = value;
			}
		}

		// Token: 0x17000EFE RID: 3838
		public object this[object key]
		{
			[SecurityCritical]
			get
			{
				foreach (DictionaryEntry dictionaryEntry in this._extraData)
				{
					if (dictionaryEntry.Key.Equals(key))
					{
						return dictionaryEntry.Value;
					}
				}
				return null;
			}
			[SecurityCritical]
			set
			{
				if (this._extraData == null)
				{
					this._extraData = new DictionaryEntry[1];
					this._extraData[0] = new DictionaryEntry(key, value);
					return;
				}
				int num = this._extraData.Length;
				DictionaryEntry[] array = new DictionaryEntry[num + 1];
				int i;
				for (i = 0; i < num; i++)
				{
					array[i] = this._extraData[i];
				}
				array[i] = new DictionaryEntry(key, value);
				this._extraData = array;
			}
		}

		// Token: 0x04002903 RID: 10499
		private string[] _channelURIs;

		// Token: 0x04002904 RID: 10500
		private DictionaryEntry[] _extraData;
	}
}
