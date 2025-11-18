namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK : GameServerPacket
    {
        private readonly List<Account> list_0;
        private readonly List<int> list_1;

        public PROTOCOL_ROOM_INVITE_LOBBY_USER_LIST_ACK(ChannelModel channelModel_0)
        {
            this.list_0 = channelModel_0.GetWaitPlayers();
            this.list_1 = this.method_0(this.list_0.Count, (this.list_0.Count >= 8) ? 8 : this.list_0.Count);
        }

        private List<int> method_0(int int_0, int int_1)
        {
            if ((int_0 == 0) || (int_1 == 0))
            {
                return new List<int>();
            }
            Random random = new Random();
            List<int> list = new List<int>();
            for (int i = 0; i < int_0; i++)
            {
                list.Add(i);
            }
            for (int j = 0; j < list.Count; j++)
            {
                int num3 = random.Next(list.Count);
                int num4 = list[j];
                list[j] = list[num3];
                list[num3] = num4;
            }
            return list.GetRange(0, int_1);
        }

        public override void Write()
        {
            base.WriteH((short) 0xe5c);
            base.WriteD(this.list_1.Count);
            foreach (int num in this.list_1)
            {
                Account account = this.list_0[num];
                base.WriteD(account.GetSessionId());
                base.WriteD(account.GetRank());
                base.WriteC((byte) (account.Nickname.Length + 1));
                base.WriteN(account.Nickname, account.Nickname.Length + 2, "UTF-16LE");
                base.WriteC((byte) account.NickColor);
            }
        }
    }
}

