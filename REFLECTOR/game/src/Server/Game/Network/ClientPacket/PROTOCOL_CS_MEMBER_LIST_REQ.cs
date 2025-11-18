namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Network;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CS_MEMBER_LIST_REQ : GameClientPacket
    {
        private byte byte_0;

        private void method_0(Account account_0, SyncServerPacket syncServerPacket_0)
        {
            syncServerPacket_0.WriteQ(account_0.PlayerId);
            syncServerPacket_0.WriteU(account_0.Nickname, 0x42);
            syncServerPacket_0.WriteC((byte) account_0.Rank);
            syncServerPacket_0.WriteC((byte) account_0.ClanAccess);
            syncServerPacket_0.WriteQ(ComDiv.GetClanStatus(account_0.Status, account_0.IsOnline));
            syncServerPacket_0.WriteD(account_0.ClanDate);
            syncServerPacket_0.WriteC((byte) account_0.NickColor);
            syncServerPacket_0.WriteD(account_0.Statistic.Clan.MatchWins);
            syncServerPacket_0.WriteD(account_0.Statistic.Clan.MatchLoses);
            syncServerPacket_0.WriteD(account_0.Equipment.NameCardId);
            syncServerPacket_0.WriteC(0);
            syncServerPacket_0.WriteD(10);
            syncServerPacket_0.WriteD(20);
            syncServerPacket_0.WriteD(30);
        }

        public override void Read()
        {
            this.byte_0 = base.ReadC();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    int clanId = (player.ClanId == 0) ? player.FindClanId : player.ClanId;
                    if (ClanManager.GetClan(clanId).Id == 0)
                    {
                        base.Client.SendPacket(new PROTOCOL_CS_MEMBER_LIST_ACK(uint.MaxValue, 0xff, 0xff, new byte[0]));
                    }
                    else
                    {
                        List<Account> list = ClanManager.GetClanPlayers(clanId, -1L, false);
                        using (SyncServerPacket packet = new SyncServerPacket())
                        {
                            byte num2 = 0;
                            int num3 = this.byte_0 * 14;
                            while (true)
                            {
                                if (num3 < list.Count)
                                {
                                    this.method_0(list[num3], packet);
                                    num2 = (byte) (num2 + 1);
                                    if (num2 != 14)
                                    {
                                        num3++;
                                        continue;
                                    }
                                }
                                base.Client.SendPacket(new PROTOCOL_CS_MEMBER_LIST_ACK(0, this.byte_0, num2, packet.ToArray()));
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_MEMBER_LIST_REQ " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

