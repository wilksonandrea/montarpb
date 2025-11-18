using System;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200082C RID: 2092
	internal class RegisteredChannelList
	{
		// Token: 0x06005997 RID: 22935 RVA: 0x0013BD59 File Offset: 0x00139F59
		internal RegisteredChannelList()
		{
			this._channels = new RegisteredChannel[0];
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x0013BD6D File Offset: 0x00139F6D
		internal RegisteredChannelList(RegisteredChannel[] channels)
		{
			this._channels = channels;
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06005999 RID: 22937 RVA: 0x0013BD7C File Offset: 0x00139F7C
		internal RegisteredChannel[] RegisteredChannels
		{
			get
			{
				return this._channels;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x0600599A RID: 22938 RVA: 0x0013BD84 File Offset: 0x00139F84
		internal int Count
		{
			get
			{
				if (this._channels == null)
				{
					return 0;
				}
				return this._channels.Length;
			}
		}

		// Token: 0x0600599B RID: 22939 RVA: 0x0013BD98 File Offset: 0x00139F98
		internal IChannel GetChannel(int index)
		{
			return this._channels[index].Channel;
		}

		// Token: 0x0600599C RID: 22940 RVA: 0x0013BDA7 File Offset: 0x00139FA7
		internal bool IsSender(int index)
		{
			return this._channels[index].IsSender();
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x0013BDB6 File Offset: 0x00139FB6
		internal bool IsReceiver(int index)
		{
			return this._channels[index].IsReceiver();
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x0600599E RID: 22942 RVA: 0x0013BDC8 File Offset: 0x00139FC8
		internal int ReceiverCount
		{
			get
			{
				if (this._channels == null)
				{
					return 0;
				}
				int num = 0;
				for (int i = 0; i < this._channels.Length; i++)
				{
					if (this.IsReceiver(i))
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x0600599F RID: 22943 RVA: 0x0013BE04 File Offset: 0x0013A004
		internal int FindChannelIndex(IChannel channel)
		{
			for (int i = 0; i < this._channels.Length; i++)
			{
				if (channel == this.GetChannel(i))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060059A0 RID: 22944 RVA: 0x0013BE34 File Offset: 0x0013A034
		[SecurityCritical]
		internal int FindChannelIndex(string name)
		{
			for (int i = 0; i < this._channels.Length; i++)
			{
				if (string.Compare(name, this.GetChannel(i).ChannelName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x040028D5 RID: 10453
		private RegisteredChannel[] _channels;
	}
}
