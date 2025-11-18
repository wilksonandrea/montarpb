namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_GET_PLAYERINFO_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly Account account_0;
        private readonly PlayerInventory playerInventory_0;
        private readonly PlayerEquipment playerEquipment_0;
        private readonly StatisticTotal statisticTotal_0;
        private readonly StatisticSeason statisticSeason_0;
        private readonly ClanModel clanModel_0;
        private readonly int[] int_0;

        public PROTOCOL_ROOM_GET_PLAYERINFO_ACK(uint uint_1, Account account_1, int[] int_1)
        {
            this.uint_0 = uint_1;
            this.account_0 = account_1;
            if (account_1 != null)
            {
                this.playerInventory_0 = account_1.Inventory;
                this.playerEquipment_0 = account_1.Equipment;
                this.statisticTotal_0 = account_1.Statistic.Basic;
                this.statisticSeason_0 = account_1.Statistic.Season;
                this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
            }
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe0d);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteD(this.int_0[1]);
                base.WriteD(this.int_0[0]);
                base.WriteD(this.statisticSeason_0.Matches);
                base.WriteD(this.statisticSeason_0.MatchWins);
                base.WriteD(this.statisticSeason_0.MatchLoses);
                base.WriteD(this.statisticSeason_0.MatchDraws);
                base.WriteD(this.statisticSeason_0.KillsCount);
                base.WriteD(this.statisticSeason_0.HeadshotsCount);
                base.WriteD(this.statisticSeason_0.DeathsCount);
                base.WriteD(this.statisticSeason_0.TotalMatchesCount);
                base.WriteD(this.statisticSeason_0.TotalKillsCount);
                base.WriteD(this.statisticSeason_0.EscapesCount);
                base.WriteD(this.statisticSeason_0.AssistsCount);
                base.WriteD(this.statisticSeason_0.MvpCount);
                base.WriteD(this.statisticTotal_0.Matches);
                base.WriteD(this.statisticTotal_0.MatchWins);
                base.WriteD(this.statisticTotal_0.MatchLoses);
                base.WriteD(this.statisticTotal_0.MatchDraws);
                base.WriteD(this.statisticTotal_0.KillsCount);
                base.WriteD(this.statisticTotal_0.HeadshotsCount);
                base.WriteD(this.statisticTotal_0.DeathsCount);
                base.WriteD(this.statisticTotal_0.TotalMatchesCount);
                base.WriteD(this.statisticTotal_0.TotalKillsCount);
                base.WriteD(this.statisticTotal_0.EscapesCount);
                base.WriteD(this.statisticTotal_0.AssistsCount);
                base.WriteD(this.statisticTotal_0.MvpCount);
                base.WriteC(3);
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.DinoItem));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.SprayId));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.NameCardId));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponPrimary));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSecondary));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponMelee));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponExplosive));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSpecial));
                base.WriteB(this.playerInventory_0.EquipmentData(this.int_0[0]));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHead));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartFace));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartJacket));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartPocket));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartGlove));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartBelt));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHolster));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartSkin));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.BeretItem));
                base.WriteD(0x90403);
                base.WriteU(this.account_0.Nickname, 0x42);
                base.WriteD(this.account_0.GetRank());
                base.WriteD(this.account_0.Rank);
                base.WriteD(this.account_0.Gold);
                base.WriteD(this.account_0.Exp);
                base.WriteD(0);
                base.WriteC(0);
                base.WriteQ((long) 0L);
                base.WriteC((byte) this.account_0.AuthLevel());
                base.WriteC(0);
                base.WriteD(this.account_0.Tags);
                base.WriteH((short) 0);
                base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
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
            }
        }
    }
}

