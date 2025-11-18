using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Update;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;
using System;
using System.Collections.Generic;

namespace Server.Game.Data.Sync.Client
{
	public class RoomDeath
	{
		public RoomDeath()
		{
		}

		public static void EndBattleByDeath(RoomModel Room, SlotModel Killer, bool IsBotMode, bool IsSuicide, FragInfos Kills)
		{
			int ınt32;
			int ınt321;
			int ınt322;
			int ınt323;
			TeamEnum teamEnum;
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
					if (Room.RoomType == RoomCondition.Destroy)
					{
						goto Label2;
					}
					if (Room.RoomType == RoomCondition.Ace)
					{
						SlotModel[] slot = new SlotModel[] { Room.GetSlot(0), Room.GetSlot(1) };
						if (slot[0].DeathState == DeadEnum.Dead)
						{
							Room.CTRounds++;
							AllUtils.BattleEndRound(Room, TeamEnum.CT_TEAM, true, Kills, Killer);
							return;
						}
						if (slot[1].DeathState == DeadEnum.Dead)
						{
							Room.FRRounds++;
							AllUtils.BattleEndRound(Room, TeamEnum.FR_TEAM, true, Kills, Killer);
							return;
						}
						else
						{
							return;
						}
					}
					else
					{
						return;
					}
				}
			Label2:
				TeamEnum teamEnum1 = TeamEnum.TEAM_DRAW;
				Room.GetPlayingPlayers(true, out ınt32, out ınt321, out ınt322, out ınt323);
				RoomDeath.smethod_0(Room, Killer, ref ınt32, ref ınt321, ref ınt322, ref ınt323, out teamEnum);
				if ((ınt322 != ınt32 ? false : teamEnum == TeamEnum.FR_TEAM) & IsSuicide && !Room.ActiveC4)
				{
					RoomDeath.smethod_1(Room, ref teamEnum1, 1);
					AllUtils.BattleEndRound(Room, teamEnum1, true, Kills, Killer);
					return;
				}
				if (ınt323 == ınt321 && teamEnum == TeamEnum.CT_TEAM)
				{
					RoomDeath.smethod_1(Room, ref teamEnum1, 2);
					AllUtils.BattleEndRound(Room, teamEnum1, true, Kills, Killer);
					return;
				}
				if (ınt322 == ınt32 && teamEnum == TeamEnum.CT_TEAM)
				{
					if (!Room.ActiveC4)
					{
						RoomDeath.smethod_1(Room, ref teamEnum1, 1);
					}
					else if (IsSuicide)
					{
						RoomDeath.smethod_1(Room, ref teamEnum1, 2);
					}
					AllUtils.BattleEndRound(Room, teamEnum1, false, Kills, Killer);
					return;
				}
				if (ınt323 == ınt321 && teamEnum == TeamEnum.FR_TEAM)
				{
					if (!IsSuicide || !Room.ActiveC4)
					{
						RoomDeath.smethod_1(Room, ref teamEnum1, 2);
					}
					else
					{
						RoomDeath.smethod_1(Room, ref teamEnum1, 1);
					}
					AllUtils.BattleEndRound(Room, teamEnum1, true, Kills, Killer);
					return;
				}
			}
		}

		public static void Load(SyncClientPacket C)
		{
			int ınt32 = C.ReadH();
			int ınt321 = C.ReadH();
			short ınt16 = C.ReadH();
			byte num = C.ReadC();
			byte num1 = C.ReadC();
			int ınt322 = C.ReadD();
			float single = C.ReadT();
			float single1 = C.ReadT();
			float single2 = C.ReadT();
			byte num2 = C.ReadC();
			byte num3 = C.ReadC();
			if ((int)C.ToArray().Length > 28 + num * 25)
			{
				CLogger.Print(string.Format("Invalid Death (Length > 53): {0}", (int)C.ToArray().Length), LoggerType.Warning, null);
			}
			ChannelModel channel = ChannelsXML.GetChannel(ınt16, ınt321);
			if (channel == null)
			{
				return;
			}
			RoomModel room = channel.GetRoom(ınt32);
			if (room != null && !room.RoundTime.IsTimer() && room.State == RoomState.BATTLE)
			{
				SlotModel slot = room.GetSlot((int)num1);
				if (slot != null && slot.State == SlotState.BATTLE)
				{
					FragInfos fragInfo = new FragInfos()
					{
						KillsCount = num,
						KillerSlot = num1,
						WeaponId = ınt322,
						X = single,
						Y = single1,
						Z = single2,
						Flag = num2,
						Unk = num3
					};
					bool flag = false;
					for (int i = 0; i < num; i++)
					{
						byte num4 = C.ReadC();
						byte num5 = C.ReadC();
						byte num6 = C.ReadC();
						float single3 = C.ReadT();
						float single4 = C.ReadT();
						float single5 = C.ReadT();
						byte num7 = C.ReadC();
						byte num8 = C.ReadC();
						byte[] numArray = C.ReadB(8);
						SlotModel slotModel = room.GetSlot((int)num4);
						if (slotModel != null && slotModel.State == SlotState.BATTLE)
						{
							FragModel fragModel = new FragModel()
							{
								VictimSlot = num4,
								WeaponClass = num5,
								HitspotInfo = num6,
								X = single3,
								Y = single4,
								Z = single5,
								AssistSlot = num7,
								Unk = num8,
								Unks = numArray
							};
							if (fragInfo.KillerSlot == num4)
							{
								flag = true;
							}
							fragInfo.Frags.Add(fragModel);
						}
					}
					fragInfo.KillsCount = (byte)fragInfo.Frags.Count;
					KillFragInfo.GenDeath(room, slot, fragInfo, flag);
				}
			}
		}

		public static void RegistryFragInfos(RoomModel Room, SlotModel Killer, out int Score, bool IsBotMode, bool IsSuicide, FragInfos Kills)
		{
			Score = 0;
			ItemClass ıdStatics = (ItemClass)ComDiv.GetIdStatics(Kills.WeaponId, 1);
			ClassType classType = (ClassType)ComDiv.GetIdStatics(Kills.WeaponId, 2);
			foreach (FragModel frag in Kills.Frags)
			{
				CharaDeath hitspotInfo = (CharaDeath)(frag.HitspotInfo >> 4);
				if (Kills.KillsCount - IsSuicide <= 1)
				{
					int ınt32 = 0;
					if (hitspotInfo == CharaDeath.HEADSHOT)
					{
						ınt32 = 4;
					}
					else if (hitspotInfo == CharaDeath.DEFAULT && ıdStatics == ItemClass.Melee)
					{
						ınt32 = 6;
					}
					if (ınt32 <= 0)
					{
						Killer.LastKillState = 0;
						Killer.RepeatLastState = false;
					}
					else
					{
						int lastKillState = Killer.LastKillState >> 12;
						if (ınt32 == 4)
						{
							if (lastKillState != 4)
							{
								Killer.RepeatLastState = false;
							}
							Killer.LastKillState = ınt32 << 12 | Killer.KillsOnLife + 1;
							if (!Killer.RepeatLastState)
							{
								FragModel killFlag = frag;
								killFlag.KillFlag = killFlag.KillFlag | KillingMessage.Headshot;
								Killer.RepeatLastState = true;
							}
							else
							{
								FragModel fragModel = frag;
								fragModel.KillFlag = fragModel.KillFlag | ((Killer.LastKillState & 16383) <= 1 ? KillingMessage.Headshot : KillingMessage.ChainHeadshot);
							}
						}
						else if (ınt32 == 6)
						{
							if (lastKillState != 6)
							{
								Killer.RepeatLastState = false;
							}
							Killer.LastKillState = ınt32 << 12 | Killer.KillsOnLife + 1;
							if (!Killer.RepeatLastState || (Killer.LastKillState & 16383) <= 1)
							{
								Killer.RepeatLastState = true;
							}
							else
							{
								FragModel killFlag1 = frag;
								killFlag1.KillFlag = killFlag1.KillFlag | KillingMessage.ChainSlugger;
							}
						}
					}
				}
				else
				{
					FragModel fragModel1 = frag;
					fragModel1.KillFlag = fragModel1.KillFlag | (hitspotInfo == CharaDeath.BOOM || hitspotInfo == CharaDeath.OBJECT_EXPLOSION || hitspotInfo == CharaDeath.POISON || hitspotInfo == CharaDeath.HOWL || hitspotInfo == CharaDeath.TRAMPLED || classType == ClassType.Shotgun ? KillingMessage.MassKill : KillingMessage.PiercingShot);
				}
				byte victimSlot = frag.VictimSlot;
				byte assistSlot = frag.AssistSlot;
				SlotModel slots = Room.Slots[victimSlot];
				SlotModel slotModel = Room.Slots[assistSlot];
				if (slots.KillsOnLife > 3)
				{
					FragModel killFlag2 = frag;
					killFlag2.KillFlag = killFlag2.KillFlag | KillingMessage.ChainStopper;
				}
				if (Kills.WeaponId != 19016 && Kills.WeaponId != 19022 || Kills.KillerSlot != victimSlot || !slots.SpecGM)
				{
					slots.AllDeaths++;
				}
				if (Kills.KillerSlot != assistSlot)
				{
					slotModel.AllAssists++;
				}
				if (Room.RoomType == RoomCondition.FreeForAll)
				{
					Killer.AllKills++;
					if (Killer.DeathState == DeadEnum.Alive)
					{
						Killer.KillsOnLife++;
					}
				}
				else if (Killer.Team != slots.Team)
				{
					Score += AllUtils.GetKillScore(frag.KillFlag);
					Killer.AllKills++;
					if (Killer.DeathState == DeadEnum.Alive)
					{
						Killer.KillsOnLife++;
					}
					if (slots.Team != TeamEnum.FR_TEAM)
					{
						Room.CTDeaths++;
						Room.FRKills++;
					}
					else
					{
						Room.FRDeaths++;
						Room.CTKills++;
					}
					if (Room.IsDinoMode("DE"))
					{
						if (Killer.Team != TeamEnum.FR_TEAM)
						{
							Room.CTDino += 4;
						}
						else
						{
							Room.FRDino += 4;
						}
					}
				}
				slots.LastKillState = 0;
				slots.KillsOnLife = 0;
				slots.RepeatLastState = false;
				slots.PassSequence = 0;
				slots.DeathState = DeadEnum.Dead;
				if (!IsBotMode)
				{
					switch (classType)
					{
						case ClassType.Assault:
						{
							Killer.AR[0]++;
							slots.AR[1]++;
							goto case ClassType.Mission;
						}
						case ClassType.SMG:
						{
							Killer.SMG[0]++;
							slots.SMG[1]++;
							goto case ClassType.Mission;
						}
						case ClassType.Sniper:
						{
							Killer.SR[0]++;
							slots.SR[1]++;
							goto case ClassType.Mission;
						}
						case ClassType.Shotgun:
						{
							Killer.SG[0]++;
							slots.SG[1]++;
							goto case ClassType.Mission;
						}
						case ClassType.ThrowingGrenade:
						case ClassType.ThrowingSpecial:
						case ClassType.Mission:
						{
							AllUtils.CompleteMission(Room, slots, MissionType.DEATH, 0);
							break;
						}
						case ClassType.Machinegun:
						{
							Killer.MG[0]++;
							slots.MG[1]++;
							goto case ClassType.Mission;
						}
						default:
						{
							if (classType == ClassType.Shield)
							{
								Killer.SHD[0]++;
								slots.SHD[1]++;
								goto case ClassType.Mission;
							}
							else
							{
								goto case ClassType.Mission;
							}
						}
					}
				}
				if (hitspotInfo != CharaDeath.HEADSHOT)
				{
					continue;
				}
				Killer.AllHeadshots++;
			}
		}

		private static void smethod_0(RoomModel roomModel_0, SlotModel slotModel_0, ref int int_0, ref int int_1, ref int int_2, ref int int_3, out TeamEnum teamEnum_0)
		{
			teamEnum_0 = slotModel_0.Team;
			if (roomModel_0.SwapRound)
			{
				teamEnum_0 = ((int)teamEnum_0 == 0 ? TeamEnum.CT_TEAM : TeamEnum.FR_TEAM);
				int int0 = int_0;
				int int1 = int_1;
				int_1 = int0;
				int_0 = int1;
				int1 = int_2;
				int0 = int_3;
				int_3 = int1;
				int_2 = int0;
			}
		}

		private static void smethod_1(RoomModel roomModel_0, ref TeamEnum teamEnum_0, int int_0)
		{
			if (int_0 == 1)
			{
				if (roomModel_0.SwapRound)
				{
					teamEnum_0 = TeamEnum.FR_TEAM;
					roomModel_0.FRRounds++;
					return;
				}
				teamEnum_0 = TeamEnum.CT_TEAM;
				roomModel_0.CTRounds++;
				return;
			}
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
		}
	}
}