using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000140 RID: 320
	public class PROTOCOL_BASE_GET_CHANNELLIST_REQ : GameClientPacket
	{
		// Token: 0x0600031E RID: 798 RVA: 0x00004F84 File Offset: 0x00003184
		public override void Read()
		{
			this.int_0 = base.ReadD();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00018E58 File Offset: 0x00017058
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

		// Token: 0x06000320 RID: 800 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_GET_CHANNELLIST_REQ()
		{
		}

		// Token: 0x04000241 RID: 577
		private int int_0;
	}
}
