using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Client;
using Server.Game.Data.Utils;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Network.ClientPacket;

public class PROTOCOL_BATTLE_DEATH_REQ : GameClientPacket
{
	private FragInfos fragInfos_0;

	private bool bool_0;

	public override void Read()
	{
		fragInfos_0 = new FragInfos
		{
			KillingType = (CharaKillType)ReadC(),
			KillsCount = ReadC(),
			KillerSlot = ReadC(),
			WeaponId = ReadD(),
			X = ReadT(),
			Y = ReadT(),
			Z = ReadT(),
			Flag = ReadC(),
			Unk = ReadC()
		};
		for (int i = 0; i < fragInfos_0.KillsCount; i++)
		{
			FragModel fragModel = new FragModel
			{
				VictimSlot = ReadC(),
				WeaponClass = ReadC(),
				HitspotInfo = ReadC(),
				KillFlag = (KillingMessage)ReadH(),
				Unk = ReadC(),
				X = ReadT(),
				Y = ReadT(),
				Z = ReadT(),
				AssistSlot = ReadC(),
				Unks = ReadB(8)
			};
			fragInfos_0.Frags.Add(fragModel);
			if (fragModel.VictimSlot == fragInfos_0.KillerSlot)
			{
				bool_0 = true;
			}
		}
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
			if (room == null || room.RoundTime.IsTimer() || room.State < RoomState.BATTLE)
			{
				return;
			}
			bool flag = room.IsBotMode();
			SlotModel slot = room.GetSlot(fragInfos_0.KillerSlot);
			if (slot == null || (!flag && (slot.State < SlotState.BATTLE || slot.Id != player.SlotId)))
			{
				return;
			}
			RoomDeath.RegistryFragInfos(room, slot, out var Score, flag, bool_0, fragInfos_0);
			if (flag)
			{
				slot.Score += slot.KillsOnLife + room.IngameAiLevel + Score;
				if (slot.Score > 65535)
				{
					slot.Score = 65535;
					AllUtils.ValidateBanPlayer(player, $"AI Score Cheating! ({slot.Score})");
				}
				fragInfos_0.Score = slot.Score;
			}
			else
			{
				slot.Score += Score;
				AllUtils.CompleteMission(room, player, slot, fragInfos_0, MissionType.NA, 0);
				fragInfos_0.Score = Score;
			}
			using (PROTOCOL_BATTLE_DEATH_ACK packet = new PROTOCOL_BATTLE_DEATH_ACK(room, fragInfos_0, slot))
			{
				room.SendPacketToPlayers(packet, SlotState.BATTLE, 0);
			}
			RoomDeath.EndBattleByDeath(room, slot, flag, bool_0, fragInfos_0);
		}
		catch (Exception ex)
		{
			CLogger.Print("PROTOCOL_BATTLE_DEATH_REQ: " + ex.Message, LoggerType.Error, ex);
		}
	}
}
