namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Auth.Network;
    using System;

    public class PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK : AuthServerPacket
    {
        private readonly FriendModel friendModel_0;
        private readonly int int_0;
        private readonly FriendState friendState_0;
        private readonly FriendChangeState friendChangeState_0;

        public PROTOCOL_AUTH_FRIEND_INFO_CHANGE_ACK(FriendChangeState friendChangeState_1, FriendModel friendModel_1, FriendState friendState_1, int int_1)
        {
            this.friendState_0 = friendState_1;
            this.friendModel_0 = friendModel_1;
            this.friendChangeState_0 = friendChangeState_1;
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x717);
            base.WriteC((byte) this.friendChangeState_0);
            base.WriteC((byte) this.int_0);
            if ((this.friendChangeState_0 != FriendChangeState.Insert) && (this.friendChangeState_0 != FriendChangeState.Update))
            {
                base.WriteB(new byte[0x18]);
            }
            else
            {
                PlayerInfo info = this.friendModel_0.Info;
                if (info == null)
                {
                    base.WriteB(new byte[0x18]);
                }
                else
                {
                    base.WriteC((byte) (info.Nickname.Length + 1));
                    base.WriteN(info.Nickname, info.Nickname.Length + 2, "UTF-16LE");
                    base.WriteQ(this.friendModel_0.PlayerId);
                    base.WriteD(ComDiv.GetFriendStatus(this.friendModel_0, this.friendState_0));
                    base.WriteD(uint.MaxValue);
                    base.WriteC((byte) info.Rank);
                    base.WriteB(new byte[6]);
                }
            }
        }
    }
}

