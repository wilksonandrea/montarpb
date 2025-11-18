namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Auth.Data.Models;
    using Server.Auth.Data.Utils;
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_BASE_LOGIN_ACK : AuthServerPacket
    {
        private readonly EventErrorEnum eventErrorEnum_0;
        private readonly Account account_0;
        private readonly uint uint_0;

        public PROTOCOL_BASE_LOGIN_ACK(EventErrorEnum eventErrorEnum_1, Account account_1, uint uint_1)
        {
            this.eventErrorEnum_0 = eventErrorEnum_1;
            this.account_0 = account_1;
            this.uint_0 = uint_1;
        }

        private byte[] method_0(EventErrorEnum eventErrorEnum_1, Account account_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                if (!eventErrorEnum_1.Equals(EventErrorEnum.SUCCESS))
                {
                    packet.WriteB(Bitwise.HexStringToByteArray("00 00 00 00 00 00 00 00 00 00 00"));
                }
                else
                {
                    packet.WriteC((byte) $"{account_1.PlayerId}".Length);
                    packet.WriteS($"{account_1.PlayerId}", $"{account_1.PlayerId}".Length);
                    packet.WriteC(0);
                    packet.WriteC((byte) account_1.Username.Length);
                    packet.WriteS(account_1.Username, account_1.Username.Length);
                    packet.WriteQ(account_1.PlayerId);
                }
                return packet.ToArray();
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x503);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            base.WriteB(new byte[12]);
            base.WriteD(AllUtils.GetFeatures());
            base.WriteH((short) 0x57a);
            base.WriteB(new byte[0x10]);
            base.WriteB(this.method_0(this.eventErrorEnum_0, this.account_0));
            base.WriteD((uint) this.eventErrorEnum_0);
        }
    }
}

