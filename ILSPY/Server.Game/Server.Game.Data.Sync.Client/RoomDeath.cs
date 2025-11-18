using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Data.Sync.Update;
using Server.Game.Data.Utils;
using Server.Game.Data.XML;

namespace Server.Game.Data.Sync.Client;

public class RoomDeath
{
	public static void Load(SyncClientPacket C)
	{
		int id = C.ReadH();
		int ıd = C.ReadH();
		short serverId = C.ReadH();
		byte b = C.ReadC();
		byte b2 = C.ReadC();
		int weaponId = C.ReadD();
		float x = C.ReadT();
		float y = C.ReadT();
		float z = C.ReadT();
		byte flag = C.ReadC();
		byte unk = C.ReadC();
		int num = b * 25;
		if (C.ToArray().Length > 28 + num)
		{
			CLogger.Print($"Invalid Death (Length > 53): {C.ToArray().Length}", LoggerType.Warning);
		}
		ChannelModel channel = ChannelsXML.GetChannel(serverId, ıd);
		if (channel == null)
		{
			return;
		}
		RoomModel room = channel.GetRoom(id);
		if (room == null || room.RoundTime.IsTimer() || room.State != RoomState.BATTLE)
		{
			return;
		}
		SlotModel slot = room.GetSlot(b2);
		if (slot == null || slot.State != SlotState.BATTLE)
		{
			return;
		}
		FragInfos fragInfos = new FragInfos
		{
			KillsCount = b,
			KillerSlot = b2,
			WeaponId = weaponId,
			X = x,
			Y = y,
			Z = z,
			Flag = flag,
			Unk = unk
		};
		bool ısSuicide = false;
		for (int i = 0; i < b; i++)
		{
			byte b3 = C.ReadC();
			byte weaponClass = C.ReadC();
			byte hitspotInfo = C.ReadC();
			float x2 = C.ReadT();
			float y2 = C.ReadT();
			float z2 = C.ReadT();
			byte assistSlot = C.ReadC();
			byte unk2 = C.ReadC();
			byte[] unks = C.ReadB(8);
			SlotModel slot2 = room.GetSlot(b3);
			if (slot2 != null && slot2.State == SlotState.BATTLE)
			{
				FragModel item = new FragModel
				{
					VictimSlot = b3,
					WeaponClass = weaponClass,
					HitspotInfo = hitspotInfo,
					X = x2,
					Y = y2,
					Z = z2,
					AssistSlot = assistSlot,
					Unk = unk2,
					Unks = unks
				};
				if (fragInfos.KillerSlot == b3)
				{
					ısSuicide = true;
				}
				fragInfos.Frags.Add(item);
			}
		}
		fragInfos.KillsCount = (byte)fragInfos.Frags.Count;
		KillFragInfo.GenDeath(room, slot, fragInfos, ısSuicide);
	}

