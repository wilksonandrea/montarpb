using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ : GameClientPacket
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
			RoomModel room = player.Room;
			if (room == null || !room.GetSlot(int_0, out var Slot))
			{
				return;
			}
			Account playerBySlot = room.GetPlayerBySlot(Slot);
			if (playerBySlot != null)
			{
				if (player.Nickname != playerBySlot.Nickname)
				{
					player.FindPlayer = playerBySlot.Nickname;
				}
				int int_ = int.MaxValue;
				switch (room.ValidateTeam(Slot.Team, Slot.CostumeTeam))
				{
				case TeamEnum.FR_TEAM:
					int_ = playerBySlot.Equipment.CharaRedId;
					break;
				case TeamEnum.CT_TEAM:
					int_ = playerBySlot.Equipment.CharaBlueId;
					break;
				}
				Client.SendPacket(new PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(0u, playerBySlot, int_));
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BASE_GET_ROOM_USER_DETAIL_INFO_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
