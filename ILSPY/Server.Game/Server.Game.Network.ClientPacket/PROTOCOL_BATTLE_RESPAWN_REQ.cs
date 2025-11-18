using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Server;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_RESPAWN_REQ : GameClientPacket
{
	private int[] int_0;

	private int int_1;

	public override void Read()
	{
		int_0 = new int[16];
		int_0[0] = ReadD();
		ReadUD();
		int_0[1] = ReadD();
		ReadUD();
		int_0[2] = ReadD();
		ReadUD();
		int_0[3] = ReadD();
		ReadUD();
		int_0[4] = ReadD();
		ReadUD();
		int_0[5] = ReadD();
		ReadUD();
		int_0[6] = ReadD();
		ReadUD();
		int_0[7] = ReadD();
		ReadUD();
		int_0[8] = ReadD();
		ReadUD();
		int_0[9] = ReadD();
		ReadUD();
		int_0[10] = ReadD();
		ReadUD();
		int_0[11] = ReadD();
		ReadUD();
		int_0[12] = ReadD();
		ReadUD();
		int_0[13] = ReadD();
		ReadUD();
		int_0[14] = ReadD();
		ReadUD();
		int_1 = ReadH();
		int_0[15] = ReadD();
		ReadUD();
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
			if (room == null || room.State != RoomState.BATTLE)
			{
				return;
			}
			SlotModel slot = room.GetSlot(player.SlotId);
			if (slot == null || slot.State != SlotState.BATTLE)
			{
				return;
			}
			if (slot.DeathState.HasFlag(DeadEnum.Dead) || slot.DeathState.HasFlag(DeadEnum.UseChat))
			{
				slot.DeathState = DeadEnum.Alive;
			}
			PlayerEquipment playerEquipment = AllUtils.ValidateRespawnEQ(slot, int_0);
			if (playerEquipment != null)
			{
				ComDiv.CheckEquipedItems(playerEquipment, player.Inventory.Items, BattleRules: true);
				AllUtils.ClassicModeCheck(room, playerEquipment);
				slot.Equipment = playerEquipment;
				if ((int_1 & 8) > 0)
				{
					AllUtils.InsertItem(playerEquipment.WeaponPrimary, slot);
				}
				if ((int_1 & 4) > 0)
				{
					AllUtils.InsertItem(playerEquipment.WeaponSecondary, slot);
				}
				if ((int_1 & 2) > 0)
				{
					AllUtils.InsertItem(playerEquipment.WeaponMelee, slot);
				}
				if ((int_1 & 1) > 0)
				{
					AllUtils.InsertItem(playerEquipment.WeaponExplosive, slot);
				}
				AllUtils.InsertItem(playerEquipment.WeaponSpecial, slot);
				AllUtils.InsertItem(playerEquipment.PartHead, slot);
				AllUtils.InsertItem(playerEquipment.PartFace, slot);
				AllUtils.InsertItem(playerEquipment.BeretItem, slot);
				AllUtils.InsertItem(playerEquipment.AccessoryId, slot);
				int ıdStatics = ComDiv.GetIdStatics(int_0[5], 1);
				int ıdStatics2 = ComDiv.GetIdStatics(int_0[5], 2);
				int ıdStatics3 = ComDiv.GetIdStatics(int_0[5], 5);
				switch (ıdStatics)
				{
				case 6:
					if (ıdStatics2 != 1 && ıdStatics3 != 632)
					{
						if (ıdStatics2 == 2 || ıdStatics3 == 664)
						{
							AllUtils.InsertItem(playerEquipment.CharaBlueId, slot);
						}
					}
					else
					{
						AllUtils.InsertItem(playerEquipment.CharaRedId, slot);
					}
					break;
				case 15:
					AllUtils.InsertItem(playerEquipment.DinoItem, slot);
					break;
				}
			}
			using (PROTOCOL_BATTLE_RESPAWN_ACK packet = new PROTOCOL_BATTLE_RESPAWN_ACK(room, slot))
			{
				room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
			}
			if (slot.FirstRespawn)
			{
				slot.FirstRespawn = false;
				EquipmentSync.SendUDPPlayerSync(room, slot, player.Effects, 0);
			}
			else
			{
				EquipmentSync.SendUDPPlayerSync(room, slot, player.Effects, 2);
			}
			if (room.WeaponsFlag != (RoomWeaponsFlag)int_1)
			{
				CLogger.Print($"Player '{player.Nickname}' Weapon Flags Doesn't Match! (Room: {(int)room.WeaponsFlag}; Player: {int_1})", LoggerType.Warning);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_RESPAWN_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
