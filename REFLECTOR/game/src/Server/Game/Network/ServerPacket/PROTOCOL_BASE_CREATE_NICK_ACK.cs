namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_CREATE_NICK_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly Account account_0;
        private readonly PlayerInventory playerInventory_0;
        private readonly PlayerEquipment playerEquipment_0;

        public PROTOCOL_BASE_CREATE_NICK_ACK(uint uint_1, Account account_1)
        {
            this.uint_0 = uint_1;
            this.account_0 = account_1;
            if (account_1 != null)
            {
                this.playerInventory_0 = account_1.Inventory;
                this.playerEquipment_0 = account_1.Equipment;
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x917);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteC(1);
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.DinoItem));
                base.WriteC((byte) (this.account_0.Nickname.Length * 2));
                base.WriteU(this.account_0.Nickname, this.account_0.Nickname.Length * 2);
            }
        }
    }
}

