namespace Server.Auth.Network.ServerPacket
{
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
                this.uint_0 = 0x80000000;
            }
            else
            {
                this.playerInventory_0 = account_1.Inventory;
                this.playerEquipment_0 = account_1.Equipment;
                this.playerStatistic_0 = account_1.Statistic;
                this.uint_1 = uint.Parse(account_1.LastLoginDate.ToString("yyMMddHHmm"));
                this.clanModel_0 = ClanManager.GetClanDB(account_1.ClanId, 1);
                this.list_0 = account_1.Quickstart.Quickjoins;
                this.list_1 = account_1.Character.Characters;
                this.eventVisitModel_0 = EventVisitXML.GetRunningEvent();
            }
        }

        private byte[] method_0(Account account_1, EventVisitModel eventVisitModel_1, uint uint_2)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                PlayerEvent event2 = account_1.Event;
                if ((eventVisitModel_1 == null) || ((event2 == null) || !eventVisitModel_1.EventIsEnabled()))
                {
                    packet.WriteB(new byte[9]);
                }
                else
                {
                    packet.WriteD(eventVisitModel_1.Id);
                    packet.WriteC((byte) event2.LastVisitCheckDay);
                    packet.WriteC((byte) (event2.LastVisitCheckDay - 1));
                    packet.WriteC((uint.Parse($"{DateTimeUtil.Convert($"{event2.LastVisitDate}"):yyMMdd}") < uint.Parse($"{DateTimeUtil.Convert($"{uint_2}"):yyMMdd}")) ? ((byte) 1) : ((byte) 2));
                    packet.WriteC((byte) event2.LastVisitSeqType);
                    packet.WriteC(1);
                }
                return packet.ToArray();
            }
        }

        private byte[] method_1(Account account_1, EventVisitModel eventVisitModel_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                PlayerEvent event2 = account_1.Event;
                if ((eventVisitModel_1 == null) || !eventVisitModel_1.EventIsEnabled())
                {
                    packet.WriteB(new byte[0x196]);
                }
                else
                {
                    EventVisitModel model = EventVisitXML.GetEvent(eventVisitModel_1.Id + 1);
                    packet.WriteU(eventVisitModel_1.Title, 70);
                    packet.WriteC((byte) event2.LastVisitCheckDay);
                    packet.WriteC((byte) eventVisitModel_1.Checks);
                    packet.WriteD(eventVisitModel_1.Id);
                    packet.WriteD(eventVisitModel_1.BeginDate);
                    packet.WriteD(eventVisitModel_1.EndedDate);
                    packet.WriteD((model != null) ? model.BeginDate : 0);
                    packet.WriteD((model != null) ? model.EndedDate : 0);
                    packet.WriteD(0);
                    for (int i = 0; i < 0x1f; i++)
                    {
                        VisitBoxModel model2 = eventVisitModel_1.Boxes[i];
                        packet.WriteC((byte) model2.IsBothReward);
                        packet.WriteC((byte) model2.RewardCount);
                        packet.WriteD(model2.Reward1.GoodId);
                        packet.WriteD(model2.Reward2.GoodId);
                    }
                }
                return packet.ToArray();
            }
        }

        private byte[] method_2(List<QuickstartModel> list_2)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) list_2.Count);
                foreach (QuickstartModel model in list_2)
                {
                    packet.WriteC((byte) model.MapId);
                    packet.WriteC((byte) model.Rule);
                    packet.WriteC((byte) model.StageOptions);
                    packet.WriteC((byte) model.Type);
                }
                return packet.ToArray();
            }
        }

        private byte[] method_3(int int_0)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) int_0);
                int num = 0;
                while (true)
                {
                    if (num >= int_0)
                    {
                        buffer = packet.ToArray();
                        break;
                    }
                    packet.WriteC(0);
                    packet.WriteC(3);
                    packet.WriteB(new byte[0x2b]);
                    num++;
                }
            }
            return buffer;
        }

        private byte[] method_4(int int_0)
        {
            byte[] buffer;
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) int_0);
                int num = 0;
                while (true)
                {
                    if (num >= int_0)
                    {
                        buffer = packet.ToArray();
                        break;
                    }
                    packet.WriteB(new byte[0x2d]);
                    num++;
                }
            }
            return buffer;
        }

        public override void Write()
        {
            base.WriteH((short) 0x90d);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteB(new byte[0x15]);
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
                base.WriteC((byte) this.list_1.Count);
                base.WriteC((byte) base.NATIONS);
                base.WriteC(0);
                base.WriteB(this.method_2(this.list_0));
                base.WriteB(new byte[0x21]);
                base.WriteC(4);
                base.WriteB(new byte[20]);
                base.WriteD(this.account_0.Title.Slots);
                base.WriteC(3);
                base.WriteC((byte) this.account_0.Title.Equiped1);
                base.WriteC((byte) this.account_0.Title.Equiped2);
                base.WriteC((byte) this.account_0.Title.Equiped3);
                base.WriteQ(this.account_0.Title.Flags);
                base.WriteC(160);
                base.WriteB(this.account_0.Mission.List1);
                base.WriteB(this.account_0.Mission.List2);
                base.WriteB(this.account_0.Mission.List3);
                base.WriteB(this.account_0.Mission.List4);
                base.WriteC((byte) this.account_0.Mission.ActualMission);
                base.WriteC((byte) this.account_0.Mission.Card1);
                base.WriteC((byte) this.account_0.Mission.Card2);
                base.WriteC((byte) this.account_0.Mission.Card3);
                base.WriteC((byte) this.account_0.Mission.Card4);
                base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission1, this.account_0.Mission.List1));
                base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission2, this.account_0.Mission.List2));
                base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission3, this.account_0.Mission.List3));
                base.WriteB(ComDiv.GetMissionCardFlags(this.account_0.Mission.Mission4, this.account_0.Mission.List4));
                base.WriteC((byte) this.account_0.Mission.Mission1);
                base.WriteC((byte) this.account_0.Mission.Mission2);
                base.WriteC((byte) this.account_0.Mission.Mission3);
                base.WriteC((byte) this.account_0.Mission.Mission4);
                base.WriteD(this.account_0.MasterMedal);
                base.WriteD(this.account_0.Medal);
                base.WriteD(this.account_0.Ensign);
                base.WriteD(this.account_0.Ribbon);
                base.WriteD(0);
                base.WriteC(0);
                base.WriteD(0);
                base.WriteC(2);
                base.WriteB(new byte[0x196]);
                base.WriteB(this.method_1(this.account_0, this.eventVisitModel_0));
                base.WriteC(2);
                base.WriteD(0);
                base.WriteC(0);
                base.WriteD(0);
                base.WriteB(this.method_0(this.account_0, this.eventVisitModel_0, this.uint_1));
                base.WriteB(ComDiv.AddressBytes("127.0.0.1"));
                base.WriteD(this.uint_1);
                this.WriteC((this.list_1.Count == 0) ? ((byte) 0) : ((byte) this.account_0.Character.GetCharacter(this.playerEquipment_0.CharaRedId).Slot));
                this.WriteC((this.list_1.Count == 0) ? ((byte) 1) : ((byte) this.account_0.Character.GetCharacter(this.playerEquipment_0.CharaBlueId).Slot));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.DinoItem));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.SprayId));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.NameCardId));
                base.WriteQ(AllUtils.LoadCouponEffects(this.account_0));
                base.WriteD(0);
                base.WriteC(0);
                base.WriteT(this.account_0.PointUp());
                base.WriteT(this.account_0.ExpUp());
                base.WriteC(0);
                base.WriteC((byte) this.account_0.NickColor);
                base.WriteD(this.account_0.Bonus.FakeRank);
                base.WriteD(this.account_0.Bonus.FakeRank);
                base.WriteU(this.account_0.Bonus.FakeNick, 0x42);
                base.WriteH((short) this.account_0.Bonus.CrosshairColor);
                base.WriteH((short) this.account_0.Bonus.MuzzleColor);
                base.WriteC((byte) this.account_0.Bonus.NickBorderColor);
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
                base.WriteU(this.account_0.Nickname, 0x42);
                base.WriteD(this.account_0.Rank);
                base.WriteD(this.account_0.GetRank());
                base.WriteD(this.account_0.Gold);
                base.WriteD(this.account_0.Exp);
                base.WriteD(0);
                base.WriteC(0);
                base.WriteQ((long) 0L);
                base.WriteC((byte) this.account_0.AuthLevel());
                base.WriteC(0);
                base.WriteD(this.account_0.Tags);
                base.WriteH((short) 0);
                base.WriteD(this.uint_1);
                base.WriteH((ushort) this.account_0.InventoryPlus);
                base.WriteD(this.account_0.Cash);
                base.WriteD(this.clanModel_0.Id);
                base.WriteD(this.account_0.ClanAccess);
                base.WriteQ(this.account_0.StatusId());
                base.WriteC((byte) this.account_0.CafePC);
                base.WriteC((byte) this.account_0.Country);
                base.WriteU(this.clanModel_0.Name, 0x22);
                base.WriteC((byte) this.clanModel_0.Rank);
                base.WriteC((byte) this.clanModel_0.GetClanUnit());
                base.WriteD(this.clanModel_0.Logo);
                base.WriteC((byte) this.clanModel_0.NameColor);
                base.WriteC((byte) this.clanModel_0.Effect);
                this.WriteC(AuthXender.Client.Config.EnableBlood ? ((byte) this.account_0.Age) : ((byte) 0x2a));
            }
        }
    }
}

