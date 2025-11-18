using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GM_LOG_ROOM_REQ : GameClientPacket
{
	private int int_0;

	public override void Read()
	{
		int_0 = ReadC();
	}

	public override void Run()
	{
		try
		{
			Account player = Client.Player;
			if (player != null && player.IsGM())
			{
				RoomModel room = player.Room;
				if (room != null && room.GetPlayerBySlot(int_0, out var Player))
				{
					Client.SendPacket(new PROTOCOL_GM_LOG_ROOM_ACK(0u, Player));
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_GM_LOG_ROOM_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
