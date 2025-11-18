namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CS_DEPORTATION_REQ : GameClientPacket
    {
        private List<long> list_0 = new List<long>();
        private uint uint_0;

        private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = clanModel_0.Name;
            model1.SenderId = long_1;
            model1.ClanId = clanModel_0.Id;
            model1.Type = NoteMessageType.Clan;
            model1.State = NoteMessageState.Unreaded;
            model1.ClanNote = NoteMessageClan.Deportation;
            MessageModel message = model1;
            return (DaoManagerSQL.CreateMessage(long_0, message) ? message : null);
        }

        public override void Read()
        {
            int num = base.ReadC();
            for (int i = 0; i < num; i++)
            {
                long item = base.ReadQ();
                this.list_0.Add(item);
            }
        }

        public override void Run()
        {
            try
            {
                ClanModel clan;
                Account player = base.Client.Player;
                if (player != null)
                {
                    clan = ClanManager.GetClan(player.ClanId);
                    if ((clan.Id == 0) || (((player.ClanAccess < 1) || (player.ClanAccess > 2)) && (clan.OwnerId != base.Client.PlayerId)))
                    {
                        this.uint_0 = 0x80001059;
                        return;
                    }
                }
                else
                {
                    return;
                }
                List<Account> players = ClanManager.GetClanPlayers(clan.Id, -1L, true);
                int num = 0;
                while (true)
                {
                    if (num < this.list_0.Count)
                    {
                        Account account2 = AccountManager.GetAccount(this.list_0[num], 0x1f);
                        if ((account2 != null) && ((account2.ClanId == clan.Id) && (account2.Match == null)))
                        {
                            string[] cOLUMNS = new string[] { "clan_id", "clan_access" };
                            object[] vALUES = new object[] { 0, 0 };
                            if (ComDiv.UpdateDB("accounts", "player_id", account2.PlayerId, cOLUMNS, vALUES))
                            {
                                string[] textArray2 = new string[] { "clan_matches", "clan_match_wins" };
                                object[] objArray2 = new object[] { 0, 0 };
                                if (ComDiv.UpdateDB("player_stat_clans", "owner_id", account2.PlayerId, textArray2, objArray2))
                                {
                                    using (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK protocol_cs_member_info_delete_ack = new PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(account2.PlayerId))
                                    {
                                        ClanManager.SendPacket(protocol_cs_member_info_delete_ack, players, account2.PlayerId);
                                    }
                                    account2.ClanId = 0;
                                    account2.ClanAccess = 0;
                                    SendClanInfo.Load(account2, null, 0);
                                    if (DaoManagerSQL.GetMessagesCount(account2.PlayerId) < 100)
                                    {
                                        MessageModel model2 = this.method_0(clan, account2.PlayerId, base.Client.PlayerId);
                                        if ((model2 != null) && account2.IsOnline)
                                        {
                                            account2.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model2), false);
                                        }
                                    }
                                    if (account2.IsOnline)
                                    {
                                        account2.SendPacket(new PROTOCOL_CS_DEPORTATION_RESULT_ACK(), false);
                                    }
                                    this.uint_0++;
                                    players.Remove(account2);
                                    num++;
                                    continue;
                                }
                            }
                        }
                        this.uint_0 = 0x80001059;
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_DEPORTATION_ACK(this.uint_0));
                    break;
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_DEPORTATION_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

