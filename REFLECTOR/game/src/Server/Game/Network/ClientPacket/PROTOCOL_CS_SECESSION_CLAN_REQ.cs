namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;

    public class PROTOCOL_CS_SECESSION_CLAN_REQ : GameClientPacket
    {
        private uint uint_0;

        private MessageModel method_0(ClanModel clanModel_0, Account account_0)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = clanModel_0.Name;
            model1.SenderId = account_0.PlayerId;
            model1.ClanId = clanModel_0.Id;
            model1.Type = NoteMessageType.Clan;
            model1.Text = account_0.Nickname;
            model1.State = NoteMessageState.Unreaded;
            model1.ClanNote = NoteMessageClan.Secession;
            MessageModel message = model1;
            return (DaoManagerSQL.CreateMessage(clanModel_0.OwnerId, message) ? message : null);
        }

        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (player != null)
                {
                    if (player.ClanId <= 0)
                    {
                        this.uint_0 = 0x8000105b;
                    }
                    else
                    {
                        ClanModel clan = ClanManager.GetClan(player.ClanId);
                        if ((clan.Id <= 0) || (clan.OwnerId == player.PlayerId))
                        {
                            this.uint_0 = 0x8000105e;
                        }
                        else
                        {
                            string[] cOLUMNS = new string[] { "clan_id", "clan_access" };
                            object[] vALUES = new object[] { 0, 0 };
                            if (ComDiv.UpdateDB("accounts", "player_id", player.PlayerId, cOLUMNS, vALUES))
                            {
                                string[] textArray2 = new string[] { "clan_matches", "clan_match_wins" };
                                object[] objArray2 = new object[] { 0, 0 };
                                if (ComDiv.UpdateDB("player_stat_clans", "owner_id", player.PlayerId, textArray2, objArray2))
                                {
                                    using (PROTOCOL_CS_MEMBER_INFO_DELETE_ACK protocol_cs_member_info_delete_ack = new PROTOCOL_CS_MEMBER_INFO_DELETE_ACK(player.PlayerId))
                                    {
                                        ClanManager.SendPacket(protocol_cs_member_info_delete_ack, player.ClanId, player.PlayerId, true, true);
                                    }
                                    long ownerId = clan.OwnerId;
                                    if (DaoManagerSQL.GetMessagesCount(ownerId) < 100)
                                    {
                                        MessageModel model2 = this.method_0(clan, player);
                                        if (model2 != null)
                                        {
                                            Account account2 = AccountManager.GetAccount(ownerId, 0x1f);
                                            if ((account2 != null) && account2.IsOnline)
                                            {
                                                account2.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model2), false);
                                            }
                                        }
                                    }
                                    player.ClanId = 0;
                                    player.ClanAccess = 0;
                                    goto TR_0005;
                                }
                            }
                            this.uint_0 = 0x8000106b;
                        }
                    }
                }
                else
                {
                    return;
                }
            TR_0005:
                base.Client.SendPacket(new PROTOCOL_CS_SECESSION_CLAN_ACK(this.uint_0));
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_SECESSION_CLAN_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

