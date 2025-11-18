using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Sync.Server;
using System;
using System.Collections.Generic;

namespace Server.Match.Data.Managers
{
	public static class DamageManager
	{
		public static void BoomDeath(RoomModel Room, PlayerModel Killer, int Damage, int WeaponId, List<DeathServerData> Deaths, List<ObjectHitInfo> Objs, List<int> BoomPlayers, CharaHitPart HitPart, CharaDeath DeathType)
		{
			PlayerModel playerModel;
			if (BoomPlayers == null || BoomPlayers.Count == 0)
			{
				return;
			}
			foreach (int boomPlayer in BoomPlayers)
			{
				if (!Room.GetPlayer(boomPlayer, out playerModel) || playerModel.Dead)
				{
					continue;
				}
				DamageManager.smethod_0(Room, Deaths, playerModel, Killer, DeathType);
				if (Damage <= 0)
				{
					continue;
				}
				if (ConfigLoader.UseHitMarker)
				{
					SendMatchInfo.SendHitMarkerSync(Room, Killer, DeathType, HitType.Normal, Damage);
				}
				Objs.Add(new ObjectHitInfo(2)
				{
					ObjId = playerModel.Slot,
					ObjLife = playerModel.Life,
					HitPart = HitPart,
					KillerSlot = Killer.Slot,
					Position = playerModel.Position - Killer.Position,
					DeathType = DeathType,
					WeaponId = WeaponId
				});
			}
		}

		public static void SabotageDestroy(RoomModel Room, PlayerModel Player, ObjectModel ObjM, ObjectInfo ObjI, int Damage)
		{
			if (ObjM.UltraSync > 0 && (Room.RoomType == RoomCondition.Destroy || Room.RoomType == RoomCondition.Defense))
			{
				if (ObjM.UltraSync != 1)
				{
					if (ObjM.UltraSync == 3)
					{
						goto Label2;
					}
					if (ObjM.UltraSync == 2 || ObjM.UltraSync == 4)
					{
						Room.Bar2 = ObjI.Life;
						goto Label0;
					}
					else
					{
						goto Label0;
					}
				}
			Label2:
				Room.Bar1 = ObjI.Life;
			Label0:
				RoomModel room = Room;
				SendMatchInfo.SendSabotageSync(room, Player, Damage, (ObjM.UltraSync == 4 ? 2 : 1));
			}
		}

		public static void SimpleDeath(RoomModel Room, List<DeathServerData> Deaths, List<ObjectHitInfo> Objs, PlayerModel Killer, PlayerModel Victim, int Damage, int WeaponId, CharaHitPart HitPart, CharaDeath DeathType)
		{
			Victim.Life -= Damage;
			DamageManager.smethod_2(Room, Victim, Killer, Victim.Life);
			if (Victim.Life <= 0)
			{
				DamageManager.smethod_0(Room, Deaths, Victim, Killer, DeathType);
				return;
			}
			DamageManager.smethod_1(Objs, Victim, Killer, DeathType, HitPart);
		}

		private static void smethod_0(RoomModel roomModel_0, List<DeathServerData> list_0, PlayerModel playerModel_0, PlayerModel playerModel_1, CharaDeath charaDeath_0)
		{
			playerModel_0.Life = 0;
			playerModel_0.Dead = true;
			playerModel_0.LastDie = DateTimeUtil.Now();
			DeathServerData deathServerDatum = new DeathServerData()
			{
				Player = playerModel_0,
				DeathType = charaDeath_0
			};
			AssistServerData assist = AssistManager.GetAssist(playerModel_0.Slot, roomModel_0.RoomId);
			if (assist == null)
			{
				deathServerDatum.AssistSlot = playerModel_1.Slot;
			}
			else
			{
				deathServerDatum.AssistSlot = (assist.IsAssist ? assist.Killer : playerModel_1.Slot);
			}
			list_0.Add(deathServerDatum);
			AssistManager.RemoveAssist(assist);
		}

		private static void smethod_1(List<ObjectHitInfo> list_0, PlayerModel playerModel_0, PlayerModel playerModel_1, CharaDeath charaDeath_0, CharaHitPart charaHitPart_0)
		{
			list_0.Add(new ObjectHitInfo(5)
			{
				ObjId = playerModel_0.Slot,
				KillerSlot = playerModel_1.Slot,
				DeathType = charaDeath_0,
				ObjLife = playerModel_0.Life,
				HitPart = charaHitPart_0
			});
		}

		private static void smethod_2(RoomModel roomModel_0, PlayerModel playerModel_0, PlayerModel playerModel_1, int int_0)
		{
			AssistServerData assistServerDatum = new AssistServerData()
			{
				RoomId = roomModel_0.RoomId,
				Killer = playerModel_1.Slot,
				Victim = playerModel_0.Slot,
				IsKiller = int_0 <= 0,
				VictimDead = int_0 <= 0,
				IsAssist = !assistServerDatum.IsKiller
			};
			if (assistServerDatum.Killer != assistServerDatum.Victim)
			{
				AssistManager.AddAssist(assistServerDatum);
			}
		}
	}
}