using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Match.Data.Managers;
using Server.Match.Data.Models;
using Server.Match.Data.Utils;
using Server.Match.Data.XML;
using Server.Match.Network.Packets;

namespace Server.Match.Data.Sync.Client;

public class RespawnSync
{
	[CompilerGenerated]
	private sealed class Class1
	{
		public PlayerModel playerModel_0;

		public RoomModel roomModel_0;

		public Predicate<AssistServerData> predicate_0;

		internal bool method_0(AssistServerData assistServerData_0)
		{
			if (assistServerData_0.Victim == playerModel_0.Slot)
			{
				return assistServerData_0.RoomId == roomModel_0.RoomId;
			}
			return false;
		}
	}

	public static void Load(SyncClientPacket C)
	{
		uint uniqueRoomId = C.ReadUD();
		uint seed = C.ReadUD();
		long startTick = C.ReadQ();
		int num = C.ReadC();
		int num2 = C.ReadC();
		int num3 = C.ReadC();
		int num4 = C.ReadC();
		int num5 = C.ReadC();
		int num6 = 0;
		int percent = 0;
		int wpnPrimary = 0;
		int wpnSecondary = 0;
		int wpnMelee = 0;
		int wpnExplosive = 0;
		int wpnSpecial = 0;
		int accessory = 0;
		bool flag = false;
		if (num != 0 && num != 2)
		{
			if (C.ToArray().Length > 23)
			{
				CLogger.Print($"RespawnSync (Length > 23): {C.ToArray().Length}", LoggerType.Warning);
			}
		}
		else
		{
			num6 = C.ReadD();
			percent = C.ReadC();
			flag = C.ReadC() == 1;
			wpnPrimary = C.ReadD();
			wpnSecondary = C.ReadD();
			wpnMelee = C.ReadD();
			wpnExplosive = C.ReadD();
			wpnSpecial = C.ReadD();
			accessory = C.ReadD();
			if (C.ToArray().Length > 53)
			{
				CLogger.Print($"RespawnSync (Length > 53): {C.ToArray().Length}", LoggerType.Warning);
			}
		}
		RoomModel roomModel_0 = RoomsManager.GetRoom(uniqueRoomId, seed);
		if (roomModel_0 == null)
		{
			return;
		}
		roomModel_0.ResyncTick(startTick, seed);
		PlayerModel playerModel_0 = roomModel_0.GetPlayer(num3, Active: true);
		if (playerModel_0 != null && playerModel_0.PlayerIdByUser != num5)
		{
			CLogger.Print($"Invalid User Ids: [By User: {playerModel_0.PlayerIdByUser} / Server: {num5}]", LoggerType.Warning);
		}
		if (playerModel_0 == null || playerModel_0.PlayerIdByUser != num5)
		{
			return;
		}
		playerModel_0.PlayerIdByServer = num5;
		playerModel_0.RespawnByServer = num4;
		playerModel_0.Integrity = false;
		if (num2 > roomModel_0.ServerRound)
		{
			roomModel_0.ServerRound = num2;
		}
		if (num == 0 || num == 2)
		{
			AssistServerData assist = AssistManager.GetAssist(playerModel_0.Slot, roomModel_0.RoomId);
			if (assist != null && AssistManager.RemoveAssist(assist))
			{
				foreach (AssistServerData item3 in AssistManager.Assists.FindAll((AssistServerData assistServerData_0) => assistServerData_0.Victim == playerModel_0.Slot && assistServerData_0.RoomId == roomModel_0.RoomId))
				{
					AssistManager.RemoveAssist(item3);
				}
			}
			Equipment equip = new Equipment
			{
				WpnPrimary = wpnPrimary,
				WpnSecondary = wpnSecondary,
				WpnMelee = wpnMelee,
				WpnExplosive = wpnExplosive,
				WpnSpecial = wpnSpecial,
				Accessory = accessory
			};
			playerModel_0.Dead = false;
			playerModel_0.PlantDuration = ConfigLoader.PlantDuration;
			playerModel_0.DefuseDuration = ConfigLoader.DefuseDuration;
			playerModel_0.Equip = equip;
			if (flag)
			{
				playerModel_0.PlantDuration -= ComDiv.Percentage(ConfigLoader.PlantDuration, 50);
				playerModel_0.DefuseDuration -= ComDiv.Percentage(ConfigLoader.DefuseDuration, 25);
			}
			if (!roomModel_0.BotMode)
			{
				if (roomModel_0.SourceToMap == -1)
				{
					roomModel_0.RoundResetRoomF1(num2);
				}
				else
				{
					roomModel_0.RoundResetRoomS1(num2);
				}
			}
			if (num6 == -1)
			{
				playerModel_0.Immortal = true;
			}
			else
			{
				playerModel_0.Immortal = false;
				int charaHP = CharaStructureXML.GetCharaHP(num6);
				charaHP += ComDiv.Percentage(charaHP, percent);
				playerModel_0.MaxLife = charaHP;
				playerModel_0.ResetLife();
			}
		}
		if (roomModel_0.BotMode || num == 2 || !roomModel_0.ObjectsIsValid())
		{
			return;
		}
		List<ObjectHitInfo> list = new List<ObjectHitInfo>();
		ObjectInfo[] objects = roomModel_0.Objects;
		foreach (ObjectInfo objectInfo in objects)
		{
			ObjectModel model = objectInfo.Model;
			if (model != null && ((num != 2 && model.Destroyable && objectInfo.Life != model.Life) || model.NeedSync))
			{
				ObjectHitInfo item = new ObjectHitInfo(3)
				{
					ObjSyncId = (model.NeedSync ? 1 : 0),
					AnimId1 = model.Animation,
					AnimId2 = ((objectInfo.Animation != null) ? objectInfo.Animation.Id : 255),
					DestroyState = objectInfo.DestroyState,
					ObjId = model.Id,
					ObjLife = objectInfo.Life,
					SpecialUse = AllUtils.GetDuration(objectInfo.UseDate)
				};
				list.Add(item);
			}
		}
		PlayerModel[] players = roomModel_0.Players;
		foreach (PlayerModel playerModel in players)
		{
			if (playerModel.Slot != num3 && playerModel.AccountIdIsValid() && !playerModel.Immortal && playerModel.StartTime != default(DateTime) && (playerModel.MaxLife != playerModel.Life || playerModel.Dead))
			{
				ObjectHitInfo item2 = new ObjectHitInfo(4)
				{
					ObjId = playerModel.Slot,
					ObjLife = playerModel.Life
				};
				list.Add(item2);
			}
		}
		if (list.Count > 0)
		{
			byte[] actions = PROTOCOL_EVENTS_ACTION.GET_CODE(list);
			byte[] data = AllUtils.BaseWriteCode(4, actions, 255, AllUtils.GetDuration(roomModel_0.StartTime), num2, num4, 0, num5);
			MatchXender.Client.SendPacket(data, playerModel_0.Client);
		}
		list.Clear();
		list = null;
	}
}
