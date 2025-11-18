using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Auth.Data.Models;
using Server.Auth.Data.XML;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Network.ClientPacket
{
	// Token: 0x0200003D RID: 61
	public class PROTOCOL_BASE_GET_CHANNELLIST_REQ : AuthClientPacket
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000073F4 File Offset: 0x000055F4
		public override void Run()
		{
			try
			{
				if (this.Client.Player != null)
				{
					List<ChannelModel> channels = ChannelsXML.GetChannels(this.int_0);
					if (channels.Count == 11)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_GET_CHANNELLIST_ACK(SChannelXML.GetServer(this.int_0), channels));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002A1D File Offset: 0x00000C1D
		public PROTOCOL_BASE_GET_CHANNELLIST_REQ()
		{
		}

		// Token: 0x04000072 RID: 114
		private int int_0;
	}
}
