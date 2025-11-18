using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Data.Utils;
using Server.Auth.Network;
using System;
using System.Collections.Generic;

namespace Server.Auth.Network.ServerPacket
{
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
			this.account_0 = account_1;
			if (account_1 == null)
			{
				this.uint_0 = -2147483648;
				return;
			}
			this.playerInventory_0 = account_1.Inventory;
			this.playerEquipment_0 = account_1.Equipment;
			this.playerStatistic_0 = account_1.Statistic;
			this.uint_1 = uint.Parse(account_1.LastLoginDate.ToString("yyMMddHHmm"));
			this.clanModel_0 = ClanManager.GetClanDB(account_1.ClanId, 1);
			this.list_0 = account_1.Quickstart.Quickjoins;
			this.list_1 = account_1.Character.Characters;
			this.eventVisitModel_0 = EventVisitXML.GetRunningEvent();
		}

		private byte[] method_0(Account account_1, EventVisitModel eventVisitModel_1, uint uint_2)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				PlayerEvent @event = account_1.Event;
				if (eventVisitModel_1 == null || @event == null || !eventVisitModel_1.EventIsEnabled())
				{
					syncServerPacket.WriteB(new byte[9]);
				}
				else
				{
					uint uInt32 = uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", uint_2))));
					uint uInt321 = uint.Parse(string.Format("{0:yyMMdd}", DateTimeUtil.Convert(string.Format("{0}", @event.LastVisitDate))));
					syncServerPacket.WriteD(eventVisitModel_1.Id);
					syncServerPacket.WriteC((byte)@event.LastVisitCheckDay);
					syncServerPacket.WriteC((byte)(@event.LastVisitCheckDay - 1));
					syncServerPacket.WriteC((byte)((uInt321 < uInt32 ? 1 : 2)));
					syncServerPacket.WriteC((byte)@event.LastVisitSeqType);
					syncServerPacket.WriteC(1);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_1(Account account_1, EventVisitModel eventVisitModel_1)
		{
			byte[] array;
			uint beginDate;
			uint endedDate;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				PlayerEvent @event = account_1.Event;
				if (eventVisitModel_1 == null || !eventVisitModel_1.EventIsEnabled())
				{
					syncServerPacket.WriteB(new byte[406]);
				}
				else
				{
					EventVisitModel eventVisitModel = EventVisitXML.GetEvent(eventVisitModel_1.Id + 1);
					syncServerPacket.WriteU(eventVisitModel_1.Title, 70);
					syncServerPacket.WriteC((byte)@event.LastVisitCheckDay);
					syncServerPacket.WriteC((byte)eventVisitModel_1.Checks);
					syncServerPacket.WriteD(eventVisitModel_1.Id);
					syncServerPacket.WriteD(eventVisitModel_1.BeginDate);
					syncServerPacket.WriteD(eventVisitModel_1.EndedDate);
					SyncServerPacket syncServerPacket1 = syncServerPacket;
					if (eventVisitModel != null)
					{
						beginDate = eventVisitModel.BeginDate;
					}
					else
					{
						beginDate = 0;
					}
					syncServerPacket1.WriteD(beginDate);
					SyncServerPacket syncServerPacket2 = syncServerPacket;
					if (eventVisitModel != null)
					{
						endedDate = eventVisitModel.EndedDate;
					}
					else
					{
						endedDate = 0;
					}
					syncServerPacket2.WriteD(endedDate);
					syncServerPacket.WriteD(0);
					for (int i = 0; i < 31; i++)
					{
						VisitBoxModel ıtem = eventVisitModel_1.Boxes[i];
						syncServerPacket.WriteC((byte)ıtem.IsBothReward);
						syncServerPacket.WriteC((byte)ıtem.RewardCount);
						syncServerPacket.WriteD(ıtem.Reward1.GoodId);
						syncServerPacket.WriteD(ıtem.Reward2.GoodId);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_2(List<QuickstartModel> list_2)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)list_2.Count);
				foreach (QuickstartModel list2 in list_2)
				{
					syncServerPacket.WriteC((byte)list2.MapId);
					syncServerPacket.WriteC((byte)list2.Rule);
					syncServerPacket.WriteC((byte)list2.StageOptions);
					syncServerPacket.WriteC((byte)list2.Type);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_3(int int_0)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)int_0);
				for (int i = 0; i < int_0; i++)
				{
					syncServerPacket.WriteC(0);
					syncServerPacket.WriteC(3);
					syncServerPacket.WriteB(new byte[43]);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_4(int int_0)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				syncServerPacket.WriteC((byte)int_0);
				for (int i = 0; i < int_0; i++)
				{
					syncServerPacket.WriteB(new byte[45]);
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		public override void Write()
		{
			object slot;
			base.WriteH(2317);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteB(new byte[21]);
				base.WriteD(1);
				base.WriteC(1);
				base.WriteC(6);
				base.WriteC(1);
				base.WriteB(new byte[160]);
				base.WriteD(this.playerStatistic_0.Battlecup.Matches);
				base.WriteD(this.playerStatistic_0.GetBCWinRatio());
				base.WriteD(this.playerStatistic_0.Battlecup.MatchLoses);
				base.WriteD(this.playerStatistic_0.Battlecup.KillsCount);
				base.WriteD(this.playerStatistic_0.Battlecup.DeathsCount);
				base.WriteD(this.playerStatistic_0.Battlecup.HeadshotsCount);
				base.WriteD(this.playerStatistic_0.Battlecup.AssistsCount);
				base.WriteD(this.playerStatistic_0.Battlecup.EscapesCount);
				base.WriteD(this.playerStatistic_0.GetBCKDRatio());
				base.WriteD(this.playerStatistic_0.Battlecup.MatchWins);
				base.WriteD(this.playerStatistic_0.Battlecup.AverageDamage);
				base.WriteD(this.playerStatistic_0.Battlecup.PlayTime);
				base.WriteD(this.playerStatistic_0.Acemode.Matches);
				base.WriteD(this.playerStatistic_0.Acemode.MatchWins);
				base.WriteD(this.playerStatistic_0.Acemode.MatchLoses);
				base.WriteD(this.playerStatistic_0.Acemode.Kills);
				base.WriteD(this.playerStatistic_0.Acemode.Deaths);
				base.WriteD(this.playerStatistic_0.Acemode.Headshots);
				base.WriteD(this.playerStatistic_0.Acemode.Assists);
				base.WriteD(this.playerStatistic_0.Acemode.Escapes);
				base.WriteD(this.playerStatistic_0.Acemode.Winstreaks);
				base.WriteD(0);
				base.WriteD(0);
				base.WriteD(0);
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.AccessoryId));
				base.WriteB(this.method_4(3));
				base.WriteB(this.method_3(3));
				base.WriteD(this.playerStatistic_0.Weapon.AssaultKills);
				base.WriteD(this.playerStatistic_0.Weapon.AssaultDeaths);
				base.WriteD(this.playerStatistic_0.Weapon.SmgKills);
				base.WriteD(this.playerStatistic_0.Weapon.SmgDeaths);
				base.WriteD(this.playerStatistic_0.Weapon.SniperKills);
				base.WriteD(this.playerStatistic_0.Weapon.SniperDeaths);
				base.WriteD(this.playerStatistic_0.Weapon.MachinegunKills);
				base.WriteD(this.playerStatistic_0.Weapon.MachinegunDeaths);
				base.WriteD(this.playerStatistic_0.Weapon.ShotgunKills);
				base.WriteD(this.playerStatistic_0.Weapon.ShotgunDeaths);
				base.WriteD(this.playerStatistic_0.Weapon.ShieldKills);
				base.WriteD(this.playerStatistic_0.Weapon.ShieldDeaths);
				base.WriteC((byte)this.list_1.Count);
				base.WriteC((byte)this.NATIONS);
				base.WriteC(0);
				base.WriteB(this.method_2(this.list_0));
				base.WriteB(new byte[33]);
				base.WriteC(4);
				base.WriteB(new byte[20]);
				base.WriteD(this.account_0.Title.Slots);
				base.WriteC(3);
				base.WriteC((byte)this.account_0.Title.Equiped1);
				base.WriteC((byte)this.account_0.Title.Equiped2);
				base.WriteC((byte)this.account_0.Title.Equiped3);
				base.WriteQ(this.account_0.Title.Flags);
				base.WriteC(160);
				base.WriteB(this.account_0.Mission.List1);
				base.WriteB(this.account_0.Mission.List2);
				base.WriteB(this.account_0.Mission.List3);
				base.WriteB(this.account_0.Mission.List4);
				base.WriteC((byte)this.account_0.Mission.ActualMission);
				base.WriteC((byte)this.account_0.Mission.Card1);
				base.WriteC((byte)this.account_0.Mission.Card2);
				base.WriteC((byte)this.account_0.Mission.Card3);
				base.WriteC((byte)this.account_0.Mission.Card4);
				base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission1, this.account_0.Mission.List1));
				base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission2, this.account_0.Mission.List2));
				base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission3, this.account_0.Mission.List3));
				base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission4, this.account_0.Mission.List4));
				base.WriteC((byte)this.account_0.Mission.Mission1);
				base.WriteC((byte)this.account_0.Mission.Mission2);
				base.WriteC((byte)this.account_0.Mission.Mission3);
				base.WriteC((byte)this.account_0.Mission.Mission4);
				base.WriteD(this.account_0.MasterMedal);
				base.WriteD(this.account_0.Medal);
				base.WriteD(this.account_0.Ensign);
				base.WriteD(this.account_0.Ribbon);
				base.WriteD(0);
				base.WriteC(0);
				base.WriteD(0);
				base.WriteC(2);
				base.WriteB(new byte[406]);
				base.WriteB(this.method_1(this.account_0, this.eventVisitModel_0));
				base.WriteC(2);
				base.WriteD(0);
				base.WriteC(0);
				base.WriteD(0);
				base.WriteB(this.method_0(this.account_0, this.eventVisitModel_0, this.uint_1));
				base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
				base.WriteD(this.uint_1);
				if (this.list_1.Count == 0)
				{
					slot = null;
				}
				else
				{
					slot = this.account_0.Character.GetCharacter(this.playerEquipment_0.CharaRedId).Slot;
				}
				base.WriteC((byte)slot);
				base.WriteC((byte)((this.list_1.Count == 0 ? 1 : this.account_0.Character.GetCharacter(this.playerEquipment_0.CharaBlueId).Slot)));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.DinoItem));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.SprayId));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.NameCardId));
				base.WriteQ(AllUtils.LoadCouponEffects(this.account_0));
				base.WriteD(0);
				base.WriteC(0);
				base.WriteT(this.account_0.PointUp());
				base.WriteT(this.account_0.ExpUp());
				base.WriteC(0);
				base.WriteC((byte)this.account_0.NickColor);
				base.WriteD(this.account_0.Bonus.FakeRank);
				base.WriteD(this.account_0.Bonus.FakeRank);
				base.WriteU(this.account_0.Bonus.FakeNick, 66);
				base.WriteH((short)this.account_0.Bonus.CrosshairColor);
				base.WriteH((short)this.account_0.Bonus.MuzzleColor);
				base.WriteC((byte)this.account_0.Bonus.NickBorderColor);
				base.WriteD(this.playerStatistic_0.Season.Matches);
				base.WriteD(this.playerStatistic_0.Season.MatchWins);
				base.WriteD(this.playerStatistic_0.Season.MatchLoses);
				base.WriteD(this.playerStatistic_0.Season.MatchDraws);
				base.WriteD(this.playerStatistic_0.Season.KillsCount);
				base.WriteD(this.playerStatistic_0.Season.HeadshotsCount);
				base.WriteD(this.playerStatistic_0.Season.DeathsCount);
				base.WriteD(this.playerStatistic_0.Season.TotalMatchesCount);
				base.WriteD(this.playerStatistic_0.Season.TotalKillsCount);
				base.WriteD(this.playerStatistic_0.Season.EscapesCount);
				base.WriteD(this.playerStatistic_0.Season.AssistsCount);
				base.WriteD(this.playerStatistic_0.Season.MvpCount);
				base.WriteD(this.playerStatistic_0.Basic.Matches);
				base.WriteD(this.playerStatistic_0.Basic.MatchWins);
				base.WriteD(this.playerStatistic_0.Basic.MatchLoses);
				base.WriteD(this.playerStatistic_0.Basic.MatchDraws);
				base.WriteD(this.playerStatistic_0.Basic.KillsCount);
				base.WriteD(this.playerStatistic_0.Basic.HeadshotsCount);
				base.WriteD(this.playerStatistic_0.Basic.DeathsCount);
				base.WriteD(this.playerStatistic_0.Basic.TotalMatchesCount);
				base.WriteD(this.playerStatistic_0.Basic.TotalKillsCount);
				base.WriteD(this.playerStatistic_0.Basic.EscapesCount);
				base.WriteD(this.playerStatistic_0.Basic.AssistsCount);
				base.WriteD(this.playerStatistic_0.Basic.MvpCount);
				base.WriteU(this.account_0.Nickname, 66);
				base.WriteD(this.account_0.Rank);
				base.WriteD(this.account_0.GetRank());
				base.WriteD(this.account_0.Gold);
				base.WriteD(this.account_0.Exp);
				base.WriteD(0);
				base.WriteC(0);
				base.WriteQ(0L);
				base.WriteC((byte)this.account_0.AuthLevel());
				base.WriteC(0);
				base.WriteD(this.account_0.Tags);
				base.WriteH(0);
				base.WriteD(this.uint_1);
				base.WriteH((ushort)this.account_0.InventoryPlus);
				base.WriteD(this.account_0.Cash);
				base.WriteD(this.clanModel_0.Id);
				base.WriteD(this.account_0.ClanAccess);
				base.WriteQ(this.account_0.StatusId());
				base.WriteC((byte)this.account_0.CafePC);
				base.WriteC((byte)this.account_0.Country);
				base.WriteU(this.clanModel_0.Name, 34);
				base.WriteC((byte)this.clanModel_0.Rank);
				base.WriteC((byte)this.clanModel_0.GetClanUnit());
				base.WriteD(this.clanModel_0.Logo);
				base.WriteC((byte)this.clanModel_0.NameColor);
				base.WriteC((byte)this.clanModel_0.Effect);
				base.WriteC((byte)((AuthXender.Client.Config.EnableBlood ? this.account_0.Age : 42)));
			}
		}
	}
}