using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Update;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001F9 RID: 505
	public class RoomDeath
	{
		// Token: 0x060005E4 RID: 1508 RVA: 0x0002FE9C File Offset: 0x0002E09C
		public static void Load(SyncClientPacket C)
		{
			int num = (int)C.ReadH();
			int num2 = (int)C.ReadH();
			int num3 = (int)C.ReadH();
			byte b = C.ReadC();
			byte b2 = C.ReadC();
			int num4 = C.ReadD();
			float num5 = C.ReadT();
			float num6 = C.ReadT();
			float num7 = C.ReadT();
			byte b3 = C.ReadC();
			byte b4 = C.ReadC();
			int num8 = (int)(b * 25);
			if (C.ToArray().Length > 28 + num8)
			{
				CLogger.Print(string.Format("Invalid Death (Length > 53): {0}", C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(num3, num2);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(num);
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE)
			{
				SlotModel slot = room.GetSlot((int)b2);
				if (slot != null && slot.State == SlotState.BATTLE)
				{
					FragInfos fragInfos = new FragInfos
					{
						KillsCount = b,
						KillerSlot = b2,
						WeaponId = num4,
						X = num5,
						Y = num6,
						Z = num7,
						Flag = b3,
						Unk = b4
					};
					bool flag = false;
					for (int i = 0; i < (int)b; i++)
					{
						byte b5 = C.ReadC();
						byte b6 = C.ReadC();
						byte b7 = C.ReadC();
						float num9 = C.ReadT();
						float num10 = C.ReadT();
						float num11 = C.ReadT();
						byte b8 = C.ReadC();
						byte b9 = C.ReadC();
						byte[] array = C.ReadB(8);
						SlotModel slot2 = room.GetSlot((int)b5);
						if (slot2 != null && slot2.State == SlotState.BATTLE)
						{
							FragModel fragModel = new FragModel
							{
								VictimSlot = b5,
								WeaponClass = b6,
								HitspotInfo = b7,
								X = num9,
								Y = num10,
								Z = num11,
								AssistSlot = b8,
								Unk = b9,
								Unks = array
							};
							if (fragInfos.KillerSlot == b5)
							{
								flag = true;
							}
							fragInfos.Frags.Add(fragModel);
						}
					}
					fragInfos.KillsCount = (byte)fragInfos.Frags.Count;
					KillFragInfo.GenDeath(room, slot, fragInfos, flag);
				}
			}
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000300D8 File Offset: 0x0002E2D8
		public static void RegistryFragInfos(RoomModel Room, SlotModel Killer, out int Score, bool IsBotMode, bool IsSuicide, FragInfos Kills)
		{
			Score = 0;
			ItemClass idStatics = (ItemClass)ComDiv.GetIdStatics(Kills.WeaponId, 1);
			ClassType idStatics2 = (ClassType)ComDiv.GetIdStatics(Kills.WeaponId, 2);
			foreach (FragModel fragModel in Kills.Frags)
			{
				CharaDeath charaDeath = (CharaDeath)(fragModel.HitspotInfo >> 4);
				if (Kills.KillsCount - ((IsSuicide > false) ? 1 : 0) > 1)
				{
					fragModel.KillFlag |= ((charaDeath == CharaDeath.BOOM || charaDeath == CharaDeath.OBJECT_EXPLOSION || charaDeath == CharaDeath.POISON || charaDeath == CharaDeath.HOWL || charaDeath == CharaDeath.TRAMPLED || idStatics2 == ClassType.Shotgun) ? KillingMessage.MassKill : KillingMessage.PiercingShot);
				}
				else
				{
					int num = 0;
					if (charaDeath == CharaDeath.HEADSHOT)
					{
						num = 4;
					}
					else if (charaDeath == CharaDeath.DEFAULT && idStatics == ItemClass.Melee)
					{
						num = 6;
					}
					if (num > 0)
					{
						int num2 = Killer.LastKillState >> 12;
						if (num == 4)
						{
							if (num2 != 4)
							{
								Killer.RepeatLastState = false;
							}
							Killer.LastKillState = (num << 12) | (Killer.KillsOnLife + 1);
							if (Killer.RepeatLastState)
							{
								fragModel.KillFlag |= (((Killer.LastKillState & 16383) <= 1) ? KillingMessage.Headshot : KillingMessage.ChainHeadshot);
							}
							else
							{
								fragModel.KillFlag |= KillingMessage.Headshot;
								Killer.RepeatLastState = true;
							}
						}
						else if (num == 6)
						{
							if (num2 != 6)
							{
								Killer.RepeatLastState = false;
							}
							Killer.LastKillState = (num << 12) | (Killer.KillsOnLife + 1);
							if (Killer.RepeatLastState && (Killer.LastKillState & 16383) > 1)
							{
								fragModel.KillFlag |= KillingMessage.ChainSlugger;
							}
							else
							{
								Killer.RepeatLastState = true;
							}
						}
					}
					else
					{
						Killer.LastKillState = 0;
						Killer.RepeatLastState = false;
					}
				}
				byte victimSlot = fragModel.VictimSlot;
				byte assistSlot = fragModel.AssistSlot;
				SlotModel slotModel = Room.Slots[(int)victimSlot];
				SlotModel slotModel2 = Room.Slots[(int)assistSlot];
				if (slotModel.KillsOnLife > 3)
				{
					fragModel.KillFlag |= KillingMessage.ChainStopper;
				}
				if ((Kills.WeaponId != 19016 && Kills.WeaponId != 19022) || Kills.KillerSlot != victimSlot || !slotModel.SpecGM)
				{
					slotModel.AllDeaths++;
				}
				if (Kills.KillerSlot != assistSlot)
				{
					slotModel2.AllAssists++;
				}
				if (Room.RoomType == RoomCondition.FreeForAll)
				{
					Killer.AllKills++;
					if (Killer.DeathState == DeadEnum.Alive)
					{
						Killer.KillsOnLife++;
					}
				}
				else if (Killer.Team != slotModel.Team)
				{
					Score += AllUtils.GetKillScore(fragModel.KillFlag);
					Killer.AllKills++;
					if (Killer.DeathState == DeadEnum.Alive)
					{
						Killer.KillsOnLife++;
					}
					if (slotModel.Team == TeamEnum.FR_TEAM)
					{
						Room.FRDeaths++;
						Room.CTKills++;
					}
					else
					{
						Room.CTDeaths++;
						Room.FRKills++;
					}
					if (Room.IsDinoMode("DE"))
					{
						if (Killer.Team == TeamEnum.FR_TEAM)
						{
							Room.FRDino += 4;
						}
						else
						{
							Room.CTDino += 4;
						}
					}
				}
				slotModel.LastKillState = 0;
				slotModel.KillsOnLife = 0;
				slotModel.RepeatLastState = false;
				slotModel.PassSequence = 0;
				slotModel.DeathState = DeadEnum.Dead;
				if (!IsBotMode)
				{
					switch (idStatics2)
					{
					case ClassType.Assault:
						Killer.AR[0]++;
						slotModel.AR[1]++;
						break;
					case ClassType.SMG:
						Killer.SMG[0]++;
						slotModel.SMG[1]++;
						break;
					case ClassType.Sniper:
						Killer.SR[0]++;
						slotModel.SR[1]++;
						break;
					case ClassType.Shotgun:
						Killer.SG[0]++;
						slotModel.SG[1]++;
						break;
					case ClassType.ThrowingGrenade:
					case ClassType.ThrowingSpecial:
					case ClassType.Mission:
						break;
					case ClassType.Machinegun:
						Killer.MG[0]++;
						slotModel.MG[1]++;
						break;
					default:
						if (idStatics2 == ClassType.Shield)
						{
							Killer.SHD[0]++;
							slotModel.SHD[1]++;
						}
						break;
					}
					AllUtils.CompleteMission(Room, slotModel, MissionType.DEATH, 0);
				}
				if (charaDeath == CharaDeath.HEADSHOT)
				{
					Killer.AllHeadshots++;
				}
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00030588 File Offset: 0x0002E788
		public static void EndBattleByDeath(RoomModel Room, SlotModel Killer, bool IsBotMode, bool IsSuicide, FragInfos Kills)
		{
			if (Room.RoomType == RoomCondition.DeathMatch && !IsBotMode)
			{
				AllUtils.BattleEndKills(Room, IsBotMode);
				return;
			}
			if (Room.RoomType == RoomCondition.FreeForAll)
			{
				AllUtils.BattleEndKillsFreeForAll(Room);
				return;
			}
			if (!Killer.SpecGM && (Room.RoomType == RoomCondition.Bomb || Room.RoomType == RoomCondition.Annihilation || Room.RoomType == RoomCondition.Destroy || Room.RoomType == RoomCondition.Ace))
			{
				if (Room.RoomType != RoomCondition.Bomb && Room.RoomType != RoomCondition.Annihilation)
				{
					if (Room.RoomType != RoomCondition.Destroy)
					{
						if (Room.RoomType != RoomCondition.Ace)
						{
							return;
						}
						SlotModel[] array = new SlotModel[]
						{
							Room.GetSlot(0),
							Room.GetSlot(1)
						};
						if (array[0].DeathState == DeadEnum.Dead)
						{
							Room.CTRounds++;
							AllUtils.BattleEndRound(Room, TeamEnum.CT_TEAM, true, Kills, Killer);
							return;
						}
						if (array[1].DeathState == DeadEnum.Dead)
						{
							Room.FRRounds++;
							AllUtils.BattleEndRound(Room, TeamEnum.FR_TEAM, true, Kills, Killer);
							return;
						}
						return;
					}
				}
				TeamEnum teamEnum = TeamEnum.TEAM_DRAW;
				int num;
				int num2;
				int num3;
				int num4;
				Room.GetPlayingPlayers(true, out num, out num2, out num3, out num4);
				TeamEnum teamEnum2;
				RoomDeath.smethod_0(Room, Killer, ref num, ref num2, ref num3, ref num4, out teamEnum2);
				if (num3 == num && teamEnum2 == TeamEnum.FR_TEAM && IsSuicide && !Room.ActiveC4)
				{
					RoomDeath.smethod_1(Room, ref teamEnum, 1);
					AllUtils.BattleEndRound(Room, teamEnum, true, Kills, Killer);
					return;
				}
				if (num4 == num2 && teamEnum2 == TeamEnum.CT_TEAM)
				{
					RoomDeath.smethod_1(Room, ref teamEnum, 2);
					AllUtils.BattleEndRound(Room, teamEnum, true, Kills, Killer);
					return;
				}
				if (num3 == num && teamEnum2 == TeamEnum.CT_TEAM)
				{
					if (!Room.ActiveC4)
					{
						RoomDeath.smethod_1(Room, ref teamEnum, 1);
					}
					else if (IsSuicide)
					{
						RoomDeath.smethod_1(Room, ref teamEnum, 2);
					}
					AllUtils.BattleEndRound(Room, teamEnum, false, Kills, Killer);
					return;
				}
				if (num4 == num2 && teamEnum2 == TeamEnum.FR_TEAM)
				{
					if (IsSuicide && Room.ActiveC4)
					{
						RoomDeath.smethod_1(Room, ref teamEnum, 1);
					}
					else
					{
						RoomDeath.smethod_1(Room, ref teamEnum, 2);
					}
					AllUtils.BattleEndRound(Room, teamEnum, true, Kills, Killer);
					return;
				}
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00030768 File Offset: 0x0002E968
		private static void smethod_0(RoomModel roomModel_0, SlotModel slotModel_0, ref int int_0, ref int int_1, ref int int_2, ref int int_3, out TeamEnum teamEnum_0)
		{
			teamEnum_0 = slotModel_0.Team;
			if (roomModel_0.SwapRound)
			{
				teamEnum_0 = ((teamEnum_0 == TeamEnum.FR_TEAM) ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
				int num = int_0;
				int num2 = int_1;
				int_1 = num;
				int_0 = num2;
				num2 = int_2;
				num = int_3;
				int_3 = num2;
				int_2 = num;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000307B0 File Offset: 0x0002E9B0
		private static void smethod_1(RoomModel roomModel_0, ref TeamEnum teamEnum_0, int int_0)
		{
			if (int_0 != 1)
			{
				if (int_0 == 2)
				{
					if (roomModel_0.SwapRound)
					{
						teamEnum_0 = TeamEnum.CT_TEAM;
						roomModel_0.CTRounds++;
						return;
					}
					teamEnum_0 = TeamEnum.FR_TEAM;
					roomModel_0.FRRounds++;
				}
				return;
			}
			if (roomModel_0.SwapRound)
			{
				teamEnum_0 = TeamEnum.FR_TEAM;
				roomModel_0.FRRounds++;
				return;
			}
			teamEnum_0 = TeamEnum.CT_TEAM;
			roomModel_0.CTRounds++;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000025DF File Offset: 0x000007DF
		public RoomDeath()
		{
		}
	}
}
