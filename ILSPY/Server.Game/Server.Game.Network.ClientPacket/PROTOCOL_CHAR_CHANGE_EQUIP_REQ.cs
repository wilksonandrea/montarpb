using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Server.Game.Data.Models;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_CHAR_CHANGE_EQUIP_REQ : GameClientPacket
{
	private int int_0;

	private int int_1;

	private bool bool_0;

	private bool bool_1;

	private bool bool_2;

	private bool bool_3;

	private bool bool_4;

	private readonly int[] int_2 = new int[2];

	private readonly int[] int_3 = new int[14];

	private readonly SortedList<int, int> sortedList_0 = new SortedList<int, int>();

	private readonly SortedList<int, int> sortedList_1 = new SortedList<int, int>();

	private readonly SortedList<int, int> sortedList_2 = new SortedList<int, int>();

	public override void Read()
	{
		int_0 = ReadD();
		ReadUD();
		bool_0 = ReadC() == 1;
		byte b = ReadC();
		for (byte b2 = 0; b2 < b; b2 = (byte)(b2 + 1))
		{
			int value = ReadD();
			sortedList_0.Add(b2, value);
		}
		bool_1 = ReadC() == 1;
		ReadC();
		byte b3 = ReadC();
		for (byte b4 = 0; b4 < b3; b4 = (byte)(b4 + 1))
		{
			int value2 = ReadD();
			sortedList_1.Add(b4, value2);
		}
		bool_2 = ReadC() == 1;
		ReadC();
		ReadC();
		int_3[0] = ReadD();
		ReadUD();
		int_3[1] = ReadD();
		ReadUD();
		int_3[2] = ReadD();
		ReadUD();
		int_3[3] = ReadD();
		ReadUD();
		int_3[4] = ReadD();
		ReadUD();
		int_1 = ReadD();
		ReadUD();
		int_3[5] = ReadD();
		ReadUD();
		int_3[6] = ReadD();
		ReadUD();
		int_3[7] = ReadD();
		ReadUD();
		int_3[8] = ReadD();
		ReadUD();
		int_3[9] = ReadD();
		ReadUD();
		int_3[10] = ReadD();
		ReadUD();
		int_3[11] = ReadD();
		ReadUD();
		int_3[12] = ReadD();
		ReadUD();
		int_3[13] = ReadD();
		ReadUD();
		bool_3 = ReadC() == 1;
		byte b5 = ReadC();
		for (byte b6 = 0; b6 < b5; b6 = (byte)(b6 + 1))
		{
			int value3 = ReadD();
			ReadUD();
			sortedList_2.Add(b6, value3);
		}
		bool_4 = ReadC() == 1;
		ReadC();
		int_2[0] = ReadC();
		int_2[1] = ReadC();
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
			if (player.Character.Characters.Count > 0)
			{
				if (bool_0)
				{
					AllUtils.ValidateAccesoryEquipment(player, int_0);
				}
				if (bool_1)
				{
					AllUtils.ValidateDisabledCoupon(player, sortedList_0);
				}
				if (bool_2)
				{
					AllUtils.ValidateEnabledCoupon(player, sortedList_1);
				}
				if (bool_3)
				{
					AllUtils.ValidateCharacterEquipment(player, player.Equipment, int_3, int_1, int_2);
				}
				if (bool_4)
				{
					AllUtils.ValidateItemEquipment(player, sortedList_2);
				}
				AllUtils.ValidateCharacterSlot(player, player.Equipment, int_2);
			}
			RoomModel room = player.Room;
			if (room != null)
			{
				AllUtils.UpdateSlotEquips(player, room);
			}
			Client.SendPacket(new PROTOCOL_CHAR_CHANGE_EQUIP_ACK(0u));
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}
}
