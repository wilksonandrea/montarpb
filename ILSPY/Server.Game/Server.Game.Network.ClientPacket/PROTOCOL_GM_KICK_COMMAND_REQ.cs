using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_GM_KICK_COMMAND_REQ : GameClientPacket
{
	private byte byte_0;

	public override void Read()
	{
		byte_0 = ReadC();
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
			if (room != null)
			{
				Account playerBySlot = room.GetPlayerBySlot(byte_0);
				if (playerBySlot != null && !playerBySlot.IsGM())
				{
					room.RemovePlayer(playerBySlot, WarnAllPlayers: true);
				}
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(GetType().Name + ": " + ex.Message, LoggerType.Error, ex);
		}
	}
}
