using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket
{
	// Token: 0x02000157 RID: 343
	public class PROTOCOL_BASE_SELECT_CHANNEL_REQ : GameClientPacket
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00005091 File Offset: 0x00003291
		public override void Read()
		{
			base.ReadB(4);
			this.int_0 = (int)base.ReadH();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001A61C File Offset: 0x0001881C
		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.ChannelId < 0)
				{
					ChannelModel channel = ChannelsXML.GetChannel(this.Client.ServerId, this.int_0);
					if (channel != null)
					{
						if (AllUtils.ChannelRequirementCheck(player, channel))
						{
							this.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(2147484162U, -1, -1));
						}
						else if (channel.Players.Count >= SChannelXML.GetServer(this.Client.ServerId).ChannelPlayers)
						{
							this.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(2147484161U, -1, -1));
						}
						else
						{
							player.ServerId = channel.ServerId;
							player.ChannelId = channel.Id;
							this.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(0U, player.ServerId, player.ChannelId));
							this.Client.SendPacket(new PROTOCOL_CS_MEDAL_INFO_ACK());
							player.Status.UpdateServer((byte)player.ServerId);
							player.Status.UpdateChannel((byte)player.ChannelId);
							player.UpdateCacheInfo();
						}
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(2147483648U, -1, -1));
					}
				}
			}
			catch (Exception ex)
			{
				CLogger.Print("PROTOCOL_BASE_SELECT_CHANNEL_REQ: " + ex.Message, LoggerType.Error, ex);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00004CD2 File Offset: 0x00002ED2
		public PROTOCOL_BASE_SELECT_CHANNEL_REQ()
		{
		}

		// Token: 0x04000276 RID: 630
		private int int_0;
	}
}
