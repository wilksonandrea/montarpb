namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly Account account_0;
        private readonly PlayerInventory playerInventory_0;
        private readonly PlayerEquipment playerEquipment_0;
        private readonly StatisticSeason statisticSeason_0;
        private readonly StatisticWeapon statisticWeapon_0;
        private readonly StatisticAcemode statisticAcemode_0;
        private readonly StatisticBattlecup statisticBattlecup_0;
        private readonly int int_0;

        public PROTOCOL_BASE_GET_USER_DETAIL_INFO_ACK(uint uint_1, Account account_1, int int_1)
        {
            this.uint_0 = uint_1;
            this.account_0 = account_1;
            if (account_1 != null)
            {
                this.playerInventory_0 = account_1.Inventory;
                this.playerEquipment_0 = account_1.Equipment;
                this.statisticSeason_0 = account_1.Statistic.Season;
                this.statisticWeapon_0 = account_1.Statistic.Weapon;
                this.statisticAcemode_0 = account_1.Statistic.Acemode;
                this.statisticBattlecup_0 = account_1.Statistic.Battlecup;
                this.int_0 = (int_1 == 0x7fffffff) ? this.playerEquipment_0.CharaRedId : int_1;
            }
        }

        private byte[] method_0(Account account_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                ClanModel clan = ClanManager.GetClan(account_1.ClanId);
                packet.WriteU(clan.Name, 0x22);
                packet.WriteD(clan.Logo);
                packet.WriteC((byte) clan.Effect);
                return packet.ToArray();
            }
        }

        private byte[] method_1(Account account_1)
        {
            FriendState state;
            int num;
            int num2;
            int num3;
            if (((account_1 != null) ? account_1.Status : null) == null)
            {
                byte[] buffer1 = new byte[4];
                buffer1[0] = 3;
                return buffer1;
            }
            ComDiv.GetPlayerLocation(account_1.Status, account_1.IsOnline, out state, out num, out num2, out num3);
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                packet.WriteC((byte) state);
                packet.WriteC((byte) num3);
                packet.WriteC((byte) num2);
                packet.WriteC((byte) num);
                return packet.ToArray();
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x986);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteQ(this.account_0.PlayerId);
                base.WriteU(this.account_0.Nickname, 0x42);
                base.WriteB(this.method_0(this.account_0));
                if ((this.account_0.GetRank() != 0x35) && (this.account_0.GetRank() != 0x36))
                {
                    base.WriteC((byte) this.account_0.GetRank());
                }
                else
                {
                    base.WriteC(0x37);
                }
                base.WriteD(this.account_0.Exp);
                base.WriteD(this.statisticSeason_0.Matches);
                base.WriteD(this.statisticSeason_0.MatchWins);
                base.WriteD(this.statisticSeason_0.MatchDraws);
                base.WriteD(this.statisticSeason_0.MatchLoses);
                base.WriteD(this.statisticSeason_0.EscapesCount);
                base.WriteD(this.statisticSeason_0.KillsCount);
                base.WriteD(this.statisticSeason_0.DeathsCount);
                base.WriteD(this.statisticSeason_0.HeadshotsCount);
                base.WriteD(this.statisticSeason_0.AssistsCount);
                base.WriteD(this.statisticSeason_0.MvpCount);
                base.WriteB(new byte[0x2d]);
                base.WriteD(0x3e8);
                base.WriteB(new byte[0x52]);
                base.WriteD(0);
                base.WriteB(new byte[0x80]);
                base.WriteC(5);
                base.WriteC(1);
                base.WriteC(0);
                base.WriteD(ComDiv.GetPlayerStatus(this.account_0.Status, this.account_0.IsOnline));
                base.WriteB(this.method_1(this.account_0));
                base.WriteC(8);
                base.WriteC(0);
                base.WriteC(7);
                base.WriteC(0);
                base.WriteD(0x34);
                base.WriteC(1);
                base.WriteD(0);
                base.WriteD(0);
                base.WriteD(0);
                base.WriteC(0);
                base.WriteC((byte) this.account_0.NickColor);
                base.WriteD(this.account_0.Bonus.FakeRank);
                base.WriteD(this.account_0.Bonus.FakeRank);
                base.WriteU(this.account_0.Bonus.FakeNick, 0x42);
                base.WriteH((short) this.account_0.Bonus.CrosshairColor);
                base.WriteH((short) this.account_0.Bonus.MuzzleColor);
                base.WriteC((byte) this.account_0.Bonus.NickBorderColor);
                base.WriteC(2);
                base.WriteC(0);
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponPrimary));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSecondary));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponMelee));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponExplosive));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSpecial));
                base.WriteB(this.playerInventory_0.EquipmentData(this.int_0));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHead));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartFace));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartJacket));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartPocket));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartGlove));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartBelt));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHolster));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartSkin));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.BeretItem));
                base.WriteC(0);
                base.WriteD(this.playerEquipment_0.CharaRedId);
                base.WriteD(this.playerEquipment_0.CharaBlueId);
                base.WriteB(new byte[0x277]);
                base.WriteD(this.statisticWeapon_0.AssaultKills);
                base.WriteD(this.statisticWeapon_0.AssaultDeaths);
                base.WriteD(this.statisticWeapon_0.SmgKills);
                base.WriteD(this.statisticWeapon_0.SmgDeaths);
                base.WriteD(this.statisticWeapon_0.SniperKills);
                base.WriteD(this.statisticWeapon_0.SniperDeaths);
                base.WriteD(this.statisticWeapon_0.MachinegunKills);
                base.WriteD(this.statisticWeapon_0.MachinegunDeaths);
                base.WriteD(this.statisticWeapon_0.ShotgunKills);
                base.WriteD(this.statisticWeapon_0.ShotgunDeaths);
                base.WriteD(this.statisticWeapon_0.ShieldKills);
                base.WriteD(this.statisticWeapon_0.ShieldDeaths);
                base.WriteD(this.playerEquipment_0.CharaRedId);
                base.WriteD(this.playerEquipment_0.CharaBlueId);
                base.WriteC(0);
                base.WriteD(0x10);
                base.WriteD(this.playerEquipment_0.NameCardId);
                base.WriteC(0);
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.SprayId));
                base.WriteD(this.statisticAcemode_0.Matches);
                base.WriteD(this.statisticAcemode_0.MatchWins);
                base.WriteD(this.statisticAcemode_0.MatchLoses);
                base.WriteD(this.statisticAcemode_0.Kills);
                base.WriteD(this.statisticAcemode_0.Deaths);
                base.WriteD(this.statisticAcemode_0.Headshots);
                base.WriteD(this.statisticAcemode_0.Assists);
                base.WriteD(this.statisticAcemode_0.Escapes);
                base.WriteD(this.statisticAcemode_0.Winstreaks);
                base.WriteD(this.statisticBattlecup_0.Matches);
                base.WriteD(this.account_0.Statistic.GetBCWinRatio());
                base.WriteD(this.statisticBattlecup_0.MatchLoses);
                base.WriteD(this.statisticBattlecup_0.KillsCount);
                base.WriteD(this.statisticBattlecup_0.DeathsCount);
                base.WriteD(this.statisticBattlecup_0.HeadshotsCount);
                base.WriteD(this.statisticBattlecup_0.AssistsCount);
                base.WriteD(this.statisticBattlecup_0.EscapesCount);
                base.WriteD(this.account_0.Statistic.GetBCKDRatio());
                base.WriteD(this.statisticBattlecup_0.MatchWins);
                base.WriteD(this.statisticBattlecup_0.AverageDamage);
                base.WriteD(this.statisticBattlecup_0.PlayTime);
            }
        }
    }
}