	public static void RegistryFragInfos(RoomModel Room, SlotModel Killer, out int Score, bool IsBotMode, bool IsSuicide, FragInfos Kills)
	{
		Score = 0;
		ItemClass ıdStatics = (ItemClass)ComDiv.GetIdStatics(Kills.WeaponId, 1);
		ClassType ıdStatics2 = (ClassType)ComDiv.GetIdStatics(Kills.WeaponId, 2);
		foreach (FragModel frag in Kills.Frags)
		{
			CharaDeath charaDeath = (CharaDeath)(frag.HitspotInfo >> 4);
			if ((int)Kills.KillsCount - (IsSuicide ? 1 : 0) > 1)
			{
				frag.KillFlag |= (KillingMessage)((charaDeath != CharaDeath.BOOM && charaDeath != CharaDeath.OBJECT_EXPLOSION && charaDeath != CharaDeath.POISON && charaDeath != CharaDeath.HOWL && charaDeath != CharaDeath.TRAMPLED && ıdStatics2 != ClassType.Shotgun) ? 1 : 2);
			}
			else
			{
				int num = 0;
				switch (charaDeath)
				{
				case CharaDeath.HEADSHOT:
					num = 4;
					break;
				case CharaDeath.DEFAULT:
					if (ıdStatics == ItemClass.Melee)
					{
						num = 6;
					}
					break;
				}
				if (num > 0)
				{
					int num2 = Killer.LastKillState >> 12;
					switch (num)
					{
					case 4:
						if (num2 != 4)
						{
							Killer.RepeatLastState = false;
						}
						Killer.LastKillState = (num << 12) | (Killer.KillsOnLife + 1);
						if (Killer.RepeatLastState)
						{
							frag.KillFlag |= (KillingMessage)(((Killer.LastKillState & 0x3FFF) <= 1) ? 8 : 16);
							break;
						}
						frag.KillFlag |= KillingMessage.Headshot;
						Killer.RepeatLastState = true;
						break;
					case 6:
						if (num2 != 6)
						{
							Killer.RepeatLastState = false;
						}
						Killer.LastKillState = (num << 12) | (Killer.KillsOnLife + 1);
						if (Killer.RepeatLastState && (Killer.LastKillState & 0x3FFF) > 1)
						{
							frag.KillFlag |= KillingMessage.ChainSlugger;
						}
						else
						{
							Killer.RepeatLastState = true;
						}
						break;
					}
				}
				else
				{
					Killer.LastKillState = 0;
					Killer.RepeatLastState = false;
				}
			}
			byte victimSlot = frag.VictimSlot;
			byte assistSlot = frag.AssistSlot;
			SlotModel slotModel = Room.Slots[victimSlot];
			SlotModel slotModel2 = Room.Slots[assistSlot];
			if (slotModel.KillsOnLife > 3)
			{
				frag.KillFlag |= KillingMessage.ChainStopper;
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
				Score += AllUtils.GetKillScore(frag.KillFlag);
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
				switch (ıdStatics2)
				{
				case ClassType.Shield:
					Killer.SHD[0]++;
					slotModel.SHD[1]++;
					break;
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
				case ClassType.Machinegun:
					Killer.MG[0]++;
					slotModel.MG[1]++;
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

	public static void EndBattleByDeath(RoomModel Room, SlotModel Killer, bool IsBotMode, bool IsSuicide, FragInfos Kills)
	{
		if (Room.RoomType == RoomCondition.DeathMatch && !IsBotMode)
		{
			AllUtils.BattleEndKills(Room, IsBotMode);
		}
		else if (Room.RoomType == RoomCondition.FreeForAll)
		{
			AllUtils.BattleEndKillsFreeForAll(Room);
		}
		else
		{
			if (Killer.SpecGM || (Room.RoomType != RoomCondition.Bomb && Room.RoomType != RoomCondition.Annihilation && Room.RoomType != RoomCondition.Destroy && Room.RoomType != RoomCondition.Ace))
			{
				return;
			}
			if (Room.RoomType != RoomCondition.Bomb && Room.RoomType != RoomCondition.Annihilation && Room.RoomType != RoomCondition.Destroy)
			{
				if (Room.RoomType == RoomCondition.Ace)
				{
					SlotModel[] array = new SlotModel[2]
					{
						Room.GetSlot(0),
						Room.GetSlot(1)
					};
					if (array[0].DeathState == DeadEnum.Dead)
					{
						Room.CTRounds++;
						AllUtils.BattleEndRound(Room, TeamEnum.CT_TEAM, ForceRestart: true, Kills, Killer);
					}
					else if (array[1].DeathState == DeadEnum.Dead)
					{
						Room.FRRounds++;
						AllUtils.BattleEndRound(Room, TeamEnum.FR_TEAM, ForceRestart: true, Kills, Killer);
					}
				}
				return;
			}
			TeamEnum teamEnum_ = TeamEnum.TEAM_DRAW;
			Room.GetPlayingPlayers(InBattle: true, out var PlayerFR, out var PlayerCT, out var DeathFR, out var DeathCT);
			smethod_0(Room, Killer, ref PlayerFR, ref PlayerCT, ref DeathFR, ref DeathCT, out var teamEnum_2);
			if (DeathFR == PlayerFR && teamEnum_2 == TeamEnum.FR_TEAM && IsSuicide && !Room.ActiveC4)
			{
				smethod_1(Room, ref teamEnum_, 1);
				AllUtils.BattleEndRound(Room, teamEnum_, ForceRestart: true, Kills, Killer);
			}
			else if (DeathCT == PlayerCT && teamEnum_2 == TeamEnum.CT_TEAM)
			{
				smethod_1(Room, ref teamEnum_, 2);
				AllUtils.BattleEndRound(Room, teamEnum_, ForceRestart: true, Kills, Killer);
			}
			else if (DeathFR == PlayerFR && teamEnum_2 == TeamEnum.CT_TEAM)
			{
				if (!Room.ActiveC4)
				{
					smethod_1(Room, ref teamEnum_, 1);
				}
				else if (IsSuicide)
				{
					smethod_1(Room, ref teamEnum_, 2);
				}
				AllUtils.BattleEndRound(Room, teamEnum_, ForceRestart: false, Kills, Killer);
			}
			else if (DeathCT == PlayerCT && teamEnum_2 == TeamEnum.FR_TEAM)
			{
				if (IsSuicide && Room.ActiveC4)
				{
					smethod_1(Room, ref teamEnum_, 1);
				}
				else
				{
					smethod_1(Room, ref teamEnum_, 2);
				}
				AllUtils.BattleEndRound(Room, teamEnum_, ForceRestart: true, Kills, Killer);
			}
		}
	}

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

	private static void smethod_1(RoomModel roomModel_0, ref TeamEnum teamEnum_0, int int_0)
	{
		switch (int_0)
		{
		case 1:
			if (roomModel_0.SwapRound)
			{
				teamEnum_0 = TeamEnum.FR_TEAM;
				roomModel_0.FRRounds++;
			}
			else
			{
				teamEnum_0 = TeamEnum.CT_TEAM;
				roomModel_0.CTRounds++;
			}
			break;
		case 2:
			if (roomModel_0.SwapRound)
			{
				teamEnum_0 = TeamEnum.CT_TEAM;
				roomModel_0.CTRounds++;
			}
			else
			{
				teamEnum_0 = TeamEnum.FR_TEAM;
				roomModel_0.FRRounds++;
			}
			break;
		}
	}
}
