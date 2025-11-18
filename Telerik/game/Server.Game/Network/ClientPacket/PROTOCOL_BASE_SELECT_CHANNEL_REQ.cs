using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ClientPacket
{
	public class PROTOCOL_BASE_SELECT_CHANNEL_REQ : GameClientPacket
	{
		private int int_0;

		public PROTOCOL_BASE_SELECT_CHANNEL_REQ()
		{
		}

		public override void Read()
		{
			base.ReadB(4);
			this.int_0 = base.ReadH();
		}

		public override void Run()
		{
			try
			{
				Account player = this.Client.Player;
				if (player != null && player.ChannelId < 0)
				{
					ChannelModel channel = ChannelsXML.GetChannel(this.Client.ServerId, this.int_0);
					if (channel == null)
					{
						this.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(-2147483648, -1, -1));
					}
					else if (AllUtils.ChannelRequirementCheck(player, channel))
					{
						this.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(-2147483134, -1, -1));
					}
					else if (channel.Players.Count < SChannelXML.GetServer(this.Client.ServerId).ChannelPlayers)
					{
						player.ServerId = channel.ServerId;
						player.ChannelId = channel.Id;
						this.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(0, player.ServerId, player.ChannelId));
						this.Client.SendPacket(new PROTOCOL_CS_MEDAL_INFO_ACK());
						player.Status.UpdateServer((byte)player.ServerId);
						player.Status.UpdateChannel((byte)player.ChannelId);
						player.UpdateCacheInfo();
					}
					else
					{
						this.Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(-2147483135, -1, -1));
					}
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(string.Concat("PROTOCOL_BASE_SELECT_CHANNEL_REQ: ", exception.Message), LoggerType.Error, exception);
			}
		}
	}
}