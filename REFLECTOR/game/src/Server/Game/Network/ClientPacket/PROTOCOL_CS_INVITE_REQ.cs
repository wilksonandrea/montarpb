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

    public class PROTOCOL_CS_INVITE_REQ : GameClientPacket
    {
        private uint uint_0;

        private void method_0(Account account_0, int int_0)
        {
            if (DaoManagerSQL.GetMessagesCount(account_0.PlayerId) >= 100)
            {
                this.uint_0 = 0x80000000;
            }
            else
            {
                MessageModel model = this.method_1(int_0, account_0.PlayerId, base.Client.PlayerId);
                if ((model != null) && account_0.IsOnline)
                {
                    account_0.SendPacket(new PROTOCOL_MESSENGER_NOTE_RECEIVE_ACK(model), false);
                }
            }
        }

        private MessageModel method_1(int int_0, long long_0, long long_1)
        {
            MessageModel model1 = new MessageModel(15.0);
            model1.SenderName = ClanManager.GetClan(int_0).Name;
            model1.ClanId = int_0;
            model1.SenderId = long_1;
            model1.Type = NoteMessageType.ClanAsk;
            model1.State = NoteMessageState.Unreaded;
            model1.ClanNote = NoteMessageClan.Invite;
            MessageModel message = model1;
            return (DaoManagerSQL.CreateMessage(long_0, message) ? message : null);
        }

        public override void Read()
        {
        }

        public override void Run()
        {
            try
            {
                Account player = base.Client.Player;
                if (((player != null) && ((player.ClanId != 0) && (player.FindPlayer != ""))) && (player.FindPlayer.Length != 0))
                {
                    Account account2 = AccountManager.GetAccount(player.FindPlayer, 1, 0);
                    if (account2 == null)
                    {
                        this.uint_0 = 0x80000000;
                    }
                    else if ((account2.ClanId == 0) && (player.ClanId != 0))
                    {
                        this.method_0(account2, player.ClanId);
                    }
                    else
                    {
                        this.uint_0 = 0x80000000;
                    }
                    base.Client.SendPacket(new PROTOCOL_CS_INVITE_ACK(this.uint_0));
                }
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }
}

