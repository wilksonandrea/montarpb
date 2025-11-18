using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_ROOM_CHANGE_PASSWD_REQ : GameClientPacket
{
	private string string_0;

	public override void Read()
	{
		string_0 = ReadS(4);
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
			if (room == null || room.LeaderSlot != player.SlotId || !(room.Password != string_0))
			{
				return;
			}
			room.Password = string_0;
			using PROTOCOL_ROOM_CHANGE_PASSWD_ACK packet = new PROTOCOL_ROOM_CHANGE_PASSWD_ACK(string_0);
			room.SendPacketToPlayers(packet);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
