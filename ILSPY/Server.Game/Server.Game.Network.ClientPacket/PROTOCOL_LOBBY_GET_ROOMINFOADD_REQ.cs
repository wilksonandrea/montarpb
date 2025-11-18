using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadD();
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
			if (channel != null)
			{
				RoomModel room = channel.GetRoom(int_0);
				if (room != null && room.GetLeader() != null)
				{
					Client.SendPacket(new PROTOCOL_LOBBY_GET_ROOMINFOADD_ACK(room));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_LOBBY_GET_ROOMINFOADD_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
