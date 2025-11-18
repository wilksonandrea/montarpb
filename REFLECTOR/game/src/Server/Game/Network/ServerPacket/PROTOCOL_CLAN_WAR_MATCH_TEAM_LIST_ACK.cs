namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK : GameServerPacket
    {
        private readonly List<MatchModel> list_0;
        private readonly int int_0;
        private readonly int int_1;

        public PROTOCOL_CLAN_WAR_MATCH_TEAM_LIST_ACK(List<MatchModel> list_1, int int_2)
        {
            this.int_0 = int_2;
            this.list_0 = list_1;
            this.int_1 = list_1.Count - 1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1b05);
            base.WriteH((ushort) this.int_1);
            if (this.int_1 > 0)
            {
                base.WriteH((short) 1);
                base.WriteH((short) 0);
                base.WriteC((byte) this.int_1);
                foreach (MatchModel model in this.list_0)
                {
                    if (model.MatchId != this.int_0)
                    {
                        base.WriteH((short) model.MatchId);
                        base.WriteH((short) model.GetServerInfo());
                        base.WriteH((short) model.GetServerInfo());
                        base.WriteC((byte) model.State);
                        base.WriteC((byte) model.FriendId);
                        base.WriteC((byte) model.Training);
                        base.WriteC((byte) model.GetCountPlayers());
                        base.WriteD(model.Leader);
                        base.WriteC(0);
                        base.WriteD(model.Clan.Id);
                        base.WriteC((byte) model.Clan.Rank);
                        base.WriteD(model.Clan.Logo);
                        base.WriteS(model.Clan.Name, 0x11);
                        base.WriteT(model.Clan.Points);
                        base.WriteC((byte) model.Clan.NameColor);
                        Account leader = model.GetLeader();
                        if (leader == null)
                        {
                            base.WriteB(new byte[0x2b]);
                            continue;
                        }
                        base.WriteC((byte) leader.Rank);
                        base.WriteS(leader.Nickname, 0x21);
                        base.WriteQ(leader.PlayerId);
                        base.WriteC((byte) model.Slots[model.Leader].State);
                    }
                }
            }
        }
    }
}

