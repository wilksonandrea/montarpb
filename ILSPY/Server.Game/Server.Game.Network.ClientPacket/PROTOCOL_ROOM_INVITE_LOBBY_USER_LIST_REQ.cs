using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_REQ : GameClientPacket
{
	public override void Read()
	{
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null)
			{
				ChannelModel channel = player.GetChannel();
				if (channel != null)
				{
					Client.SendPacket(new PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK(channel));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_ROOM_GET_LOBBY_USER_LIST_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
