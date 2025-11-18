using System;
using System.Collections.Generic;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Utility;
using Server.Match.Data.Enums;
using Server.Match.Data.Models;
using Server.Match.Data.Sync.Server;

namespace Server.Match.Data.Managers
{
	// Token: 0x02000061 RID: 97
	public static class DamageManager
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000B9A8 File Offset: 0x00009BA8
		public static void SabotageDestroy(RoomModel Room, PlayerModel Player, ObjectModel ObjM, ObjectInfo ObjI, int Damage)
		{
			if (ObjM.UltraSync > 0 && (Room.RoomType == RoomCondition.Destroy || Room.RoomType == RoomCondition.Defense))
			{
				if (ObjM.UltraSync != 1)
				{
					if (ObjM.UltraSync != 3)
					{
						if (ObjM.UltraSync == 2 || ObjM.UltraSync == 4)
						{
							Room.Bar2 = ObjI.Life;
							goto IL_5D;
						}
						goto IL_5D;
					}
				}
				Room.Bar1 = ObjI.Life;
				IL_5D:
				SendMatchInfo.SendSabotageSync(Room, Player, Damage, (ObjM.UltraSync == 4) ? 2 : 1);
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000BA28 File Offset: 0x00009C28
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

		// Token: 0x060001D4 RID: 468 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		private static void smethod_1(List<ObjectHitInfo> list_0, PlayerModel playerModel_0, PlayerModel playerModel_1, CharaDeath charaDeath_0, CharaHitPart charaHitPart_0)
		{
			ObjectHitInfo objectHitInfo = new ObjectHitInfo(5)
			{
				ObjId = playerModel_0.Slot,
				KillerSlot = playerModel_1.Slot,
				DeathType = charaDeath_0,
				ObjLife = playerModel_0.Life,
				HitPart = charaHitPart_0
			};
			list_0.Add(objectHitInfo);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000BB00 File Offset: 0x00009D00
		public static void BoomDeath(RoomModel Room, PlayerModel Killer, int Damage, int WeaponId, List<DeathServerData> Deaths, List<ObjectHitInfo> Objs, List<int> BoomPlayers, CharaHitPart HitPart, CharaDeath DeathType)
		{
			if (BoomPlayers != null && BoomPlayers.Count != 0)
			{
				foreach (int num in BoomPlayers)
				{
					PlayerModel playerModel;
					if (Room.GetPlayer(num, out playerModel) && !playerModel.Dead)
					{
						DamageManager.smethod_0(Room, Deaths, playerModel, Killer, DeathType);
						if (Damage > 0)
						{
							if (ConfigLoader.UseHitMarker)
							{
								SendMatchInfo.SendHitMarkerSync(Room, Killer, DeathType, HitType.Normal, Damage);
							}
							ObjectHitInfo objectHitInfo = new ObjectHitInfo(2)
							{
								ObjId = playerModel.Slot,
								ObjLife = playerModel.Life,
								HitPart = HitPart,
								KillerSlot = Killer.Slot,
								Position = playerModel.Position - Killer.Position,
								DeathType = DeathType,
								WeaponId = WeaponId
							};
							Objs.Add(objectHitInfo);
						}
					}
				}
				return;
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000BC14 File Offset: 0x00009E14
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

		// Token: 0x060001D7 RID: 471 RVA: 0x0000BC68 File Offset: 0x00009E68
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
}
