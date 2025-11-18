using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_USER_SOPETYPE_REQ : GameClientPacket
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
			if (player == null)
			{
				return;
			}
			RoomModel room = player.Room;
			if (room == null)
			{
				return;
			}
			player.Sight = int_0;
			using (new PROTOCOL_BATTLE_USER_SOPETYPE_ACK(player))
			{
				room.SendPacketToPlayers(new PROTOCOL_BATTLE_USER_SOPETYPE_ACK(player));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_USER_SOPETYPE_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
