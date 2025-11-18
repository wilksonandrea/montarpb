namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_GET_SLOTONEINFO_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly RoomModel roomModel_0;
        private readonly ClanModel clanModel_0;

        public PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(Account account_1)
        {
            this.account_0 = account_1;
            if (account_1 != null)
            {
                this.roomModel_0 = account_1.Room;
                this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
            }
        }

        public PROTOCOL_ROOM_GET_SLOTONEINFO_ACK(Account account_1, ClanModel clanModel_1)
        {
            this.account_0 = account_1;
            if (account_1 != null)
            {
                this.roomModel_0 = account_1.Room;
            }
            this.clanModel_0 = clanModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe04);
            base.WriteH((short) 0);
            base.WriteC((byte) this.roomModel_0.GetSlot(this.account_0.SlotId).Team);
            base.WriteC((byte) this.roomModel_0.GetSlot(this.account_0.SlotId).State);
            base.WriteC((byte) this.account_0.GetRank());
            base.WriteD(this.clanModel_0.Id);
            base.WriteD(this.account_0.ClanAccess);
            base.WriteC((byte) this.clanModel_0.Rank);
            base.WriteD(this.clanModel_0.Logo);
            base.WriteC((byte) this.account_0.CafePC);
            base.WriteC((byte) this.account_0.Country);
            base.WriteQ((long) this.account_0.Effects);
            base.WriteC((byte) this.clanModel_0.Effect);
            base.WriteC((byte) this.roomModel_0.GetSlot(this.account_0.SlotId).ViewType);
            base.WriteC((byte) base.NATIONS);
            base.WriteC(0);
            base.WriteD(this.account_0.Equipment.NameCardId);
            base.WriteC((byte) this.account_0.Bonus.NickBorderColor);
            base.WriteC((byte) this.account_0.AuthLevel());
            base.WriteU(this.clanModel_0.Name, 0x22);
            base.WriteC((byte) this.account_0.SlotId);
            base.WriteU(this.account_0.Nickname, 0x42);
            base.WriteC((byte) this.account_0.NickColor);
            base.WriteC((byte) this.account_0.Bonus.MuzzleColor);
            base.WriteC(0);
            base.WriteC(0xff);
            base.WriteC(0xff);
        }
    }
}

