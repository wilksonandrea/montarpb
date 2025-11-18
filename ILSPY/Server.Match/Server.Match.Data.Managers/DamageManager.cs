using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.SharpDX;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Sync.Server;

namespace Server.Match.Data.Managers;

public static class DamageManager
{
	public static void SabotageDestroy(RoomModel Room, PlayerModel Player, ObjectModel ObjM, ObjectInfo ObjI, int Damage)
	{
		if (ObjM.UltraSync <= 0 || (Room.RoomType != RoomCondition.Destroy && Room.RoomType != RoomCondition.Defense))
		{
			return;
		}
		if (ObjM.UltraSync != 1 && ObjM.UltraSync != 3)
		{
			if (ObjM.UltraSync == 2 || ObjM.UltraSync == 4)
			{
				Room.Bar2 = ObjI.Life;
			}
		}
		else
		{
			Room.Bar1 = ObjI.Life;
		}
		SendMatchInfo.SendSabotageSync(Room, Player, Damage, (ObjM.UltraSync != 4) ? 1 : 2);
	}

	private static void smethod_0(RoomModel roomModel_0, List<DeathServerData> list_0, PlayerModel playerModel_0, PlayerModel playerModel_1, CharaDeath charaDeath_0)
	{
		playerModel_0.Life = 0;
		playerModel_0.Dead = true;
		playerModel_0.LastDie = DateTimeUtil.Now();
		DeathServerData deathServerData = new DeathServerData
		{
			Player = playerModel_0,
			DeathType = charaDeath_0
		};
		AssistServerData assist = AssistManager.GetAssist(playerModel_0.Slot, roomModel_0.RoomId);
		if (assist != null)
		{
			deathServerData.AssistSlot = (assist.IsAssist ? assist.Killer : playerModel_1.Slot);
		}
		else
		{
			deathServerData.AssistSlot = playerModel_1.Slot;
		}
		list_0.Add(deathServerData);
		AssistManager.RemoveAssist(assist);
	}

	private static void smethod_1(List<ObjectHitInfo> list_0, PlayerModel playerModel_0, PlayerModel playerModel_1, CharaDeath charaDeath_0, CharaHitPart charaHitPart_0)
	{
		ObjectHitInfo item = new ObjectHitInfo(5)
		{
			ObjId = playerModel_0.Slot,
			KillerSlot = playerModel_1.Slot,
			DeathType = charaDeath_0,
			ObjLife = playerModel_0.Life,
			HitPart = charaHitPart_0
		};
		list_0.Add(item);
	}

	public static void BoomDeath(RoomModel Room, PlayerModel Killer, int Damage, int WeaponId, List<DeathServerData> Deaths, List<ObjectHitInfo> Objs, List<int> BoomPlayers, CharaHitPart HitPart, CharaDeath DeathType)
	{
		if (BoomPlayers == null || BoomPlayers.Count == 0)
		{
			return;
		}
		foreach (int BoomPlayer in BoomPlayers)
		{
			if (!Room.GetPlayer(BoomPlayer, out var Player) || Player.Dead)
			{
				continue;
			}
			smethod_0(Room, Deaths, Player, Killer, DeathType);
			if (Damage > 0)
			{
				if (ConfigLoader.UseHitMarker)
				{
					SendMatchInfo.SendHitMarkerSync(Room, Killer, DeathType, HitType.Normal, Damage);
				}
				ObjectHitInfo item = new ObjectHitInfo(2)
				{
					ObjId = Player.Slot,
					ObjLife = Player.Life,
					HitPart = HitPart,
					KillerSlot = Killer.Slot,
					Position = (Vector3)Player.Position - (Vector3)Killer.Position,
					DeathType = DeathType,
					WeaponId = WeaponId
				};
				Objs.Add(item);
			}
		}
	}

	public static void SimpleDeath(RoomModel Room, List<DeathServerData> Deaths, List<ObjectHitInfo> Objs, PlayerModel Killer, PlayerModel Victim, int Damage, int WeaponId, CharaHitPart HitPart, CharaDeath DeathType)
	{
		Victim.Life -= Damage;
		smethod_2(Room, Victim, Killer, Victim.Life);
		if (Victim.Life <= 0)
		{
			smethod_0(Room, Deaths, Victim, Killer, DeathType);
		}
		else
		{
			smethod_1(Objs, Victim, Killer, DeathType, HitPart);
		}
	}

	private static void smethod_2(RoomModel roomModel_0, PlayerModel playerModel_0, PlayerModel playerModel_1, int int_0)
	{
		AssistServerData assistServerData = new AssistServerData
		{
			RoomId = roomModel_0.RoomId,
			Killer = playerModel_1.Slot,
			Victim = playerModel_0.Slot,
			IsKiller = (int_0 <= 0),
			VictimDead = (int_0 <= 0)
		};
		assistServerData.IsAssist = !assistServerData.IsKiller;
		if (assistServerData.Killer != assistServerData.Victim)
		{
			AssistManager.AddAssist(assistServerData);
		}
	}
}
