namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Auth.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_AUTH_FRIEND_INFO_ACK : AuthServerPacket
    {
        private readonly List<FriendModel> list_0;

        public PROTOCOL_AUTH_FRIEND_INFO_ACK(List<FriendModel> list_1)
        {
            this.list_0 = list_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x712);
            base.WriteC((byte) this.list_0.Count);
            foreach (FriendModel model in this.list_0)
            {
                PlayerInfo info = model.Info;
                if (info == null)
                {
                    base.WriteB(new byte[0x18]);
                    continue;
                }
                base.WriteC((byte) (info.Nickname.Length + 1));
                base.WriteN(info.Nickname, info.Nickname.Length + 2, "UTF-16LE");
                base.WriteQ(info.PlayerId);
                base.WriteD(ComDiv.GetFriendStatus(model));
                base.WriteD(uint.MaxValue);
                base.WriteC((byte) info.Rank);
                base.WriteB(new byte[6]);
            }
        }
    }
}

