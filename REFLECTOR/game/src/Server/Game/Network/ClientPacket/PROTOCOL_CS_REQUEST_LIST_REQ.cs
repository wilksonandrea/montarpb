namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.SQL;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CS_REQUEST_LIST_REQ : GameClientPacket
    {
        private int int_0;

        private void method_0(ClanInvite clanInvite_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteQ(clanInvite_0.PlayerId);
            Account account = AccountManager.GetAccount(clanInvite_0.PlayerId, 0x1f);
            if (account != null)
            {
                syncServerPacket_0.WriteU(account.Nickname, 0x42);
                syncServerPacket_0.WriteC((byte) account.Rank);
                syncServerPacket_0.WriteC((byte) account.NickColor);
                syncServerPacket_0.WriteD(clanInvite_0.InviteDate);
                syncServerPacket_0.WriteD(account.Statistic.Basic.KillsCount);
                syncServerPacket_0.WriteD(account.Statistic.Basic.DeathsCount);
                syncServerPacket_0.WriteD(account.Statistic.Basic.Matches);
                syncServerPacket_0.WriteD(account.Statistic.Basic.MatchWins);
                syncServerPacket_0.WriteD(account.Statistic.Basic.MatchLoses);
                syncServerPacket_0.WriteN(clanInvite_0.Text, clanInvite_0.Text.Length + 2, "UTF-16LE");
            }
            syncServerPacket_0.WriteD(clanInvite_0.InviteDate);
        }

        public override void Read()
        {
            this.int_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    if (player.ClanId == 0)
                    {
                        base.Client.SendPacket(new PROTOCOL_CS_REQUEST_LIST_ACK(-1));
                    }
                    else
                    {
                        List<ClanInvite> clanRequestList = DaoManagerSQL.GetClanRequestList(player.ClanId);
                        using (SyncServerPacket packet = new SyncServerPacket())
                        {
                            int num = 0;
                            int num2 = (this.int_0 != 0) ? 14 : 13;
                            int num3 = this.int_0 * num2;
                            while (true)
                            {
                                if (num3 < clanRequestList.Count)
                                {
                                    this.method_0(clanRequestList[num3], packet);
                                    if (++num != 13)
                                    {
                                        num3++;
                                        continue;
                                    }
                                }
                                base.Client.SendPacket(new PROTOCOL_CS_REQUEST_LIST_ACK(0, num, this.int_0, packet.ToArray()));
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_REQUEST_LIST_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

