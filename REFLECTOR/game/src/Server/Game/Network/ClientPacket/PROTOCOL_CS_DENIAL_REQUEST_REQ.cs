namespace Server.Game.Network.ClientPacket
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CS_DENIAL_REQUEST_REQ : GameClientPacket
    {
        private List<long> list_0 = new List<long>();
        private int int_0;

        private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = clanModel_0.Name;
            model1.SenderId = long_1;
            model1.ClanId = clanModel_0.Id;
            model1.Type = NoteMessageType.Clan;
            model1.State = NoteMessageState.Unreaded;
            model1.ClanNote = NoteMessageClan.InviteDenial;
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
                Account player = base.Client.Player;
                if (player != null)
                {
                    ClanModel clan = ClanManager.GetClan(player.ClanId);
                    if ((clan.Id > 0) && (((player.ClanAccess >= 1) && (player.ClanAccess <= 2)) || (clan.OwnerId == player.PlayerId)))
                    {
                        for (int i = 0; i < this.list_0.Count; i++)
                        {
                            long playerId = this.list_0[i];
                            if (DaoManagerSQL.DeleteClanInviteDB(clan.Id, playerId))
                            {
                                if (DaoManagerSQL.GetMessagesCount(playerId) < 100)
                                {
                                    MessageModel model2 = this.method_0(clan, playerId, player.PlayerId);
                                    if (model2 != null)
                                    {
                                        Account account2 = AccountManager.GetAccount(playerId, 0x1f);
                                        if ((account2 != null) && account2.IsOnline)
                                        {
                                            account2.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model2), false);
                                        }
                                    }
                                }
                                this.int_0++;
                            }
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_DENIAL_REQUEST_ACK(this.int_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_DENIAL_REQUEST_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

