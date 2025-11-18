using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_LEAVE_REQ : GameClientPacket
{
	private uint uint_0;

	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player == null)
			{
				return;
			}
			ChannelModel channel = player.GetChannel();
			if (player.Room == null && player.Match == null)
			{
				if (channel == null || player.Session == null || !channel.RemovePlayer(player))
				{
					uint_0 = 2147483648u;
				}
				Client.SendPacket(new PROTOCOL_LOBBY_LEAVE_ACK(uint_0));
				if (uint_0 == 0)
				{
					player.ResetPages();
					player.Status.UpdateChannel(byte.MaxValue);
					AllUtils.SyncPlayerToFriends(player, all: false);
					AllUtils.SyncPlayerToClanMembers(player);
				}
				else
				{
					Client.Close(1000, DestroyConnection: true);
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_LOBBY_LEAVE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
