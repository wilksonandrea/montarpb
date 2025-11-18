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

    public class PROTOCOL_CS_NOTE_REQ : GameClientPacket
    {
        private int int_0;
        private string string_0;

        private MessageModel method_0(ClanModel clanModel_0, long long_0, long long_1)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = clanModel_0.Name;
            model1.SenderId = long_1;
            model1.ClanId = clanModel_0.Id;
            model1.Type = NoteMessageType.ClanInfo;
            model1.Text = this.string_0;
            model1.State = NoteMessageState.Unreaded;
            MessageModel message = model1;
            return (DaoManagerSQL.CreateMessage(long_0, message) ? message : null);
        }

        public override void Read()
        {
            this.int_0 = base.ReadC();
            this.string_0 = base.ReadU(base.ReadC() * 2);
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if ((this.string_0.Length <= 120) && (player != null))
                {
                    ClanModel clan = ClanManager.GetClan(player.ClanId);
                    int num = 0;
                    if ((clan.Id > 0) && (clan.OwnerId == base.Client.PlayerId))
                    {
                        List<Account> list = ClanManager.GetClanPlayers(clan.Id, base.Client.PlayerId, true);
                        for (int i = 0; i < list.Count; i++)
                        {
                            Account account2 = list[i];
                            if ((((this.int_0 == 0) || ((account2.ClanAccess == 2) && (this.int_0 == 1))) || ((account2.ClanAccess == 3) && (this.int_0 == 2))) && (DaoManagerSQL.GetMessagesCount(account2.PlayerId) < 100))
                            {
                                num++;
                                MessageModel model2 = this.method_0(clan, account2.PlayerId, base.Client.PlayerId);
                                if ((model2 != null) && account2.IsOnline)
                                {
                                    account2.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model2), false);
                                }
                            }
                        }
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_NOTE_ACK(num));
                    if (num > 0)
                    {
                        base.Client.SendPacket(new PROTOCOL_MESSENGER_NOTE_SEND_ACK(0));
                    }
                }
            }
            catch (Exception exception)
            {
                CLogger.Print("PROTOCOL_CS_NOTE_REQ: " + exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

