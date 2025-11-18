using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_SELECT_CHANNEL_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		ReadB(4);
		int_0 = ReadH();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null || player.ChannelId >= 0)
			{
				return;
			}
			ChannelModel channel = ChannelsXML.GetChannel(Client.ServerId, int_0);
			if (channel != null)
			{
				if (AllUtils.ChannelRequirementCheck(player, channel))
				{
					Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(2147484162u, -1, -1));
					return;
				}
				if (channel.Players.Count >= SChannelXML.GetServer(Client.ServerId).ChannelPlayers)
				{
					Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(2147484161u, -1, -1));
					return;
				}
				player.ServerId = channel.ServerId;
				player.ChannelId = channel.Id;
				Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(0u, player.ServerId, player.ChannelId));
				Client.SendPacket(new PROTOCOL_CS_MEDAL_INFO_ACK());
				player.Status.UpdateServer((byte)player.ServerId);
				player.Status.UpdateChannel((byte)player.ChannelId);
				player.UpdateCacheInfo();
			}
			else
			{
				Client.SendPacket(new PROTOCOL_BASE_SELECT_CHANNEL_ACK(2147483648u, -1, -1));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_SELECT_CHANNEL_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
