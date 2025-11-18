namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;
    using System.Runtime.InteropServices;

    public class PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK : GameServerPacket
    {
        private readonly MatchModel matchModel_0;
        private readonly uint uint_0;

        public PROTOCOL_CLAN_WAR_JOIN_TEAM_ACK(uint uint_1, MatchModel matchModel_1 = null)
        {
            this.uint_0 = uint_1;
            this.matchModel_0 = matchModel_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1b09);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteH((short) this.matchModel_0.MatchId);
                base.WriteH((ushort) this.matchModel_0.GetServerInfo());
                base.WriteH((ushort) this.matchModel_0.GetServerInfo());
                base.WriteC((byte) this.matchModel_0.State);
                base.WriteC((byte) this.matchModel_0.FriendId);
                base.WriteC((byte) this.matchModel_0.Training);
                base.WriteC((byte) this.matchModel_0.GetCountPlayers());
                base.WriteD(this.matchModel_0.Leader);
                base.WriteC(0);
                base.WriteD(this.matchModel_0.Clan.Id);
                base.WriteC((byte) this.matchModel_0.Clan.Rank);
                base.WriteD(this.matchModel_0.Clan.Logo);
                base.WriteS(this.matchModel_0.Clan.Name, 0x11);
                base.WriteT(this.matchModel_0.Clan.Points);
                base.WriteC((byte) this.matchModel_0.Clan.NameColor);
                for (int i = 0; i < this.matchModel_0.Training; i++)
                {
                    SlotMatch slot = this.matchModel_0.Slots[i];
                    Account playerBySlot = this.matchModel_0.GetPlayerBySlot(slot);
                    if (playerBySlot == null)
                    {
                        base.WriteB(new byte[0x2b]);
                    }
                    else
                    {
                        base.WriteC((byte) playerBySlot.Rank);
                        base.WriteS(playerBySlot.Nickname, 0x21);
                        base.WriteQ(playerBySlot.PlayerId);
                        base.WriteC((byte) slot.State);
                    }
                }
            }
        }
    }
}

