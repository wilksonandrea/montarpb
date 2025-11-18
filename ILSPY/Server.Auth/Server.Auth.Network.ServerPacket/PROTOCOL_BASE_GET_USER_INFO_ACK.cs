using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GET_USER_INFO_ACK : AuthServerPacket
{
	private readonly Account account_0;

	private readonly ClanModel clanModel_0;

	private readonly PlayerInventory playerInventory_0;

	private readonly PlayerEquipment playerEquipment_0;

	private readonly PlayerStatistic playerStatistic_0;

	private readonly EventVisitModel eventVisitModel_0;

	private readonly List<QuickstartModel> list_0;

	private readonly List<CharacterModel> list_1;

	private readonly uint uint_0;

	private readonly uint uint_1;

	public PROTOCOL_BASE_GET_USER_INFO_ACK(Account account_1)
	{
		account_0 = account_1;
		if (account_1 != null)
		{
			playerInventory_0 = account_1.Inventory;
			playerEquipment_0 = account_1.Equipment;
			playerStatistic_0 = account_1.Statistic;
			uint_1 = uint.Parse(account_1.LastLoginDate.ToString("yyMMddHHmm"));
			clanModel_0 = ClanManager.GetClanDB(account_1.ClanId, 1);
			list_0 = account_1.Quickstart.Quickjoins;
			list_1 = account_1.Character.Characters;
			eventVisitModel_0 = EventVisitXML.GetRunningEvent();
		}
		else
		{
			uint_0 = 2147483648u;
		}
	}

	public override void Write()
	{
		WriteH(2317);
		WriteH(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteB(new byte[21]);
			WriteD(1);
			WriteC(1);
			WriteC(6);
			WriteC(1);
			WriteB(new byte[160]);
			WriteD(playerStatistic_0.Battlecup.Matches);
			WriteD(playerStatistic_0.GetBCWinRatio());
			WriteD(playerStatistic_0.Battlecup.MatchLoses);
			WriteD(playerStatistic_0.Battlecup.KillsCount);
			WriteD(playerStatistic_0.Battlecup.DeathsCount);
			WriteD(playerStatistic_0.Battlecup.HeadshotsCount);
			WriteD(playerStatistic_0.Battlecup.AssistsCount);
			WriteD(playerStatistic_0.Battlecup.EscapesCount);
			WriteD(playerStatistic_0.GetBCKDRatio());
			WriteD(playerStatistic_0.Battlecup.MatchWins);
			WriteD(playerStatistic_0.Battlecup.AverageDamage);
			WriteD(playerStatistic_0.Battlecup.PlayTime);
			WriteD(playerStatistic_0.Acemode.Matches);
			WriteD(playerStatistic_0.Acemode.MatchWins);
			WriteD(playerStatistic_0.Acemode.MatchLoses);
			WriteD(playerStatistic_0.Acemode.Kills);
			WriteD(playerStatistic_0.Acemode.Deaths);
			WriteD(playerStatistic_0.Acemode.Headshots);
			WriteD(playerStatistic_0.Acemode.Assists);
			WriteD(playerStatistic_0.Acemode.Escapes);
			WriteD(playerStatistic_0.Acemode.Winstreaks);
			WriteD(0);
			WriteD(0);
			WriteD(0);
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.AccessoryId));
			WriteB(method_4(3));
			WriteB(method_3(3));
			WriteD(playerStatistic_0.Weapon.AssaultKills);
			WriteD(playerStatistic_0.Weapon.AssaultDeaths);
			WriteD(playerStatistic_0.Weapon.SmgKills);
			WriteD(playerStatistic_0.Weapon.SmgDeaths);
			WriteD(playerStatistic_0.Weapon.SniperKills);
			WriteD(playerStatistic_0.Weapon.SniperDeaths);
			WriteD(playerStatistic_0.Weapon.MachinegunKills);
			WriteD(playerStatistic_0.Weapon.MachinegunDeaths);
			WriteD(playerStatistic_0.Weapon.ShotgunKills);
			WriteD(playerStatistic_0.Weapon.ShotgunDeaths);
			WriteD(playerStatistic_0.Weapon.ShieldKills);
			WriteD(playerStatistic_0.Weapon.ShieldDeaths);
			WriteC((byte)list_1.Count);
			WriteC((byte)NATIONS);
			WriteC(0);
			WriteB(method_2(list_0));
			WriteB(new byte[33]);
			WriteC(4);
			WriteB(new byte[20]);
			WriteD(account_0.Title.Slots);
			WriteC(3);
			WriteC((byte)account_0.Title.Equiped1);
			WriteC((byte)account_0.Title.Equiped2);
			WriteC((byte)account_0.Title.Equiped3);
			WriteQ(account_0.Title.Flags);
			WriteC(160);
			WriteB(account_0.Mission.List1);
			WriteB(account_0.Mission.List2);
			WriteB(account_0.Mission.List3);
			WriteB(account_0.Mission.List4);
			WriteC((byte)account_0.Mission.ActualMission);
			WriteC((byte)account_0.Mission.Card1);
			WriteC((byte)account_0.Mission.Card2);
			WriteC((byte)account_0.Mission.Card3);
			WriteC((byte)account_0.Mission.Card4);
			WriteB(ComDiv.GetMissionCardFlags(account_0.Mission.Mission1, account_0.Mission.List1));
			WriteB(ComDiv.GetMissionCardFlags(account_0.Mission.Mission2, account_0.Mission.List2));
			WriteB(ComDiv.GetMissionCardFlags(account_0.Mission.Mission3, account_0.Mission.List3));
			WriteB(ComDiv.GetMissionCardFlags(account_0.Mission.Mission4, account_0.Mission.List4));
			WriteC((byte)account_0.Mission.Mission1);
			WriteC((byte)account_0.Mission.Mission2);
			WriteC((byte)account_0.Mission.Mission3);
			WriteC((byte)account_0.Mission.Mission4);
			WriteD(account_0.MasterMedal);
			WriteD(account_0.Medal);
			WriteD(account_0.Ensign);
			WriteD(account_0.Ribbon);
			WriteD(0);
			WriteC(0);
			WriteD(0);
			WriteC(2);
			WriteB(new byte[406]);
			WriteB(method_1(account_0, eventVisitModel_0));
			WriteC(2);
			WriteD(0);
			WriteC(0);
			WriteD(0);
			WriteB(method_0(account_0, eventVisitModel_0, uint_1));
			WriteB(ComDiv.AddressBytes("127.0.0.1"));
			WriteD(uint_1);
			WriteC((byte)((list_1.Count != 0) ? ((uint)account_0.Character.GetCharacter(playerEquipment_0.CharaRedId).Slot) : 0u));
			WriteC((byte)((list_1.Count == 0) ? 1u : ((uint)account_0.Character.GetCharacter(playerEquipment_0.CharaBlueId).Slot)));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.DinoItem));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.SprayId));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.NameCardId));
			WriteQ(AllUtils.LoadCouponEffects(account_0));
			WriteD(0);
			WriteC(0);
			WriteT(account_0.PointUp());
			WriteT(account_0.ExpUp());
			WriteC(0);
			WriteC((byte)account_0.NickColor);
			WriteD(account_0.Bonus.FakeRank);
			WriteD(account_0.Bonus.FakeRank);
			WriteU(account_0.Bonus.FakeNick, 66);
			WriteH((short)account_0.Bonus.CrosshairColor);
			WriteH((short)account_0.Bonus.MuzzleColor);
			WriteC((byte)account_0.Bonus.NickBorderColor);
			WriteD(playerStatistic_0.Season.Matches);
			WriteD(playerStatistic_0.Season.MatchWins);
			WriteD(playerStatistic_0.Season.MatchLoses);
			WriteD(playerStatistic_0.Season.MatchDraws);
			WriteD(playerStatistic_0.Season.KillsCount);
			WriteD(playerStatistic_0.Season.HeadshotsCount);
			WriteD(playerStatistic_0.Season.DeathsCount);
			WriteD(playerStatistic_0.Season.TotalMatchesCount);
			WriteD(playerStatistic_0.Season.TotalKillsCount);
			WriteD(playerStatistic_0.Season.EscapesCount);
			WriteD(playerStatistic_0.Season.AssistsCount);
			WriteD(playerStatistic_0.Season.MvpCount);
			WriteD(playerStatistic_0.Basic.Matches);
			WriteD(playerStatistic_0.Basic.MatchWins);
			WriteD(playerStatistic_0.Basic.MatchLoses);
			WriteD(playerStatistic_0.Basic.MatchDraws);
			WriteD(playerStatistic_0.Basic.KillsCount);
			WriteD(playerStatistic_0.Basic.HeadshotsCount);
			WriteD(playerStatistic_0.Basic.DeathsCount);
			WriteD(playerStatistic_0.Basic.TotalMatchesCount);
			WriteD(playerStatistic_0.Basic.TotalKillsCount);
			WriteD(playerStatistic_0.Basic.EscapesCount);
			WriteD(playerStatistic_0.Basic.AssistsCount);
			WriteD(playerStatistic_0.Basic.MvpCount);
			WriteU(account_0.Nickname, 66);
			WriteD(account_0.Rank);
			WriteD(account_0.GetRank());
			WriteD(account_0.Gold);
			WriteD(account_0.Exp);
			WriteD(0);
			WriteC(0);
			WriteQ(0L);
			WriteC((byte)account_0.AuthLevel());
			WriteC(0);
			WriteD(account_0.Tags);
			WriteH(0);
			WriteD(uint_1);
			WriteH((ushort)account_0.InventoryPlus);
			WriteD(account_0.Cash);
			WriteD(clanModel_0.Id);
			WriteD(account_0.ClanAccess);
			WriteQ(account_0.StatusId());
			WriteC((byte)account_0.CafePC);
			WriteC((byte)account_0.Country);
			WriteU(clanModel_0.Name, 34);
			WriteC((byte)clanModel_0.Rank);
			WriteC((byte)clanModel_0.GetClanUnit());
			WriteD(clanModel_0.Logo);
			WriteC((byte)clanModel_0.NameColor);
			WriteC((byte)clanModel_0.Effect);
			WriteC((byte)(AuthXender.Client.Config.EnableBlood ? ((uint)account_0.Age) : 42u));
		}
	}

	private byte[] method_0(Account account_1, EventVisitModel eventVisitModel_1, uint uint_2)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		PlayerEvent @event = account_1.Event;
		if (eventVisitModel_1 != null && @event != null && eventVisitModel_1.EventIsEnabled())
		{
			uint num = uint.Parse($"{DateTimeUtil.Convert($"{uint_2}"):yyMMdd}");
			uint num2 = uint.Parse($"{DateTimeUtil.Convert($"{@event.LastVisitDate}"):yyMMdd}");
			syncServerPacket.WriteD(eventVisitModel_1.Id);
			syncServerPacket.WriteC((byte)@event.LastVisitCheckDay);
			syncServerPacket.WriteC((byte)(@event.LastVisitCheckDay - 1));
			syncServerPacket.WriteC((byte)((num2 < num) ? 1u : 2u));
			syncServerPacket.WriteC((byte)@event.LastVisitSeqType);
			syncServerPacket.WriteC(1);
		}
		else
		{
			syncServerPacket.WriteB(new byte[9]);
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_1(Account account_1, EventVisitModel eventVisitModel_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		PlayerEvent @event = account_1.Event;
		if (eventVisitModel_1 != null && eventVisitModel_1.EventIsEnabled())
		{
			EventVisitModel event2 = EventVisitXML.GetEvent(eventVisitModel_1.Id + 1);
			syncServerPacket.WriteU(eventVisitModel_1.Title, 70);
			syncServerPacket.WriteC((byte)@event.LastVisitCheckDay);
			syncServerPacket.WriteC((byte)eventVisitModel_1.Checks);
			syncServerPacket.WriteD(eventVisitModel_1.Id);
			syncServerPacket.WriteD(eventVisitModel_1.BeginDate);
			syncServerPacket.WriteD(eventVisitModel_1.EndedDate);
			syncServerPacket.WriteD(event2?.BeginDate ?? 0);
			syncServerPacket.WriteD(event2?.EndedDate ?? 0);
			syncServerPacket.WriteD(0);
			for (int i = 0; i < 31; i++)
			{
				VisitBoxModel visitBoxModel = eventVisitModel_1.Boxes[i];
				syncServerPacket.WriteC(visitBoxModel.IsBothReward ? ((byte)1) : ((byte)0));
				syncServerPacket.WriteC((byte)visitBoxModel.RewardCount);
				syncServerPacket.WriteD(visitBoxModel.Reward1.GoodId);
				syncServerPacket.WriteD(visitBoxModel.Reward2.GoodId);
			}
		}
		else
		{
			syncServerPacket.WriteB(new byte[406]);
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_2(List<QuickstartModel> list_2)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)list_2.Count);
		foreach (QuickstartModel item in list_2)
		{
			syncServerPacket.WriteC((byte)item.MapId);
			syncServerPacket.WriteC((byte)item.Rule);
			syncServerPacket.WriteC((byte)item.StageOptions);
			syncServerPacket.WriteC((byte)item.Type);
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_3(int int_0)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)int_0);
		for (int i = 0; i < int_0; i++)
		{
			syncServerPacket.WriteC(0);
			syncServerPacket.WriteC(3);
			syncServerPacket.WriteB(new byte[43]);
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_4(int int_0)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		syncServerPacket.WriteC((byte)int_0);
		for (int i = 0; i < int_0; i++)
		{
			syncServerPacket.WriteB(new byte[45]);
		}
		return syncServerPacket.ToArray();
	}
}
