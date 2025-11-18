using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ : GameClientPacket
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
			if (player != null)
			{
				RoomModel room = player.Room;
				if (room != null && room.GetPlayerBySlot(int_0, out var Player))
				{
					Client.SendPacket(new PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_ACK(Player));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_ROOM_GET_ACEMODE_PLAYERINFO_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
