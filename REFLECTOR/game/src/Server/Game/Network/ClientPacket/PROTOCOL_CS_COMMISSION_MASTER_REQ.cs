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

    public class PROTOCOL_CS_COMMISSION_MASTER_REQ : GameClientPacket
    {
        private long long_0;
        private uint uint_0;

        private MessageModel method_0(ClanModel clanModel_0, long long_1, long long_2)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = clanModel_0.Name;
            model1.SenderId = long_2;
            model1.ClanId = clanModel_0.Id;
            model1.Type = NoteMessageType.Clan;
            model1.State = NoteMessageState.Unreaded;
            model1.ClanNote = NoteMessageClan.Master;
            MessageModel message = model1;
            return (DaoManagerSQL.CreateMessage(long_1, message) ? message : null);
        }

        public override void Read()
        {
            this.long_0 = base.ReadQ();
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((player != null) && (player.ClanAccess == 1))
                {
                    Account account2 = AccountManager.GetAccount(this.long_0, 0x1f);
                    int clanId = player.ClanId;
                    if ((account2 == null) || (account2.ClanId != clanId))
                    {
                        this.uint_0 = 0x80000000;
                    }
                    else if (account2.Rank <= 10)
                    {
                        this.uint_0 = 0x800010b8;
                    }
                    else
                    {
                        ClanModel clan = ClanManager.GetClan(clanId);
                        if ((clan.Id <= 0) || ((clan.OwnerId != base.Client.PlayerId) || ((account2.ClanAccess != 2) || (!ComDiv.UpdateDB("system_clan", "owner_id", this.long_0, "id", clanId) || (!ComDiv.UpdateDB("accounts", "clan_access", 1, "player_id", this.long_0) || !ComDiv.UpdateDB("accounts", "clan_access", 2, "player_id", player.PlayerId))))))
                        {
                            this.uint_0 = 0x80001000;
                        }
                        else
                        {
                            account2.ClanAccess = 1;
                            player.ClanAccess = 2;
                            clan.OwnerId = this.long_0;
                            if (DaoManagerSQL.GetMessagesCount(account2.PlayerId) < 100)
                            {
                                MessageModel model2 = this.method_0(clan, account2.PlayerId, player.PlayerId);
                                if ((model2 != null) && account2.IsOnline)
                                {
                                    account2.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model2), false);
                                }
                            }
                            if (account2.IsOnline)
                            {
                                account2.SendPacket(new PROTOCOL_CS_COMMISSION_MASTER_RESULT_ACK(), false);
                            }
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_COMMISSION_MASTER_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_COMMISSION_MASTER_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

