namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK : GameServerPacket
    {
        private readonly List<MatchModel> list_0;
        private readonly int int_0;

        public PROTOCOL_CS_CLAN_MATCH_RESULT_LIST_ACK(int int_1, List<MatchModel> list_1)
        {
            this.int_0 = int_1;
            this.list_0 = list_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x7a5);
            this.WriteC((this.int_0 == 0) ? ((byte) this.list_0.Count) : ((byte) this.int_0));
            if ((this.int_0 <= 0) && (this.list_0.Count != 0))
            {
                base.WriteC(1);
                base.WriteC(0);
                base.WriteC((byte) this.list_0.Count);
                for (int i = 0; i < this.list_0.Count; i++)
                {
                    MatchModel model = this.list_0[i];
                    base.WriteH((short) model.MatchId);
                    base.WriteH((ushort) model.GetServerInfo());
                    base.WriteH((ushort) model.GetServerInfo());
                    base.WriteC((byte) model.State);
                    base.WriteC((byte) model.FriendId);
                    base.WriteC((byte) model.Training);
                    base.WriteC((byte) model.GetCountPlayers());
                    base.WriteC(0);
                    base.WriteD(model.Leader);
                    Account leader = model.GetLeader();
                    if (leader == null)
                    {
                        base.WriteB(new byte[0x4c]);
                    }
                    else
                    {
                        base.WriteC((byte) leader.Rank);
                        base.WriteU(leader.Nickname, 0x42);
                        base.WriteQ(leader.PlayerId);
                        base.WriteC((byte) model.Slots[model.Leader].State);
                    }
                }
            }
        }
    }
}

